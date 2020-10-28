using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.ViewModels
{
    public class REntity
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Domain { get; set; }
        public string ComponentName { get; set; }
        public bool IsEnum { get; set; }
        public int Count { get; set; }
        public IEnumerable<RProperty> Properties { get; set; }
    }

    public class RProperty
    {        
        public string Name { get; set;  }
        public string Type { get; set; }
    }
}
