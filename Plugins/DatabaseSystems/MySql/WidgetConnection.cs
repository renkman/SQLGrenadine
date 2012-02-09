// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  WidgetConnection.cs is part of SQLGrenadine 
//  
//  Author:
//       Jan Renken <lachsfilet@fsfe.org>
// 
//  Copyright (c) 2012 Jan Renken
// 
//  SQLGrenadine is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  SQLGrenadine is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using SQLGrenadine.Database.Core;

namespace SQLGrenadine.Plugins.DatabaseSystems.MySql
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetConnection : Gtk.Bin, IWidgetConnection
	{
		public WidgetConnection ()
		{
			this.Build ();
		}
		
		public string Password
		{
			get
			{
				return entryPassword.Text;
			}
		}
		
		public ConnectionDataBase ConnectionData 
		{
			get 
			{
				return SQLGrenadine.Plugins.DatabaseSystems.MySql.ConnectionData.Create(
					comboboxentryHost.ActiveText,
					comboboxentryDatabase.ActiveText,
					comboboxentryUser.ActiveText,
					checkbuttonPooling.Active
				);
			}
		}
	}
}
