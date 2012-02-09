using System;
using Gtk;
using SQLGrenadine.Database.Core;

namespace SQLGrenadineGui
{
	public partial class MainWindow: Gtk.Window
	{
		private int _tabCount;
		private TreeStore _treestoreDatabases;
		
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			AddTabPage();
			
			// Initialize database connections and elements treeview
			var elementColumn = new TreeViewColumn();
			elementColumn.Title="Database Connections";
			var elementCell = new CellRendererText();
			elementColumn.PackStart(elementCell, true);
			treeviewDatabase.AppendColumn(elementColumn);
			elementColumn.AddAttribute(elementCell, "text", 0);
			
			// Setup tree model
			_treestoreDatabases=new TreeStore(typeof(string));
			treeviewDatabase.Model=_treestoreDatabases;
		}
		
		protected void AddTabPage()
		{
			_tabCount++;
			
			// Create new query widget to append to notebook 
			// and get current pages number
			var queryWidget = new QueryWidget();
			var index = notebookContent.NPages;
			var label = new NotebookCloseButtonLabel(String.Format("New Query {0}", _tabCount));
			
			// Add new widget to notebook and select it
			notebookContent.AppendPage(queryWidget, label);
			queryWidget.Show();
			notebookContent.CurrentPage=index;
			
			// Add close event to close button of the label
			label.OnClosePage=OnClosePage;
		}
		
		#region Events	
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnNewQuery (object sender, System.EventArgs e)
		{
			AddTabPage();
		}

		protected void OnQuit (object sender, System.EventArgs e)
		{
			Application.Quit();
		}
		
		protected void OnClosePage (NotebookCloseButtonLabel label)
		{
			var notebook=label.Parent as Notebook;
			if(notebook==null)
				return;
			
			for(int i=0; i<notebook.NPages; i++)
			{
				var page = notebook.GetNthPage(i);
				var curLabel = notebook.GetTabLabel(page);
				
				if(curLabel==label)
				{
					notebookContent.RemovePage(i);
					break;
				}				
			}
		}

		protected void OnClose (object sender, System.EventArgs e)
		{
			notebookContent.RemovePage(notebookContent.CurrentPage);
		}


		protected void OnConnect (object sender, System.EventArgs e)
		{
			var window=ConnectionDialog.Create();
			window.OnConnected=OnConnected;
			window.Show();
		}
		
		protected void OnConnected(DatabaseBase database)
		{
			TreeIter iterDatabase = _treestoreDatabases.AppendValues(database.Name);
			TreeIter iterTables = _treestoreDatabases.AppendValues(iterDatabase, "Tables");
			var tables = database.GetTables();
			foreach(var table in tables)
			{
				_treestoreDatabases.AppendValues(iterTables, table.Name);
			}
			treeviewDatabase.Show();
		}
		#endregion		
	}
}