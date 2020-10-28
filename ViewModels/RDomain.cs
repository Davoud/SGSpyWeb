using SGSpyWeb.Model;
using SGSpyWeb.ViewModels;
using System.Collections.Generic;


namespace SGSpyWeb.ViewModel
{
    public class RDomain
    {
        private ISet<RComponent> _components;
        public string Name { get; set; }                
        public int Count { get; set; }
        public RDomain(string name, params Component[] components)
        {
            Name = name;
            _components = new SortedSet<RComponent>();
            foreach (var cmp in components)
                _components.Add(
                    new RComponent
                    {
                        Name = cmp.Name,
                        Version = cmp.Version,
                        ID = cmp.ID,
                    });
        }

        public void Add(Component cmp)
        {
            _components.Add(new RComponent
            {
                Name = cmp.Name,
                Version = cmp.Version,
                ID = cmp.ID,
            });
        }
    }

    


}
