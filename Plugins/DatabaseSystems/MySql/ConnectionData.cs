// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  ConnectionData.cs is part of SQLGrenadine 
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
	public class ConnectionData : ConnectionDataBase
	{
		public bool Pooling {get; private set;}
		
		public override string GetConnectionString (string password)
		{
			return String.Format(ConnectionTemplate, Host, DatabaseName, User, password, Pooling);
		}
		
		private ConnectionData ()
		{
			ConnectionTemplate="Server={0};Database={1};User ID={2};Password={3};Pooling={4}";
		}
		
		public static ConnectionData Create(string host, string name, string user, bool pooling=true)
		{
			return new ConnectionData()
			{
				Host=host,
				DatabaseName=name,
				User=user,
				Pooling=pooling
			};
		}
	}
}

