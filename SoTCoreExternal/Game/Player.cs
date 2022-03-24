using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    public class Player : UE4Actor
    {
        public ulong PlayerPawn
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + SotCore.Instance.Offsets["APlayerController.Pawn"]);

            }
        }

        public ulong Character
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + SotCore.Instance.Offsets["APlayerController.Character"]);

            }
        }

        private UHealthComponent HealthComponent
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<UHealthComponent>(SotCore.Instance.Memory.ReadProcessMemory<ulong>(PlayerPawn + SotCore.Instance.Offsets["AActor.HealthComponent"]));
            }
        }

        private DrowningComponent DrowningComponent
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<DrowningComponent>(SotCore.Instance.Memory.ReadProcessMemory<ulong>(PlayerPawn + SotCore.Instance.Offsets["AthenaPlayerCharacter.DrowningComponent"]));
            }
        }

        private String _PlayerName;

        public String PlayerName
        {
            get
            {
                if (_PlayerName != null) return _PlayerName;
                ulong PlayerState = SotCore.Instance.Memory.ReadProcessMemory<ulong>(PlayerPawn + SotCore.Instance.Offsets["AActor.PlayerState"]);

                _PlayerName = SotCore.Instance.Memory.ReadProcessMemory<FString>(PlayerState + SotCore.Instance.Offsets["APlayerState.PlayerName"]).ToString();
                return _PlayerName;
            }
        }

        public String CurrentWieldedItem
        {
            get
            {
                ulong WieldedItemComponent = SotCore.Instance.Memory.ReadProcessMemory<ulong>(PlayerPawn + SotCore.Instance.Offsets["AActor.WieldedItemComponent"]);
                ulong CurrentlyWieldedItem = SotCore.Instance.Memory.ReadProcessMemory<ulong>(WieldedItemComponent + SotCore.Instance.Offsets["UWieldedItemComponent.WieldedItem"]);
                ulong ItemInfo = SotCore.Instance.Memory.ReadProcessMemory<ulong>(CurrentlyWieldedItem + SotCore.Instance.Offsets["AWieldableItem.ItemInfo"]);
                ulong ItemDesc = SotCore.Instance.Memory.ReadProcessMemory<ulong>(ItemInfo + SotCore.Instance.Offsets["AItemProxy.AItemInfo"]);
                ulong Name = SotCore.Instance.Memory.ReadProcessMemory<ulong>(ItemDesc + SotCore.Instance.Offsets["AItemInfo.UItemDesc"]);
                return SotCore.Instance.Memory.ReadProcessMemory<FString>(Name).ToString();
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

        public float OxygenLevel
        {
            get
            {
                return DrowningComponent.OxygenLevel;
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
