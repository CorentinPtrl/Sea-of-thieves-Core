using SoT.Util;
using System;
using SoT.Game;

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

        public SotCore()
        {
            if (Instance == null)
                Instance = this;
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

                return true;
            }
                return false;
        }

        public UE4Actor[] GetActors()
        {
            UEObject Level = new UEObject(Memory.ReadProcessMemory<UInt64>(Memory.ReadProcessMemory<UInt64>(UWorld)+ 0x30));
            UEObject Actors = new UEObject(Level.Address + 0xA0);
            UE4Actor[] result = new UE4Actor[Actors.Num];

            for (var i = 0u; i < Actors.Num; i++)
            {
                result[i] = new UE4Actor(Actors[i].Address);
            }

            return result;
        }
    }
}
