using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyringePump
{
    public partial class frmMain : Form
    {
        internal ComPortManager Comm { get; set; } = new ComPortManager();

        string transType = string.Empty;

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

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadValues();
            SetDefaults();
            SetControlState();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            Comm.PortName = cboPort.Text;
            Comm.Parity = cboParity.Text;
            Comm.StopBits = cboStop.Text;
            Comm.DataBits = cboData.Text;
            Comm.BaudRate = cboBaud.Text;
            //Comm.DisplayWindow = rtbDisplay;
            Comm.OpenPort();

            if (true == Comm.IsPortOpen)
            {
                cmdOpen.Enabled = false;
                cmdClose.Enabled = true;
                cmdSend.Enabled = true;
                txtSend.Enabled = true;
            }
        }

        /// <summary>
        /// Method to initialize serial port
        /// values to standard defaults
        /// </summary>
        private void SetDefaults()
        {
            cboPort.SelectedIndex = 0;
            cboBaud.SelectedText = "9600";
            cboParity.SelectedIndex = 0;
            cboStop.SelectedIndex = 1;
            cboData.SelectedIndex = 1;
        }

        /// <summary>
        /// methos to load our serial
        /// port option values
        /// </summary>
        private void LoadValues()
        {
            Comm.DisplayWindow = rtbDisplay;
            Comm.SetPortNameValues(cboPort);
            Comm.SetParityValues(cboParity);
            Comm.SetStopBitValues(cboStop);
        }

        /// <summary>
        /// method to set the state of controls
        /// when the form first loads
        /// </summary>
        private void SetControlState()
        {
            rdoText.Checked = true;
            cmdSend.Enabled = false;
            cmdClose.Enabled = false;

            string[] accels = Enum.GetNames(typeof(Accel));
            cbSetAccel.Items.AddRange(accels);
            cbSetAccel.SelectedIndex = 2;
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            SendData();
        }

        private void SendData()
        {
            Comm.WriteData(txtSend.Text);
            if (!Comm.DataReadyEvent.WaitOne(1000))
            {
                Comm.DisplayData(ComPortManager.MessageType.Error, "Data was not received in 1 second!\n");
            }
            Comm.DataReadyEvent.Reset();
            txtSend.SelectAll();
        }

        private void rdoHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHex.Checked == true)
            {
                Comm.CurrentTransmissionType = ComPortManager.TransmissionType.Hex;
            }
            else
            {
                Comm.CurrentTransmissionType = ComPortManager.TransmissionType.Text;
            }

        }

        private void chkBoxEOL_CheckedChanged(object sender, EventArgs e)
        {
            Comm.AutoEOL = chkBoxEOL.Checked;
        }

        private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x0D)
            {
                SendData();
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Comm.ClosePort();
            if (false == Comm.IsPortOpen)
            {
                cmdOpen.Enabled = true;
                cmdClose.Enabled = false;
                cmdSend.Enabled = false;
                txtSend.Enabled = false;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool PumpBusy => Comm.RcvdMsg != "/0`\u0003";

        private void btnQuery_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}Q";
            SendData();
        }

        private void btnInitToLeft_Click(object sender, EventArgs e)
        {
            // Send initialization, with output port to left
            txtSend.Text = $"/{Pump}Y";
            SendData();
        }

        private void btnInitToRight_Click(object sender, EventArgs e)
        {
            // Send initialization, with output port to right
            txtSend.Text = $"/{Pump}Z";
            SendData();
        }

        private void btnAbsPos_Click(object sender, EventArgs e)
        {
            // Send initialization, with output port to right
            txtSend.Text = $"/{Pump}A" + numAbsPos.Value.ToString();
            SendData();
        }

        private void btnRelPickup_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}P" + numRelPickup.Value.ToString();
            SendData();
        }

        private void btnRelDispense_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}D" + numRelDispense.Value.ToString();
            SendData();
        }

        private void btnMoveValve_Click(object sender, EventArgs e)
        {
            string valve = GetValve();
            txtSend.Text = $"/{Pump}" + GetValve();
            SendData();
        }

        private string GetValve()
        {
            string valve = string.Empty;
            var checkedButton = grpValvePos.Controls.OfType<RadioButton>()
                                           .FirstOrDefault(r => r.Checked);
            if (checkedButton == rbInput)
            {
                valve = "I";
            }
            else if (checkedButton == rbExtra)
            {
                valve = "E";
            }
            else if (checkedButton == rbOutput)
            {
                valve = "O";
            }

            return valve;
        }

        private string Pump
        {
            get
            {
                var checkedPump = grpPump.Controls.OfType<RadioButton>()
                                         .FirstOrDefault(r => r.Checked);
                string pump = string.Empty;
                if (checkedPump == rbLane1)
                {
                    pump = ((char)PumpAddress.Lane1).ToString();
                }
                else if (checkedPump == rbLane2)
                {
                    pump = ((char)PumpAddress.Lane2).ToString();
                }
                else if (checkedPump == rbAll)
                {
                    pump = ((char)PumpAddress.All).ToString();
                }

                return pump;
            }
        }

        private void btnSetAccel_Click(object sender, EventArgs e)
        {
            Accel a = (Accel)Enum.Parse(typeof(Accel), cbSetAccel.SelectedItem.ToString());
            txtSend.Text = $"/{Pump}L" + ((int)a).ToString();
            SendData();
        }

        private void btnSetStartVelocity_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}v" + numStartVelocity.Value.ToString();
            SendData();
        }

        private void btnSetMaxVelocity_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}V" + numMaxVelocity.Value.ToString();
            SendData();
        }

        private void btnSetStopVelocity_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}c" + numStopVelocity.Value.ToString();
            SendData();
        }

        private void btnAuxInput1Status_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}?13";
            SendData();
        }

        private void btnAuxInput2Status_Click(object sender, EventArgs e)
        {
            txtSend.Text = $"/{Pump}?14";
            SendData();
        }

        private void btnExecInitToLeft_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            // Send initialization, with output port to left
            txtSend.Text = $"/{Pump}YR";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecInitToRight_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            // Send initialization, with output port to right
            txtSend.Text = $"/{Pump}ZR";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecAbsPos_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}A" + numAbsPos.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecRelPickup_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}P" + numRelPickup.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecRelDispense_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}D" + numRelDispense.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecMoveValve_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}" + GetValve() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecSetAccel_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            Accel a = (Accel)Enum.Parse(typeof(Accel), cbSetAccel.SelectedItem.ToString());
            txtSend.Text = $"/{Pump}L" + ((int)a).ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecSetStartVelocity_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}v" + numStartVelocity.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecSetMaximumVelocity_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}V" + numMaxVelocity.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecSetStopVelocity_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = $"/{Pump}c" + numStopVelocity.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            // Send execute command - broadcast
            txtSend.Text = $"/{Pump}R";
            SendData();
        }

        private void WaitForPumpNotBusy(PumpAddress addr = PumpAddress.Lane1)
        {
            // Make sure pump is not busy
            do
            {
                txtSend.Text = $"/{Pump}Q";
                SendData();
                System.Threading.Thread.Sleep(100);
            } while (PumpBusy);
        }

    }
}
