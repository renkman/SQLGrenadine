// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  DatabaseObject.cs is part of SQLGrenadine 
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
using SQLGrenadine.Database.Core;

namespace SQLGrenadine.Database.DatabaseObjects
{
	/// <summary>
	/// Represents a database table.
	/// </summary>
	public abstract class DatabaseObject
	{
		/// <summary>
		/// Gets or sets the name of the database object.
		/// </summary>
		/// <value>
		/// The name of the binded database object.
		/// </value>
		public string Name { get; protected set;}
		
		/// <summary>
		/// Gets or sets the permissions of the current database object.
		/// </summary>
		/// <value>
		/// The permissions of the binded database object.
		/// </value>
		public List<IPermissionSet> Permissions {get; protected set;}
		
		public DatabaseBase Database {get; protected set;}
	}
}

