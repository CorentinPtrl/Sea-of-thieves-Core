using System;
using SoT;
namespace SotEspCoreTest
{
    class Program
    {

        public static string convertVecToStr(VectorUE4 vec)
        {
            return "(X: " + vec.x + " Y: " +vec.y+ " Z: " + vec.z + ")";
        }
        static void Main(string[] args)
        {
            SotCore core = new SotCore();
            if (core.Prepare())
            {
                UE4Actor[] actors = core.GetActors();
                core.GetLocalPlayer();
                foreach (UE4Actor actor in actors)
                {
                    Console.WriteLine("Name : " + actor.name + " Pos: " + convertVecToStr(actor.pos));
                }
            }
           
        }
    }
}
