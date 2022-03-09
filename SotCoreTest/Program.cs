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
                    Console.WriteLine(actor.Name);
                }
            }
        }
    }
}        