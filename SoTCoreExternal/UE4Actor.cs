using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT
{
    public struct FTransform
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public Vector3 Translation;
        public Vector3 Scale3D;
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct SceneComponent
    {
        [FieldOffset(0x140)]
        public FTransform transform;
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


        public class UE4Actor : UEObject
    {
        String _Name;
        public String Name
        {
            get
            {
                if (_Name != null) return _Name;
                _Name = SotCore.Instance.Engine.GetName(Id);
                return _Name;
            }
        }

        int _Id = -1;
        public int Id
        {
            get
            {
                if (_Id != -1) return _Id;
                _Id = SotCore.Instance.Memory.ReadProcessMemory<int>(Address + 0x18);
                return _Id;
            }
        }

        AActor _actor;
        private AActor actor
        {
            get
            {
                if (_actor.Equals(default)) return _actor;
                _actor = SotCore.Instance.Memory.ReadProcessMemory<AActor>(Address);
                return _actor;
            }
        }

        public Vector3 Position
        {
            get
            {
                return actor.GetRootComponent().transform.Translation;
            }
        }

        public UE4Actor(ulong address) : base(address)
        {
        }
    }
}
