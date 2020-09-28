using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.Model
{
    public class Domain
    {
        private ISet<ComponentHeader> _components;

        public string Name { get; }
        public IEnumerable<ComponentHeader> Components => _components;

        public Domain(string name, params RComponent[] components)
        {
            Name = name;
            _components = new HashSet<ComponentHeader>();
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

    public class ComponentHeader
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string ID { get; set; }

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is ComponentHeader r && r.ID == ID);
    }

    public class RComponent
    {
        public string Name { get; private set; }
        public string Version { get; private set; }

        public string ID { get; private set; }
        public string Domain { get; private set; }

        private dynamic _source;
        

        public RComponent(dynamic compInfo)
        {         
            Domain = (string)compInfo.ModuleName;
            Name = (string)compInfo.ShortComponentName;
            Version = ((string)compInfo.Version).Replace("Version: ", "");
            ID = $"{Domain}.{Name}.{Version}";
            _source = compInfo;
        }

        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is RComponent r && r.ID == ID);
                        

    }


}
