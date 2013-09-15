using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;

namespace PointerTrap
{
	public class PointerLocker
	{
		#region fields
		public bool active = false;
		private bool previousState;
		private bool suspended = false;

		private Screen lockedScreen;
		private Settings settings;
		private Rectangle wholeArea;
		private Point middlePointOfLock;
		#endregion

		#region delegates
		public delegate void paramlessDelegate();
		public delegate void activityChange(bool status);
		public delegate void cycleDone(object sender, bool state);
		#endregion

		#region events
		public event cycleDone CycleDone;
		public event activityChange ActivityChanged;
		public event paramlessDelegate Activated;
		public event paramlessDelegate Deactivated;
		#endregion

		#region constructors
		public PointerLocker(ref Settings settings)
		{
			this.settings = settings;
			wholeArea = new Rectangle();
			foreach (Screen screen in Screen.AllScreens)
			{
				wholeArea = Rectangle.Union(wholeArea, screen.Bounds);
			}
		}
		#endregion

		#region threading methods
		internal void Run()
		{
			while (true)
			{
				if (previousState != active)
				{
					previousState = active;
					if (ActivityChanged != null)
					{
						ActivityChanged.Invoke(active);
					}

					if (active)
					{
						if (Activated != null)
						{
							Activated.Invoke();
						}
					}
					else
					{
						if (Deactivated != null)
						{
							Deactivated.Invoke();
						}
					}
				}

				if (suspended)
					continue;

				if (active)
				{
					this.LockPointer();
				}
				else
				{
					this.UnlockPointer();
				}

				if (CycleDone != null)
				{
					CycleDone.Invoke(this, active);
				}
			}
		}

		internal void Suspend()
		{
			suspended = true;
		}

		internal void Resume()
		{
			suspended = false;
		}
		#endregion

		#region methods
		public void LockPointer()
		{
			try
			{
				if (!IsMouseTrapped())
				{
					if (settings.lockType == LockType.Monitor)
					{
						lockedScreen = Screen.AllScreens.First(s => s.Bounds.Contains(Cursor.Position));
						Cursor.Clip = lockedScreen.Bounds;
					}
					else if (settings.lockType == LockType.Process)
					{
						Process proc = ProcessHelper.GetProcessByMainModuleName(settings.processLockName);
						RECT rct = new RECT();
						GetWindowRect(proc.MainWindowHandle, ref rct);
						Rectangle rectangle = new Rectangle(new Point(rct.Left, rct.Top), new Size(rct.Right - rct.Left, rct.Bottom - rct.Top));
						Cursor.Clip = rectangle;
					}

					middlePointOfLock = new Point(Cursor.Clip.Left + Cursor.Clip.Width / 2, Cursor.Clip.Top + Cursor.Clip.Height / 2);
				}
			}
			catch
			{
				active = false;
			}
		}

		public void UnlockPointer()
		{
			Cursor.Clip = wholeArea;
		}

		private bool IsMouseTrapped()
		{
			return Cursor.Clip != wholeArea;
		}
		#endregion

		#region user32.dll import
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}
		#endregion
	}
}