using System;
using System.Threading;
using SoT;
namespace SotEspCoreTest
{
    class Program
    {

        public static string convertVecToStr(VectorUE4 vec)
        {
            return "(X: " + vec.getX() + " Y: " + vec.getY() + " Z: " + vec.getZ() + ")";
        }
        static void Main(string[] args)
        {
            SotCore core = new SotCore();
            if (core.Prepare(false))
            {
                UE4Actor[] actors = core.GetActors();
                foreach (UE4Actor actor in actors)
                {
                    Console.WriteLine("Name : " + actor.BaseName + " | Actual Name :" + actor.getName() + " Pos :" + convertVecToStr(actor.getPos()));
                }
                UE4Actor localPlayer = core.GetLocalPlayer();
                Console.WriteLine("LocalPlayer Name : " + localPlayer.BaseName + " | LocalPlayer Actual Name :" + localPlayer.getName() + " LocalPlayer Pos :" + convertVecToStr(localPlayer.getPos()));
            }
        }
    }        
    
}