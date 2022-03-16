using System;
using System.Threading;
using SoT;
using SoT.UE4;

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
                }
            }
        }
    }
}       