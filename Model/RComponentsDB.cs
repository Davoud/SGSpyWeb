using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.Model
{
    public static class RComponentsDB
    {
        private static readonly ConcurrentDictionary<string, RComponent> db = new ConcurrentDictionary<string, RComponent>();

        public static IEnumerable<RComponent> LoadedComponents() => db.Values;                

        public static void Load(string path)
        {
            Console.WriteLine("Loading From: " + path);

            var loader = new Utils.Concurrency.ChannelAgent<string>();

            loader.Subscribe(filePath =>
            {
                try
                {
                    var  cmp = new RComponent(JObject.Parse(File.ReadAllText(filePath)));                    
                    db[cmp.ID] = cmp;
                    Console.WriteLine($"Added {cmp.ID}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            var commons = Directory.GetFiles(path, "*Retail*.json");
            foreach (var dllInfo in commons)
                loader.Send(dllInfo);
            

            Console.WriteLine("Sent all for loading");
        }

        public static RComponent GetByID(string componentId) => db[componentId];
    }

   
}
