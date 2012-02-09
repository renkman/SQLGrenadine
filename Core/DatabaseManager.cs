// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  DatabaseManager.cs is part of SQLGrenadine 
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
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using SQLGrenadine.Database.Core;
using SQLGrenadine.Database.DatabaseObjects;
using SQLGrenadine.Plugins.DatabaseSystems;

namespace SQLGrenadine.Core
{
	public class DatabaseManager
	{
		private static DatabaseManager _databaseManager;
		
		private List<string> _classes;
		
		private Dictionary<string, DatabaseType> _databaseTypes;
		
		private DatabaseManager ()
		{
			_classes=new List<string>() {
				"ConnectionData",
				"Database",
				"WidgetConnection"
			};
			_databaseTypes = new Dictionary<string, DatabaseType>();
		}
		
		public void LoadDatabaseSystems()
		{
			// Load plugin assembly
			var assembly = Assembly.LoadFrom("Plugins.dll");
			
			// Get types of the database plugins (filter for _classes list)
			// and group by namespace
			var typeList = new List<Type>(assembly.GetTypes());
			var types = typeList.Where(x=>_classes.Contains(x.Name))
				.GroupBy(x=>x.Namespace);
			
			// Create DatabaseTypes
			foreach(var grouping in types)
			{
				// Get assembly namespace
				var key = grouping.Key;
				var parts = key.Split(new char[]{'.'});
				var name = parts[parts.Length-1];
				
				var database=grouping.Where(x=>x.Name=="Database").Single();
				var connectionData=grouping.Where(x=>x.Name=="ConnectionData").Single();
				var widget=grouping.Where(x=>x.Name=="WidgetConnection").Single();
				
				var databaseType = DatabaseType.Create(name, key, database, connectionData, widget);
				_databaseTypes[key]=databaseType;
			}
		}
		
		public DatabaseType GetDatabaseType(string key)
		{
			if(_databaseTypes.ContainsKey(key))
				return _databaseTypes[key];
			return null;
		}
		
		public List<DatabaseType> GetDatabaseTypes()
		{
			return _databaseTypes.Values.ToList();
		}
		
		/// <summary>
		/// Create this instance.
		/// </summary>
		public static DatabaseManager GetInstance()
		{
			if(_databaseManager!=null)
				return _databaseManager;
			
			// Singleton creation
			_databaseManager=new DatabaseManager();
			_databaseManager.LoadDatabaseSystems();
			return _databaseManager;
		}
	}
}

