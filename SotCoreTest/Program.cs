using System;
using System.Threading;
using SoT;
namespace SotEspCoreTest
{
    class Program
    {

        public static string convertVecToStr(VectorUE4 vec)
        {
            if (vec == null)
                return "NULL";
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
                int i = 1;
                while (true)
                {
                    Console.WriteLine("LocalPlayer Name : {0} | LocalPlayer Actual Name : {1} LocalPlayer Pos : {2} index = {3}", localPlayer.BaseName, localPlayer.getName(), convertVecToStr(localPlayer.getPos()), i);
                    i++;
                }
            }
        }
    }        
    
}