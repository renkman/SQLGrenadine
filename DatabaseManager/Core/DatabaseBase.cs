// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  DatabaseBase.cs is part of SQLGrenadine 
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
using System.Data;
using System.Collections.Generic;
using SQLGrenadine.Database.DatabaseObjects;


namespace SQLGrenadine.Database.Core
{
	public abstract class DatabaseBase
	{				
		private static Dictionary<int, DatabaseBase> _databaseStore;
	
		/// <summary>
		/// Gets or sets the connection.
		/// </summary>
		/// <value>
		/// The database connection of the database object instance.
		/// </value>
		public IDbConnection Connection {get; protected set;}
		
		public ConnectionDataBase ConnectionData {get; protected set;}
		
		public int Id {get; protected set;}
		public string Key {get; protected set;}
		public string Name {get; protected set;}
		public string Vendor {get; protected set;}
		public string Version {get; protected set;}
		
		public bool Connected {get; protected set;}
		public string Message {get; protected set;}
		public string LastError {get; protected set;}
		
		public static void AddDatabase(DatabaseBase database)
		{
			_databaseStore = _databaseStore ?? new Dictionary<int, DatabaseBase>();
			if(_databaseStore.ContainsValue(database))
				return;
			var id = _databaseStore.Count+1;
			database.Id=id;
			_databaseStore.Add(id,database);
		}
		
		public static DatabaseBase GetDatabase(int id)
		{
			if(_databaseStore.ContainsKey(id))
				return _databaseStore[id];
			return null;
		}
		
		public static DatabaseBase GetLastAddedDatabase()
		{
			var count=_databaseStore.Count;
			if(count>0)
				return _databaseStore[count-1];
			return null;
		}
		
		public abstract List<Table> GetTables();
	}
}

