using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Utils;
using static Utils.OptionHelpers;


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

            var commons = Directory.GetFiles(path, "*.json");
            foreach (var dllInfo in commons)
                loader.Send(dllInfo);
            

            Console.WriteLine("Sent all for loading");
        }

        public static IEnumerable<ComponentHeader> GetByDomain(string domain)
        {
            foreach(var cmd in db.Values)
            {
                if(cmd.Domain == domain)
                {
                    yield return new ComponentHeader
                    {
                        ID = cmd.ID,
                        Version = cmd.Version,
                        Name = cmd.Name,
                    };
                }
            }
        }

        public static Option<RComponent> GetByID(string componentId) =>
            db.TryGetValue(componentId, out var cmp) ? Some(cmp) : None;
    }

   
}
