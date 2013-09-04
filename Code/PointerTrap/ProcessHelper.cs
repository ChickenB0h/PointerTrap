using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PointerTrap
{
	public static class ProcessHelper
	{
		public static List<Process> GetAllWindows()
		{
			List<Process> result = new List<Process>();
			foreach (Process proc in Process.GetProcesses())
			{
				if (proc.MainWindowHandle != IntPtr.Zero)
				{
					result.Add(proc);
				}
			}

			return result;
		}

		public static Process GetProcessByMainModuleName(String name)
		{
			return GetAllWindows().FirstOrDefault(p => p.MainModule.ModuleName == name);
		}
	}
}
