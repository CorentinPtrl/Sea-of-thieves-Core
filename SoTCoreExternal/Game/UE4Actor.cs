using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
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

        public Quaternion Rotation
        {
            get
            {
                return actor.GetRootComponent().transform.Rotation;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return actor.GetRootComponent().transform.Scale3D;
            }
        }

        public UE4Actor(ulong address) : base(address)
        {
        }
    }
}
