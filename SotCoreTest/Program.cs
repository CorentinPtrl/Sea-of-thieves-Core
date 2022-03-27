using System;
using SoT;
using SoT.Data;
using SoT.Game;
using SoT.Game.Service;

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
                    Console.WriteLine("Name : {0} Class Name : {1} Parent Class Name {2}", actor.Name, actor.ClassName, actor.ParentClassName);
                    Console.WriteLine("Position {0} Custom Position {1}", actor.Position, actor.GetCustomPosition<Vector3>());

                    if (actor.Name.Equals("BP_PlayerPirate_C"))
                    {
                        Player PiratePlayer = new Player(actor);
                        Console.WriteLine("Player Name : {0} Current Health : {1} Max Health : {2} Wielded Item : {3}", PiratePlayer.PlayerName, PiratePlayer.CurrentHealth, PiratePlayer.MaxHealth, PiratePlayer.CurrentWieldedItem);
                    }
                    else if (actor.Name.Equals("BP_SmallShipTemplate_C") || actor.Name.Equals("BP_SmallShipNetProxy") || actor.Name.Equals("BP_MediumShipTemplate_C") || actor.Name.Equals("BP_MediumShipNetProxy") || actor.Name.Equals("BP_LargeShipTemplate_C") || actor.Name.Equals("BP_LargeShipNetProxy") || actor.Name.Equals("BP_Rowboat_C") || actor.Name.Equals("BP_RowboatRowingSeat_C") || actor.Name.Equals("BP_RowboatRowingSeat_C") || actor.Name.Equals("BP_Rowboat_WithFrontHarpoon_C"))
                    {
                        Ship ship = new Ship(actor);
                        ShipInternalWater InternalWaterComponent = ship.ShipInternalWater;
                        SinkingShipParams SinkingShipParams = ship.SinkingShipParams;
                        Console.WriteLine("Water Level {0} Water Amount {1} Crew Id : {2}", InternalWaterComponent.CurrentVisualWaterLevel, InternalWaterComponent.WaterAmount, ship.CrewId);
                    }
                    else if(actor.Name.Equals("IslandService"))
                    {
                        IslandService IslandService = new IslandService(actor);
                        TArray<Island> IslandArray = IslandService.IslandArray;
                        for (int i = 0; i < IslandArray.Length; i++)
                        {
                            Island Island = IslandArray.GetValue(i);
                            Console.WriteLine("Island Name : {0}", core.Engine.GetName(Island.FNameIndex));
                        }
                    }
                    else if (actor.Name.Equals("CrewService"))
                    {
                        CrewService CrewService = new CrewService(actor);
                        TArray<Crew> Crews = CrewService.Crews;
                        for (int i = 0; i < Crews.Length; i++)
                        {
                            Crew Crew = new Crew(Crews.GetValueAddress(i));
                            TArray<Player> Players = Crew.Players;
                            for(int d = 0; d < Players.Length; d++)
                            {
                                Player player = new Player(Players.GetValuePtr(d), true);
                                Console.WriteLine("Player Name : {0} Crew : {1}",player.PlayerName, Crew.CrewId);
;                            }
                        }
                    }
                }
                Player LocalPlayer = core.LocalPlayer;
                Console.WriteLine("Local Player Name : {0} Current Health : {1} Max Health : {2} Oxygen Level : {3} Wielded Item : {4}", LocalPlayer.PlayerName, LocalPlayer.CurrentHealth, LocalPlayer.MaxHealth, LocalPlayer.OxygenLevel, LocalPlayer.CurrentWieldedItem);

                CameraManager cameraManager = core.CameraManager;
                Console.WriteLine("Camera Manager Location : {0} Rotation : {1} FOV : {2}", cameraManager.Location, cameraManager.Rotation, cameraManager.FOV);
            }
        }
    }
}