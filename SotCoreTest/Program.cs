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
                Player localPlayer = new Player(core.GetLocalPlayer());
                Crew crew = null;
                foreach (UE4Actor actor in actors)
                {
                    if (actor.getName().Equals("BP_SmallShipTemplate_C") || actor.getName().Equals("BP_SmallShipNetProxy") || actor.getName().Equals("BP_MediumShipTemplate_C") || actor.getName().Equals("BP_MediumShipNetProxy") || actor.getName().Equals("BP_LargeShipTemplate_C") || actor.getName().Equals("BP_LargeShipNetProxy") || actor.getName().Equals("BP_Rowboat_C") || actor.getName().Equals("BP_RowboatRowingSeat_C") || actor.getName().Equals("BP_RowboatRowingSeat_C") || actor.getName().Equals("BP_Rowboat_WithFrontHarpoon_C"))
                    {
                        Ship ship = new Ship(actor);
                        float CurrentWaterLevel = ship.getCurrentWaterLevel();
                        Console.WriteLine("Ship");
                    }
                    /*else if(actor.getName().Contains("Proxy") && !actor.getName().Contains("NetProxy"))
                    {
                        ItemProxy item = new ItemProxy(actor);
                        Console.WriteLine("Treasure {0} Treasure Type {1} Rarity ID {2}", item.getTreasureName(), item.getTreasureType(), item.getTreasureRarity());
                    }*/
                    else if(actor.getName().Equals("CrewService"))
                    {
                        CrewService crewService = new CrewService(actor);
                        Crew[] crews = crewService.getCrews();
                        for (int i = 0; i < crews.Length; i++)
                        {
                            Crew c = crews[i];
                            foreach(string name in c.getPlayers())
                            {
                                if(name == localPlayer.getPlayerName())
                                {
                                    crew = c;
                                }
                            }
                        }
                    }
                    Console.WriteLine("Name : {0}  | Actual Name : {1} Pos : {2} Rot : {3}", actor.BaseName, actor.getName(), actor.getPos().ToString(), actor.getRot().ToString());
                }

                Console.WriteLine("LocalPlayer Name : {0} | LocalPlayer Actual Name : {1} LocalPlayer Pos : {2} LocalPlayer Rot : {3}", localPlayer.BaseName, localPlayer.getName(), localPlayer.getPos().ToString(), localPlayer.getRot().ToString());
                Console.WriteLine("Local Player Ship Type : {0} ", crew.getShipType());
                CameraManager cameraManager = core.GetCameraManager();
                Console.WriteLine("Camera Manager | Pos : {0}  Rot : {1}  FOV {2}", cameraManager.getPos().ToString(), cameraManager.getRot().ToString(), cameraManager.getFOV());

                Console.WriteLine("LocalPlayer in-game Name : {0} | Health : {1} | Max Health : {2} | Wielded Item Name {3} ", localPlayer.getPlayerName(), localPlayer.getHealth(), localPlayer.getMaxHealth(), localPlayer.getWieldedItem());

            }
        }
    }
}        