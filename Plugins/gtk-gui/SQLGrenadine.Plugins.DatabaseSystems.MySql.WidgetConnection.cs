
// This file has been generated by the GUI designer. Do not modify.
namespace SQLGrenadine.Plugins.DatabaseSystems.MySql
{
	public partial class WidgetConnection
	{
		private global::Gtk.Fixed fixedConnectionData;
		private global::Gtk.Label labelHost;
		private global::Gtk.Label labelDatabase;
		private global::Gtk.Label labelUser;
		private global::Gtk.Label labelPassword;
		private global::Gtk.Label labelPooling;
		private global::Gtk.CheckButton checkbuttonPooling;
		private global::Gtk.ComboBoxEntry comboboxentryHost;
		private global::Gtk.ComboBoxEntry comboboxentryDatabase;
		private global::Gtk.Entry entryPassword;
		private global::Gtk.ComboBoxEntry comboboxentryUser;
		private global::Gtk.Label labelInfo;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget SQLGrenadine.Plugins.DatabaseSystems.MySql.WidgetConnection
			global::Stetic.BinContainer.Attach (this);
			this.Name = "SQLGrenadine.Plugins.DatabaseSystems.MySql.WidgetConnection";
			// Container child SQLGrenadine.Plugins.DatabaseSystems.MySql.WidgetConnection.Gtk.Container+ContainerChild
			this.fixedConnectionData = new global::Gtk.Fixed ();
			this.fixedConnectionData.Name = "fixedConnectionData";
			this.fixedConnectionData.HasWindow = false;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.labelHost = new global::Gtk.Label ();
			this.labelHost.Name = "labelHost";
			this.labelHost.LabelProp = global::Mono.Unix.Catalog.GetString ("Host:");
			this.fixedConnectionData.Add (this.labelHost);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.labelHost]));
			w1.X = 15;
			w1.Y = 40;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.labelDatabase = new global::Gtk.Label ();
			this.labelDatabase.Name = "labelDatabase";
			this.labelDatabase.LabelProp = global::Mono.Unix.Catalog.GetString ("Database:");
			this.fixedConnectionData.Add (this.labelDatabase);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.labelDatabase]));
			w2.X = 15;
			w2.Y = 70;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.labelUser = new global::Gtk.Label ();
			this.labelUser.Name = "labelUser";
			this.labelUser.LabelProp = global::Mono.Unix.Catalog.GetString ("User:");
			this.fixedConnectionData.Add (this.labelUser);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.labelUser]));
			w3.X = 15;
			w3.Y = 100;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.labelPassword = new global::Gtk.Label ();
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.LabelProp = global::Mono.Unix.Catalog.GetString ("Password:");
			this.fixedConnectionData.Add (this.labelPassword);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.labelPassword]));
			w4.X = 15;
			w4.Y = 130;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.labelPooling = new global::Gtk.Label ();
			this.labelPooling.Name = "labelPooling";
			this.labelPooling.LabelProp = global::Mono.Unix.Catalog.GetString ("Enable pooling:");
			this.fixedConnectionData.Add (this.labelPooling);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.labelPooling]));
			w5.X = 15;
			w5.Y = 160;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.checkbuttonPooling = new global::Gtk.CheckButton ();
			this.checkbuttonPooling.CanFocus = true;
			this.checkbuttonPooling.Name = "checkbuttonPooling";
			this.checkbuttonPooling.Label = "";
			this.checkbuttonPooling.Active = true;
			this.checkbuttonPooling.DrawIndicator = true;
			this.checkbuttonPooling.UseUnderline = true;
			this.fixedConnectionData.Add (this.checkbuttonPooling);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.checkbuttonPooling]));
			w6.X = 130;
			w6.Y = 154;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.comboboxentryHost = global::Gtk.ComboBoxEntry.NewText ();
			this.comboboxentryHost.Name = "comboboxentryHost";
			this.fixedConnectionData.Add (this.comboboxentryHost);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.comboboxentryHost]));
			w7.X = 130;
			w7.Y = 34;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.comboboxentryDatabase = global::Gtk.ComboBoxEntry.NewText ();
			this.comboboxentryDatabase.Name = "comboboxentryDatabase";
			this.fixedConnectionData.Add (this.comboboxentryDatabase);
			global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.comboboxentryDatabase]));
			w8.X = 130;
			w8.Y = 64;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.entryPassword = new global::Gtk.Entry ();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.Visibility = false;
			this.entryPassword.InvisibleChar = '•';
			this.fixedConnectionData.Add (this.entryPassword);
			global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.entryPassword]));
			w9.X = 130;
			w9.Y = 124;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.comboboxentryUser = global::Gtk.ComboBoxEntry.NewText ();
			this.comboboxentryUser.Name = "comboboxentryUser";
			this.fixedConnectionData.Add (this.comboboxentryUser);
			global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.comboboxentryUser]));
			w10.X = 130;
			w10.Y = 94;
			// Container child fixedConnectionData.Gtk.Fixed+FixedChild
			this.labelInfo = new global::Gtk.Label ();
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("<i>Enter your MySql connection data.</i>");
			this.labelInfo.UseMarkup = true;
			this.fixedConnectionData.Add (this.labelInfo);
			global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixedConnectionData [this.labelInfo]));
			w11.X = 15;
			w11.Y = 10;
			this.Add (this.fixedConnectionData);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
