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
using Pango;
using System.Text.RegularExpressions;

namespace SQLGrenadineGui
{
	public delegate void ExecuteCommand();
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class QueryWidget : Gtk.Bin
	{
		private TextTag _tagKeyword;
				
		public DatabaseBase Database { get; set; }
		public ExecuteCommand OnExecuteCommand;
		
		public QueryWidget ()
		{	
			this.Build ();
			textviewEditor.ModifyFont(FontDescription.FromString("Monospace"));
			
			// Setup keyword format
			_tagKeyword = new TextTag("keyword");
			_tagKeyword.Weight = Weight.Bold;
			_tagKeyword.Foreground = "#0000ff";
			textviewEditor.Buffer.TagTable.Add(_tagKeyword);
		}
		
		/// <summary>
		/// Executes the currently selected SQL command.
		/// </summary>
		public void ExecuteCommand()
		{
			if(Database==null)
				return;
			
			var buffer = textviewEditor.Buffer;
			
			// Check if a part of the text is selected and use this selected 
			// area as command, take the whole text otherwise.
			string commandText;
			if(buffer.HasSelection)
			{
				// Get selection marks
				var selectionStart = buffer.SelectionBound;
				var selectionEnd = buffer.InsertMark;
				// Get text iterators
				var iterStart = buffer.GetIterAtMark(selectionStart);
				var iterEnd = buffer.GetIterAtMark(selectionEnd);
				
				// Get the selected text
				commandText = iterStart.GetText(iterEnd);
			}
			else
				commandText = textviewEditor.Buffer.Text;
			
			
			// Fire the command
			var message="";
			var result = Database.ExecuteCommand(commandText, ref message);
			
			// Setup result message
			textviewMessage.Buffer.Text = message;
			
			if(result==null)
				return;
			
			// Add result table to result treeview
			var types = new Type[result.Columns.Count];
			var index=0;
			foreach(DataColumn column in result.Columns)
			{
				var resultColumn = new TreeViewColumn();
				resultColumn.Title=column.Caption;
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
		
		public void HighlightCode()
		{
			var buffer = textviewEditor.Buffer;
			var text = buffer.Text;
			
			// Reset keyword tag
			TextIter start = buffer.GetIterAtOffset(0);
			TextIter end = buffer.GetIterAtOffset(text.Length);		
			buffer.RemoveTag(_tagKeyword, start, end);
			
			if(Database == null || Database.Keywords == null)
				return;
			
			foreach(var keyword in Database.Keywords)
			{
				if(!text.ToUpper().Contains(keyword))
					continue;
				
				// Use keyword tag at text buffer
				var regex = new Regex(String.Format(@"(^|\s+)({0}(\s+|$))+", keyword), RegexOptions.IgnoreCase);
				var matches = regex.Matches(text);
				foreach(Match match in matches)
				{
					start = buffer.GetIterAtOffset(match.Index);
					end = buffer.GetIterAtOffset(match.Index + match.Length);
					buffer.ApplyTag(_tagKeyword, start, end);
				}
			}			
		}

		protected void OnKeyReleased (object o, Gtk.KeyReleaseEventArgs args)
		{
			HighlightCode();
		}
	}
}