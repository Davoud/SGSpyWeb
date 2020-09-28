using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SGSpyWeb.Model
{
    public class Domain
    {
        private ISet<ComponentHeader> _components;

        public string Name { get; }
        public IEnumerable<ComponentHeader> Components => _components;
        public bool Opened { get; }

        public Domain(string name, params RComponent[] components)
        {
            Name = name;
            _components = new SortedSet<ComponentHeader>();
            foreach (var cmp in components)
                _components.Add(
                    new ComponentHeader
                    {
                        Name = cmp.Name,
                        Version = cmp.Version,
                        ID = cmp.ID,
                    });
        }

        public void Add(RComponent cmp)
        {
            _components.Add(new ComponentHeader
            {
                Name = cmp.Name,
                Version = cmp.Version,
                ID = cmp.ID,
            });
        }
    }

    public class ComponentHeader: IComparable<ComponentHeader>
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string ID { get; set; }

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is ComponentHeader r && r.ID == ID);

        public int CompareTo([AllowNull] ComponentHeader other) => Name.CompareTo(other.Name);
        
    }

    public class REntity
    {
        public REntity(string componentId, string name, IEnumerable<RProperty> properties)
        {
            ID = $"{componentId}.{name}";
            Name = name;
            Properties = properties;
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public IEnumerable<RProperty> Properties { get; }
        public override string ToString() => $"{Name} ({ID})";        
    }

    public class RProperty
    {
        public RProperty(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public string Type { get; }
    }
    public class RComponent
    {
        public string Name { get; private set; }
        public string Version { get; private set; }

        public string ID { get; private set; }
        public string Domain { get; private set; }

        private dynamic _source;

        private Lazy<ImmutableList<REntity>> _entities;

        public RComponent(dynamic compInfo)
        {
            Domain = (string)compInfo.ModuleName;
            Name = (string)compInfo.ShortComponentName;
            Version = ((string)compInfo.Version).Replace("Version: ", "");
            //ID = $"{Domain}.{Name}.{Version}";
            ID = $"{Domain}.{Name}";
            _source = compInfo;
            _entities = new Lazy<ImmutableList<REntity>>(GetEntites);

        }

        public IEnumerable<REntity> Entities => _entities.Value;

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
                entites.Add(new REntity(ID, (string)ent.Name, properties.ToImmutableList()));
            }

            return entites.ToImmutableList();
        }
            

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is RComponent r && r.ID == ID);
                        

    }


}
