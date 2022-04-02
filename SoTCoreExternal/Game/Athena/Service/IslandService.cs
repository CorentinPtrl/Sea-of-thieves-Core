using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoT.Game.Engine;

namespace SoT.Game.Athena.Service
{
    public class IslandService : UE4Actor
    {
        public TArray<Island> IslandArray
        {
            get
            {
                return new TArray<Island>(Address + SotCore.Instance.Offsets["IslandService.IslandArray"], TArrayType.Struct);
            }
        }

        public IslandDataAsset IslandDataAsset
        {
            get
            {
                return new IslandDataAsset(SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + SotCore.Instance.Offsets["IslandService.IslandDataAsset"]));
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
