using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.Model
{
    public class ServiceInterface : IAssemblyItem
    {
        public string Name { get; }

        public string Domain { get; }

        public string ComponentName { get; }

        public string ID { get; }

        public bool IsComponentService { get; }

        
        public IEnumerable<RMethod> Members { get; }
        public ServiceInterface(Component component, string name, bool isComSrv, dynamic @interface)
        {
            Name = name;
            ComponentName = component.ComponentName;
            Domain = component.Domain;
            ID = $"{component.ID}.{name}";
            IsComponentService = isComSrv;
            Members = ExtractMembers(@interface);
        }

        private IEnumerable<RMethod> ExtractMembers(dynamic @interface)
        {
            var members = new List<RMethod>();
            foreach (dynamic method in (JArray)@interface.Methods)
            {
                members.Add(new RMethod(this, (string)method.Name));
            }
            return members.ToImmutableList();
        }

    }

    public class RMethod: IAssemblyItem
    {
        public RMethod(ServiceInterface @interface, string name)
        {
            Name = name;
            Interface = @interface;            
            ID = $"{Interface.ID}.{name}";
        }
        public string Name { get; }

        public string Domain => Interface.Domain;

        public string ComponentName => Interface.ComponentName;

        public string ID { get; }

        public ServiceInterface Interface { get; }

        
    }


}
