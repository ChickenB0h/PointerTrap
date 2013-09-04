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
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.Controls;
using MouseKeyboardActivityMonitor.WinApi;

namespace PointerTrap
{
	public partial class Form1 : Form
	{
		#region fields
		private readonly string settingsPath = AppDomain.CurrentDomain.BaseDirectory + "PointerTrap.set";
		private MouseHookListener mouseHook;
		private KeyboardHookListener keyboardHook;
		private List<Keys> pressed = new List<Keys>();
		private List<Keys> pressedSettings = new List<Keys>();
		private List<Keys> pressedModifiersSettings = new List<Keys>();

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
				hotkeyBox.Text = keysToString(settings.hotkey);
				lockTypeCombo.SelectedItem = settings.lockType;
				hardLock.Checked = settings.hardLock;
				cbTray.Checked = settings.minimizeToTray;
				cbTrayBalloons.Checked = settings.showBalloons;
			}
			catch
			{
				settings.lockType = LockType.Monitor;
			}
			finally
			{
				mouseHook = new MouseHookListener(new GlobalHooker());
				keyboardHook = new KeyboardHookListener(new GlobalHooker());

				keyboardHook.KeyDown += keyboardHook_KeyDown;
				keyboardHook.KeyUp += keyboardHook_KeyUp;
				keyboardHook.Start();
				mouseHook.Start();

				FormClosing += Form1_FormClosing;
				Resize += Form1_Resize;

				SetProcessListEnable();
				locker = new PointerLocker(ref settings);
				locker.CycleDone += locker_CycleDone;
				locker.Activated += locker_Activated;
				locker.Deactivated += locker_Deactivated;
				pointerTrapThread = new Thread(new ThreadStart(locker.Run));
				pointerTrapThread.Start();
			}
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			pointerTrapThread.Abort();
			mouseHook.Stop();
			keyboardHook.Stop();
			settings.WindowLocation = this.Location;
			settings.Save();
		}

		private void keyboardHook_KeyUp(object sender, KeyEventArgs e)
		{
			pressed.Clear();
		}
		private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
		{
			Keys key = e.KeyCode;
			if (IsKeyModifier(e.KeyValue))
			{
				key = ConvertToModifier(e.KeyValue);
			}

			pressed.Add(key);
			if (isHotkeyMatched())
			{
				locker.active = !locker.active;
			}
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
			Keys key = NormilizeKeys(e.KeyCode);
			if (!pressedSettings.Contains(key))
			{
				pressedSettings.Add(key);
				TextBox source = sender as TextBox;
				source.Text = keysToString(pressedSettings);
			}
		}
		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			pressedSettings.Remove(NormilizeKeys(e.KeyCode));
			TextBox source = sender as TextBox;
			source.Text = keysToString(pressedSettings);
		}
		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}
		private void setHotkey_Click(object sender, EventArgs e)
		{
			settings.hotkey = pressedSettings;
			hotkeyBox.Text = keysToString(pressedSettings);
			SwitchVisibility();
			this.Focus();
			locker.Resume();
			pressed.Clear();
		}
		private void changeHotkey_Click(object sender, EventArgs e)
		{
			locker.Suspend();
			SwitchVisibility();
			hotkeyBox.Focus();
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
		private void hideCursorBox_CheckedChanged(object sender, EventArgs e)
		{
			settings.hardLock = hardLock.Checked;
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
		private string keysToString(List<Keys> keys)
		{
			string result = string.Empty;
			foreach (Keys key in keys)
			{
				result += key.ToString() + " + ";
			}

			if (result.Contains("+"))
				result = result.Remove(result.Length - 3);

			return result;
		}
		private void SetProcessListEnable()
		{
			processBox.Enabled = settings.lockType == LockType.Process;
			RefreshProcList.Enabled = settings.lockType == LockType.Process;
			RefreshProcessList();
		}
		private bool isHotkeyMatched()
		{
			return settings.hotkey.All(key => pressed.Contains(key)) && pressed.All(key => settings.hotkey.Contains(key));
		}
		#endregion

		#region key helpers
		private Keys ConvertToModifier(int key)
		{
			switch (key)
			{
				case 160:
				case 161: return Keys.Shift;
				case 162:
				case 163: return Keys.Control;
				case 164:
				case 165: return Keys.Alt;
				default: return Keys.None;
			}
		}
		private bool IsKeyModifier(int key)
		{
			int[] modValues = new int[6] { 160, 161, 162, 163, 164, 165 };
			return modValues.Any(v => v == key);
		}
		private Keys NormilizeKeys(Keys key)
		{
			switch (key)
			{
				case Keys.ShiftKey: return Keys.Shift;
				case Keys.Menu: return Keys.Alt;
				case Keys.ControlKey: return Keys.Control;
				default: return key;
			}
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

	}
}
