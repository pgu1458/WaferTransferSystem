namespace WaferTransferSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStatus = new Label();
            lblStep = new Label();
            grpSensor = new GroupBox();
            chkDoorLock = new CheckBox();
            chkWafer = new CheckBox();
            chkDoor = new CheckBox();
            grpOutput = new GroupBox();
            chkLock = new CheckBox();
            chkMotor = new CheckBox();
            chkVacuum = new CheckBox();
            btnStart = new Button();
            btnStop = new Button();
            btnReset = new Button();
            label1 = new Label();
            rtbLog = new RichTextBox();
            grpSensor.SuspendLayout();
            grpOutput.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("굴림", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblStatus.ForeColor = Color.Lime;
            lblStatus.Location = new Point(20, 20);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(233, 28);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "장비 상태 : IDLE";
            // 
            // lblStep
            // 
            lblStep.AutoSize = true;
            lblStep.Font = new Font("굴림", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblStep.ForeColor = Color.White;
            lblStep.Location = new Point(20, 60);
            lblStep.Name = "lblStep";
            lblStep.Size = new Size(137, 20);
            lblStep.TabIndex = 0;
            lblStep.Text = "현재 STEP : -";
            // 
            // grpSensor
            // 
            grpSensor.BackColor = Color.Transparent;
            grpSensor.Controls.Add(chkDoorLock);
            grpSensor.Controls.Add(chkWafer);
            grpSensor.Controls.Add(chkDoor);
            grpSensor.ForeColor = Color.White;
            grpSensor.Location = new Point(20, 100);
            grpSensor.Name = "grpSensor";
            grpSensor.Size = new Size(200, 150);
            grpSensor.TabIndex = 1;
            grpSensor.TabStop = false;
            grpSensor.Text = "센서 상태";
            // 
            // chkDoorLock
            // 
            chkDoorLock.AutoCheck = false;
            chkDoorLock.AutoSize = true;
            chkDoorLock.Enabled = false;
            chkDoorLock.Location = new Point(10, 90);
            chkDoorLock.Name = "chkDoorLock";
            chkDoorLock.Size = new Size(101, 24);
            chkDoorLock.TabIndex = 0;
            chkDoorLock.Text = "Door Lock";
            chkDoorLock.UseVisualStyleBackColor = true;
            // 
            // chkWafer
            // 
            chkWafer.AutoCheck = false;
            chkWafer.AutoSize = true;
            chkWafer.Enabled = false;
            chkWafer.Location = new Point(10, 60);
            chkWafer.Name = "chkWafer";
            chkWafer.Size = new Size(120, 24);
            chkWafer.TabIndex = 0;
            chkWafer.Text = "Wafer Detect";
            chkWafer.UseVisualStyleBackColor = true;
            // 
            // chkDoor
            // 
            chkDoor.AutoCheck = false;
            chkDoor.AutoSize = true;
            chkDoor.Enabled = false;
            chkDoor.Location = new Point(10, 30);
            chkDoor.Name = "chkDoor";
            chkDoor.Size = new Size(108, 24);
            chkDoor.TabIndex = 0;
            chkDoor.Text = "Door Open";
            chkDoor.UseVisualStyleBackColor = true;
            // 
            // grpOutput
            // 
            grpOutput.BackColor = Color.Transparent;
            grpOutput.Controls.Add(chkLock);
            grpOutput.Controls.Add(chkMotor);
            grpOutput.Controls.Add(chkVacuum);
            grpOutput.ForeColor = Color.White;
            grpOutput.Location = new Point(240, 100);
            grpOutput.Name = "grpOutput";
            grpOutput.Size = new Size(200, 150);
            grpOutput.TabIndex = 2;
            grpOutput.TabStop = false;
            grpOutput.Text = "출력 상태";
            // 
            // chkLock
            // 
            chkLock.AutoCheck = false;
            chkLock.AutoSize = true;
            chkLock.Enabled = false;
            chkLock.Location = new Point(10, 90);
            chkLock.Name = "chkLock";
            chkLock.Size = new Size(132, 24);
            chkLock.TabIndex = 0;
            chkLock.Text = "Door Lock Out";
            chkLock.UseVisualStyleBackColor = true;
            // 
            // chkMotor
            // 
            chkMotor.AutoCheck = false;
            chkMotor.AutoSize = true;
            chkMotor.Enabled = false;
            chkMotor.Location = new Point(10, 60);
            chkMotor.Name = "chkMotor";
            chkMotor.Size = new Size(105, 24);
            chkMotor.TabIndex = 0;
            chkMotor.Text = "Motor Fwd";
            chkMotor.UseVisualStyleBackColor = true;
            // 
            // chkVacuum
            // 
            chkVacuum.AutoCheck = false;
            chkVacuum.AutoSize = true;
            chkVacuum.Enabled = false;
            chkVacuum.Location = new Point(10, 30);
            chkVacuum.Name = "chkVacuum";
            chkVacuum.Size = new Size(87, 24);
            chkVacuum.TabIndex = 0;
            chkVacuum.Text = "Vacuum";
            chkVacuum.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Lime;
            btnStart.Font = new Font("굴림", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.ForeColor = Color.Black;
            btnStart.Location = new Point(20, 270);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 40);
            btnStart.TabIndex = 3;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.Red;
            btnStop.Font = new Font("굴림", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(130, 270);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 40);
            btnStop.TabIndex = 3;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.Orange;
            btnReset.Font = new Font("굴림", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnReset.ForeColor = Color.Black;
            btnReset.Location = new Point(240, 270);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 40);
            btnReset.TabIndex = 3;
            btnReset.Text = "RESET";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 310);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 5;
            label1.Text = "알람 로그";
            // 
            // rtbLog
            // 
            rtbLog.BackColor = Color.Black;
            rtbLog.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            rtbLog.Location = new Point(20, 330);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(740, 200);
            rtbLog.TabIndex = 6;
            rtbLog.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(782, 553);
            Controls.Add(rtbLog);
            Controls.Add(label1);
            Controls.Add(btnReset);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(grpOutput);
            Controls.Add(grpSensor);
            Controls.Add(lblStep);
            Controls.Add(lblStatus);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Wafer Transfer System";
            grpSensor.ResumeLayout(false);
            grpSensor.PerformLayout();
            grpOutput.ResumeLayout(false);
            grpOutput.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Label lblStep;
        private GroupBox grpSensor;
        private CheckBox chkDoorLock;
        private CheckBox chkWafer;
        private CheckBox chkDoor;
        private GroupBox grpOutput;
        private CheckBox chkLock;
        private CheckBox chkMotor;
        private CheckBox chkVacuum;
        private Button btnStart;
        private Button btnStop;
        private Button btnReset;
        private Label label1;
        private RichTextBox rtbLog;
    }
}
