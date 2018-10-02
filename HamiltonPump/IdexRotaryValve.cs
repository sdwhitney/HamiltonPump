using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    /// <summary>
    /// Class for Idex Rotary Valve control
    /// </summary>
    public class IdexRotaryValve : IRotaryValve
    {
        /// <summary>
        /// Com port for this rotary valve
        /// </summary>
        private ComPortManager ComPort { get; set; }

        /// <summary>
        /// Enumeration of all rotary valve positions
        /// </summary>
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

        /// <summary>
        /// Constructor for Idex rotary valve
        /// </summary>
        public IdexRotaryValve()
        {
            // Create and open single instance of com port if needed
            if (ComPort == null)
            {
                ComPort = new ComPortManager
                {
                    PortName = "COM21",
                    BaudRate = "19200",
                    DataBits = "8",
                    Parity = "None",
                    StopBits = "1",
                    AutoEOL = true,
                    CurrentTransmissionType = ComPortManager.TransmissionType.Text,
                    // TODO:  ComPort should not know anything about displaying data!!!
#warning ComPortManager needs DisplayWindow set to RichTextBox presently! Change to JBL message event!
                    //DisplayWindow = frmMain.rtbValveDisplay
                };

                if (!ComPort.IsPortOpen)
                {
                    ComPort.OpenPort();
                    ComPort.NewLine = "\r"; // Commands and responses end with <CR> on rotary valve
                }
            }
        }

        /// <summary>
        /// Moves the rotary valve to its home position
        /// </summary>
        public void MoveToHome()
        {
            SendData($"M");
            if (ComPort.RcvdMsg != string.Empty)
            {
                throw new Exception($"Home command returned {ComPort.RcvdMsg} instead of an empty string");
            }
        }

        /// <summary>
        /// Move the rotary valve to the specified position
        /// </summary>
        /// <param name="position">Position to move rotary valve to</param>
        public void MoveToPosition(int position)
        {
            List<RotaryValvePosition> positionCodes = Enum.GetValues(typeof(RotaryValvePosition)).Cast<RotaryValvePosition>().ToList();
            if (!positionCodes.Contains((RotaryValvePosition)position))
            {
                throw new ArgumentOutOfRangeException($"Position code {position} is not valid");
            }

            //IdexRotaryValve.RotaryValvePosition p = (IdexRotaryValve.RotaryValvePosition)Enum.Parse(typeof(IdexRotaryValve.RotaryValvePosition), cboSetValvePosition.SelectedItem.ToString());
            SendData($"P{position.ToString("X2")}");
            if (ComPort.RcvdMsg != string.Empty)
            {
                throw new Exception($"Move To Position command returned {ComPort.RcvdMsg} instead of an empty string");
            }
        }

        /// <summary>
        /// Queries the status of the rotary valve
        /// </summary>
        /// <returns>Rotary valve status - position or error code</returns>
        public string QueryStatus()
        {
            SendData($"S");
            return ComPort.RcvdMsg;
        }

        /// <summary>
        /// Send the command to the rotary valve
        /// </summary>
        /// <param name="cmd"></param>
        private void SendData(string cmd)
        {
            ComPort.WriteData(cmd);
            if (!ComPort.DataReadyEvent.WaitOne(5000))
            {
                ComPort.DisplayData(ComPortManager.MessageType.Error, "Rotary valve data was not received in 5 seconds!\n");
            }
            ComPort.DataReadyEvent.Reset();
        }
    }
}
