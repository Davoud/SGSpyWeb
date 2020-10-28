using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.ViewModels
{
    public class RServiceInterface
    {        
        public string ID { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Component { get; set; }
        public bool IsComponentService { get; set; }
        public int Count { get; set; }

    }
}
