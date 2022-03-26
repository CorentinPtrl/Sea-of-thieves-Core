using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoT.Util
{
    public class JsonManager
    {
        public static Dictionary<string, Actor> GetJsonActors()
        {

            Stream stream = Assembly.GetAssembly(typeof(SotCore)).GetManifestResourceStream(Assembly.GetAssembly(typeof(SotCore)).GetName().Name + ".Resources.actors.json");
            if (stream == null)
                return null;
            StreamReader streamReader = new StreamReader(stream);
            string jsonText = streamReader.ReadToEnd();
            ConcurrentDictionary<string, Actor>  Json = JsonConvert.DeserializeObject<ConcurrentDictionary<string, Actor>>(jsonText);
            Dictionary<String, Actor> result = new Dictionary<string, Actor>();
            foreach(KeyValuePair<String, Actor> actor in Json.ToArray())
            {
                Actor newActor = new Actor();
                newActor.Name = actor.Key;
                newActor.Category = actor.Value.Category;
                result.Add(actor.Value.Name, newActor);
            }
            return result;
        }

        public static Dictionary<string, ulong> GetJsonOffsets()
        {

            Stream stream = Assembly.GetAssembly(typeof(SotCore)).GetManifestResourceStream(Assembly.GetAssembly(typeof(SotCore)).GetName().Name + ".Resources.offsets.json");
            if (stream == null)
                return null;
            StreamReader streamReader = new StreamReader(stream);
            string jsonText = streamReader.ReadToEnd();
            Dictionary<String, ulong> json = JsonConvert.DeserializeObject<Dictionary<String, ulong>>(jsonText);
            return json;
        }

        public struct Actor
        {
            public String Name;
            public String Category;
        }
    }
}
