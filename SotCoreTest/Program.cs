using System;
using System.Threading;
using SoT;
namespace SotEspCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SotCore core = new SotCore();
            
            if (core.Prepare(false))
            {
                UE4Actor[] actors = core.GetActors();
                foreach (UE4Actor actor in actors)
                {
                    Console.WriteLine("Name : {0} | Actual Name : {1} Pos : {2} Rot : {3}", actor.BaseName, actor.getName(), actor.getPos(), actor.getRot());
                }
                UE4Actor localPlayer = core.GetLocalPlayer();
                int i = 1;
                while (true)
                {
                    Console.WriteLine("LocalPlayer Name : {0} | LocalPlayer Actual Name : {1} LocalPlayer Pos : {2} LocalPLayer Rot : {3} index = {4}", localPlayer.BaseName, localPlayer.getName(), localPlayer.getPos(),localPlayer.getRot(), i);
                    i++;
                }
            }
        }
    }        
    
}