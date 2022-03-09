using SotCore.Util;
using System;

namespace SotCore
{
    public class SotCore
    {

        public static SotCore Instance { get; private set; }
        private Memory Memory = null;

        private UInt64 UWorld;
        private UInt64 GNames;
        private UInt64 GObjects;


        public SotCore()
        {
            if (Instance == null)
                Instance = this;
        }

        public bool Prepare(bool IsSteam)
        {
            Memory = new Memory("SoTGame.exe");

            UInt64 UWorldPattern = (UInt64)Memory.FindPattern("48 8B 05 ? ? ? ? 48 8B 88  ? ? ? 48 85 C9 74 06 48 8B 49 70");
            UInt64 GNamesPattern = (UInt64)Memory.FindPattern("48 8B 1D ? ? ? ? 48 85 ? 75 3A");
            UInt64 GObjectsPattern = (UInt64)Memory.FindPattern("48 8B 15 ? ? ? ? 3B 42 1C");

            UInt32 offset = Memory.ReadProcessMemory<UInt32>(GNamesPattern + 3);
            GNames = Memory.ReadProcessMemory<UInt64>(GNamesPattern + offset + 7);

            offset = Memory.ReadProcessMemory<UInt32>(GObjectsPattern + 3) + 7;
            GObjects = Memory.ReadProcessMemory<UInt64>(GObjectsPattern + offset + 7);

            offset = Memory.ReadProcessMemory<UInt32>(UWorldPattern + 3);
            UWorld = UWorldPattern + offset + 7;

            return true;
        }
    }
}
