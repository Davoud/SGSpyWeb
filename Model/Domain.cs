using System.Collections.Generic;

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

    


}
