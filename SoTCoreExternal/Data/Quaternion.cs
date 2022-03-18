using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Data
{
    public struct Quaternion
    {
        public float X;
        public float Y;
        public float Z;
        public float W;
        public override string ToString()
        {
            return String.Format("(X : {0} Y: {1} Z: {2} W: {3} )", X, Y, Z, W);
        }
    }
}
