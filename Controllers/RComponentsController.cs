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
            var domains = new Dictionary<string, Domain>();
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

        // GET api/<RComponentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RComponentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RComponentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RComponentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
