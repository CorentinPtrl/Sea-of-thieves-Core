using SoT.Data;
using SoT.Game.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Athena
{
    public class IslandDataAssetEntry : UEObject
    {
        public string IslandName
        {
            get
            {
                return SotCore.Instance.Engine.GetName(SotCore.Instance.Memory.ReadProcessMemory<int>(Address + SotCore.Instance.Offsets["UIslandDataAssetEntry.IslandName"]));
            }
        }

        public TArray<TreasureMapData> TreasureMaps
        {
            get
            {
                return new TArray<TreasureMapData>(Address + SotCore.Instance.Offsets["UIslandDataAssetEntry.TreasureMaps"], TArrayType.Address);
            }
        }

        public TArray<Vector3> UndergroundTreasureLocations
        {
            get
            {
                return new TArray<Vector3>(Address + SotCore.Instance.Offsets["UIslandDataAssetEntry.UndergroundTreasureLocations"], TArrayType.Struct);
            }
        }



        public IslandDataAssetEntry(UEObject obj) : base(obj.Address)
        {
        }
        public IslandDataAssetEntry(ulong address) : base(address)
        {
        }
    }
}
