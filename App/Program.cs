using System.Reflection;

namespace Ranker
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Model m = new Model();

			m.Start();

			m.LoadPlayerEvents();
			m.RestoreGames();

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();

			MainForm mf = new MainForm( m );


			Application.Run(mf);

			m.End();
		}
	}
}