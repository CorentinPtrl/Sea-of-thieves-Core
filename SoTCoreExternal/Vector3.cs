using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
        public override string ToString()
        {
            return "(X : " + X + " Y: " + Y + " Z: " + Z + ")";
        }
    }
}
