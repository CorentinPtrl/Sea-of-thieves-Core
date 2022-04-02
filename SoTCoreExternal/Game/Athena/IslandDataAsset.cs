using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoT.Game.Engine;

namespace SoT.Game.Athena
{
    public class IslandDataAsset : UEObject
    {
        public TArray<IslandDataAssetEntry> IslandDataEntries
        { 
            get
            {
                return new TArray<IslandDataAssetEntry>(Address + SotCore.Instance.Offsets["UIslandDataAsset.IslandDataEntries"], TArrayType.Pointer);
            }
        }

        public IslandDataAsset(UEObject obj) : base(obj.Address)
        {
        }
        public IslandDataAsset(ulong address) : base(address)
        {
        }
    }
}
