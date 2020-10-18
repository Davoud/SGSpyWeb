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
    public abstract class RTreeNode
    {
        protected List<RTreeNode> _children = new List<RTreeNode>();
        public string Title { get; protected set; }
        public string ID { get; protected set; }
        public string Type { get; protected set; }
        public virtual IEnumerable<RTreeNode> Children => _children;
        public virtual void AddChildren(params RTreeNode[] children) 
        {
            foreach (var child in children)
                _children.Add(child);
        }
    }

    public class RDomainNode: RTreeNode
    {       
        public RDomainNode(string name, params RComponent[] components)
        {
            Title = name;
            ID = name;
            Type = "Domain";            
        }
    }

    public class RComponentNode: RTreeNode
    {
        public RComponentNode(RComponent component)
        {
            Title = component.Name;
            ID = component.ID.Replace(".", "/");
            Type = "Component";

            var entities = new RCategoryNode("Entities", $"{ID}/ent");            
            var dependencies = new RCategoryNode("Dependencies", $"{ID}/dep");
            var services = new RCategoryNode("Services", $"{ID}/srv");

            AddChildren(entities, services, dependencies);

        }
    }

    public class RCategoryNode: RTreeNode
    {
        public RCategoryNode(string title, string id)
        {
            Title = title;
            ID = id;
            Type = "Other";
        }
    }

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

    public class RDependency
    {
        public string Name { get; }
        public string ComponentID { get; }
        public RDependency(string name)
        {
            Name = name;
            if(name.StartsWith("SystemGroup"))
            {
                ComponentID = name.Replace("SystemGroup.", null).Replace(".Common", null);
            }
        }
    }

    public class REntity
    {
        public REntity(string componentId, string name, bool isEnum, IEnumerable<RProperty> properties)
        {
            ID = $"{componentId}.{name}";
            Name = name;
            Properties = properties;
            IsEnum = isEnum;
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public bool IsEnum { get; private set; }
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

        public IEnumerable<REntity> Entities => _entities.Value;
        public IEnumerable<RDependency> Dependencies => _dependencies.Value;

        private dynamic _source;

        private Lazy<ImmutableList<REntity>> _entities;
        private Lazy<ImmutableList<RDependency>> _dependencies;

        public RComponent(dynamic compInfo)
        {
            Domain = (string)compInfo.ModuleName;
            Name = (string)compInfo.ShortComponentName;
            Version = ((string)compInfo.Version).Replace("Version: ", "");
         
            ID = $"{Domain}.{Name}";    //ID = $"{Domain}.{Name}.{Version}";
            _source = compInfo;
            _entities = new Lazy<ImmutableList<REntity>>(GetEntites);
            _dependencies = new Lazy<ImmutableList<RDependency>>(GetDependencies);

        }

        private ImmutableList<RDependency> GetDependencies()
        {
            var deps = new List<RDependency>();
            foreach (dynamic dep in (JArray)_source.Dependencies)
            {
                deps.Add(new RDependency((string)dep));
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
                entites.Add(new REntity(ID, (string)ent.Name, (bool)ent.IsEnum, properties.ToImmutableList()));
            }

            return entites.ToImmutableList();
        }
            

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is RComponent r && r.ID == ID);
                        

    }


}
