using System;
using CLI;
namespace SotEspCoreTest
{
    class Program
    {

        public static string convertVecToStr(VectorUE4 vec)
        {
            return "(X: " + vec.x + " Y: " +vec.y+ " Z: " + vec.z + ")";
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SotCore core = new SotCore();
            bool test = core.Prepare();
            Console.WriteLine("Finish");
            SotLevel level = new SotLevel();
            UE4ActorWrapper[] actors = level.getActors();
            foreach(UE4ActorWrapper actor in actors)
            {
                Console.WriteLine("Name : "+actor.name + " Pos: "+ convertVecToStr(actor.pos));
            }
        }
    }
}
