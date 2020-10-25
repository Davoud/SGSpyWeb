using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.Model
{
    public class RServiceInterface : IAssemblyItem
    {
        public string Name { get; }

        public string Domain { get; }

        public string Component { get; }

        public string ID { get; }

        public bool IsComponentService { get; }

        public IEnumerable<RService> Services { get; }
        public RServiceInterface(RComponent component, string name, bool isComSrv)
        {
            Name = name;
            Component = component.Component;
            Domain = component.Domain;
            ID = $"{component.ID}.{name}";
            IsComponentService = isComSrv;
        }
    }

    public class RService: IAssemblyItem
    {
        public string Name { get; }

        public string Domain { get; }

        public string Component { get; }

        public string ID { get; }

        public RServiceInterface Interface { get; }
    }
}
