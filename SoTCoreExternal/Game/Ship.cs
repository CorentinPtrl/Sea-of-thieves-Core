using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    public class Ship : UE4Actor
    {
        public SinkingShipParams SinkingShipParams
        {
            get
            {
                ulong SinkingComponent = SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + SotCore.Instance.Offsets["AShip.SinkingComponent"]);
                return SotCore.Instance.Memory.ReadProcessMemory<SinkingShipParams>(SinkingComponent + SotCore.Instance.Offsets["SinkingComponent.SinkingParams"]);
            }
        }

        public ShipInternalWater ShipInternalWater
        {
            get
            {
                ulong ChildActorComponent = (SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + SotCore.Instance.Offsets["AShip.ShipInternalWaterComponent"]));
                ulong IntervalWater = SotCore.Instance.Memory.ReadProcessMemory<ulong>(ChildActorComponent + SotCore.Instance.Offsets["UChildActorComponent.ChildActor"]);
                ShipInternalWater water = SotCore.Instance.Memory.ReadProcessMemory<ShipInternalWater>(IntervalWater);
                return water;
            }
        }

        public Guid CrewId
        {
            get
            {
                ulong CrewOwnershipComponent = (SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address + SotCore.Instance.Offsets["AShip.CrewOwnershipComponent"]));
                ulong CrewIdPtr = SotCore.Instance.Memory.ReadProcessMemory<ulong>(CrewOwnershipComponent + SotCore.Instance.Offsets["UCrewOwnershipComponent.CrewId"]);
                Guid CrewId = SotCore.Instance.Memory.ReadProcessMemory<Guid>(CrewIdPtr);
                return CrewId;
            }
        }

        public Ship(ulong address) : base(address)
        {
        }

        public Ship(UE4Actor actor) : base(actor.Address)
        {
        }
    }
}
