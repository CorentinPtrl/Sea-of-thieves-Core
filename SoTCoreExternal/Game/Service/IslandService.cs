using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Service
{
    public class IslandService : UE4Actor
    {
        public TArray<Island> IslandArray
        {
            get
            {
                return new TArray<Island>(Address + SotCore.Instance.Offsets["IslandService.IslandArray"]);
            }
        }
        public IslandService(UE4Actor actor) : base(actor.Address)
        {
        }

        public IslandService(ulong address) : base(address)
        {
        }
    }
}
