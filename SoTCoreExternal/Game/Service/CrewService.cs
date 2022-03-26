using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Service
{
    public class CrewService : UE4Actor
    {
        private TArray Crews
        {
            get
            {
                TArray Crews = SotCore.Instance.Memory.ReadProcessMemory<TArray>(Address + SotCore.Instance.Offsets["ACrewService.Crews"]);
                return Crews;
            }
        }

        public new uint Num
        {
            get
            {
                return (uint)Crews.NumElements;
            }
        }

        public CrewService(UE4Actor actor) : base(actor.Address)
        {
        }

        public CrewService(ulong address) : base(address)
        {
        }

        public Crew GetCrewByIndex(int index)
        {
            return Crews.GetValue<Crew>(index, (int)SotCore.Instance.Offsets["Crew.Size"]);
        }
    }
}