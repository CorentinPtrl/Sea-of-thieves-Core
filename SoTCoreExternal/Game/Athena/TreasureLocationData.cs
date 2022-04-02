using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Athena
{
    [OffsetAttribute("TreasureLocationData")]
    public class TreasureLocationData
    {
        public ulong Address;

        public Vector3 WorldSpaceLocation
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<Vector3>(Address + SotCore.Instance.Offsets["FTreasureLocationData.WorldSpaceLocation"]);
            }
        }

        public Vector3 IslandSpaceLocation
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<Vector3>(Address+ +SotCore.Instance.Offsets["FTreasureLocationData.IslandSpaceLocation"]);
            }
        }

        public TreasureLocationData(ulong address)
        {
            this.Address = address;
        }
    }
}
