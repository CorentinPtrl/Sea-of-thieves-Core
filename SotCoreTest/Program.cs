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
                UEObject[] actors = core.GetActors();
                foreach (UEObject actor in actors)
                {
                    Console.WriteLine(SotCore.Instance.GetName(SotCore.Instance.Memory.ReadProcessMemory<int>(actor.Address + 0x18)));
                }
            }
        }
    }
}        