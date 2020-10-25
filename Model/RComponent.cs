using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SGSpyWeb.Model
{
    public class RComponent: IAssemblyItem
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Component { get; private set; }
        public string ID { get; private set; }
        public string Domain { get; private set; }

        public IEnumerable<REntity> Entities => _entities.Value;
        public IEnumerable<RDependency> Dependencies => _dependencies.Value;
        public IEnumerable<RServiceInterface> Services => _services.Value;

        private dynamic _source;

        private Lazy<ImmutableList<REntity>> _entities;
        private Lazy<ImmutableList<RDependency>> _dependencies;
        private Lazy<ImmutableList<RServiceInterface>> _services;

        public RComponent(dynamic compInfo)
        {
            Domain = (string)compInfo.ModuleName;
            Name = (string)compInfo.ShortComponentName;
            Component = Name;
            Version = ((string)compInfo.Version).Replace("Version:", "").Trim();
         
            if(Name == "Common")
            {
                Name = Domain;
                Domain = "Framework";
            }

            ID = $"{Domain}.{Name}.{Version}";
            _source = compInfo;
            _entities = new Lazy<ImmutableList<REntity>>(GetEntites);
            _dependencies = new Lazy<ImmutableList<RDependency>>(GetDependencies);
            _services = new Lazy<ImmutableList<RServiceInterface>>(GetServices);

        }

        private ImmutableList<RServiceInterface> GetServices()
        {
            var servies = new List<RServiceInterface>();
            foreach (dynamic @interface in (JArray)_source.ServiceInterfaces)
            {
                var srv = new RServiceInterface(this, (string)@interface.Name, (int)@interface.Type == 0);
                servies.Add(srv);
            }
            return servies.ToImmutableList();
        }

        private ImmutableList<RDependency> GetDependencies()
        {
            var deps = new List<RDependency>();
            foreach (dynamic dep in (JArray)_source.Dependencies)
            {
                deps.Add(new RDependency(ID, (string)dep));
            }
            return deps.ToImmutableList();
        }
       
        private ImmutableList<REntity> GetEntites()
        {
            var entites = new List<REntity>();
           
            foreach(dynamic ent in (JArray)_source.Entities)
            {
                var properties = new List<RProperty>();
                foreach(dynamic property in (JArray)ent.Properties)
                {
                    properties.Add(new RProperty((string)property.Name, (string)property.TypeName));
                }
                entites.Add(new REntity(this, (string)ent.Name, (bool)ent.IsEnum, properties.ToImmutableList()));
            }

            return entites.ToImmutableList();
        }
            

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is RComponent r && r.ID == ID);
                        

    }


}
