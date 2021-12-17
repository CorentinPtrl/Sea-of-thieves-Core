using System;
using System.Threading;
using SoT;
namespace SotEspCoreTest
{
    class Program
    {

        public static string convertVecToStr(VectorUE4 vec)
        {
            return "(X: " + vec.getX() + " Y: " +vec.getY()+ " Z: " + vec.getZ() + ")";
        }
        static void Main(string[] args)
        {
            SotCore core = new SotCore();
            if (core.Prepare())
            {
                UE4Actor[] actors = core.GetActors();
                foreach (UE4Actor actor in actors)
                {
                    if (actor.getName().Equals("BP_PlayerPirate_C"))
                    {
                        while (true)
                        {
                            Console.WriteLine("Name : " + actor.BaseName + " | Actual Name :" + actor.getName() + " Pos :" + convertVecToStr(actor.getPos()));
                            Thread.Sleep(500);
                        }
                    }
                }
            }
           
        }
    }
}
