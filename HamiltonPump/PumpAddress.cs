using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public enum PumpAddress
    {
        All   = (int)'_', // broadcast address to all pumps
        Lane1 = (int)'1', // first pump address
        Lane2 = (int)'2'  // second pump address
    }
}
