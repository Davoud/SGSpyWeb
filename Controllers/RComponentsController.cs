using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGSpyWeb.Model;
using SGSpyWeb.ViewModel;
using SGSpyWeb.ViewModels;
using Utils;
using Utils.Option;


namespace SGSpyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RComponentsController : ControllerBase
    {
       
        [HttpGet]
        public IEnumerable<RDomain> Get()
        {
            var domains = new SortedList<string, RDomain>();
            foreach (var cmp in ComponentsDB.LoadedComponents())
            {
                if (domains.TryGetValue(cmp.Domain, out var domain))
                {
                    domain.Add(cmp);
                }
                else
                {
                    domains.Add(cmp.Domain, new RDomain(cmp.Domain, cmp));
                }
            }
            return domains.Values;
        }

        
        [HttpGet("{domain}")]
        public IEnumerable<RComponent> GetComponents(string domain)
        {
            foreach (var cmd in ComponentsDB.LoadedComponents())
            {
                if (cmd.Domain == domain)
                {
                    yield return new RComponent
                    {
                        ID = cmd.ID,
                        Version = cmd.Version,
                        Name = cmd.Name,
                    };
                }
            }
        }



        [HttpGet("{domain}/{component}/entities")]
        public IEnumerable<REntity> GetEntities(string domain, string component)
        {
            var entities = ComponentsDB.Get(domain, component)
               .Match(
                   none: () => Enumerable.Empty<Entity>(),
                   some: cmp => cmp.Entities);

            foreach(var entity in entities)
            {
                yield return new REntity
                {
                    ID = entity.ID,
                    Domain = entity.Domain,
                    ComponentName = entity.Domain,
                    Name = entity.Name,
                    IsEnum = entity.IsEnum,
                    Count = entity.Properties.Count(),
                    Properties = ToViewModel(entity.Properties),
                };
            }
        }
        
        private IEnumerable<RProperty> ToViewModel(IEnumerable<Property> properties) =>                    
             properties.Aggregate(new List<RProperty>(properties.Count()), 
                (rproperties, p) =>
                {
                    rproperties.Add(new RProperty { Name = p.Name, Type = p.Type });
                    return rproperties;
                });
        
        
        [HttpGet("{domain}/{component}/dependencies")]
        public IEnumerable<RDependency> GetDependencies(string domain, string component)
        {            
            var deps = ComponentsDB.Get(domain, component)
                .Match(
                    none: () => Enumerable.Empty<Dependency>(), 
                    some: cmp => cmp.Dependencies);
            
            foreach(var dependency in deps)
            {
                yield return new RDependency
                {
                    ID = dependency.ID,
                    Domain = dependency.Domain,
                    ComponentName = dependency.ComponentName,
                    Name = dependency.Name,
                };
            }
        }


        [HttpGet("{domain}/{component}/services")]
        public IEnumerable<RServiceInterface> GetInterfaces(string domain, string component)
        {
            var services = ComponentsDB.Get(domain, component)
                .Match(
                    none: () => Enumerable.Empty<ServiceInterface>(),
                    some: cmp => cmp.Services);

            foreach (var service in services)
            {
                yield return new RServiceInterface
                {
                    ID = service.ID,
                    Name = service.Name,
                    Domain = service.Domain,
                    Component = service.ComponentName,
                    IsComponentService = service.IsComponentService
                };
            }
        }

        [HttpGet("{domain}/{component}/services/{interfaceName}")]
        public IEnumerable<RMethod> GetMethods(string domain, string component, string interfaceName)
        {
            return null;
        }
    }
}
