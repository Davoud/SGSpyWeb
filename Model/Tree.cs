using System;
using System.Collections.Generic;
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
        public string Name { get; protected set; }
        public string Domain { get; protected set; }
        public string Component { get; protected set; }
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
            Name = name;          
            ID = name;
            Type = "Domain";            
        }
    }

    public class RComponentNode: RTreeNode
    {
        public RComponentNode(RComponent component)
        {
            Name = component.Name;
            ID = component.ID;
            Domain = component.Domain;
            Component = component.Component;
            Type = "Component";        
        }
    }

    public class RCategoryNode: RTreeNode
    {
        public RCategoryNode(string title, string id)
        {
            Name = title;
            ID = id;
            Type = "Other";
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

    


}
