using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public enum PumpAddress
    {
        All   = -1, // broadcast to all pumps
        Lane1 =  0, // first pump
        Lane2 =  1  // second pump
    }
}
