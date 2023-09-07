using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Ranker
{
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			Model m = new Model();

			m.Start();

			m.LoadPlayerEvents();
//            m.HackyThing();
			m.RestoreGames();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);


			MainForm mf = new MainForm();
			mf.model = m;


			Application.Run(mf);

			m.End();

		}
		
	}
}
