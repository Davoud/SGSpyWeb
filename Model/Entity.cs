using System.Collections.Generic;
using System.Collections.Immutable;

namespace SGSpyWeb.Model
{
    public class Entity: IAssemblyItem
    {        
        public Entity(Component component, string name, bool isEnum, IEnumerable<Property> properties)
        {
            ID = $"{component.ID}.{name}";
            Name = name;
            ComponentName = component.ComponentName;
            Domain = component.Domain;
            IsEnum = isEnum;
            Properties = properties;
        }

        public string Name { get; private set; }
        public string ID { get; private set; }
        public string Domain { get; private set; }
        public string ComponentName { get; private set; }

        public bool IsEnum { get; private set; }
        public IEnumerable<Property> Properties { get; }
        public override string ToString() => IsEnum ? $"[Enum: {ID}]" : $"[Entity: {ID}]";        
    }

    public class Property
    {
        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public string Type { get; }
    }
}
