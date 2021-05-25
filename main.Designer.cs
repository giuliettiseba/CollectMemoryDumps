namespace CollectMemoryDumps
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.listBoxMilestoneProcesses = new System.Windows.Forms.ListBox();
            this.groupBox_Options = new System.Windows.Forms.GroupBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox_Output = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_PID = new System.Windows.Forms.TextBox();
            this.labelPid = new System.Windows.Forms.Label();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.textBox_CustomArguments = new System.Windows.Forms.TextBox();
            this.groupBox_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxMilestoneProcesses
            // 
            this.listBoxMilestoneProcesses.BackColor = System.Drawing.Color.Black;
            this.listBoxMilestoneProcesses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxMilestoneProcesses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.listBoxMilestoneProcesses.FormattingEnabled = true;
            this.listBoxMilestoneProcesses.Location = new System.Drawing.Point(12, 12);
            this.listBoxMilestoneProcesses.Name = "listBoxMilestoneProcesses";
            this.listBoxMilestoneProcesses.Size = new System.Drawing.Size(263, 275);
            this.listBoxMilestoneProcesses.TabIndex = 0;
            this.listBoxMilestoneProcesses.SelectedIndexChanged += new System.EventHandler(this.ListBox_MilestoneProcesses_SelectedIndexChanged);
            this.listBoxMilestoneProcesses.Enter += new System.EventHandler(this.listBoxMilestoneProcesses_Enter);
            // 
            // groupBox_Options
            // 
            this.groupBox_Options.Controls.Add(this.textBox_CustomArguments);
            this.groupBox_Options.Controls.Add(this.radioButton9);
            this.groupBox_Options.Controls.Add(this.radioButton8);
            this.groupBox_Options.Controls.Add(this.radioButton10);
            this.groupBox_Options.Controls.Add(this.radioButton7);
            this.groupBox_Options.Controls.Add(this.radioButton6);
            this.groupBox_Options.Controls.Add(this.radioButton4);
            this.groupBox_Options.Controls.Add(this.radioButton5);
            this.groupBox_Options.Controls.Add(this.radioButton3);
            this.groupBox_Options.Controls.Add(this.radioButton2);
            this.groupBox_Options.Controls.Add(this.radioButton1);
            this.groupBox_Options.Location = new System.Drawing.Point(12, 319);
            this.groupBox_Options.Name = "groupBox_Options";
            this.groupBox_Options.Size = new System.Drawing.Size(263, 234);
            this.groupBox_Options.TabIndex = 5;
            this.groupBox_Options.TabStop = false;
            this.groupBox_Options.Text = "Options";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(187, 106);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(70, 17);
            this.radioButton9.TabIndex = 11;
            this.radioButton9.Text = "CPU 90%";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(94, 106);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(70, 17);
            this.radioButton8.TabIndex = 12;
            this.radioButton8.Text = "CPU 75%";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(6, 106);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(70, 17);
            this.radioButton7.TabIndex = 13;
            this.radioButton7.Text = "CPU 50%";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(150, 77);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(101, 17);
            this.radioButton6.TabIndex = 14;
            this.radioButton6.Text = "After 30 minutes";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 77);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(101, 17);
            this.radioButton4.TabIndex = 15;
            this.radioButton4.Text = "After 10 minutes";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(150, 48);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(107, 17);
            this.radioButton5.TabIndex = 10;
            this.radioButton5.Text = "Memory 1000 Mb";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 48);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(101, 17);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.Text = "Memory 500 Mb";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(150, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "Exception";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(57, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Instant";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox_Output
            // 
            this.textBox_Output.BackColor = System.Drawing.Color.Black;
            this.textBox_Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox_Output.Location = new System.Drawing.Point(281, 12);
            this.textBox_Output.Multiline = true;
            this.textBox_Output.Name = "textBox_Output";
            this.textBox_Output.ReadOnly = true;
            this.textBox_Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Output.Size = new System.Drawing.Size(637, 570);
            this.textBox_Output.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 559);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(263, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Collect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Collect_Clicked);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(924, 36);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(490, 546);
            this.flowLayoutPanel1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(925, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Waiting procdump instances";
            // 
            // textBox_PID
            // 
            this.textBox_PID.BackColor = System.Drawing.Color.Black;
            this.textBox_PID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox_PID.Location = new System.Drawing.Point(52, 293);
            this.textBox_PID.Name = "textBox_PID";
            this.textBox_PID.Size = new System.Drawing.Size(223, 20);
            this.textBox_PID.TabIndex = 20;
            this.textBox_PID.Enter += new System.EventHandler(this.textBox_PID_Enter);
            // 
            // labelPid
            // 
            this.labelPid.AutoSize = true;
            this.labelPid.Location = new System.Drawing.Point(15, 296);
            this.labelPid.Name = "labelPid";
            this.labelPid.Size = new System.Drawing.Size(31, 13);
            this.labelPid.TabIndex = 21;
            this.labelPid.Text = "PID: ";
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(6, 135);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(113, 17);
            this.radioButton10.TabIndex = 13;
            this.radioButton10.Text = "Custom Arguments";
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // textBox_CustomArguments
            // 
            this.textBox_CustomArguments.BackColor = System.Drawing.Color.Black;
            this.textBox_CustomArguments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.textBox_CustomArguments.Location = new System.Drawing.Point(6, 158);
            this.textBox_CustomArguments.Multiline = true;
            this.textBox_CustomArguments.Name = "textBox_CustomArguments";
            this.textBox_CustomArguments.Size = new System.Drawing.Size(251, 69);
            this.textBox_CustomArguments.TabIndex = 22;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1428, 592);
            this.Controls.Add(this.labelPid);
            this.Controls.Add(this.textBox_PID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_Output);
            this.Controls.Add(this.groupBox_Options);
            this.Controls.Add(this.listBoxMilestoneProcesses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.Text = "CollectMemoryDumps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.groupBox_Options.ResumeLayout(false);
            this.groupBox_Options.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxMilestoneProcesses;
        private System.Windows.Forms.GroupBox groupBox_Options;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox_Output;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_PID;
        private System.Windows.Forms.Label labelPid;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.TextBox textBox_CustomArguments;
    }
}

