using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Data
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class OffsetAttribute : Attribute
    {
        public String OffsetName;
        
        public OffsetAttribute(String OffsetName)
        {
            this.OffsetName = OffsetName;
        }

        public ulong getSize()
        {
            return SotCore.Instance.Offsets[this.OffsetName+".Size"];
        }
    }
}
