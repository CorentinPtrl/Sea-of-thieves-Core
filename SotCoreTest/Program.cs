using System;
using System.Threading;
using SoT;
using SoT.Game;

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
                    Console.WriteLine(actor.Position);

                    if(actor.Name.Equals("BP_PlayerPirate_C"))
                    {
                        Player player = new Player(actor);
                        Console.WriteLine("Player Name : {0} Current Health : {1} Max Health : {1}",player.PlayerName, player.CurrentHealth, player.MaxHealth);
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}       