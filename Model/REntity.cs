using System.Collections.Generic;
using System.Collections.Immutable;

namespace SGSpyWeb.Model
{
    public class REntity: IAssemblyItem
    {        
        public REntity(RComponent component, string name, bool isEnum, IEnumerable<RProperty> properties)
        {
            ID = $"{component.ID}.{name}";
            Name = name;
            Component = component.Component;
            Domain = component.Domain;
            IsEnum = isEnum;
            Properties = properties;
        }

        public string Name { get; private set; }
        public string ID { get; private set; }
        public string Domain { get; private set; }
        public string Component { get; private set; }

        public bool IsEnum { get; private set; }
        public IEnumerable<RProperty> Properties { get; }
        public override string ToString() => IsEnum ? $"[Enum: {ID}]" : $"[Entity: {ID}]";        
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
}
