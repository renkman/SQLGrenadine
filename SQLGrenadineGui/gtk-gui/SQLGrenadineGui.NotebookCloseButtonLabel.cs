
// This file has been generated by the GUI designer. Do not modify.
namespace SQLGrenadineGui
{
	public partial class NotebookCloseButtonLabel
	{
		private global::Gtk.HBox hboxLabel;
		private global::Gtk.Label labelTab;
		private global::Gtk.Button buttonClose;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget SQLGrenadineGui.NotebookCloseButtonLabel
			global::Stetic.BinContainer.Attach (this);
			this.Name = "SQLGrenadineGui.NotebookCloseButtonLabel";
			// Container child SQLGrenadineGui.NotebookCloseButtonLabel.Gtk.Container+ContainerChild
			this.hboxLabel = new global::Gtk.HBox ();
			this.hboxLabel.Name = "hboxLabel";
			this.hboxLabel.Spacing = 6;
			// Container child hboxLabel.Gtk.Box+BoxChild
			this.labelTab = new global::Gtk.Label ();
			this.labelTab.Name = "labelTab";
			this.labelTab.LabelProp = global::Mono.Unix.Catalog.GetString ("label");
			this.hboxLabel.Add (this.labelTab);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hboxLabel [this.labelTab]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hboxLabel.Gtk.Box+BoxChild
			this.buttonClose = new global::Gtk.Button ();
			this.buttonClose.CanFocus = true;
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.UseUnderline = true;
			this.buttonClose.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child buttonClose.Gtk.Container+ContainerChild
			global::Gtk.Alignment w2 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w3 = new global::Gtk.HBox ();
			w3.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w4 = new global::Gtk.Image ();
			w4.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-dialog-error", global::Gtk.IconSize.Menu);
			w3.Add (w4);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w6 = new global::Gtk.Label ();
			w3.Add (w6);
			w2.Add (w3);
			this.buttonClose.Add (w2);
			this.hboxLabel.Add (this.buttonClose);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hboxLabel [this.buttonClose]));
			w10.PackType = ((global::Gtk.PackType)(1));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			this.Add (this.hboxLabel);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonClose.Clicked += new global::System.EventHandler (this.OnClickButtonClose);
		}
	}
}
