using System;
using System.Collections.Generic;
using System.Text;

namespace pZDock
{
    public class PBoxArray : System.Collections.CollectionBase
    {
        private readonly System.Windows.Forms.Form HostForm;
        
        /* Declare in main form:
         * PBoxArray aPBox;
         * public static PictureBox aPBoxClicked;
         * public static int aPBoxClickedNum;
         * 
         * Execute in Form_Load:
         * aPBox = new PBoxArray(this);
         * 
         * Creating a new label:
         * aPBox.NewPBox();
         */

        public System.Windows.Forms.PictureBox NewPBox()
        {
            // Create a new instance of the pbox class.
            System.Windows.Forms.PictureBox aPBox = new
               System.Windows.Forms.PictureBox();
            // Add the pbox to the collection's internal list.
            this.List.Add(aPBox);
            // Add the pbox to the controls collection of the form 
            // referenced by the HostForm field.
            HostForm.Controls.Add(aPBox);
            // Set intial properties for the pbox object.
            aPBox.Top = Count * 25;
            aPBox.Left = 100;
            aPBox.Tag = this.Count;
            aPBox.Click += new System.EventHandler(ClickHandler);
            return aPBox;
        }

        // The constructor hack
        public PBoxArray(System.Windows.Forms.Form host)
        {
            HostForm = host;
            this.NewPBox();
        }

        // The default property
        public System.Windows.Forms.PictureBox this[int Index]
        {
            get
            {
                return (System.Windows.Forms.PictureBox)this.List[Index];
            }
        }

        // The remove method
        public void Remove()
        {
            // Check to be sure there is a pbox to remove.
            if (this.Count > 0)
            {
                // Remove the last pbox added to the array from the host form 
                // controls collection. Note the use of the indexer in accessing 
                // the array.
                HostForm.Controls.Remove(this[this.Count - 1]);
                this.List.RemoveAt(this.Count - 1);
            }
        }

        // Our goal - the common event handler
        public void ClickHandler(Object sender, System.EventArgs e)
        {
            frmMain.aPBoxClicked = ((System.Windows.Forms.PictureBox)sender);
            frmMain.aPBoxClickedNum = Convert.ToInt32(((System.Windows.Forms.PictureBox)sender).Tag.ToString())-1;
        }
    }
}
