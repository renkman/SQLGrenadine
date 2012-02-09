// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  DatabaseType.cs is part of SQLGrenadine 
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

namespace SQLGrenadine.Core
{
	public class DatabaseType
	{
		public string Name {get; private set;}
		public string Namespace {get; private set;}
		public Type Database {get; private set;}
		public Type ConnectionData  {get; private set;}
		public Type WidgetConnection {get; private set;}

		private DatabaseType ()
		{
			
		}
		
		public static DatabaseType Create(string name, string namespaceName, Type database, 
			Type connectionData, Type widgetConnection)
		{
			return new DatabaseType() {
				Name=name,
				Database=database,
				ConnectionData=connectionData,
				WidgetConnection=widgetConnection,
				Namespace=namespaceName
			};			
		}
	}
}

