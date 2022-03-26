using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    public struct TArray
    {
        public T GetValue<T>(int index, int size)
        {
            ulong place = (ulong)(size * index);
            return SotCore.Instance.Memory.ReadProcessMemory<T>(Data + place);
        }

        ulong Data;
        public Int32 MaxElements;
        public Int32 NumElements;
    }
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

    public struct SinkingShipParams
    {
        public float DragWhenGrindingToHalt;
        public float MinSpdToStopToBeforeLowering;
        public float LowerIntoWaterTime;
        public float TimeIntoLoweringToStartOcclusionZoneShrinkage;
        public float AngularDragDuringSinkingSequence;
        public float KeeledOverTime;
        public float TurnOffBuoyancyTime;
        public float FinalSinkingBuoyancy;
        public float SinkingTimeUntilDestroy;
        public float ReduceWaterOcclusionZoneTime;
        public float ReduceWaterOcclusionZoneTimeHurryUp;
        public float TimeIntoKeelingOverToTeleportPlayer;
        public float MinSampleSubmersionToConsiderInWater;
        public float MinPctSamplesRequiredSubmergedToBeAbleToSink;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct ShipInternalWater
    {
        [FieldOffset(0x418)]
        public float CurrentVisualWaterLevel;

        [FieldOffset(0x41c)]
        public float WaterAmount;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CameraManager
    {
        [FieldOffset(0x450)]
        public Vector3 Location;

        [FieldOffset(0x45C)]
        public Vector3 Rotation;

        [FieldOffset(0x478)]
        public float FOV;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct DrowningComponent
    {
        [FieldOffset(0x108)]
        public float OxygenLevel;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct Island
    {
        [FieldOffset(0x0)]
        public int FNameIndex;
    };
}
