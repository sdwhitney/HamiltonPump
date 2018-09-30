using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public class IdexRotaryValve : IRotaryValve
    {
        public enum RotaryValvePosition
        {
            Position1 = 0x01,
            Position2 = 0x02,
            Position3 = 0x03,
            Position4 = 0x04,
            Position5 = 0x05,
            Position6 = 0x06,
            Position7 = 0x07,
            Position8 = 0x08,
            Position9 = 0x09,
            Position10 = 0x0A,
            Position11 = 0x0B,
            Position12 = 0x0C,
            Position13 = 0x0D,
            Position14 = 0x0E,
            Position15 = 0x0F,
            Position16 = 0x10,
            Position17 = 0x11,
            Position18 = 0x12,
            Position19 = 0x13,
            Position20 = 0x14,
            Position21 = 0x15,
            Position22 = 0x16,
            Position23 = 0x17,
            Position24 = 0x18
        }

        public void MoveToHome()
        {
            throw new NotImplementedException();
        }

        public void MoveToPosition(int position)
        {
            throw new NotImplementedException();
        }

        public int QueryStatus()
        {
            throw new NotImplementedException();
        }

    }
}
