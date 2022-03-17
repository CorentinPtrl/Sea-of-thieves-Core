﻿using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    public struct FTransform
    {
        public Quaternion Rotation;
        public Vector3 Translation;
        public Vector3 Scale3D;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct SceneComponent
    {
        [FieldOffset(0x140)]
        public FTransform transform;
    }

    public struct FString
    {
        private ulong pData;
        private int DataSize;
        public override string ToString()
        {
            return Encoding.Unicode.GetString(SotCore.Instance.Memory.ReadProcessMemory(pData, DataSize * 0x4)).Split('\0')[0];
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct AActor
    {
        [FieldOffset(0x18)]
        public int ObjectID;


        [FieldOffset(0x170)]
        private ulong RootComponentPtr;

        public SceneComponent GetRootComponent()
        {
            return SotCore.Instance.Memory.ReadProcessMemory<SceneComponent>(RootComponentPtr);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UHealthComponent
    {
        [FieldOffset(0x00E0)]
        public float MaxHealth;

        [FieldOffset(0x00E4)]
        public float CurrentHealth;
    };
}
