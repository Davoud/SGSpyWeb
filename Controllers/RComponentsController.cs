using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGSpyWeb.Model;
using Utils;
using Utils.Option;


namespace SGSpyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RComponentsController : ControllerBase
    {
       
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
             RComponentsDB.Get(domain, component)
                .Match(
                    none: () => Enumerable.Empty<REntity>(), 
                    some: cmp => cmp.Entities);
        
        
        [HttpGet("{domain}/{component}/dependencies")]
        public IEnumerable<RDependency> GetDependencies(string domain, string component)
        {            
            return RComponentsDB
                .Get(domain, component)
                .Match(
                    none: () => Enumerable.Empty<RDependency>(), 
                    some: cmp => cmp.Dependencies);
        }


        [HttpGet("{domain}/{component}/services")]
        public IEnumerable<RServiceInterface> GetServices(string domain, string component)
        {
            return RComponentsDB
                .Get(domain, component)
                .Match(
                    none: () => Enumerable.Empty<RServiceInterface>(),
                    some: cmp => cmp.Services);
        }
    }
}
