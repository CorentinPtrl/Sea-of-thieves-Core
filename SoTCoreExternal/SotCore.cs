using SoT.Util;
using System;
using SoT.Game.Athena;
using SoT.Game.Engine;
using System.Collections.Generic;
using System.Timers;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using System.IO;
using SoT.Game.Athena.Service;
using System.Threading;

namespace SoT
{
    public class SotCore
    {

        public static SotCore Instance { get; private set; }
        public Memory Memory { get; private set; }
        public UE4Engine Engine { get; private set; }

        private Island[] _Islands;
        private Crew[] _Crews;
        private ulong PlayerController;

        internal UInt64 UWorld { get; private set; }
        internal UInt64 GNames { get; private set; }
        internal UInt64 GObjects { get; private set; }


        public Player LocalPlayer
        {
            get
            {
                return new Player(PlayerController, IsLocalPlayer: true);
            }
        }

        public CameraManager CameraManager
        {
            get
            {
                return Memory.ReadProcessMemory<CameraManager>(Memory.ReadProcessMemory<ulong>(PlayerController + Offsets["APlayerController.CameraManager"]));
            }
        }

        public Island[] Islands
        {
            get
            {
                if (_Islands != null)
                    return _Islands;
                else
                {
                    Update();
                    return _Islands;
                }
            }
        }

        public Crew[] Crews
        {
            get
            {
                if (_Crews != null)
                    return _Crews;
                else
                {
                    Update();
                    return Crews;
                }
            }
        }

        public Dictionary<String, JsonManager.Actor> ActorsName = new Dictionary<String, JsonManager.Actor>();
        public Dictionary<String, ulong> Offsets = new Dictionary<String, ulong>();
        private UE4Actor[] Actors;
        private List<String> IncludesActors = new List<string>();
        private int IntervalUpdate;
        private Thread ThreadUpdate;


        public SotCore(int IntervalUpdate = 1)
        {
            if (Instance == null)
                Instance = this;

            ActorsName = JsonManager.GetJsonActors();
            Offsets = JsonManager.GetJsonOffsets();
            this.IntervalUpdate = IntervalUpdate;

            IncludesActors.AddRange(new String[] { "BP_PlayerPirate_C", "IslandService", "CrewService", "BP_Cannon_C" });
            IncludesActors.AddRange(ActorsName.Keys);
        }

        public bool Prepare(bool IsSteam)
        {
            Memory = new Memory("SoTGame");

            if (Memory.Process != null)
            {
                Engine = new UE4Engine();

                UInt64 UWorldPattern = (UInt64)Memory.FindPattern(new byte[] { 0x48, 0x8B, 0x05, 0x00, 0x00, 0x00, 0x00, 0x48, 0x8B, 0x88, 0x00, 0x00, 0x00, 0x00, 0x48, 0x85, 0xC9, 0x74, 0x06, 0x48, 0x8B, 0x49, 0x70 }, "xxx????xxx????xxxxxxxxx");

                UInt64 GNamesPattern = 0;
                UInt64 GObjectsPattern = 0;

                if (IsSteam)
                {
                    GNamesPattern = (UInt64)Memory.FindPattern(new byte[] { 0x48, 0x8B, 0x1D, 0x00, 0x00, 0x00, 0x00, 0x48, 0x85, 0xDB, 0x75, 0x00, 0xB9, 0x08, 0x04, 0x00, 0x00 }, "xxx????xxxx?xxxxx");
                    GObjectsPattern = (UInt64)Memory.FindPattern(new byte[] { 0x89, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x48, 0x8B, 0xDF, 0x48, 0x89, 0x5C, 0x2 }, "xx????xxxxxxx");
                }
                else
                {
                    GNamesPattern = (UInt64)Memory.FindPattern(new byte[] { 0x48, 0x8B, 0x1D, 0x00, 0x00, 0x00, 0x00, 0x48, 0x85, 0x00, 0x75, 0x3A }, "xxx????xx?xx");
                    GObjectsPattern = (UInt64)Memory.FindPattern(new byte[] { 0x48, 0x8B, 0x15, 0x00, 0x00, 0x00, 0x00, 0x3B, 0x42, 0x1C }, "xxx????xxx");
                }

                UInt32 offset = Memory.ReadProcessMemory<UInt32>(GNamesPattern + 3);
                GNames = Memory.ReadProcessMemory<UInt64>(GNamesPattern + offset + 7);

                offset = Memory.ReadProcessMemory<UInt32>(GObjectsPattern + 3) + 7;
                GObjects = Memory.ReadProcessMemory<UInt64>(GObjectsPattern + offset + 7);

                offset = Memory.ReadProcessMemory<UInt32>(UWorldPattern + 3);
                UWorld = UWorldPattern + offset + 7;

                ThreadUpdate = new Thread(UpdateThread);
                ThreadUpdate.Start();
                return true;
            }
            return false;
        }

        private void Update()
        {
            UEObject Level = new UEObject(Memory.ReadProcessMemory<UInt64>(Memory.ReadProcessMemory<UInt64>(UWorld) + 0x30));
            UE4Actor Actors = new UE4Actor(Level.Address + 0xA0);
            List<UE4Actor> actorList = new List<UE4Actor>();
            for (var i = 0u; i < Actors.Num; i++)
            {
                UE4Actor act = new UE4Actor(Actors[i].Address);
                if (IncludesActors.Contains(act.Name))
                {
                    actorList.Add(act);
                }
                if (act.Name.Equals("IslandService"))
                {
                    IslandService IslandService = new IslandService(act);
                    TArray<Island> IslandArray = IslandService.IslandArray;
                    _Islands = new Island[IslandArray.Length];
                    for (int b = 0; b < IslandArray.Length; b++)
                    {
                        _Islands[b] = IslandArray.GetValue(b);
                    }
                }
                else if (act.Name.Equals("CrewService"))
                {
                    CrewService CrewService = new CrewService(act);
                    TArray<Crew> Crews = CrewService.Crews;
                    this._Crews = new Crew[Crews.Length];
                    for (int b = 0; b < Crews.Length; b++)
                    {
                        Crew Crew = new Crew(Crews.GetValueAddress(b));
                        TArray<Player> Players = Crew.Players;
                        List<Player> CrewPlayers = new List<Player>();
                        for (int c = 0; c < Players.Length; c++)
                        {
                            if (Crew.Players.Length > 4) continue;
                            CrewPlayers.Add(new Player(Players.GetValuePtr(c), true));
                        }
                        Crew.PreProcessedPlayers = CrewPlayers.ToArray();
                        this._Crews[b] = Crew;
                    }
                }
            }

            this.Actors = actorList.ToArray();

            ulong OwningGameInstance = Memory.ReadProcessMemory<ulong>(Memory.ReadProcessMemory<UInt64>(UWorld) + Offsets["UWorld.OwningGameInstance"]);
            ulong LocalPlayer = Memory.ReadProcessMemory<ulong>(Memory.ReadProcessMemory<ulong>(OwningGameInstance + Offsets["UGameInstance.LocalPlayers"]));
            this.PlayerController = Memory.ReadProcessMemory<ulong>(LocalPlayer + 0x30);
        }

        private void UpdateThread()
        {
            while (true)
            {
                Update();
                Thread.Sleep(IntervalUpdate);
            }
        }

        public UE4Actor[] GetActors()
        {
            if (this.Actors != null)
                return this.Actors;

            Update();

            return this.Actors;
        }
    }
}