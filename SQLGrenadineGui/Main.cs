using System;
using Gtk;
using SQLGrenadine.Plugins.DatabaseSystems.MySql;
using SQLGrenadine.Core;

namespace SQLGrenadineGui
{
	/// SQL Grenadine is REally Neither A Database Interface Nor an Editor 
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();		
		}
	}
}
