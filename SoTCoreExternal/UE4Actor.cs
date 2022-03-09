using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT
{
    public class UE4Actor : UEObject
    {
        String _Name;
        public String Name
        {
            get
            {
                if (_Name != null) return _Name;
                _Name = SotCore.Instance.Engine.GetName(Id);
                return _Name;
            }
        }

        int _Id = -1;
        public int Id
        {
            get
            {
                if (_Id != -1) return _Id;
                _Id = SotCore.Instance.Memory.ReadProcessMemory<int>(Address + 0x18);
                return _Id;
            }
        }

        public UE4Actor(ulong address) : base(address)
        {
        }
    }
}
