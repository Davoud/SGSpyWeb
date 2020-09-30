using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGSpyWeb.Model;

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
            foreach(var cmp in RComponentsDB.LoadedComponents())
            {
                if(domains.TryGetValue(cmp.Domain, out var domain))
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

        // GET <RComponentsController>/id/entities
        [HttpGet("{id}/entities")]
        public IEnumerable<REntity> GetEntities(string id)
        {
            return RComponentsDB.GetByID(id).Entities;

        }

        // GET <RComponentsController>/id/dependencies
        [HttpGet("{id}/dependencies")]
        public IEnumerable<RDependency> GetDependencies(string id)
        {
            return RComponentsDB.GetByID(id).Dependencies;

        }

    }
}
