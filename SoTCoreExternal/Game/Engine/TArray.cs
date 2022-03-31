using SoT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Game.Engine
{
    public class TArray<T>
    {
        private ulong Address;

        public ulong Data
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<ulong>(Address);
            }
        }

        public int Length
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<int>(Address + 0x8);
            }
        }

        public int Max
        {
            get
            {
                return SotCore.Instance.Memory.ReadProcessMemory<int>(Address + 0xC);
            }
        }

        public TArray(ulong address)
        {
            Address = address;
        }

        public T GetValue(int index, int size)
        {
            ulong place = (ulong)(size * index);
            return SotCore.Instance.Memory.ReadProcessMemory<T>(Data + place);
        }

        public T GetValue(int index)
        {
            object[] Attributes = typeof(T).GetCustomAttributes(false);
            if (Attributes.Length > 0)
            {
                foreach(object attr in Attributes)
                {
                    if(attr.GetType() == typeof(OffsetAttribute))
                    {
                        OffsetAttribute offset = (OffsetAttribute)attr;
                        ulong place = offset.getSize() * (ulong)index;
                        return SotCore.Instance.Memory.ReadProcessMemory<T>(Data + place);
                    }
                }
            }
            throw new NotImplementedException("Type does not have OffsetAttribute");
        }

        public ulong GetValueAddress(int index)
        {
            object[] Attributes = typeof(T).GetCustomAttributes(false);
            if (Attributes.Length > 0)
            {
                foreach (object attr in Attributes)
                {
                    if (attr.GetType() == typeof(OffsetAttribute))
                    {
                        OffsetAttribute offset = (OffsetAttribute)attr;
                        ulong place = offset.getSize() * (ulong)index;
                        return Data + place;
                    }
                }
            }
            throw new NotImplementedException("Type does not have OffsetAttribute");
        }



        public ulong GetValuePtr(int index)
        {
            return SotCore.Instance.Memory.ReadProcessMemory<ulong>(Data + (ulong)(8 * index));
        }
    }
}
