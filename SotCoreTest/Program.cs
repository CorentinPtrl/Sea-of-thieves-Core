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
                    Console.WriteLine("Name : {0}  | Actual Name : {1} Pos : {2} Rot : {3}", actor.BaseName, actor.getName(), actor.getPos().ToString(), actor.getRot().ToString());
                }
                Player localPlayer = new Player(core.GetLocalPlayer());

                Console.WriteLine("LocalPlayer Name : {0} | LocalPlayer Actual Name : {1} LocalPlayer Pos : {2} LocalPlayer Rot : {3}", localPlayer.BaseName, localPlayer.getName(), localPlayer.getPos().ToString(), localPlayer.getRot().ToString());

                CameraManager cameraManager = core.GetCameraManager();
                Console.WriteLine("Camera Manager | Pos : {0}  Rot : {1}  FOV {2}", cameraManager.getPos().ToString(), cameraManager.getRot().ToString(), cameraManager.getFOV());

                Console.WriteLine("LocalPlayer in-game Name : {0} | Health : {1} | Max Health : {2} | Wielded Item Name {3} ", localPlayer.getPlayerName(), localPlayer.getHealth(), localPlayer.getMaxHealth(), localPlayer.getWieldedItem());

            }
        }
    }
}        