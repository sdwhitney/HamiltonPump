using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public class HamiltonSyringe : ISyringePump
    {
        /// <summary>
        /// One instance of com port for all syringe pumps
        /// </summary>
        static ComPortManager ComPort;

        /// <summary>
        /// Address of this syringe pump
        /// </summary>
        private PumpAddress PumpAddress { get; set; }

        /// <summary>
        /// Valid acceleration values
        /// </summary>
        public enum Accel
        {
            A2500 = 1,
            A5000 = 2,
            A7500 = 3,
            A10000 = 4,
            A12500 = 5,
            A15000 = 6,
            A17500 = 7,
            A20000 = 8,
            A22500 = 9,
            A25000 = 10,
            A27500 = 11,
            A30000 = 12,
            A32500 = 13,
            A35000 = 14,
            A37500 = 15,
            A40000 = 16,
            A42500 = 17,
            A45000 = 18,
            A47500 = 19,
            A50000 = 20
        }

        public HamiltonSyringe(PumpAddress addr)
        {
            PumpAddress = addr;

            // Create and open single instance of com port if needed
            if (ComPort == null)
            {
                ComPort = new ComPortManager
                {
                    PortName = "COM18",
                    BaudRate = "9600",
                    DataBits = "8",
                    Parity = "None",
                    StopBits = "1",
                    AutoEOL = true,
                    CurrentTransmissionType = ComPortManager.TransmissionType.Text,
                    // TODO:  ComPort should not know anything about displaying data!!!
#warning ComPortManager needs DisplayWindow set to RichTextBox presently! Change to JBL message event!
                    //DisplayWindow = frmMain.ExerciserOutput
                };
            }

            if (!ComPort.IsPortOpen)
            {
                ComPort.OpenPort();
            }
        }

        /// <summary>
        /// Send command that executes command buffer
        /// </summary>
        public void ExecuteCommandBuffer()
        {
            SendDataAndWait($"/{PumpAddress}R");
        }

        /// <summary>
        /// Initialize syringe pump, with output valve in left or right port position as specified
        /// </summary>
        /// <param name="outputToLeft">If true, set valve output to left port position, otherwise set to right port position</param>
        public void InitializeSyringeAndValve(bool outputToLeft = true)
        {
            // TODO: May want to keep track of current valve position!
            if (outputToLeft)
            {
                // Send initialization, with output port to left
                SendDataAndWait($"/{PumpAddress}YR");
            }
            else
            {
                // Send initialization, with output port to right
                SendDataAndWait($"/{PumpAddress}ZR");
            }
        }

        /// <summary>
        /// Move syringe to specified absolute position in steps
        /// </summary>
        /// <param name="steps">Number of steps to move, 0 to 192,000</param>
        public void AbsolutePosition(int steps)
        {
            if (steps < 0 || steps > 192000)
            {
                throw new ArgumentOutOfRangeException("steps must be between 0 and 192000");
            }

            SendDataAndWait($"/{PumpAddress}A" + steps.ToString() + "R");
        }

        /// <summary>
        /// Aspirate (pickup) using the specified number of steps
        /// </summary>
        /// <param name="steps">Number of steps to move, 0 to 192,000</param>
        public void RelativePickup(int steps)
        {
            // TODO:  Should keep track of where the pump is to validate motion
            if (steps < 0 || steps > 192000)
            {
                throw new ArgumentOutOfRangeException("steps must be between 0 and 192000");
            }

            SendDataAndWait($"/{PumpAddress}P" + steps.ToString() + "R");
        }

        /// <summary>
        /// Dispense using the specified number of steps
        /// </summary>
        /// <param name="steps">Number of steps to move, 0 to 192,000</param>
        public void RelativeDispense(int steps)
        {
            // TODO:  Should keep track of where the pump is to validate motion
            if (steps < 0 || steps > 192000)
            {
                throw new ArgumentOutOfRangeException("steps must be between 0 and 192000");
            }

            SendDataAndWait($"/{PumpAddress}D" + steps.ToString() + "R");
        }

        /// <summary>
        /// Move the pump valve to the specified positin
        /// </summary>
        /// <param name="valvePosition">
        /// 
        /// </param>
        public void MoveValve(int valvePosition)
        {
            string valvePos;
            switch (valvePosition)
            {
                case 1:
                    valvePos = "I";
                    break;
                case 2:
                    valvePos = "O";
                    break;
                case 3:
                    valvePos = "E";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("valvePosition must be 1 (I), 2 (O), or 3 (E)");
            }

            SendDataAndWait($"/{PumpAddress}" + valvePos + "R");
        }

        /// <summary>
        /// Set the pump acceleration to the desired acceleration code
        /// </summary>
        /// <param name="acceleration">Acceleration code</param>
        public void SetAcceleration(int acceleration)
        {
            List<Accel> accelCodes = Enum.GetValues(typeof(Accel)).Cast<Accel>().ToList();
            if (!accelCodes.Contains((Accel)acceleration))
            {
                throw new ArgumentOutOfRangeException("Acceleration code is not valid");
            }

            SendDataAndWait($"/{PumpAddress}L" + acceleration.ToString() + "R");
        }

        /// <summary>
        /// Set the pump deceleration to the desired deceleration code
        /// </summary>
        /// <param name="acceleration">Deceleration code</param>
        public void SetDeceleration(int deceleration)
        {
            SetAcceleration(deceleration);  // Acceleration and deceleration are the same on Hamilton pump
        }

        /// <summary>
        /// Set the starting pump velocity in steps per second
        /// </summary>
        /// <param name="stepsPerSec">Starting steps per seconds, 50 to 800</param>
        public void SetStartVelocity(int stepsPerSec)
        {
            if (stepsPerSec < 50 || stepsPerSec > 800)
            {
                throw new ArgumentOutOfRangeException("Start velocity must be between 50 and 800 steps per second");
            }

            SendDataAndWait($"/{PumpAddress}v" + stepsPerSec.ToString() + "R");
        }

        /// <summary>
        /// Set the velocity of the pump in steps per second
        /// </summary>
        /// <param name="stepsPerSec">Velocity in steps per seconds, 2 to 3400</param>
        public void SetSpeed(int stepsPerSec)
        {
            if (stepsPerSec < 2 || stepsPerSec > 3400)
            {
                throw new ArgumentOutOfRangeException("Speed must be between 2 and 3400 steps per second");
            }

            SendDataAndWait($"/{PumpAddress}V" + stepsPerSec.ToString() + "R");
        }

        /// <summary>
        /// Set the stopping pump velocity in steps per second
        /// </summary>
        /// <param name="stepsPerSec">Stopping pump velocity in steps per second, 50 to 1700</param>
        public void SetStopVelocity(int stepsPerSec)
        {
            if (stepsPerSec < 50 || stepsPerSec > 1700)
            {
                throw new ArgumentOutOfRangeException("Stop velocity must be between 50 and 1700 steps per second");
            }

            SendDataAndWait($"/{PumpAddress}c" + stepsPerSec.ToString() + "R");
        }

        /// <summary>
        /// Set the backlash in steps
        /// </summary>
        /// <param name="steps">Number of steps to correct for backlash, 0 to 6400</param>
        public void SetBacklash(int steps)
        {
            if (steps < 0 || steps > 6400)
            {
                throw new ArgumentOutOfRangeException("Backlash steps must be between 0 and 6400 steps");
            }

            // Backlash correction is called "Return Steps" for Hamilton syringes, using the "Kx" command
            SendDataAndWait($"/{PumpAddress}K" + steps.ToString() + "R");
        }

        /// <summary>
        /// Immediately terminate execution of the command buffer.  Will not terminate a valve movement,
        /// but will terminate command sequence at end of valve move.
        /// </summary>
        /// <note>
        /// May cause motor to lose steps during a syringe move.  Pump should be reinitialized after this command.
        /// </note>
        public void TerminateCommandBuffer()
        {
            SendData($"/{PumpAddress}T");

            WaitForPumpNotBusy();
        }

        /// <summary>
        /// Get the pump status
        /// </summary>
        /// <param name="status">Pump status character</param>
        /// <returns>Is pump busy? false if pump is ready (idle), true if pump is busy</returns>
        public bool QueryStatus(out byte status)
        {
            SendData($"/{PumpAddress}Q");

            byte[] bytes = Encoding.ASCII.GetBytes(ComPort.RcvdMsg);
            if (bytes.Length != 4)
            {
                throw new Exception($"Query command returned {bytes.Length} bytes instead of 4 bytes");
            }

            // "\0<status><ETX>" expected
            status = bytes[2];
            if ((status & 0x20) != 0)
            {
                return false;    // Bit 5 set if pump ready
            }
            else
            {
                return true;   // Bit 5 clear if pump busy
            }
        }

        /// <summary>
        /// Command pump to delay for the specified number of milliseconds
        /// </summary>
        /// <param name="msec">Milliseconds to delay</param>
        public void Delay(int msec)
        {
            if (msec < 5 || msec > 30000)
            {
                throw new ArgumentOutOfRangeException("Delay must be between 5 and 30000 milliseconds");
            }

            SendDataAndWait($"/{PumpAddress}M" + msec.ToString() + "R");
        }

        /// <summary>
        /// Halt execution of command buffer until control command or digital input(s) go from high to low
        /// </summary>
        /// <param name="digitalInput">
        /// 0 = Wait for control command or either input 1 or input 2 to go from high to low
        /// 1 = Wait for control command or input 1 to go from high to low
        /// 2 = Wait for control command or input 2 to go from high to low
        /// </param>
        public void HaltCommandExecution(int digitalInput)
        {
            if (digitalInput != 0 || digitalInput != 1 || digitalInput != 2)
            {
                throw new ArgumentOutOfRangeException("Halt argument must be 0 (both), 1 (input 1), or 2 (input 2)");
            }

            SendDataAndWait($"/{PumpAddress}H" + digitalInput.ToString());
        }

        /// <summary>
        /// Send command string to com port, waiting until pump no longer busy
        /// </summary>
        /// <param name="cmd">Command string to syringe pump</param>
        private void SendDataAndWait(string cmd)
        {
            WaitForPumpNotBusy();
            SendData(cmd);
            WaitForPumpNotBusy();
        }

        /// <summary>
        /// Send command string to com port
        /// </summary>
        /// <param name="cmd">Command string to syringe pump</param>
        private void SendData(string cmd)
        {
            ComPort.WriteData(cmd);
            if (!ComPort.DataReadyEvent.WaitOne(1000))
            {
                ComPort.DisplayData(ComPortManager.MessageType.Error, "Syringe pump data was not received in 1 second!\n");
            }
            ComPort.DataReadyEvent.Reset();
        }

        /// <summary>
        /// Wait for pump to not be busy
        /// </summary>
        private void WaitForPumpNotBusy()
        {
            // Make sure pump is not busy
            int sleepMsec = 0;
            do
            {
                SendData($"/{PumpAddress}Q");
                System.Threading.Thread.Sleep(100);
                sleepMsec += 100;
                if (sleepMsec > 10000)
                {
                    throw new Exception("Wait for pump not busy took longer than 10 seconds");
                }
            } while (PumpBusy);
        }

        /// <summary>
        /// See if pump status is still busy
        /// </summary>
        private bool PumpBusy => ComPort.RcvdMsg != "/0`\u0003";
    }
}
