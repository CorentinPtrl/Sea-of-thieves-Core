using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoT.Game.Engine;

namespace SoT.Game.Athena
{
    [OffsetAttribute("Crew")]
    public class Crew
    {
        public ulong Address;
        public Player[] PreProcessedPlayers;
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
                return new TArray<Player>(Address + SotCore.Instance.Offsets["Crew.Players"], TArrayType.Pointer);
            }
        }

        public int MaxMatchmakingPlayers
        {
            get
            {
                ulong CrewSessionTemplate = Address + SotCore.Instance.Offsets["Crew.CrewSessionTemplate"];
                return SotCore.Instance.Memory.ReadProcessMemory<int>(CrewSessionTemplate + SotCore.Instance.Offsets["FCrewSessionTemplate.MaxMatchmakingPlayers"]);
            }
        }

        public Crew(ulong address)
        {
            Address = address;
        }
    }
}