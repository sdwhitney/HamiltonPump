using System;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace SyringePump
{
    class ComPortManager
    {
        #region Manager Enums
        /// <summary>
        /// enumeration to hold our transmission types
        /// </summary>
        public enum TransmissionType { Text, Hex }

        /// <summary>
        /// enumeration to hold our message types
        /// </summary>
        public enum MessageType { Incoming, Outgoing, Normal, Warning, Error };
        #endregion

        #region Manager Variables
        //global manager variables
        private readonly Color[] MessageColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };
        private SerialPort comPort = new SerialPort();
        #endregion

        #region Manager Properties

        /// <summary>
        /// Automatic EOL
        /// </summary>
        public bool AutoEOL { get; set; } = true;

        /// <summary>
        /// Return port status
        /// </summary>
        public bool IsPortOpen
        {
            get { return comPort.IsOpen; }
        }

        /// <summary>
        /// Property to hold the BaudRate
        /// of our manager class
        /// </summary>
        public string BaudRate { get; set; }

        /// <summary>
        /// property to hold the Parity
        /// of our manager class
        /// </summary>
        public string Parity { get; set; } = string.Empty;

        /// <summary>
        /// property to hold the StopBits
        /// of our manager class
        /// </summary>
        public string StopBits { get; set; } = string.Empty;

        /// <summary>
        /// property to hold the DataBits
        /// of our manager class
        /// </summary>
        public string DataBits { get; set; } = string.Empty;

        /// <summary>
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName { get; set; } = string.Empty;

        public string NewLine
        {
            get
            {
                if (comPort != null)
                    return comPort.NewLine;
                else
                    return string.Empty;
            }
            set
            {
                if (comPort != null)
                    comPort.NewLine = value;
            }
        }

        public string RcvdMsg { get; set; } = string.Empty;

        public ManualResetEvent DataReadyEvent { get; set; } = new ManualResetEvent(false);

        /// <summary>
        /// property to hold our TransmissionType
        /// of our manager class
        /// </summary>
        public TransmissionType CurrentTransmissionType { get; set; }

        /// <summary>
        /// property to hold our display window
        /// value
        /// </summary>
        public RichTextBox DisplayWindow { get; set; }
        #endregion

        #region Manager Constructors
        /// <summary>
        /// Constructor to set the properties of our Manager Class
        /// </summary>
        /// <param name="baud">Desired BaudRate</param>
        /// <param name="par">Desired Parity</param>
        /// <param name="sBits">Desired StopBits</param>
        /// <param name="dBits">Desired DataBits</param>
        /// <param name="name">Desired PortName</param>
        public ComPortManager(string baud, string par, string sBits, string dBits, string name, RichTextBox rtb)
        {
            BaudRate = baud;
            Parity = par;
            StopBits = sBits;
            DataBits = dBits;
            PortName = name;
            DisplayWindow = rtb;
            //now add an event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
        }

        /// <summary>
        /// Comstructor to set the properties of our
        /// serial port communicator to nothing
        /// </summary>
        public ComPortManager()
        {
            BaudRate = string.Empty;
            Parity = string.Empty;
            StopBits = string.Empty;
            DataBits = string.Empty;
            PortName = "COM1";
            DisplayWindow = null;
            //add event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
        }
        #endregion

        #region WriteData
        public void WriteData(string msg)
        {
            if (!(comPort.IsOpen == true))
            {
                DisplayData(MessageType.Error, "Open Port before sending data!\n");
            }
            switch (CurrentTransmissionType)
            {
                case TransmissionType.Text:
                    //send the message to the port
                    comPort.Write(msg);
                    SendEndOfLine();
                    //display the message
                    DisplayData(MessageType.Outgoing, msg + "\n");
                    break;
                case TransmissionType.Hex:
                    try
                    {
                        //convert the message to byte array
                        byte[] newMsg = HexToByte(msg);
                        //send the message to the port
                        comPort.Write(newMsg, 0, newMsg.Length);
                        SendEndOfLine();
                        //convert back to hex and display
                        DisplayData(MessageType.Outgoing, ByteToHex(newMsg) + "\n");
                    }
                    catch (FormatException ex)
                    {
                        //display error message
                        DisplayData(MessageType.Error, ex.Message + "\n");
                    }
                    finally
                    {
                        DisplayWindow.SelectAll();
                    }
                    break;
                default:
                    //send the message to the port
                    comPort.Write(msg);
                    SendEndOfLine();
                    //display the message
                    DisplayData(MessageType.Outgoing, msg + "\n");
                    break;
            }
        }

        /// <summary>
        /// Method to send END_OF_LINE (0x0D) if connection is open
        /// </summary>
        public void SendEndOfLine()
        {
            byte[] end_of_line = { 0x0D };
            if ((comPort.IsOpen == true) && (true == AutoEOL))
            {
                comPort.Write(end_of_line, 0, 1);
            }
        }
        #endregion

        #region HexToByte
        /// <summary>
        /// method to convert hex string into a byte array
        /// </summary>
        /// <param name="msg">string to convert</param>
        /// <returns>a byte array</returns>
        private byte[] HexToByte(string msg)
        {
            //remove any spaces from the string
            msg = msg.Replace(" ", "");
            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            //loop through the length of the provided string
            for (int i = 0; i < msg.Length; i += 2)
                //convert each set of 2 characters to a byte
                //and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            //return the array
            return comBuffer;
        }
        #endregion

        #region ByteToHex
        /// <summary>
        /// method to convert a byte array into a hex string
        /// </summary>
        /// <param name="comByte">byte array to convert</param>
        /// <returns>a hex string</returns>
        private string ByteToHex(byte[] comByte)
        {
            //create a new StringBuilder object
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            //loop through each byte in the array
            foreach (byte data in comByte)
                //convert the byte to a string and add to the stringbuilder
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            //return the converted value
            return builder.ToString().ToUpper();
        }
        #endregion

        #region DisplayData
        /// <summary>
        /// method to display the data to & from the port
        /// on the screen
        /// </summary>
        /// <param name="type">MessageType of the message</param>
        /// <param name="msg">Message to display</param>
        [STAThread]
        public void DisplayData(MessageType type, string msg)
        {
            DisplayWindow.Invoke(new EventHandler(delegate
        {
            DisplayWindow.SelectedText = string.Empty;
            DisplayWindow.SelectionFont = new Font(DisplayWindow.SelectionFont, FontStyle.Bold);
            DisplayWindow.SelectionColor = MessageColor[(int)type];

            // TODO:  Replace Unicode \u0003 with <ETX> in message

            // Add date and time to milliseconds with message to display window
            DisplayWindow.AppendText(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt") + " " + msg);
            DisplayWindow.ScrollToCaret();
        }));
        }
        #endregion

        #region OpenPort
        public bool OpenPort()
        {
            try
            {
                ClosePort();

                //set the properties of our SerialPort Object
                comPort.BaudRate = int.Parse(BaudRate);    //BaudRate
                comPort.DataBits = int.Parse(DataBits);    //DataBits
                comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBits);    //StopBits
                comPort.Parity = (Parity)Enum.Parse(typeof(Parity), Parity);    //Parity
                comPort.PortName = PortName;   //PortName
                //now open the port
                comPort.Open();
                //display message
                DisplayData(MessageType.Normal, "Port opened at " + DateTime.Now + "\n");
                //return true
                return true;
            }
            catch (Exception ex)
            {
                DisplayData(MessageType.Error, ex.Message + "\n");
                return false;
            }
        }

        /// <summary>
        /// Close Port
        /// </summary>
        public void ClosePort()
        {
            if (comPort.IsOpen == true)
            {
                comPort.Close();
            }
        }

        #endregion

        #region SetParityValues
        public void SetParityValues(object obj)
        {
            foreach (string str in Enum.GetNames(typeof(Parity)))
            {
                ((ComboBox)obj).Items.Add(str);
            }
        }
        #endregion

        #region SetStopBitValues
        public void SetStopBitValues(object obj)
        {
            foreach (string str in Enum.GetNames(typeof(StopBits)))
            {
                ((ComboBox)obj).Items.Add(str);
            }
        }
        #endregion

        #region SetPortNameValues
        public void SetPortNameValues(object obj)
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            foreach (string str in ports)
            {
                ((ComboBox)obj).Items.Add(str);
            }
        }
        #endregion

        #region comPort_DataReceived
        /// <summary>
        /// method that will be called when theres data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string msg;
            //determine the mode the user selected (binary/string)
            switch (CurrentTransmissionType)
            {
                //user chose string
                case TransmissionType.Text:
                    //read data waiting in the buffer
                    //msg = comPort.ReadExisting().Trim();
                    msg = comPort.ReadLine().Trim();
                    //display the data to the user
#if false
                    if (msg.Length > 0)
                    {
                        DataReadyEvent.Set();
                        RcvdMsg = msg;
                        DisplayData(MessageType.Incoming, msg + "\n");
                    }
#else
                    // Don't check for 0 length - ReadLine() reads until NewLine anyway, and Trim() removes trailing white space
                    DataReadyEvent.Set();
                    RcvdMsg = msg;
                    DisplayData(MessageType.Incoming, msg + "\n");
#endif
                    break;
                //user chose binary
                case TransmissionType.Hex:
                    //retrieve number of bytes in the buffer
                    int bytes = comPort.BytesToRead;
                    //create a byte array to hold the awaiting data
                    byte[] comBuffer = new byte[bytes];
                    //read the data and store it
                    comPort.Read(comBuffer, 0, bytes);
                    //display the data to the user
                    if (bytes > 0)
                    {
                        RcvdMsg = ByteToHex(comBuffer);
                        DisplayData(MessageType.Incoming, ByteToHex(comBuffer) + "\n");
                    }
                    break;
                default:
                    //read data waiting in the buffer
                    //msg = comPort.ReadExisting().Trim();
                    msg = comPort.ReadLine().Trim();
                    //display the data to the user
                    if (msg.Length > 0)
                    {
                        RcvdMsg = msg;
                        DisplayData(MessageType.Incoming, msg + "\n");
                    }
                    break;
            }
        }
#endregion
    }
}
