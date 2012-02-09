// SQL Grenadine is REally Neither A Database Interface Nor an Editor
//  
//  DialogError.cs is part of SQLGrenadine 
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
using Gdk;

namespace SQLGrenadineGui
{
	public enum ErrorState 
	{
		Info,
		Warning,
		Error
	}
	
	public partial class ErrorDialog : Gtk.Dialog
	{
		private ErrorState ErrorState
		{
			set 
			{
				switch(value)
				{
					case ErrorState.Info:
						imageErrorState.Pixbuf=null;
						break;
				}
			}
		}
		
		private ErrorDialog ()
		{
			this.Build ();
		}
		
		public static ErrorDialog Create(string title, string message, ErrorState errorState)
		{
			var dialog=new ErrorDialog() {
				ErrorState=errorState
			};
			
			dialog.labelErrorTitle.Text=title;
			dialog.labelErrorTitle.Show();
			dialog.labelErrorMessage.Text=message;
			dialog.labelErrorMessage.Show();
			return dialog;
		}
	}
}

