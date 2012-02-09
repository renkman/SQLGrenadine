// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  Database.cs is part of SQLGrenadine 
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
using SQLGrenadine.Database.Core;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SQLGrenadine.Plugins.DatabaseSystems.MySql
{
	public class Database : DatabaseBase
	{
		private Database ()
		{
			
		}

		public static DatabaseBase Create(ConnectionData connectionData, string password)
		{
			var connectionString=connectionData.GetConnectionString(password);
			var database = new Database();
			try
			{
				var connection = new MySqlConnection(connectionString);
				database.Connection=connection;
				
				// Try to connect with the database
				connection.Open();
				
				// If it works, take setup the the Database object
				database.ConnectionData=connectionData;
				database.Connected=true;
				database.Name=connection.Database;
				database.Key=connection.ConnectionString;
				database.Version=connection.ServerVersion;
				database.Vendor="Oracle Corporation";
				DatabaseBase.AddDatabase(database);
			}
			catch(Exception e)
			{
				database.Connected=false;
				database.Message=e.Message;
				database.LastError=String.Format(
					"{0}:{1}{2}{3}{4}Source:{5},{6}",
					e.Message,
					Environment.NewLine,
					e.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					Environment.NewLine,
					e.Source
				);
			}
			return database;
		}
		
		public override System.Collections.Generic.List<SQLGrenadine.Database.DatabaseObjects.Table> GetTables ()
		{
			var tables = new List<SQLGrenadine.Database.DatabaseObjects.Table>();
			var connection = Connection;
			var command = connection.CreateCommand();
			command.CommandText="SHOW TABLES";
			var reader = command.ExecuteReader();
			while(reader.Read())
			{
				var table = Table.Create(reader[0].ToString(),this);
				tables.Add(table);
			}
			reader.Close();
			command.Dispose();
			return tables;
		}
	}
}

