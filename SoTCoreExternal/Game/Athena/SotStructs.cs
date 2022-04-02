using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Athena
{
    public struct Guid
    {
        int A;
        int B;
        int C;
        int D;

        public static bool operator ==(Guid lhs, Guid rhs)
        {
            return lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C && lhs.D == rhs.D;
        }
        public static bool operator !=(Guid lhs, Guid rhs)
        {
            return lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C || lhs.D != rhs.D;
        }

        public override string ToString()
        {
            return String.Format("(A : {0}, B: {1}, C: {2}, D : {3})", A, B, C, D);
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
    [OffsetAttribute("Island")]
    public struct Island
    {
        [FieldOffset(0x0)]
        public int FNameIndex;

        [FieldOffset(0x0008)]
        public byte IslandType;

        [FieldOffset(0x0018)]
        public Vector3 IslandBoundsCentre;

        [FieldOffset(0x0024)]
        public float IslandBoundsRadius;

        [FieldOffset(0x0028)]
        public Vector3 IslandTriggerRadius;

        [FieldOffset(0x002C)]
        public float IslandSafeZoneRadius;

        [FieldOffset(0x0030)]
        public float Rotation;

        public String getName()
        {
            return SotCore.Instance.Engine.GetName(FNameIndex);
        }

        public IslandType GetIslandType()
        {
            return (Athena.IslandType)IslandType;
        }

        public override string ToString()
        {
            return String.Format("(Island Name : {0}, Island Type : {1}, IslandBoundsCentre : {2}, IslandBoundsRadius : {3}, IslandTriggerRadius : {4}, IslandSafeZoneRadius : {5}, Rotation : {6})", getName(), GetIslandType(), IslandBoundsCentre, IslandBoundsRadius, IslandTriggerRadius, IslandSafeZoneRadius, Rotation);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ACannon
    {
        [FieldOffset(0x5a4)]
        public float ProjectileSpeed;

        [FieldOffset(0x5a8)]
        public float ProjectileGravityScale;

        [FieldOffset(0x76c)]
        public float ServerPitch;

        [FieldOffset(0x770)]
        public float ServerYaw;
    }
}
