using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGSpyWeb.Model
{
    public interface IAssemblyItem
    {
        string Name { get; }
        string Domain { get; }
        string Component { get; }
        string ID { get; }
    }
}
