namespace HamiltonPump
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInitToLeft = new System.Windows.Forms.Button();
            this.btnInitToRight = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboData = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboStop = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboBaud = new System.Windows.Forms.ComboBox();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.cmdSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoText = new System.Windows.Forms.RadioButton();
            this.rdoHex = new System.Windows.Forms.RadioButton();
            this.rtbDisplay = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBoxEOL = new System.Windows.Forms.CheckBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnExecInitToLeft = new System.Windows.Forms.Button();
            this.btnExecInitToRight = new System.Windows.Forms.Button();
            this.btnAbsPos = new System.Windows.Forms.Button();
            this.btnExecAbsPos = new System.Windows.Forms.Button();
            this.numAbsPos = new System.Windows.Forms.NumericUpDown();
            this.numRelPickup = new System.Windows.Forms.NumericUpDown();
            this.btnExecRelPickup = new System.Windows.Forms.Button();
            this.btnRelPickup = new System.Windows.Forms.Button();
            this.numRelDispense = new System.Windows.Forms.NumericUpDown();
            this.btnExecRelDispense = new System.Windows.Forms.Button();
            this.btnRelDispense = new System.Windows.Forms.Button();
            this.btnExecMoveValve = new System.Windows.Forms.Button();
            this.btnMoveValve = new System.Windows.Forms.Button();
            this.grpValvePos = new System.Windows.Forms.GroupBox();
            this.rbExtra = new System.Windows.Forms.RadioButton();
            this.rbInput = new System.Windows.Forms.RadioButton();
            this.rbOutput = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAbsPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRelPickup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRelDispense)).BeginInit();
            this.grpValvePos.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitToLeft
            // 
            this.btnInitToLeft.Location = new System.Drawing.Point(128, 20);
            this.btnInitToLeft.Name = "btnInitToLeft";
            this.btnInitToLeft.Size = new System.Drawing.Size(86, 23);
            this.btnInitToLeft.TabIndex = 2;
            this.btnInitToLeft.Text = "Init To Left";
            this.btnInitToLeft.UseVisualStyleBackColor = true;
            this.btnInitToLeft.Click += new System.EventHandler(this.btnInitToLeft_Click);
            // 
            // btnInitToRight
            // 
            this.btnInitToRight.Location = new System.Drawing.Point(128, 49);
            this.btnInitToRight.Name = "btnInitToRight";
            this.btnInitToRight.Size = new System.Drawing.Size(86, 23);
            this.btnInitToRight.TabIndex = 3;
            this.btnInitToRight.Text = "Init To Right";
            this.btnInitToRight.UseVisualStyleBackColor = true;
            this.btnInitToRight.Click += new System.EventHandler(this.btnInitToRight_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboData);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cboStop);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cboParity);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboBaud);
            this.groupBox2.Controls.Add(this.cboPort);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(96, 221);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Data Bits";
            // 
            // cboData
            // 
            this.cboData.FormattingEnabled = true;
            this.cboData.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cboData.Location = new System.Drawing.Point(9, 195);
            this.cboData.Name = "cboData";
            this.cboData.Size = new System.Drawing.Size(76, 21);
            this.cboData.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Stop Bits";
            // 
            // cboStop
            // 
            this.cboStop.FormattingEnabled = true;
            this.cboStop.Location = new System.Drawing.Point(9, 155);
            this.cboStop.Name = "cboStop";
            this.cboStop.Size = new System.Drawing.Size(76, 21);
            this.cboStop.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Parity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Baud Rate";
            // 
            // cboParity
            // 
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(9, 114);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(76, 21);
            this.cboParity.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Port";
            // 
            // cboBaud
            // 
            this.cboBaud.FormattingEnabled = true;
            this.cboBaud.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "28800",
            "36000",
            "115000"});
            this.cboBaud.Location = new System.Drawing.Point(9, 74);
            this.cboBaud.Name = "cboBaud";
            this.cboBaud.Size = new System.Drawing.Size(76, 21);
            this.cboBaud.TabIndex = 11;
            // 
            // cboPort
            // 
            this.cboPort.FormattingEnabled = true;
            this.cboPort.Location = new System.Drawing.Point(9, 34);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(76, 21);
            this.cboPort.TabIndex = 10;
            // 
            // cmdSend
            // 
            this.cmdSend.Location = new System.Drawing.Point(472, 398);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(75, 27);
            this.cmdSend.TabIndex = 9;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Enabled = false;
            this.txtSend.Location = new System.Drawing.Point(12, 402);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(454, 20);
            this.txtSend.TabIndex = 8;
            this.txtSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSend_KeyPress);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 334);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 23);
            this.cmdClose.TabIndex = 10;
            this.cmdClose.Text = "Close Port";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOpen
            // 
            this.cmdOpen.Location = new System.Drawing.Point(12, 305);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(96, 23);
            this.cmdOpen.TabIndex = 11;
            this.cmdOpen.Text = "Open Port";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoText);
            this.groupBox3.Controls.Add(this.rdoHex);
            this.groupBox3.Location = new System.Drawing.Point(12, 239);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(96, 60);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mode";
            // 
            // rdoText
            // 
            this.rdoText.AutoSize = true;
            this.rdoText.Location = new System.Drawing.Point(6, 38);
            this.rdoText.Name = "rdoText";
            this.rdoText.Size = new System.Drawing.Size(46, 17);
            this.rdoText.TabIndex = 1;
            this.rdoText.TabStop = true;
            this.rdoText.Text = "Text";
            this.rdoText.UseVisualStyleBackColor = true;
            // 
            // rdoHex
            // 
            this.rdoHex.AutoSize = true;
            this.rdoHex.Location = new System.Drawing.Point(6, 16);
            this.rdoHex.Name = "rdoHex";
            this.rdoHex.Size = new System.Drawing.Size(44, 17);
            this.rdoHex.TabIndex = 0;
            this.rdoHex.TabStop = true;
            this.rdoHex.Text = "Hex";
            this.rdoHex.UseVisualStyleBackColor = true;
            this.rdoHex.CheckedChanged += new System.EventHandler(this.rdoHex_CheckedChanged);
            // 
            // rtbDisplay
            // 
            this.rtbDisplay.Location = new System.Drawing.Point(553, 30);
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.Size = new System.Drawing.Size(235, 408);
            this.rtbDisplay.TabIndex = 13;
            this.rtbDisplay.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(550, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Serial Port Communication";
            // 
            // chkBoxEOL
            // 
            this.chkBoxEOL.AutoSize = true;
            this.chkBoxEOL.Checked = true;
            this.chkBoxEOL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxEOL.Location = new System.Drawing.Point(12, 428);
            this.chkBoxEOL.Name = "chkBoxEOL";
            this.chkBoxEOL.Size = new System.Drawing.Size(69, 17);
            this.chkBoxEOL.TabIndex = 15;
            this.chkBoxEOL.Text = "Add EOL";
            this.chkBoxEOL.UseVisualStyleBackColor = true;
            this.chkBoxEOL.CheckedChanged += new System.EventHandler(this.chkBoxEOL_CheckedChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(472, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 16;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(472, 49);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 17;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExecInitToLeft
            // 
            this.btnExecInitToLeft.Location = new System.Drawing.Point(220, 20);
            this.btnExecInitToLeft.Name = "btnExecInitToLeft";
            this.btnExecInitToLeft.Size = new System.Drawing.Size(105, 23);
            this.btnExecInitToLeft.TabIndex = 18;
            this.btnExecInitToLeft.Text = "Exec Init To Left";
            this.btnExecInitToLeft.UseVisualStyleBackColor = true;
            this.btnExecInitToLeft.Click += new System.EventHandler(this.btnExecInitToLeft_Click);
            // 
            // btnExecInitToRight
            // 
            this.btnExecInitToRight.Location = new System.Drawing.Point(220, 49);
            this.btnExecInitToRight.Name = "btnExecInitToRight";
            this.btnExecInitToRight.Size = new System.Drawing.Size(105, 23);
            this.btnExecInitToRight.TabIndex = 19;
            this.btnExecInitToRight.Text = "Exec Init To Right";
            this.btnExecInitToRight.UseVisualStyleBackColor = true;
            this.btnExecInitToRight.Click += new System.EventHandler(this.btnExecInitToRight_Click);
            // 
            // btnAbsPos
            // 
            this.btnAbsPos.Location = new System.Drawing.Point(128, 78);
            this.btnAbsPos.Name = "btnAbsPos";
            this.btnAbsPos.Size = new System.Drawing.Size(86, 23);
            this.btnAbsPos.TabIndex = 20;
            this.btnAbsPos.Text = "Abs Pos";
            this.btnAbsPos.UseVisualStyleBackColor = true;
            this.btnAbsPos.Click += new System.EventHandler(this.btnAbsPos_Click);
            // 
            // btnExecAbsPos
            // 
            this.btnExecAbsPos.Location = new System.Drawing.Point(220, 78);
            this.btnExecAbsPos.Name = "btnExecAbsPos";
            this.btnExecAbsPos.Size = new System.Drawing.Size(105, 23);
            this.btnExecAbsPos.TabIndex = 21;
            this.btnExecAbsPos.Text = "Exec Abs Pos";
            this.btnExecAbsPos.UseVisualStyleBackColor = true;
            this.btnExecAbsPos.Click += new System.EventHandler(this.btnExecAbsPos_Click);
            // 
            // numAbsPos
            // 
            this.numAbsPos.Location = new System.Drawing.Point(332, 78);
            this.numAbsPos.Maximum = new decimal(new int[] {
            192000,
            0,
            0,
            0});
            this.numAbsPos.Name = "numAbsPos";
            this.numAbsPos.Size = new System.Drawing.Size(120, 20);
            this.numAbsPos.TabIndex = 22;
            // 
            // numRelPickup
            // 
            this.numRelPickup.Location = new System.Drawing.Point(332, 110);
            this.numRelPickup.Maximum = new decimal(new int[] {
            192000,
            0,
            0,
            0});
            this.numRelPickup.Name = "numRelPickup";
            this.numRelPickup.Size = new System.Drawing.Size(120, 20);
            this.numRelPickup.TabIndex = 25;
            // 
            // btnExecRelPickup
            // 
            this.btnExecRelPickup.Location = new System.Drawing.Point(220, 110);
            this.btnExecRelPickup.Name = "btnExecRelPickup";
            this.btnExecRelPickup.Size = new System.Drawing.Size(105, 23);
            this.btnExecRelPickup.TabIndex = 24;
            this.btnExecRelPickup.Text = "Exec Rel Pickup";
            this.btnExecRelPickup.UseVisualStyleBackColor = true;
            this.btnExecRelPickup.Click += new System.EventHandler(this.btnExecRelPickup_Click);
            // 
            // btnRelPickup
            // 
            this.btnRelPickup.Location = new System.Drawing.Point(128, 110);
            this.btnRelPickup.Name = "btnRelPickup";
            this.btnRelPickup.Size = new System.Drawing.Size(86, 23);
            this.btnRelPickup.TabIndex = 23;
            this.btnRelPickup.Text = "Rel Pickup";
            this.btnRelPickup.UseVisualStyleBackColor = true;
            this.btnRelPickup.Click += new System.EventHandler(this.btnRelPickup_Click);
            // 
            // numRelDispense
            // 
            this.numRelDispense.Location = new System.Drawing.Point(332, 141);
            this.numRelDispense.Maximum = new decimal(new int[] {
            192000,
            0,
            0,
            0});
            this.numRelDispense.Name = "numRelDispense";
            this.numRelDispense.Size = new System.Drawing.Size(120, 20);
            this.numRelDispense.TabIndex = 28;
            // 
            // btnExecRelDispense
            // 
            this.btnExecRelDispense.Location = new System.Drawing.Point(220, 141);
            this.btnExecRelDispense.Name = "btnExecRelDispense";
            this.btnExecRelDispense.Size = new System.Drawing.Size(105, 23);
            this.btnExecRelDispense.TabIndex = 27;
            this.btnExecRelDispense.Text = "Exec Rel Dispense";
            this.btnExecRelDispense.UseVisualStyleBackColor = true;
            this.btnExecRelDispense.Click += new System.EventHandler(this.btnExecRelDispense_Click);
            // 
            // btnRelDispense
            // 
            this.btnRelDispense.Location = new System.Drawing.Point(128, 141);
            this.btnRelDispense.Name = "btnRelDispense";
            this.btnRelDispense.Size = new System.Drawing.Size(86, 23);
            this.btnRelDispense.TabIndex = 26;
            this.btnRelDispense.Text = "Rel Dispense";
            this.btnRelDispense.UseVisualStyleBackColor = true;
            this.btnRelDispense.Click += new System.EventHandler(this.btnRelDispense_Click);
            // 
            // btnExecMoveValve
            // 
            this.btnExecMoveValve.Location = new System.Drawing.Point(220, 180);
            this.btnExecMoveValve.Name = "btnExecMoveValve";
            this.btnExecMoveValve.Size = new System.Drawing.Size(105, 23);
            this.btnExecMoveValve.TabIndex = 30;
            this.btnExecMoveValve.Text = "Exec Move Valve";
            this.btnExecMoveValve.UseVisualStyleBackColor = true;
            this.btnExecMoveValve.Click += new System.EventHandler(this.btnExecMoveValve_Click);
            // 
            // btnMoveValve
            // 
            this.btnMoveValve.Location = new System.Drawing.Point(128, 180);
            this.btnMoveValve.Name = "btnMoveValve";
            this.btnMoveValve.Size = new System.Drawing.Size(86, 23);
            this.btnMoveValve.TabIndex = 29;
            this.btnMoveValve.Text = "Move Valve";
            this.btnMoveValve.UseVisualStyleBackColor = true;
            this.btnMoveValve.Click += new System.EventHandler(this.btnMoveValve_Click);
            // 
            // grpValvePos
            // 
            this.grpValvePos.Controls.Add(this.rbOutput);
            this.grpValvePos.Controls.Add(this.rbExtra);
            this.grpValvePos.Controls.Add(this.rbInput);
            this.grpValvePos.Location = new System.Drawing.Point(332, 167);
            this.grpValvePos.Name = "grpValvePos";
            this.grpValvePos.Size = new System.Drawing.Size(116, 44);
            this.grpValvePos.TabIndex = 13;
            this.grpValvePos.TabStop = false;
            // 
            // rbExtra
            // 
            this.rbExtra.AutoSize = true;
            this.rbExtra.Checked = true;
            this.rbExtra.Location = new System.Drawing.Point(40, 16);
            this.rbExtra.Name = "rbExtra";
            this.rbExtra.Size = new System.Drawing.Size(32, 17);
            this.rbExtra.TabIndex = 1;
            this.rbExtra.TabStop = true;
            this.rbExtra.Text = "E";
            this.rbExtra.UseVisualStyleBackColor = true;
            // 
            // rbInput
            // 
            this.rbInput.AutoSize = true;
            this.rbInput.Location = new System.Drawing.Point(6, 16);
            this.rbInput.Name = "rbInput";
            this.rbInput.Size = new System.Drawing.Size(28, 17);
            this.rbInput.TabIndex = 0;
            this.rbInput.Text = "I";
            this.rbInput.UseVisualStyleBackColor = true;
            // 
            // rbOutput
            // 
            this.rbOutput.AutoSize = true;
            this.rbOutput.Location = new System.Drawing.Point(78, 16);
            this.rbOutput.Name = "rbOutput";
            this.rbOutput.Size = new System.Drawing.Size(33, 17);
            this.rbOutput.TabIndex = 2;
            this.rbOutput.Text = "O";
            this.rbOutput.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpValvePos);
            this.Controls.Add(this.btnExecMoveValve);
            this.Controls.Add(this.btnMoveValve);
            this.Controls.Add(this.numRelDispense);
            this.Controls.Add(this.btnExecRelDispense);
            this.Controls.Add(this.btnRelDispense);
            this.Controls.Add(this.numRelPickup);
            this.Controls.Add(this.btnExecRelPickup);
            this.Controls.Add(this.btnRelPickup);
            this.Controls.Add(this.numAbsPos);
            this.Controls.Add(this.btnExecAbsPos);
            this.Controls.Add(this.btnAbsPos);
            this.Controls.Add(this.btnExecInitToRight);
            this.Controls.Add(this.btnExecInitToLeft);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.chkBoxEOL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbDisplay);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOpen);
            this.Controls.Add(this.cmdSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnInitToRight);
            this.Controls.Add(this.btnInitToLeft);
            this.Name = "frmMain";
            this.Text = "Hamilton Syringe Pump";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAbsPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRelPickup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRelDispense)).EndInit();
            this.grpValvePos.ResumeLayout(false);
            this.grpValvePos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInitToLeft;
        private System.Windows.Forms.Button btnInitToRight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboBaud;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoText;
        private System.Windows.Forms.RadioButton rdoHex;
        private System.Windows.Forms.RichTextBox rtbDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBoxEOL;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnExecInitToLeft;
        private System.Windows.Forms.Button btnExecInitToRight;
        private System.Windows.Forms.Button btnAbsPos;
        private System.Windows.Forms.Button btnExecAbsPos;
        private System.Windows.Forms.NumericUpDown numAbsPos;
        private System.Windows.Forms.NumericUpDown numRelPickup;
        private System.Windows.Forms.Button btnExecRelPickup;
        private System.Windows.Forms.Button btnRelPickup;
        private System.Windows.Forms.NumericUpDown numRelDispense;
        private System.Windows.Forms.Button btnExecRelDispense;
        private System.Windows.Forms.Button btnRelDispense;
        private System.Windows.Forms.Button btnExecMoveValve;
        private System.Windows.Forms.Button btnMoveValve;
        private System.Windows.Forms.GroupBox grpValvePos;
        private System.Windows.Forms.RadioButton rbOutput;
        private System.Windows.Forms.RadioButton rbExtra;
        private System.Windows.Forms.RadioButton rbInput;
    }
}

