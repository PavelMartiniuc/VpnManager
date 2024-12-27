namespace SmsActivation
{
    partial class fmMain
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
            btStartListening = new Button();
            btStopListening = new Button();
            txtCode = new TextBox();
            btSaveAmdarisPasswordToClipboard = new Button();
            btCurrentProjectPassordToClipboad = new Button();
            txtListenAgoMinutes = new NumericUpDown();
            label1 = new Label();
            btStartProcentiaVM = new Button();
            ConnectToVM = new Button();
            ((System.ComponentModel.ISupportInitialize)txtListenAgoMinutes).BeginInit();
            SuspendLayout();
            // 
            // btStartListening
            // 
            btStartListening.Location = new Point(21, 12);
            btStartListening.Name = "btStartListening";
            btStartListening.Size = new Size(146, 50);
            btStartListening.TabIndex = 0;
            btStartListening.Text = "Start listening";
            btStartListening.UseVisualStyleBackColor = true;
            btStartListening.Click += btStartListening_Click;
            // 
            // btStopListening
            // 
            btStopListening.Enabled = false;
            btStopListening.Location = new Point(196, 12);
            btStopListening.Name = "btStopListening";
            btStopListening.Size = new Size(146, 50);
            btStopListening.TabIndex = 1;
            btStopListening.Text = "Stop listening";
            btStopListening.UseVisualStyleBackColor = true;
            btStopListening.Click += btStopListening_Click;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(129, 82);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(115, 26);
            txtCode.TabIndex = 2;
            // 
            // btSaveAmdarisPasswordToClipboard
            // 
            btSaveAmdarisPasswordToClipboard.Location = new Point(362, 12);
            btSaveAmdarisPasswordToClipboard.Name = "btSaveAmdarisPasswordToClipboard";
            btSaveAmdarisPasswordToClipboard.Size = new Size(211, 50);
            btSaveAmdarisPasswordToClipboard.TabIndex = 3;
            btSaveAmdarisPasswordToClipboard.Text = "Amdaris password to clipboard";
            btSaveAmdarisPasswordToClipboard.UseVisualStyleBackColor = true;
            btSaveAmdarisPasswordToClipboard.Click += btSavePasswordToClipboard_Click;
            // 
            // btCurrentProjectPassordToClipboad
            // 
            btCurrentProjectPassordToClipboad.Location = new Point(589, 12);
            btCurrentProjectPassordToClipboad.Name = "btCurrentProjectPassordToClipboad";
            btCurrentProjectPassordToClipboad.Size = new Size(211, 50);
            btCurrentProjectPassordToClipboad.TabIndex = 4;
            btCurrentProjectPassordToClipboad.Text = "Current password to clipboard";
            btCurrentProjectPassordToClipboad.UseVisualStyleBackColor = true;
            btCurrentProjectPassordToClipboad.Click += btCurrentProjectPassordToClipboad_Click;
            // 
            // txtListenAgoMinutes
            // 
            txtListenAgoMinutes.Location = new Point(616, 82);
            txtListenAgoMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtListenAgoMinutes.Name = "txtListenAgoMinutes";
            txtListenAgoMinutes.Size = new Size(138, 26);
            txtListenAgoMinutes.TabIndex = 5;
            txtListenAgoMinutes.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(485, 84);
            label1.Name = "label1";
            label1.Size = new Size(125, 19);
            label1.TabIndex = 6;
            label1.Text = "Listen ago minutes";
            // 
            // btStartProcentiaVM
            // 
            btStartProcentiaVM.Location = new Point(196, 130);
            btStartProcentiaVM.Name = "btStartProcentiaVM";
            btStartProcentiaVM.Size = new Size(146, 43);
            btStartProcentiaVM.TabIndex = 7;
            btStartProcentiaVM.Text = "Start Procentia VM";
            btStartProcentiaVM.UseVisualStyleBackColor = true;
            btStartProcentiaVM.Click += btStartProcentiaVM_Click;
            // 
            // ConnectToVM
            // 
            ConnectToVM.Location = new Point(21, 130);
            ConnectToVM.Name = "ConnectToVM";
            ConnectToVM.Size = new Size(157, 43);
            ConnectToVM.TabIndex = 8;
            ConnectToVM.Text = "Connect To VM";
            ConnectToVM.UseVisualStyleBackColor = true;
            ConnectToVM.Click += ConnectToVM_Click;
            // 
            // fmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(812, 194);
            Controls.Add(ConnectToVM);
            Controls.Add(btStartProcentiaVM);
            Controls.Add(label1);
            Controls.Add(txtListenAgoMinutes);
            Controls.Add(btCurrentProjectPassordToClipboad);
            Controls.Add(btSaveAmdarisPasswordToClipboard);
            Controls.Add(txtCode);
            Controls.Add(btStopListening);
            Controls.Add(btStartListening);
            Name = "fmMain";
            Text = "Sms code listener";
            ((System.ComponentModel.ISupportInitialize)txtListenAgoMinutes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btStartListening;
        private Button btStopListening;
        private TextBox txtCode;
        private Button btSaveAmdarisPasswordToClipboard;
        private Button btCurrentProjectPassordToClipboad;
        private NumericUpDown txtListenAgoMinutes;
        private Label label1;
        private Button btStartProcentiaVM;
        private Button ConnectToVM;
    }
}
