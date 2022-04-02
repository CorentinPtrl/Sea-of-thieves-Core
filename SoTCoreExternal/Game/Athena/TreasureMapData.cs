using SoT.Data;
using SoT.Game.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Athena
{
    [OffsetAttribute("TreasureMapData")]
    public class TreasureMapData
    {
        public ulong Address;

        public TArray<TreasureLocationData> TreasureLocations
        {
            get
            {
                return new TArray<TreasureLocationData>(Address + SotCore.Instance.Offsets["FTreasureMapData.TreasureLocations"], TArrayType.Address);
            }
        }

        public TreasureMapData(ulong address)
        {
            this.Address = address;
        }
    }
}
