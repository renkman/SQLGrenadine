// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  NotebookCloseButtonLabel.cs is part of SQLGrenadine 
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
using Gtk;

namespace SQLGrenadineGui
{
	public delegate void ClosePage(NotebookCloseButtonLabel label);
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class NotebookCloseButtonLabel : Gtk.Bin
	{
		public ClosePage OnClosePage;
		
		public string Text {
			get
			{
				return labelTab.Text;
			}
			set
			{
				labelTab.Text=value;
			}
		}
		
		public NotebookCloseButtonLabel (string text)
		{
			this.Build ();
			Text=text;
		}
		private void OnClickButtonClose(object sender, EventArgs e)
		{
			OnClosePage(this);
		}
	}
}

