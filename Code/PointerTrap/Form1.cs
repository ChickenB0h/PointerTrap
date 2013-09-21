using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using KeyListiner;

namespace PointerTrap
{
	public partial class Form1 : Form
	{
		#region fields
		private readonly string settingsPath = AppDomain.CurrentDomain.BaseDirectory + "PointerTrap.set";
		private KeyboardHook hook = new KeyboardHook();

		private ModifierKeys modifierSetting;
		private Keys keySetting;

		private Settings settings;
		private Thread pointerTrapThread;
		private PointerLocker locker;
		#endregion

		#region constructor
		public Form1()
		{
			InitializeComponent();
		}
		#endregion

		private void Form1_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized && settings.minimizeToTray)
			{
				notifyIcon1.Visible = true;
				this.Hide();
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{

			lockTypeCombo.Items.Add(LockType.Monitor);
			lockTypeCombo.Items.Add(LockType.Process);
			lockTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			processBox.DropDownStyle = ComboBoxStyle.DropDownList;
			try
			{
				settings = new Settings();
				settings.Load();

				Location = settings.WindowLocation;
				hotkeyBox.Text = keysToString(settings.modifierKey, settings.hotkey);
				lockTypeCombo.SelectedItem = settings.lockType;
				cbTray.Checked = settings.minimizeToTray;
				cbTrayBalloons.Checked = settings.showBalloons;
				checkBox1.Checked = settings.boarderFix;
			}
			catch
			{
				settings.lockType = LockType.Monitor;
			}
			finally
			{
				hook.KeyPressed += keyboardHook_HotKeyMatched;

				FormClosing += Form1_FormClosing;
				Resize += Form1_Resize;

				SetProcessListEnable();
				locker = new PointerLocker(ref settings);
				locker.CycleDone += locker_CycleDone;
				locker.Activated += locker_Activated;
				locker.Deactivated += locker_Deactivated;
				pointerTrapThread = new Thread(new ThreadStart(locker.Run));
				pointerTrapThread.Start();

				if (settings.modifierKey != 0 || settings.hotkey != Keys.None)
					hook.RegisterHotKey(settings.modifierKey, settings.hotkey);
			}
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			pointerTrapThread.Abort();
			settings.WindowLocation = this.Location;
			settings.Save();
		}

		private void keyboardHook_HotKeyMatched(object sender, KeyPressedEventArgs e)
		{
			locker.active = !locker.active;
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Maximized;
			this.WindowState = FormWindowState.Normal;
			notifyIcon1.Visible = false;
		}

		#region hotkey setter
		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyValue)
			{
				case 16: modifierSetting = modifierSetting | KeyListiner.ModifierKeys.Shift; break;
				case 17: modifierSetting = modifierSetting | KeyListiner.ModifierKeys.Control; break;
				case 18: modifierSetting = modifierSetting | KeyListiner.ModifierKeys.Alt; break;
				default: keySetting = e.KeyCode; break;
			}

			TextBox source = sender as TextBox;
			source.Text = keysToString(modifierSetting, keySetting);
		}
		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyValue)
			{
				case 16: modifierSetting = modifierSetting & ~KeyListiner.ModifierKeys.Shift; break;
				case 17: modifierSetting = modifierSetting & ~KeyListiner.ModifierKeys.Control; break;
				case 18: modifierSetting = modifierSetting & ~KeyListiner.ModifierKeys.Alt; break;
				default: if (keySetting == e.KeyCode) keySetting = Keys.None; break;
			}

			TextBox source = sender as TextBox;
			source.Text = keysToString(modifierSetting, keySetting);
		}
		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
		private void setHotkey_Click(object sender, EventArgs e)
		{
			settings.hotkey = keySetting;
			settings.modifierKey = modifierSetting;
			hotkeyBox.Text = keysToString(modifierSetting, keySetting);
			SwitchVisibility();
			this.Focus();
			locker.Resume();
			Thread.Sleep(500);
			if (settings.modifierKey != 0 || settings.hotkey != Keys.None)
				hook.RegisterHotKey(settings.modifierKey, settings.hotkey);
		}
		private void changeHotkey_Click(object sender, EventArgs e)
		{
			locker.Suspend();
			SwitchVisibility();
			hotkeyBox.Focus();
			hook.UnregisterHotKeys();
			settings.hotkey = Keys.None;
			settings.modifierKey = 0;
			hotkeyBox.Text = string.Empty;
		}
		private void SwitchVisibility()
		{
			changeHotkey.Visible = !changeHotkey.Visible;
			setHotkey.Visible = !setHotkey.Visible;
			hotkeyBox.Enabled = !hotkeyBox.Enabled;
		}
		#endregion

		#region settings control
		private void lockTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			settings.lockType = (LockType)((ComboBox)sender).SelectedItem;
			SetProcessListEnable();
		}
		private void processBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			settings.processLockName = (string)processBox.SelectedItem;
		}
		private void RefreshProcList_Click(object sender, EventArgs e)
		{
			RefreshProcessList();
		}
		private void cbTray_CheckedChanged(object sender, EventArgs e)
		{
			settings.minimizeToTray = cbTray.Checked;
		}
		private void cbTrayBalloons_CheckedChanged(object sender, EventArgs e)
		{
			settings.showBalloons = cbTrayBalloons.Checked;
		}
		#endregion

		#region methods
		private void RefreshProcessList()
		{
			processBox.Items.Clear();
			ProcessHelper.GetAllWindows().ForEach(p => processBox.Items.Add(p.MainModule.ModuleName));
			processBox.SelectedItem = settings.processLockName;
		}
		private string keysToString(ModifierKeys mod, Keys keys)
		{
			string result = string.Empty;
			result = mod != 0 ? mod.ToString().Replace(", ", "+") : string.Empty;
			result += mod != 0 && keys != Keys.None ? "+" : string.Empty;
			result += keys != Keys.None ? keys.ToString() : string.Empty;

			return result;
		}
		private void SetProcessListEnable()
		{
			processBox.Enabled = settings.lockType == LockType.Process;
			RefreshProcList.Enabled = settings.lockType == LockType.Process;
			RefreshProcessList();
		}
		#endregion

		#region locker event handlers

		delegate void checkState(bool state);
		delegate void paramlessDelegate();
		private void locker_CycleDone(object sender, bool state)
		{
			this.Invoke(new checkState(CheckState), state);
		}

		void locker_Activated()
		{
			this.Invoke(new paramlessDelegate(Activated));
		}

		void locker_Deactivated()
		{
			this.Invoke(new paramlessDelegate(Deactivated));
		}

		private void CheckState(bool state)
		{
			this.Enabled = !state;
		}

		private void Activated()
		{
			notifyIcon1.Text = "PointerTrap (Active)";
			if (notifyIcon1.Visible && cbTrayBalloons.Checked)
			{
				notifyIcon1.BalloonTipTitle = "PointerTrap";
				notifyIcon1.BalloonTipText = "Lock Activated";
				notifyIcon1.ShowBalloonTip(3000);
			}
		}

		private void Deactivated()
		{
			notifyIcon1.Text = "PointerTrap";
			if (notifyIcon1.Visible && cbTrayBalloons.Checked)
			{
				notifyIcon1.BalloonTipTitle = "PointerTrap";
				notifyIcon1.BalloonTipText = "Lock Deactivated";
				notifyIcon1.ShowBalloonTip(3000);
			}
		}
		#endregion

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			settings.boarderFix = checkBox1.Checked;
		}
	}
}
