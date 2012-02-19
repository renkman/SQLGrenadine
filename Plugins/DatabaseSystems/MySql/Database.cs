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
		private HashSet<string> _keywords;
		
		public override HashSet<string> Keywords {
			get {
				if(_keywords != null)
					return _keywords;
				_keywords=new HashSet<string>()	{
					"ADD", 	"ALL", 	"ALTER",
					"ANALYZE", 	"AND", 	"AS",
					"ASC", 	"ASENSITIVE", 	"BEFORE",
					"BETWEEN", 	"BIGINT", 	"BINARY",
					"BLOB", 	"BOTH", 	"BY",
					"CALL", 	"CASCADE", 	"CASE",
					"CHANGE", 	"CHAR", 	"CHARACTER",
					"CHECK", 	"COLLATE", 	"COLUMN",
					"CONDITION", 	"CONSTRAINT", 	"CONTINUE",
					"CONVERT", 	"CREATE", 	"CROSS",
					"CURRENT_DATE", 	"CURRENT_TIME", 	"CURRENT_TIMESTAMP",
					"CURRENT_USER", 	"CURSOR", 	"DATABASE",
					"DATABASES", 	"DAY_HOUR", 	"DAY_MICROSECOND",
					"DAY_MINUTE", 	"DAY_SECOND", 	"DEC",
					"DECIMAL", 	"DECLARE", 	"DEFAULT",
					"DELAYED", 	"DELETE", 	"DESC",
					"DESCRIBE", 	"DETERMINISTIC", 	"DISTINCT",
					"DISTINCTROW", 	"DIV", 	"DOUBLE",
					"DROP", 	"DUAL", 	"EACH",
					"ELSE", 	"ELSEIF", 	"ENCLOSED",
					"ESCAPED", 	"EXISTS", 	"EXIT",
					"EXPLAIN", 	"FALSE", 	"FETCH",
					"FLOAT", 	"FLOAT4", 	"FLOAT8",
					"FOR", 	"FORCE", 	"FOREIGN",
					"FROM", 	"FULLTEXT", 	"GRANT",
					"GROUP", 	"HAVING", 	"HIGH_PRIORITY",
					"HOUR_MICROSECOND", 	"HOUR_MINUTE", 	"HOUR_SECOND",
					"IF", 	"IGNORE", 	"IN",
					"INDEX", 	"INFILE", 	"INNER",
					"INOUT", 	"INSENSITIVE", 	"INSERT",
					"INT", 	"INT1", 	"INT2",
					"INT3", 	"INT4", 	"INT8",
					"INTEGER", 	"INTERVAL", 	"INTO",
					"IS", 	"ITERATE", 	"JOIN",
					"KEY", 	"KEYS", 	"KILL",
					"LEADING", 	"LEAVE", 	"LEFT",
					"LIKE", 	"LIMIT", 	"LINES",
					"LOAD", 	"LOCALTIME", 	"LOCALTIMESTAMP",
					"LOCK", 	"LONG", 	"LONGBLOB",
					"LONGTEXT", 	"LOOP", 	"LOW_PRIORITY",
					"MATCH", 	"MEDIUMBLOB", 	"MEDIUMINT",
					"MEDIUMTEXT", 	"MIDDLEINT", 	"MINUTE_MICROSECOND",
					"MINUTE_SECOND", 	"MOD", 	"MODIFIES",
					"NATURAL", 	"NOT", 	"NO_WRITE_TO_BINLOG",
					"NULL", 	"NUMERIC", 	"ON",
					"OPTIMIZE", 	"OPTION", 	"OPTIONALLY",
					"OR", 	"ORDER", 	"OUT",
					"OUTER", 	"OUTFILE", 	"PRECISION",
					"PRIMARY", 	"PROCEDURE", 	"PURGE",
					"READ", 	"READS", 	"REAL",
					"REFERENCES", 	"REGEXP", 	"RELEASE",
					"RENAME", 	"REPEAT", 	"REPLACE",
					"REQUIRE", 	"RESTRICT", 	"RETURN",
					"REVOKE", 	"RIGHT", 	"RLIKE",
					"SCHEMA", 	"SCHEMAS", 	"SECOND_MICROSECOND",
					"SELECT", 	"SENSITIVE", 	"SEPARATOR",
					"SET", 	"SHOW", 	"SMALLINT",
					"SONAME", 	"SPATIAL", 	"SPECIFIC",
					"SQL", 	"SQLEXCEPTION", 	"SQLSTATE",
					"SQLWARNING", 	"SQL_BIG_RESULT", 	"SQL_CALC_FOUND_ROWS",
					"SQL_SMALL_RESULT", 	"SSL", 	"STARTING",
					"STRAIGHT_JOIN", 	"TABLE", 	"TERMINATED",
					"THEN", 	"TINYBLOB", 	"TINYINT",
					"TINYTEXT", 	"TO", 	"TRAILING",
					"TRIGGER", 	"TRUE", 	"UNDO",
					"UNION", 	"UNIQUE", 	"UNLOCK",
					"UNSIGNED", 	"UPDATE", 	"USAGE",
					"USE", 	"USING", 	"UTC_DATE",
					"UTC_TIME", 	"UTC_TIMESTAMP", 	"VALUES",
					"VARBINARY", 	"VARCHAR", 	"VARCHARACTER",
					"VARYING", 	"WHEN", 	"WHERE",
					"WHILE", 	"WITH", 	"WRITE",
					"XOR", 	"YEAR_MONTH", 	"ZEROFILL",
					"ASENSITIVE", 	"CALL", 	"CONDITION",
					"CONNECTION", 	"CONTINUE", 	"CURSOR",
					"DECLARE", 	"DETERMINISTIC", 	"EACH",
					"ELSEIF", 	"EXIT", 	"FETCH",
					"GOTO", 	"INOUT", 	"INSENSITIVE",
					"ITERATE", 	"LABEL", 	"LEAVE",
					"LOOP", 	"MODIFIES", 	"OUT",
					"READS", 	"RELEASE", 	"REPEAT",
					"RETURN", 	"SCHEMA", 	"SCHEMAS",
					"SENSITIVE", 	"SPECIFIC", 	"SQL",
					"SQLEXCEPTION", 	"SQLSTATE", 	"SQLWARNING",
					"TRIGGER", 	"UNDO", 	"UPGRADE",
					"WHILE"
				};
				return _keywords;
			}
		}
		
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
			reader.Dispose();
			reader=null;
			command.Dispose();
			command=null;
			return tables;
		}
	}
}

