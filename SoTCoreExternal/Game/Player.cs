using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    public class Player : UE4Actor
    {
        private UHealthComponent HealthComponent
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<UHealthComponent>(SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + 2136));
            }
        }

        private String _PlayerName;       

        public String PlayerName
        {
            get
            {
                if (_PlayerName != null) return _PlayerName;
                ulong PlayerState = SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + 0x3F0);
                _PlayerName = SotCore.Instance.Memory.ReadProcessMemory<FString>(PlayerState + 0x3d8).ToString();
                return _PlayerName;
            }
        }

        public float MaxHealth
        {
            get
            {
                return HealthComponent.MaxHealth;
            }
        }

        public float CurrentHealth
        {
            get
            {
                return HealthComponent.CurrentHealth;
            }
        }

        public Player(UEObject ueobject) : base(ueobject.Address)
        {
        }

        public Player(ulong address) : base(address)
        {
        }
    }
}
