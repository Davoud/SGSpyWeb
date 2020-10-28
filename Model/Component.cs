using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SGSpyWeb.Model
{
    public class Component: IAssemblyItem
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string ComponentName { get; private set; }
        public string ID { get; private set; }
        public string Domain { get; private set; }

        public IEnumerable<Entity> Entities => _entities.Value;
        public IEnumerable<Dependency> Dependencies => _dependencies.Value;
        public IEnumerable<ServiceInterface> Services => _services.Value;

        private dynamic _source;

        private Lazy<ImmutableList<Entity>> _entities;
        private Lazy<ImmutableList<Dependency>> _dependencies;
        private Lazy<ImmutableList<ServiceInterface>> _services;

        public Component(dynamic compInfo)
        {
            Domain = (string)compInfo.ModuleName;
            Name = (string)compInfo.ShortComponentName;
            ComponentName = Name;
            Version = ((string)compInfo.Version).Replace("Version:", "").Trim();
         
            if(Name == "Common")
            {
                Name = Domain;
                Domain = "Framework";
            }

            ID = $"{Domain}.{Name}.{Version}";
            _source = compInfo;
            _entities = new Lazy<ImmutableList<Entity>>(GetEntites);
            _dependencies = new Lazy<ImmutableList<Dependency>>(GetDependencies);
            _services = new Lazy<ImmutableList<ServiceInterface>>(GetServices);

        }

        private ImmutableList<ServiceInterface> GetServices()
        {
            var servies = new List<ServiceInterface>();
            foreach (dynamic @interface in (JArray)_source.ServiceInterfaces)
            {                
                var srv = new ServiceInterface(this, (string)@interface.Name, (int)@interface.Type == 0, @interface);
                servies.Add(srv);
            }
            return servies.ToImmutableList();
        }

       

        private ImmutableList<Dependency> GetDependencies()
        {
            var deps = new List<Dependency>();
            foreach (dynamic dep in (JArray)_source.Dependencies)
            {
                deps.Add(new Dependency(ID, (string)dep));
            }
            return deps.ToImmutableList();
        }
       
        private ImmutableList<Entity> GetEntites()
        {
            var entites = new List<Entity>();
           
            foreach(dynamic ent in (JArray)_source.Entities)
            {
                var properties = new List<Property>();
                foreach(dynamic property in (JArray)ent.Properties)
                {
                    properties.Add(new Property((string)property.Name, (string)property.TypeName));
                }
                entites.Add(new Entity(this, (string)ent.Name, (bool)ent.IsEnum, properties.ToImmutableList()));
            }

            return entites.ToImmutableList();
        }
            

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is Component r && r.ID == ID);
                        

    }


}
