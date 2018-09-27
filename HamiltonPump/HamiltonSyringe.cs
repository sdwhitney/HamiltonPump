using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public class HamiltonSyringe : ISyringePump
    {
        private PumpConfig PumpConfig { get; set; }
        private PumpAddress PumpAddress { get; set; }

        public void AbsolutePosition(int steps)
        {
            throw new NotImplementedException();
        }

        public void Delay(int msec)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommandBuffer()
        {
            throw new NotImplementedException();
        }

        public void HaltCommandExecution(int digitalInput)
        {
            throw new NotImplementedException();
        }

        public void InitializeSyringeAndValve(bool outputToLeft = true)
        {
            throw new NotImplementedException();
        }

        public void MoveValve(int valvePosition)
        {
            throw new NotImplementedException();
        }

        public bool QueryStatus(out char status)
        {
#if false
            frmMain.txtSend.Text = $"/{GetPump()}Q";
            frmMain.SendData();
#else
            throw new NotImplementedException();
#endif
        }

        public void RelativeDispense(int steps)
        {
            throw new NotImplementedException();
        }

        public void RelativePickup(int steps)
        {
            throw new NotImplementedException();
        }

        public void SetAcceleration(int acceleration)
        {
            throw new NotImplementedException();
        }

        public void SetBacklash(int steps)
        {
            throw new NotImplementedException();
        }

        public void SetDeceleration(int deceleration)
        {
            throw new NotImplementedException();
        }

        public void SetSpeed(int stepsPerSec)
        {
            throw new NotImplementedException();
        }

        public void SetStartVelocity(int stepsPerSec)
        {
            throw new NotImplementedException();
        }

        public void SetStopVelocity(int stepsPerSec)
        {
            throw new NotImplementedException();
        }

        public void TerminateCommandBuffer()
        {
            throw new NotImplementedException();
        }
    }
}
