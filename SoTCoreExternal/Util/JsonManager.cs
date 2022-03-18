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
        public static Dictionary<string, string> GetJsonActors()
        {
            
            Stream stream = Assembly.GetAssembly(typeof(SotCore)).GetManifestResourceStream(Assembly.GetAssembly(typeof(SotCore)).GetName().Name+".Resources.actors.json");
            if (stream == null)
                return null;
            StreamReader streamReader = new StreamReader(stream);
            string jsonText = streamReader.ReadToEnd();
            Dictionary<String, String> json = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(jsonText).ToDictionary(x => x.Value, x => x.Key);
            return json;
        }
    }
}
