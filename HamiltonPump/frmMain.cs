using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamiltonPump
{
    public partial class frmMain : Form
    {
        ComPortManager comm = new ComPortManager();
        string transType = string.Empty;

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
            comm.PortName = cboPort.Text;
            comm.Parity = cboParity.Text;
            comm.StopBits = cboStop.Text;
            comm.DataBits = cboData.Text;
            comm.BaudRate = cboBaud.Text;
            comm.DisplayWindow = rtbDisplay;
            comm.OpenPort();

            if (true == comm.IsPortOpen)
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
            comm.SetPortNameValues(cboPort);
            comm.SetParityValues(cboParity);
            comm.SetStopBitValues(cboStop);
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
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            SendData();
        }

        private void SendData()
        {
            // Do not convert to upper case!
            //comm.WriteData(txtSend.Text.ToUpper());
            comm.WriteData(txtSend.Text);
            txtSend.SelectAll();
        }

        private void rdoHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHex.Checked == true)
            {
                comm.CurrentTransmissionType = ComPortManager.TransmissionType.Hex;
            }
            else
            {
                comm.CurrentTransmissionType = ComPortManager.TransmissionType.Text;
            }

        }

        private void chkBoxEOL_CheckedChanged(object sender, EventArgs e)
        {
            comm.AutoEOL = chkBoxEOL.Checked;
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
            comm.ClosePort();
            if (false == comm.IsPortOpen)
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

        private bool PumpBusy => comm.RcvdMsg != "/0`\u0003";

        private void btnQuery_Click(object sender, EventArgs e)
        {
            txtSend.Text = "/1Q";
            SendData();
        }

        private void btnInitToLeft_Click(object sender, EventArgs e)
        {
            // Send initialization, with output port to left
            txtSend.Text = "/1Y";
            SendData();
        }

        private void btnInitToRight_Click(object sender, EventArgs e)
        {
            // Send initialization, with output port to right
            txtSend.Text = "/1Z";
            SendData();
        }

        private void btnAbsPos_Click(object sender, EventArgs e)
        {
            // Send initialization, with output port to right
            txtSend.Text = "/1A" + numAbsPos.Value.ToString();
            SendData();
        }

        private void btnRelPickup_Click(object sender, EventArgs e)
        {
            txtSend.Text = "/1P" + numRelPickup.Value.ToString();
            SendData();
        }

        private void btnRelDispense_Click(object sender, EventArgs e)
        {
            txtSend.Text = "/1D" + numRelDispense.Value.ToString();
            SendData();
        }

        private void btnMoveValve_Click(object sender, EventArgs e)
        {
            var checkedButton = grpValvePos.Controls.OfType<RadioButton>()
                                           .FirstOrDefault(r => r.Checked);
            string valve = string.Empty;
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
            txtSend.Text = "/1" + valve;
            SendData();
        }

        private void btnExecInitToLeft_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            // Send initialization, with output port to left
            txtSend.Text = "/1YR";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecInitToRight_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            // Send initialization, with output port to right
            txtSend.Text = "/1ZR";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecAbsPos_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = "/1A" + numAbsPos.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecRelPickup_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = "/1P" + numRelPickup.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecRelDispense_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            txtSend.Text = "/1D" + numRelDispense.Value.ToString() + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecMoveValve_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            var checkedButton = grpValvePos.Controls.OfType<RadioButton>()
                                           .FirstOrDefault(r => r.Checked);
            string valve = string.Empty;
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
            txtSend.Text = "/1" + valve + "R";
            SendData();

            WaitForPumpNotBusy();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            // Send execute command - broadcast
            txtSend.Text = "/_R";
            SendData();
        }

        private void WaitForPumpNotBusy()
        {
            // Make sure pump is not busy
            do
            {
                txtSend.Text = "/1Q";
                SendData();
                Wait100();
            } while (PumpBusy);
        }

        private static void Wait100()
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(100);
            });
            try
            {
                t.Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
