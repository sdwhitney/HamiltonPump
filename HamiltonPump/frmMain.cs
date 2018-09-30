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
        internal ComPortManager PumpComm { get; set; } = new ComPortManager();
        internal ComPortManager ValveComm { get; set; } = new ComPortManager();

        string transType = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Syringe pump
            LoadValues();
            SetDefaults();
            SetControlState();

            // Rotary Valve
            LoadValveValues();
            SetValveDefaults();
            SetValveControlState();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            PumpComm.PortName = cboPort.Text;
            PumpComm.Parity = cboParity.Text;
            PumpComm.StopBits = cboStop.Text;
            PumpComm.DataBits = cboData.Text;
            PumpComm.BaudRate = cboBaud.Text;
            PumpComm.OpenPort();

            if (true == PumpComm.IsPortOpen)
            {
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                btnSend.Enabled = true;
                txtSend.Enabled = true;
            }
        }

        /// <summary>
        /// Method to initialize serial port
        /// values to standard defaults
        /// </summary>
        private void SetDefaults()
        {
            // Selects the first com port in the list
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
            PumpComm.DisplayWindow = rtbDisplay;
            PumpComm.SetPortNameValues(cboPort);
            PumpComm.SetParityValues(cboParity);
            PumpComm.SetStopBitValues(cboStop);
        }

        /// <summary>
        /// method to set the state of controls
        /// when the form first loads
        /// </summary>
        private void SetControlState()
        {
            rdoText.Checked = true;
            btnSend.Enabled = false;
            btnClose.Enabled = false;

            string[] accels = Enum.GetNames(typeof(HamiltonSyringe.Accel));
            cbSetAccel.Items.AddRange(accels);
            cbSetAccel.SelectedIndex = 2;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendData();
        }

        private void SendData()
        {
            PumpComm.WriteData(txtSend.Text);
            if (!PumpComm.DataReadyEvent.WaitOne(1000))
            {
                PumpComm.DisplayData(ComPortManager.MessageType.Error, "Data was not received in 1 second!\n");
            }
            PumpComm.DataReadyEvent.Reset();
            txtSend.SelectAll();
        }

        private void rdoHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHex.Checked == true)
            {
                PumpComm.CurrentTransmissionType = ComPortManager.TransmissionType.Hex;
            }
            else
            {
                PumpComm.CurrentTransmissionType = ComPortManager.TransmissionType.Text;
            }

        }

        private void chkBoxEOL_CheckedChanged(object sender, EventArgs e)
        {
            PumpComm.AutoEOL = chkBoxEOL.Checked;
        }

        private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x0D)
            {
                SendData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            PumpComm.ClosePort();
            if (false == PumpComm.IsPortOpen)
            {
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                btnSend.Enabled = false;
                txtSend.Enabled = false;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool PumpBusy => PumpComm.RcvdMsg != "/0`\u0003";

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
            HamiltonSyringe.Accel a = (HamiltonSyringe.Accel)Enum.Parse(typeof(HamiltonSyringe.Accel), cbSetAccel.SelectedItem.ToString());
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

            rbOutput.Checked = true;
        }

        private void btnExecInitToRight_Click(object sender, EventArgs e)
        {
            WaitForPumpNotBusy();

            // Send initialization, with output port to right
            txtSend.Text = $"/{Pump}ZR";
            SendData();

            WaitForPumpNotBusy();

            rbOutput.Checked = true;
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

            HamiltonSyringe.Accel a = (HamiltonSyringe.Accel)Enum.Parse(typeof(HamiltonSyringe.Accel), cbSetAccel.SelectedItem.ToString());
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

        /// <summary>
        /// Method to initialize rotary valve serial port
        /// values to standard defaults
        /// </summary>
        private void SetValveDefaults()
        {
            // Selects the last com port in the list
            cboValvePort.SelectedIndex = cboValvePort.Items.Count - 1;
            cboValveBaud.SelectedText = "19200";
            cboValveParity.SelectedIndex = 0;
            cboValveStop.SelectedIndex = 1;
            cboValveData.SelectedIndex = 1;
        }

        /// <summary>
        /// methods to load rotary valve serial
        /// port option values
        /// </summary>
        private void LoadValveValues()
        {
            ValveComm.DisplayWindow = rtbValveDisplay;
            ValveComm.SetPortNameValues(cboValvePort);
            ValveComm.SetParityValues(cboValveParity);
            ValveComm.SetStopBitValues(cboValveStop);
        }

        /// <summary>
        /// method to set the state of rotary valve controls
        /// when the form first loads
        /// </summary>
        private void SetValveControlState()
        {
            rdoValveText.Checked = true;
            btnValveSend.Enabled = false;
            btnValveClose.Enabled = false;

            string[] positions = Enum.GetNames(typeof(IdexRotaryValve.RotaryValvePosition));
            cboSetValvePosition.Items.AddRange(positions);
            cboSetValvePosition.SelectedIndex = 0;
        }

        private void btnValveOpen_Click(object sender, EventArgs e)
        {
            ValveComm.PortName = cboValvePort.Text;
            ValveComm.Parity = cboValveParity.Text;
            ValveComm.StopBits = cboValveStop.Text;
            ValveComm.DataBits = cboValveData.Text;
            ValveComm.BaudRate = cboValveBaud.Text;
            ValveComm.OpenPort();

            if (true == ValveComm.IsPortOpen)
            {
                ValveComm.NewLine = "\r";
                btnValveOpen.Enabled = false;
                btnValveClose.Enabled = true;
                btnValveSend.Enabled = true;
                txtValveSend.Enabled = true;
            }
        }

        private void SendValveData()
        {
            ValveComm.WriteData(txtValveSend.Text);
            if (!ValveComm.DataReadyEvent.WaitOne(5000))
            {
                ValveComm.DisplayData(ComPortManager.MessageType.Error, "Data was not received in 5 seconds!\n");
            }
            ValveComm.DataReadyEvent.Reset();
            txtValveSend.SelectAll();
        }

        private void rdoValveHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoValveHex.Checked == true)
            {
                ValveComm.CurrentTransmissionType = ComPortManager.TransmissionType.Hex;
            }
            else
            {
                ValveComm.CurrentTransmissionType = ComPortManager.TransmissionType.Text;
            }
        }

        private void chkBoxValveEOL_CheckedChanged(object sender, EventArgs e)
        {
            ValveComm.AutoEOL = chkBoxValveEOL.Checked;
        }

        private void txtValveSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x0D)
            {
                SendValveData();
            }
        }

        private void btnValveClose_Click(object sender, EventArgs e)
        {
            ValveComm.ClosePort();
            if (false == ValveComm.IsPortOpen)
            {
                btnValveOpen.Enabled = true;
                btnValveClose.Enabled = false;
                btnValveSend.Enabled = false;
                txtValveSend.Enabled = false;
            }
        }

        private void btnValveSend_Click(object sender, EventArgs e)
        {
            SendValveData();
        }

        private void btnValveHome_Click(object sender, EventArgs e)
        {
            txtValveSend.Text = $"M";
            SendValveData();
        }

        private void btnValveStatus_Click(object sender, EventArgs e)
        {
            txtValveSend.Text = $"S";
            SendValveData();
        }

        private void btnValveMoveToPosition_Click(object sender, EventArgs e)
        {
            IdexRotaryValve.RotaryValvePosition p = (IdexRotaryValve.RotaryValvePosition)Enum.Parse(typeof(IdexRotaryValve.RotaryValvePosition), cboSetValvePosition.SelectedItem.ToString());
            txtValveSend.Text = $"P" + ((int)p).ToString("X2");
            SendValveData();
        }
    }
}
