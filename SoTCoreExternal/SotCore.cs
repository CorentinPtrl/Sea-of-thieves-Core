using SoT.Util;
using System;
using SoT.Game;
using System.Collections.Generic;
using System.Timers;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using System.IO;

namespace SoT
{
    public class SotCore
    {

        public static SotCore Instance { get; private set; }
        public Memory Memory { get; private set; }
        public UE4Engine Engine { get; private set; }

        public UInt64 UWorld { get; private set; }
        public UInt64 GNames { get; private set; }
        public UInt64 GObjects { get; private set; }

        private ulong PlayerController
        {
            get
            {
                ulong OwningGameInstance = Memory.ReadProcessMemory<ulong>(Memory.ReadProcessMemory<UInt64>(UWorld) + Offsets["UWorld.OwningGameInstance"]);
                ulong LocalPlayer = Memory.ReadProcessMemory<ulong>(Memory.ReadProcessMemory<ulong>(OwningGameInstance + Offsets["UGameInstance.LocalPlayers"]));
                return Memory.ReadProcessMemory<ulong>(LocalPlayer + 0x30);
            }
        }
        public Player LocalPlayer
        {
            get
            {
                return new Player(PlayerController);
            }
        }

        public CameraManager CameraManager
        {
            get
            {
                return Memory.ReadProcessMemory<CameraManager>(Memory.ReadProcessMemory<ulong>(PlayerController + Offsets["APlayerController.CameraManager"]));
            }
        }

        private UE4Actor[] Actors;

        public Dictionary<String, JsonManager.Actor> ActorsName = new Dictionary<String, JsonManager.Actor>();
        public Dictionary<String, ulong> Offsets = new Dictionary<String, ulong>();

        private List<String> IncludesActors = new List<string>();

        private static Timer Ticker;
        public SotCore(float IntervalUpdate = 30)
        {
            if (Instance == null)
                Instance = this;
            Ticker = new Timer(IntervalUpdate);
            Ticker.SynchronizingObject = null;
            Ticker.Start();

            ActorsName = JsonManager.GetJsonActors();
            Offsets = JsonManager.GetJsonOffsets();

            IncludesActors.AddRange(new String[] { "BP_PlayerPirate_C", "IslandService", "CrewService" });
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

                Ticker.Elapsed += OnTickElapsed;
                return true;
            }
            return false;
        }

        private void OnTickElapsed(object Sender, ElapsedEventArgs e)
        {
            UEObject Level = new UEObject(Memory.ReadProcessMemory<UInt64>(Memory.ReadProcessMemory<UInt64>(UWorld) + 0x30));
            UE4Actor Actors = new UE4Actor(Level.Address + 0xA0);
            List<UE4Actor> actorList = new List<UE4Actor>();
            for (var i = 0u; i < Actors.Num; i++)
            {
                UE4Actor act = new UE4Actor(Actors[i].Address);
                if (IncludesActors.Contains(act.Name))
                    actorList.Add(act);
            }

            this.Actors = actorList.ToArray();
        }

        public UE4Actor[] GetActors()
        {
            if (this.Actors != null)
                return this.Actors;

            UEObject Level = new UEObject(Memory.ReadProcessMemory<UInt64>(Memory.ReadProcessMemory<UInt64>(UWorld) + 0x30));
            UEObject Actors = new UEObject(Level.Address + 0xA0);
            List<UE4Actor> actorList = new List<UE4Actor>();

            for (var i = 0u; i < Actors.Num; i++)
            {
                UE4Actor act = new UE4Actor(Actors[i].Address);
                if (IncludesActors.Contains(act.Name))
                    actorList.Add(act);
            }

            return actorList.ToArray();
        }
    }
}
