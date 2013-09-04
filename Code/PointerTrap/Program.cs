using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace PointerTrap
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			WindowsIdentity user = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(user);
			if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
			{
				MessageBox.Show("Please run this with admin rights.", "Admin rights error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
