using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Data
{
    [OffsetAttribute("FVector")]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float X, float Y, float Z)
        { 
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public override string ToString()
        {
            return String.Format("(X : {0} Y: {1} Z: {2})", X, Y, Z);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X*X+ Y*Y+Z*Z);
        }

        public float LengthSqr()
        {
            return X * X + Y * Y + Z * Z;
        }

        public float Length2d()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public float Length2dSqr()
        {
            return X * X + Y * Y;
        }

        public float DistTo(Vector3 vec)
        {
            Vector3 dist = new Vector3(this.X - vec.X, this.Y - vec.Y, this.Z - vec.Z);
            return dist.Length();
        }

        public float DistToSqr(Vector3 vec)
        {
            Vector3 dist = new Vector3(this.X - vec.X, this.Y - vec.Y, this.Z - vec.Z);
            return dist.LengthSqr();
        }

        public float Dot(Vector3 vec)
        {
            if (vec.X == 0 && vec.Y == 0 && vec.Z == 0)
                return 0.0f;
            return (X * vec.X + Y * vec.Y + Z * vec.Z);
        }

        public Vector3 Cross(Vector3 vec)
        {
            return new Vector3(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);
        }

        public bool IsZero()
        {
            return (X > -0.01f && X < 0.01f &&
                    Y > -0.01f && Y < 0.01f &&
                    Z > -0.01f && Z < 0.01f);
        }
    }
}
