// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  IConnectionData.cs is part of SQLGrenadine 
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

namespace SQLGrenadine.Database.Core
{
	public interface IConnectionData
	{
		/// <summary>
		/// Gets or sets the connection string template.
		/// </summary>
		/// <value>
		/// Connection string template in String.Format pattern style.
		/// </value>
		string ConnectionTemplate {get; set;}
		
		/// <summary>
		/// Gets or sets the database name.
		/// </summary>
		/// <value>
		/// The name of the database to connect.
		/// </value>
		string DatabaseName {get; set;}
		
		/// <summary>
		/// Gets or sets the host.
		/// </summary>
		/// <value>
		/// The database host name.
		/// </value>
		string Host {get; set;}
		
		/// <summary>
		/// Gets or sets the database user.
		/// </summary>
		/// <value>
		/// The database user to connect.
		/// </value>
		string User {get; set;}
		
		/// <summary>
		/// Generates and gets the connection string with the passed password.
		/// </summary>
		/// <returns>
		/// The database connection string.
		/// </returns>
		/// <param name='password'>
		/// The password for the set user for the database connection.
		/// </param>
		string GetConnectionString(string password);
	}
}

