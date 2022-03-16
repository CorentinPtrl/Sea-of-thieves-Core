using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Data
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
        public override string ToString()
        {
            return String.Format("(X : {0} Y: {1} Z: {2})", X, Y, Z);
        }
    }
}
