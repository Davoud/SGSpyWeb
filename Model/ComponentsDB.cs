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
    public static class ComponentsDB
    {
        private static readonly ConcurrentDictionary<string, Component> db = new ConcurrentDictionary<string, Component>();

        public static IEnumerable<Component> LoadedComponents() => db.Values;                

        public static void Load(string path)
        {
            Console.WriteLine("Loading From: " + path);

            var loader = new Utils.Concurrency.ChannelAgent<string>();

            loader.Subscribe(filePath =>
            {
                try
                {
                    var  cmp = new Component(JObject.Parse(File.ReadAllText(filePath)));                    
                    db[$"{cmp.ComponentName}-{cmp.Domain}"] = cmp;
                    Console.WriteLine($"Added {cmp.ID}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            var commons = Directory.GetFiles(path, "*.json");
            foreach (var dllInfo in commons) 
                if(dllInfo.Contains("Retail"))
                    loader.Send(dllInfo);
            

            Console.WriteLine("Sent all for loading");
        }

       

        public static Option<Component> GetByID(string componentId) =>
            db.TryGetValue(componentId, out var cmp) ? Some(cmp) : None;

        public static Option<Component> Get(string domain, string component) =>
            db.TryGetValue($"{component}-{domain}", out var cmp) ? Some(cmp) : None;
    }

   
}
