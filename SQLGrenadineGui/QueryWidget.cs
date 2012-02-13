// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  QueryWidget.cs is part of SQLGrenadine 
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
using Gtk;
using GLib;
using System.Data;

namespace SQLGrenadineGui
{
	public delegate void ExecuteCommand();
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class QueryWidget : Gtk.Bin
	{
		public DatabaseBase Database { get; set; }
		public ExecuteCommand OnExecuteCommand;
		
		public QueryWidget ()
		{	
			this.Build ();
		}
		
		/// <summary>
		/// Executes the currently selected SQL command.
		/// </summary>
		public void ExecuteCommand()
		{
			if(Database==null)
				return;
			
			// Fire the command
			var commandText = textviewEditor.Buffer.Text;
			var message="";
			var result = Database.ExecuteCommand(commandText, ref message);
			
			// Setup result message
			textviewEditorMessage.Buffer.Text = message;
			
			if(result==null)
				return;
			
			// Add result table to result treeview
			var types = new Type[result.Columns.Count];
			var index=0;
			foreach(var column in result.Columns)
			{
				var resultColumn = new TreeViewColumn();
				resultColumn.Title=column.ToString();
				var resultCell = new CellRendererText();
				resultColumn.PackStart(resultCell, true);
				treeviewResult.AppendColumn(resultColumn);
				resultColumn.AddAttribute(resultCell, "text", index);
				types[index++]=typeof(string);
			}
			
			// Setup treeview list model
			var listStore=new ListStore(types);
			treeviewResult.Model=listStore;
			
			
			// Add result data
			foreach(DataRow row in result.Rows)
			{
				var resultRow = new string[result.Columns.Count];
				for(var i=0;i<result.Columns.Count;i++)
					resultRow[i]=row[i].ToString();

				listStore.AppendValues(resultRow);
			}
			treeviewResult.Show();
		}
	}
}

