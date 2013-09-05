﻿namespace PointerTrap
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.setHotkey = new System.Windows.Forms.Button();
			this.hotkeyBox = new System.Windows.Forms.TextBox();
			this.processBox = new System.Windows.Forms.ComboBox();
			this.RefreshProcList = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lockTypeCombo = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.hardLock = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.changeHotkey = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.cbTray = new System.Windows.Forms.CheckBox();
			this.cbTrayBalloons = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.numCycle = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCycle)).BeginInit();
			this.SuspendLayout();
			// 
			// setHotkey
			// 
			this.setHotkey.Location = new System.Drawing.Point(284, 10);
			this.setHotkey.Name = "setHotkey";
			this.setHotkey.Size = new System.Drawing.Size(75, 23);
			this.setHotkey.TabIndex = 0;
			this.setHotkey.Text = "Set HotKey";
			this.setHotkey.UseVisualStyleBackColor = true;
			this.setHotkey.Visible = false;
			this.setHotkey.Click += new System.EventHandler(this.setHotkey_Click);
			// 
			// hotkeyBox
			// 
			this.hotkeyBox.Enabled = false;
			this.hotkeyBox.Location = new System.Drawing.Point(136, 12);
			this.hotkeyBox.Name = "hotkeyBox";
			this.hotkeyBox.Size = new System.Drawing.Size(122, 20);
			this.hotkeyBox.TabIndex = 1;
			this.hotkeyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			this.hotkeyBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			this.hotkeyBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
			// 
			// processBox
			// 
			this.processBox.FormattingEnabled = true;
			this.processBox.Location = new System.Drawing.Point(136, 65);
			this.processBox.Name = "processBox";
			this.processBox.Size = new System.Drawing.Size(121, 21);
			this.processBox.TabIndex = 2;
			this.processBox.SelectedIndexChanged += new System.EventHandler(this.processBox_SelectedIndexChanged);
			// 
			// RefreshProcList
			// 
			this.RefreshProcList.Location = new System.Drawing.Point(284, 63);
			this.RefreshProcList.Name = "RefreshProcList";
			this.RefreshProcList.Size = new System.Drawing.Size(75, 23);
			this.RefreshProcList.TabIndex = 3;
			this.RefreshProcList.Text = "RefreshList";
			this.RefreshProcList.UseVisualStyleBackColor = true;
			this.RefreshProcList.Click += new System.EventHandler(this.RefreshProcList_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Toggle active hotkey";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Process to lock on";
			// 
			// lockTypeCombo
			// 
			this.lockTypeCombo.FormattingEnabled = true;
			this.lockTypeCombo.Location = new System.Drawing.Point(136, 38);
			this.lockTypeCombo.Name = "lockTypeCombo";
			this.lockTypeCombo.Size = new System.Drawing.Size(121, 21);
			this.lockTypeCombo.TabIndex = 6;
			this.lockTypeCombo.SelectedIndexChanged += new System.EventHandler(this.lockTypeCombo_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Lock type";
			// 
			// hardLock
			// 
			this.hardLock.AutoSize = true;
			this.hardLock.Location = new System.Drawing.Point(15, 96);
			this.hardLock.Name = "hardLock";
			this.hardLock.Size = new System.Drawing.Size(72, 17);
			this.hardLock.TabIndex = 8;
			this.hardLock.Text = "Hard lock";
			this.toolTip1.SetToolTip(this.hardLock, "This will lock your pointer to the middle of the lock area.\r\n(be careful with thi" +
        "s you won\'t be able to move your pointer at all)");
			this.hardLock.UseVisualStyleBackColor = true;
			this.hardLock.CheckedChanged += new System.EventHandler(this.hideCursorBox_CheckedChanged);
			// 
			// changeHotkey
			// 
			this.changeHotkey.Location = new System.Drawing.Point(284, 10);
			this.changeHotkey.Name = "changeHotkey";
			this.changeHotkey.Size = new System.Drawing.Size(75, 23);
			this.changeHotkey.TabIndex = 9;
			this.changeHotkey.Text = "Change";
			this.changeHotkey.UseVisualStyleBackColor = true;
			this.changeHotkey.Click += new System.EventHandler(this.changeHotkey_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(290, 143);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "by ChickenB0h";
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon1.BalloonTipTitle = "PointerTrap";
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "PointerTrap";
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
			this.toolStripMenuItem1.Text = "Show";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// cbTray
			// 
			this.cbTray.AutoSize = true;
			this.cbTray.Location = new System.Drawing.Point(15, 139);
			this.cbTray.Name = "cbTray";
			this.cbTray.Size = new System.Drawing.Size(98, 17);
			this.cbTray.TabIndex = 12;
			this.cbTray.Text = "Minimize to tray";
			this.cbTray.UseVisualStyleBackColor = true;
			this.cbTray.CheckedChanged += new System.EventHandler(this.cbTray_CheckedChanged);
			// 
			// cbTrayBalloons
			// 
			this.cbTrayBalloons.AutoSize = true;
			this.cbTrayBalloons.Location = new System.Drawing.Point(136, 139);
			this.cbTrayBalloons.Name = "cbTrayBalloons";
			this.cbTrayBalloons.Size = new System.Drawing.Size(115, 17);
			this.cbTrayBalloons.TabIndex = 13;
			this.cbTrayBalloons.Text = "Show tray balloons";
			this.cbTrayBalloons.UseVisualStyleBackColor = true;
			this.cbTrayBalloons.CheckedChanged += new System.EventHandler(this.cbTrayBalloons_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 116);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(125, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Hard lock cycle time(ms):";
			// 
			// numCycle
			// 
			this.numCycle.Location = new System.Drawing.Point(136, 113);
			this.numCycle.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numCycle.Name = "numCycle";
			this.numCycle.Size = new System.Drawing.Size(120, 20);
			this.numCycle.TabIndex = 15;
			this.numCycle.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(284, 110);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 16;
			this.button1.Text = "Set";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 161);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.numCycle);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbTrayBalloons);
			this.Controls.Add(this.cbTray);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.changeHotkey);
			this.Controls.Add(this.hardLock);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lockTypeCombo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.RefreshProcList);
			this.Controls.Add(this.processBox);
			this.Controls.Add(this.hotkeyBox);
			this.Controls.Add(this.setHotkey);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(391, 200);
			this.MinimumSize = new System.Drawing.Size(391, 200);
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Pointer Trap";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numCycle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button setHotkey;
		private System.Windows.Forms.TextBox hotkeyBox;
		private System.Windows.Forms.ComboBox processBox;
		private System.Windows.Forms.Button RefreshProcList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox lockTypeCombo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox hardLock;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button changeHotkey;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.CheckBox cbTray;
		private System.Windows.Forms.CheckBox cbTrayBalloons;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numCycle;
		private System.Windows.Forms.Button button1;
	}
}

