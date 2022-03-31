using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    public class Cannon : UE4Actor
    {
        private ACannon ACannon
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<ACannon>(Address);
            }
        }

        public float ProjectileSpeed
        {
            get
            {
                return ACannon.ProjectileSpeed;
            }
        }

        public float ProjectileGravityScale
        {
            get
            {
                return ACannon.ProjectileGravityScale;
            }
        }

        public float ServerPitch
        {
            get
            {
                return ACannon.ServerPitch;
            }
        }

        public float ServerYaw
        {
            get
            {
                return ACannon.ServerYaw;
            }
        }

        public Cannon(ulong address) : base(address)
        {
        }

        public Cannon(UE4Actor actor) : base(actor.Address)
        {
        }
    }
}
