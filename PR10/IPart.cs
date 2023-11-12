using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR10
{
    public interface IPart
    {
        string Name { get; }
        bool IsBuilt { get; set; }
    }
}
