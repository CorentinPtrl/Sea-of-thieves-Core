using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Engine
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

        public String FriendlyName
        {
            get
            {
                if (SotCore.Instance.ActorsName.ContainsKey(Name))
                    return SotCore.Instance.ActorsName[Name].Name;
                return "None";
            }
        }

        public String Category
        {
            get
            {
                if (SotCore.Instance.ActorsName.ContainsKey(Name))
                    return SotCore.Instance.ActorsName[Name].Category;
                return "None";
            }
        }

        int _Id = -1;
        public int Id
        {
            get
            {
                if (_Id != -1) return _Id;
                _Id = SotCore.Instance.Memory.ReadProcessMemory<int>(Address + (byte)SotCore.Instance.Offsets["AActor.actorId"]);
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

        public Vector3 LinearVelocity
        {
            get
            {
                return actor.ReplicatedMovement.LinearVelocity;
            }
        }

        public Vector3 AngularVelocity
        {
            get
            {
                return actor.ReplicatedMovement.AngularVelocity;
            }
        }

        public UE4Actor(ulong address) : base(address)
        {
        }
        public T GetCustomRotation<T>()
        {
            ulong SceneComponent = SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + (byte)SotCore.Instance.Offsets["AActor.rootComponent"]);
            return SotCore.Instance.Memory.ReadProcessMemory<T>((SceneComponent) + (byte)SotCore.Instance.Offsets["SceneComponent.FTransform"]);
        }

        public T GetCustomPosition<T>()
        {
            ulong SceneComponent = SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + (byte)SotCore.Instance.Offsets["AActor.rootComponent"]);
            return SotCore.Instance.Memory.ReadProcessMemory<T>((SceneComponent) + (byte)SotCore.Instance.Offsets["SceneComponent.FTransform"] + 0x10);
        }

        public T GetCustomScale<T>()
        {
            ulong SceneComponent = SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + (byte)SotCore.Instance.Offsets["AActor.rootComponent"]);
            return SotCore.Instance.Memory.ReadProcessMemory<T>((SceneComponent) + (byte)SotCore.Instance.Offsets["SceneComponent.FTransform"] + 0x14);
        }

        public T GetCustomLinearVelocity<T>()
        {
            return SotCore.Instance.Memory.ReadProcessMemory<T>(Address + SotCore.Instance.Offsets["AActor.ReplicatedMovement"] + SotCore.Instance.Offsets["RepMovement.LinearVelocity"]);
        }

        public T GetCustomAngularVelocity<T>()
        {
            return SotCore.Instance.Memory.ReadProcessMemory<T>(Address + SotCore.Instance.Offsets["AActor.ReplicatedMovement"] + SotCore.Instance.Offsets["RepMovement.AngularVelocity"]);
        }
    }
}
