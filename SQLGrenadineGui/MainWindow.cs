using System;
using Gtk;
using SQLGrenadine.Database.Core;
using System.Collections.Generic;
using System.Linq;

namespace SQLGrenadineGui
{
	public partial class MainWindow: Gtk.Window
	{
		private int _tabCount;
		private TreeStore _treestoreDatabases;
		private List<DatabaseBase> _connectionMapping;
		
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{			
			// Initialize connection mapping
			_connectionMapping = new List<DatabaseBase>();
			_connectionMapping.Add(null);

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
			
			// Create new query widget to append to notebook, add currently
			// selected database and get current pages number
			var queryWidget = new QueryWidget();
			queryWidget.Database = GetSelectedDatabase();
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
			// Get notebook widget
			var notebook=label.Parent as Notebook;
			if(notebook==null)
				return;
			
			// Get page to close
			for(int i=0; i<notebook.NPages; i++)
			{
				var page = notebook.GetNthPage(i);
				var curLabel = notebook.GetTabLabel(page);
				
				// Remove page
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
			// Add new connection to database treeview
			TreeIter iterDatabase = _treestoreDatabases.AppendValues(database.Name);
			TreeIter iterTables = _treestoreDatabases.AppendValues(iterDatabase, "Tables");
			var tables = database.GetTables();
			foreach(var table in tables)
				_treestoreDatabases.AppendValues(iterTables, table.Name);

			treeviewDatabase.Show();
			var page = notebookContent.CurrentPageWidget as QueryWidget;
			if(page==null)
				return;
			
			// Add new connection to connection list
			comboboxConnections.AppendText(String.Format("{0}@{1} ({2})",database.ConnectionData.Host, 
				database.Name, database.Vendor));
			//var count = comboboxConnections.Model.IterNChildren();
			_connectionMapping.Add(database);
			
		}
		
		private void ExecuteCommand()
		{
			var page = notebookContent.CurrentPageWidget as QueryWidget;
			if (page==null)
				return;
			page.ExecuteCommand();
		}
			
		private DatabaseBase GetSelectedDatabase()
		{
			// Get the currently selected combobox index and select the
			// database object from the list
			var index = comboboxConnections.Active;
			if(index<0)
				return null;
			if(_connectionMapping.Count>index)
				return _connectionMapping[index];
			return null;
		}

		protected void OnExecuteQuery (object sender, System.EventArgs e)
		{
			ExecuteCommand();
		}


		protected void OnKeyPressed (object o, Gtk.KeyPressEventArgs args)
		{
			if(args.Event.Key.Equals(Gdk.Key.F5))
				ExecuteCommand();				
		}

		protected void OnConnectionsChanged (object sender, System.EventArgs e)
		{
			// Get the currently selected page
			var page = notebookContent.CurrentPageWidget as QueryWidget;
			var database = GetSelectedDatabase();
			if (page!=null)
				page.Database=database;
			
			// Use code highlighting of the new connection
			page.HighlightCode();
		}

		protected void OnSwitchPage (object o, Gtk.SwitchPageArgs args)
		{
			// Get the currently selected page
			var page = notebookContent.CurrentPageWidget as QueryWidget;
			if(page==null)
				return;
			
			// Get the database of the selected database and select the related
			// item in the connection combobox
			var index = _connectionMapping.IndexOf(page.Database);
			comboboxConnections.Active=index;
		}
		#endregion		
	}
}