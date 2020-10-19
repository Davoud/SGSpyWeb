using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGSpyWeb.Model;
using Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGSpyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RComponentsController : ControllerBase
    {
        // GET: <RComponentsController>
        [HttpGet]
        public IEnumerable<Domain> Get()
        {
            var domains = new SortedList<string, Domain>();
            foreach (var cmp in RComponentsDB.LoadedComponents())
            {
                if (domains.TryGetValue(cmp.Domain, out var domain))
                {
                    domain.Add(cmp);
                }
                else
                {
                    domains.Add(cmp.Domain, new Domain(cmp.Domain, cmp));
                }
            }
            return domains.Values;
        }

        
        [HttpGet("{domain}")]
        public IEnumerable<ComponentHeader> GetComponents(string domain) =>
             RComponentsDB.GetByDomain(domain);

        
        [HttpGet("{domain}/{component}/entities")]
        public IEnumerable<REntity> GetEntities(string domain, string component) =>
             RComponentsDB.GetByID($"{domain}.{component}").Match(() => Enumerable.Empty<REntity>(), cmp => cmp.Entities);
        
        
        [HttpGet("{id}/dependencies")]
        public IEnumerable<RDependency> GetDependencies(string id)
        {
            var deps = RComponentsDB.GetByID(id);
            if (deps.IsSome())
                return deps.Value.Dependencies;

            return Enumerable.Empty<RDependency>();

        }

    }
}
