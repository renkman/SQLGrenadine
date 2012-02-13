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
		
		/// <summary>
		/// Gets the tables of the currently connected database.
		/// </summary>
		/// <returns>
		/// A list with table objects.
		/// </returns>
		public abstract List<Table> GetTables();
		
		/// <summary>
		/// Executes the passed command on the database of the current connection.
		/// </summary>
		/// <returns>
		/// The resultset of the successful executed command. If the command failed
		/// or was no query, the return value is null.
		/// </returns>
		/// <param name='Command'>
		/// The SQL command to execute.
		/// </param>
		/// <param name='message'>
		/// The the info or error message, which may occur.
		/// </param>
		public DataTable ExecuteCommand(string commandText, ref string message)
		{
			var result = new DataTable();
			IDbCommand command = null;
			IDataReader reader = null;
			try
			{
				command = Connection.CreateCommand();
				command.CommandText = commandText;
				
				reader = command.ExecuteReader();
				var schema = reader.GetSchemaTable();
				
				// Setup result columns
				foreach(DataRow column in schema.Rows)	
					result.Columns.Add(column[0].ToString());
				
				// Add data rows to the resultset
				while(reader.Read())
				{
					var data = new object[schema.Rows.Count];
					for(var i=0; i<reader.FieldCount;i++)
						data[i]=reader[i];
	
					result.Rows.Add(data);
				}
				message = reader.RecordsAffected.ToString();
			}
			catch(Exception e)
			{
				message = e.Message;
			}
			finally
			{
				if(reader!=null)
				{
					reader.Close();
					reader.Dispose();
					reader = null;
				}
				if(command!=null)
				{
					command.Dispose();
					command = null;
				}
			}
			return result;
		}
	}
}

