// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  ConnectionDialog.cs is part of SQLGrenadine 
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
using System.Collections.Generic;
using SQLGrenadine.Core;
using SQLGrenadine.Database.Core;
using SQLGrenadine.Plugins;
using Gtk;

namespace SQLGrenadineGui
{
	public delegate void Connected(DatabaseBase database);
	
	public class DatabaseItem
	{
		public string ManagerKey {get; private set;}
		public Widget Widget {get; private set;}
		
		private DatabaseItem() {}
		
		public static DatabaseItem Create(string key, Widget widget)
		{
			return new DatabaseItem()
			{
				ManagerKey=key,
				Widget=widget
			};
		}
	}
	
	public partial class ConnectionDialog : Gtk.Dialog
	{
		private Dictionary<string, DatabaseItem> _items;
		private Widget _currentWidget;
		
		public Connected OnConnected;
		
		private ConnectionDialog ()
		{
			this.Build ();
			LoadDatabaseSystems();
		}
		
		public void LoadDatabaseSystems()
		{
			var dbManager = DatabaseManager.GetInstance();
			_items = new Dictionary<string, DatabaseItem>();
			foreach(var databaseType in dbManager.GetDatabaseTypes())
			{
				var widget = Activator.CreateInstance(databaseType.WidgetConnection) as Widget;
				if(widget == null)
					continue;

				comboboxSelectDatabase.AppendText(databaseType.Name);
				var item = DatabaseItem.Create(databaseType.Namespace, widget);
				_items.Add(databaseType.Name, item);
			}
			
			//Select first dropdown entry
			comboboxSelectDatabase.Active=0;
		}
		
		public static ConnectionDialog Create()
		{
			return new ConnectionDialog();
		}
		
		protected void OnButtonClose (object sender, System.EventArgs e)
		{
			Destroy();
		}

		protected void OnButtonOk (object sender, System.EventArgs e)
		{
			var key=comboboxSelectDatabase.ActiveText;
			if(key == null || !_items.ContainsKey(key))
				return;
			
			var item=_items[key];
			
			if(item.Widget==null || item.ManagerKey==null)
				return;
			
			var currentWidget = _items[key].Widget as IWidgetConnection;
			if(currentWidget == null)
				return;
						
			var databaseManager = DatabaseManager.GetInstance();
			var databaseType = databaseManager.GetDatabaseType(item.ManagerKey);
			var methodCreate = databaseType.Database.GetMethod("Create");
			var connectionData = currentWidget.ConnectionData;
			var password = currentWidget.Password;
			var connection = methodCreate.Invoke(null, new object[] { connectionData, password }) as DatabaseBase;
			if(String.IsNullOrWhiteSpace(connection.LastError))
			{
				if(OnConnected!=null)
					OnConnected(connection);
				Destroy();
				return;
			}
			
			var errorDialog=ErrorDialog.Create("Database connection error", 
				connection.LastError,
				ErrorState.Error);
			errorDialog.Show();
		}

		protected void OnComboBoxChanged (object sender, System.EventArgs e)
		{
			if(_currentWidget!=null)
				_currentWidget.Destroy();
			
			var key=comboboxSelectDatabase.ActiveText;
			if(key == null || !_items.ContainsKey(key))
				return;
			
			var item=_items[key];
			var widget=item.Widget;
			_currentWidget=widget;
			vboxConnection.PackStart(widget);
			widget.Show();
		}
	}
}

