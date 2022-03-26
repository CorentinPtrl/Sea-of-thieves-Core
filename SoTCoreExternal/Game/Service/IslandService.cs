using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Service
{
    public class IslandService : UE4Actor
    {
        private TArray IslandArray
        {
            get
            {
                TArray IslandArray = SotCore.Instance.Memory.ReadProcessMemory<TArray>(Address + SotCore.Instance.Offsets["IslandService.IslandArray"]);
                return IslandArray;
            }
        }

        public new uint Num
        {
            get
            {
                return (uint)IslandArray.NumElements;
            }
        }
        public IslandService(UE4Actor actor) : base(actor.Address)
        {
        }

        public IslandService(ulong address) : base(address)
        {
        }

        public Island GetIslandByIndex(int index)
        {
            return IslandArray.GetValue<Island>(index, (int)SotCore.Instance.Offsets["Island.Size"]);
        }
    }
}
