namespace WinFormsApp1
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
            btConnect = new Button();
            btDisconnect = new Button();
            gbSMS = new GroupBox();
            button9 = new Button();
            btnClear = new Button();
            txtSms = new TextBox();
            btConfirmSMS = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            bt1 = new Button();
            groupBox1 = new GroupBox();
            rbSetDisconnected = new RadioButton();
            rbSetConnected = new RadioButton();
            btStopCisoVpn = new Button();
            gbSMS.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btConnect
            // 
            btConnect.Font = new Font("Segoe UI", 15.7090912F, FontStyle.Regular, GraphicsUnit.Point);
            btConnect.Location = new Point(19, 10);
            btConnect.Margin = new Padding(3, 2, 3, 2);
            btConnect.Name = "btConnect";
            btConnect.Size = new Size(156, 43);
            btConnect.TabIndex = 0;
            btConnect.Text = "Connect";
            btConnect.UseVisualStyleBackColor = true;
            btConnect.Click += btConnect_Click;
            // 
            // btDisconnect
            // 
            btDisconnect.Font = new Font("Segoe UI", 15.7090912F, FontStyle.Regular, GraphicsUnit.Point);
            btDisconnect.Location = new Point(196, 10);
            btDisconnect.Margin = new Padding(3, 2, 3, 2);
            btDisconnect.Name = "btDisconnect";
            btDisconnect.Size = new Size(146, 43);
            btDisconnect.TabIndex = 1;
            btDisconnect.Text = "Disconnect";
            btDisconnect.UseVisualStyleBackColor = true;
            btDisconnect.Click += btDisconnect_Click;
            // 
            // gbSMS
            // 
            gbSMS.Controls.Add(button9);
            gbSMS.Controls.Add(btnClear);
            gbSMS.Controls.Add(txtSms);
            gbSMS.Controls.Add(btConfirmSMS);
            gbSMS.Controls.Add(button6);
            gbSMS.Controls.Add(button7);
            gbSMS.Controls.Add(button8);
            gbSMS.Controls.Add(button3);
            gbSMS.Controls.Add(button4);
            gbSMS.Controls.Add(button5);
            gbSMS.Controls.Add(button2);
            gbSMS.Controls.Add(button1);
            gbSMS.Controls.Add(bt1);
            gbSMS.Enabled = false;
            gbSMS.Font = new Font("Segoe UI", 13.7454548F, FontStyle.Regular, GraphicsUnit.Point);
            gbSMS.Location = new Point(30, 81);
            gbSMS.Margin = new Padding(3, 2, 3, 2);
            gbSMS.Name = "gbSMS";
            gbSMS.Padding = new Padding(3, 2, 3, 2);
            gbSMS.Size = new Size(416, 426);
            gbSMS.TabIndex = 2;
            gbSMS.TabStop = false;
            gbSMS.Text = "SMS code";
            // 
            // button9
            // 
            button9.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button9.Location = new Point(91, 301);
            button9.Margin = new Padding(3, 2, 3, 2);
            button9.Name = "button9";
            button9.Size = new Size(213, 49);
            button9.TabIndex = 12;
            button9.Text = "0";
            button9.UseVisualStyleBackColor = true;
            button9.Click += bt1_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(255, 359);
            btnClear.Margin = new Padding(3, 2, 3, 2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(73, 52);
            btnClear.TabIndex = 11;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // txtSms
            // 
            txtSms.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            txtSms.Location = new Point(73, 34);
            txtSms.Margin = new Padding(3, 2, 3, 2);
            txtSms.Name = "txtSms";
            txtSms.Size = new Size(255, 54);
            txtSms.TabIndex = 10;
            txtSms.KeyDown += txtSms_KeyDown;
            // 
            // btConfirmSMS
            // 
            btConfirmSMS.Font = new Font("Segoe UI", 18.3272724F, FontStyle.Regular, GraphicsUnit.Point);
            btConfirmSMS.Location = new Point(78, 359);
            btConfirmSMS.Margin = new Padding(3, 2, 3, 2);
            btConfirmSMS.Name = "btConfirmSMS";
            btConfirmSMS.Size = new Size(166, 52);
            btConfirmSMS.TabIndex = 9;
            btConfirmSMS.Text = "Confirm SMS";
            btConfirmSMS.UseVisualStyleBackColor = true;
            btConfirmSMS.Click += btConfirmSMS_Click;
            // 
            // button6
            // 
            button6.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button6.Location = new Point(245, 236);
            button6.Margin = new Padding(3, 2, 3, 2);
            button6.Name = "button6";
            button6.Size = new Size(59, 51);
            button6.TabIndex = 8;
            button6.Text = "9";
            button6.UseVisualStyleBackColor = true;
            button6.Click += bt1_Click;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button7.Location = new Point(170, 236);
            button7.Margin = new Padding(3, 2, 3, 2);
            button7.Name = "button7";
            button7.Size = new Size(58, 51);
            button7.TabIndex = 7;
            button7.Text = "8";
            button7.UseVisualStyleBackColor = true;
            button7.Click += bt1_Click;
            // 
            // button8
            // 
            button8.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button8.Location = new Point(91, 236);
            button8.Margin = new Padding(3, 2, 3, 2);
            button8.Name = "button8";
            button8.Size = new Size(56, 51);
            button8.TabIndex = 6;
            button8.Text = "7";
            button8.UseVisualStyleBackColor = true;
            button8.Click += bt1_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(245, 171);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(59, 47);
            button3.TabIndex = 5;
            button3.Text = "6";
            button3.UseVisualStyleBackColor = true;
            button3.Click += bt1_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(170, 171);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(58, 47);
            button4.TabIndex = 4;
            button4.Text = "5";
            button4.UseVisualStyleBackColor = true;
            button4.Click += bt1_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(91, 171);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(56, 47);
            button5.TabIndex = 3;
            button5.Text = "4";
            button5.UseVisualStyleBackColor = true;
            button5.Click += bt1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(245, 97);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(59, 52);
            button2.TabIndex = 2;
            button2.Text = "3";
            button2.UseVisualStyleBackColor = true;
            button2.Click += bt1_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(170, 97);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(58, 52);
            button1.TabIndex = 1;
            button1.Text = "2";
            button1.UseVisualStyleBackColor = true;
            button1.Click += bt1_Click;
            // 
            // bt1
            // 
            bt1.Font = new Font("Segoe UI", 26.181818F, FontStyle.Regular, GraphicsUnit.Point);
            bt1.Location = new Point(91, 97);
            bt1.Margin = new Padding(3, 2, 3, 2);
            bt1.Name = "bt1";
            bt1.Size = new Size(56, 52);
            bt1.TabIndex = 0;
            bt1.Text = "1";
            bt1.UseVisualStyleBackColor = true;
            bt1.Click += bt1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbSetDisconnected);
            groupBox1.Controls.Add(rbSetConnected);
            groupBox1.Location = new Point(103, 57);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(266, 20);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Change status";
            groupBox1.Visible = false;
            // 
            // rbSetDisconnected
            // 
            rbSetDisconnected.AutoSize = true;
            rbSetDisconnected.Location = new Point(142, 16);
            rbSetDisconnected.Margin = new Padding(3, 2, 3, 2);
            rbSetDisconnected.Name = "rbSetDisconnected";
            rbSetDisconnected.Size = new Size(97, 19);
            rbSetDisconnected.TabIndex = 1;
            rbSetDisconnected.TabStop = true;
            rbSetDisconnected.Text = "Disconnected";
            rbSetDisconnected.UseVisualStyleBackColor = true;
            // 
            // rbSetConnected
            // 
            rbSetConnected.AutoSize = true;
            rbSetConnected.Location = new Point(18, 16);
            rbSetConnected.Margin = new Padding(3, 2, 3, 2);
            rbSetConnected.Name = "rbSetConnected";
            rbSetConnected.Size = new Size(83, 19);
            rbSetConnected.TabIndex = 0;
            rbSetConnected.TabStop = true;
            rbSetConnected.Text = "Connected";
            rbSetConnected.UseVisualStyleBackColor = true;
            rbSetConnected.CheckedChanged += btSetConnected_CheckedChanged;
            // 
            // btStopCisoVpn
            // 
            btStopCisoVpn.Location = new Point(363, 12);
            btStopCisoVpn.Margin = new Padding(3, 2, 3, 2);
            btStopCisoVpn.Name = "btStopCisoVpn";
            btStopCisoVpn.Size = new Size(91, 40);
            btStopCisoVpn.TabIndex = 4;
            btStopCisoVpn.Text = "Stop Cisco VPN";
            btStopCisoVpn.UseVisualStyleBackColor = true;
            btStopCisoVpn.Click += btStopCisco_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 512);
            Controls.Add(btStopCisoVpn);
            Controls.Add(groupBox1);
            Controls.Add(gbSMS);
            Controls.Add(btDisconnect);
            Controls.Add(btConnect);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Vpn Manager";
            Load += Form1_Load;
            gbSMS.ResumeLayout(false);
            gbSMS.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btConnect;
        private Button btDisconnect;
        private GroupBox gbSMS;
        private Button btConfirmSMS;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button2;
        private Button button1;
        private Button bt1;
        private TextBox txtSms;
        private Button btnClear;
        private Button button9;
        private GroupBox groupBox1;
        private RadioButton rbSetDisconnected;
        private RadioButton rbSetConnected;
        private Button btStopCisoVpn;
    }
}