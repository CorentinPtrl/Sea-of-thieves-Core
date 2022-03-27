using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game
{
    [OffsetAttribute("Crew")]
    public class Crew
    {
        public ulong Address;

        public Guid CrewId
        {
            get
            {
                 return SotCore.Instance.Memory.ReadProcessMemory<Guid>(Address + SotCore.Instance.Offsets["Crew.CrewId"]);
            }
        }

        public Guid SessionId
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<Guid>(Address + SotCore.Instance.Offsets["Crew.SessionId"]);
            }
        }

        public TArray<Player> Players
        {
            get
            {
                return new TArray<Player>(Address + SotCore.Instance.Offsets["Crew.Players"]);
            }
        }

        public Crew(ulong address)
        {
            Address = address;
        }
    }
}