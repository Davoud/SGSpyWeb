using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGSpyWeb.Model;

namespace SGSpyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RExplorerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<RTreeNode> Get()
        {
            var domainNodes = new SortedList<string, RTreeNode>();

            foreach (var cmp in RComponentsDB.LoadedComponents())
            {
                if (domainNodes.TryGetValue(cmp.Domain, out var domain))
                {
                    domain.AddChildren(new RComponentNode(cmp));
                }
                else
                {
                    domain = new RDomainNode(cmp.Domain);
                    domain.AddChildren(new RComponentNode(cmp));
                    domainNodes.Add(cmp.Domain, domain);
                }
            }

            return domainNodes.Values;




        }
    }
}
