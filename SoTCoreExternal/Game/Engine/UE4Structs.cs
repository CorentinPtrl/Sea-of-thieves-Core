using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Engine
{
    public struct RepMovement
    {
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;
        public Vector3 Location;
        public Vector3 Rotation;
        public bool bSimulatedPhysicSleep;
        public bool bRepPhysics;
        public byte LocationQuantizationLevel;
        public byte VelocityQuantizationLevel;
        public byte RotationQuantizationLevel;
    };

    public struct FTransform
    {
        public Quaternion Rotation;
        public Vector3 Translation;
        public Vector3 Scale3D;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct SceneComponent
    {
        [FieldOffset(0x150)]
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

        [FieldOffset(0x94)]
        public RepMovement ReplicatedMovement;


        [FieldOffset(0x170)]
        private ulong RootComponentPtr;

        public SceneComponent GetRootComponent()
        {
            return SotCore.Instance.Memory.ReadProcessMemory<SceneComponent>(RootComponentPtr);
        }
    }
}
