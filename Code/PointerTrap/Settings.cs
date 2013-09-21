using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using KeyListiner;

namespace PointerTrap
{
	public enum LockType
	{
		Monitor = 1,
		Process = 2
	}

	[Serializable()]
	public class Settings
	{
		[NonSerialized]
		private readonly string settingsPath = AppDomain.CurrentDomain.BaseDirectory + "/PointerTrap.set";

		public Keys hotkey;
		public ModifierKeys modifierKey;
		public LockType lockType = LockType.Monitor;
		public string processLockName;
		public bool hardLock = false;
		public bool minimizeToTray;
		public bool showBalloons;
		public Point WindowLocation;
		public int warpCycle = 0;
		public bool boarderFix;

		public Settings(){}

		public void Save()
		{
			Stream stream = File.Open(settingsPath, FileMode.OpenOrCreate, FileAccess.Write);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, this);
			stream.Close();
		}

		public void Load()
		{
			Stream stream = File.Open(settingsPath, FileMode.OpenOrCreate, FileAccess.Read);
			BinaryFormatter formatter = new BinaryFormatter();
			Settings set = (Settings)formatter.Deserialize(stream);
			stream.Close();
			Copy(set);
		}

		public void Copy(Settings set)
		{
			this.hotkey = set.hotkey;
			this.lockType = set.lockType;
			this.processLockName = set.processLockName;
			this.hardLock = set.hardLock;
			this.minimizeToTray = set.minimizeToTray;
			this.showBalloons = set.showBalloons;
			this.WindowLocation = set.WindowLocation;
			this.warpCycle = set.warpCycle;
			this.modifierKey = set.modifierKey;
			this.boarderFix = set.boarderFix;
		}
	}
}
