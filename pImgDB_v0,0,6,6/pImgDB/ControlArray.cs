using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace pImgDB
{
    public class LabelArray : System.Collections.CollectionBase
    {
        private readonly Form HostForm;
        public ClickInfo Ci = new ClickInfo();

        /* Declare in main form:
         * LabelArray aLabel;
         * 
         * Execute in Form_Load:
         * aLabel = new LabelArray(this);
         * 
         * Creating a new label:
         * aLabel.NewLabel();
         */

        public Label NewLabel()
        {
            // Create a new instance of the Label class.
            Label aLabel = new Label();
            // Add the label to the collection's internal list.
            this.List.Add(aLabel);
            // Add the label to the controls collection of the form 
            // referenced by the HostForm field.
            HostForm.Controls.Add(aLabel);
            // Set intial properties for the label object.
            aLabel.Tag = this.Count;
            aLabel.Text = "Label " + this.Count.ToString();
            aLabel.Click += new System.EventHandler(ClickHandler);
            return aLabel;
        }

        // The constructor hack
        public LabelArray(Form host)
        {
            HostForm = host;
            this.NewLabel();
        }

        // The default property
        public Label this[int Index]
        {
            get
            {
                return (Label)this.List[Index];
            }
        }

        // The remove method
        public void Remove()
        {
            // Check to be sure there is a label to remove.
            if (this.Count > 0)
            {
                // Remove the last label added to the array from the host form 
                // controls collection. Note the use of the indexer in accessing 
                // the array.
                HostForm.Controls.Remove(this[this.Count - 1]);
                this.List.RemoveAt(this.Count - 1);
            }
        }

        // Our goal - the common event handler
        public void ClickHandler(Object sender, System.EventArgs e)
        {
            int iClicked = Convert.ToInt32(((Label)sender).Tag.ToString()) - 1;
            if (Ci.iIndex != iClicked) Ci.iCount = 0;
            Ci.iIndex = iClicked;
            Ci.iCount += 1;
            Ci.iKey = 1;
        }
    }
    public class PBoxArray : System.Collections.CollectionBase
    {
        private readonly Form HostForm;
        public ClickInfo Ci = new ClickInfo();

        /* Declare in main form:
         * PBoxArray aPBox;
         * 
         * Execute in Form_Load:
         * aPBox = new PBoxArray(this);
         * 
         * Creating a new label:
         * aPBox.NewPBox();
         */

        public PictureBox NewPBox()
        {
            // Create a new instance of the pbox class.
            PictureBox aPBox = new PictureBox();
            // Add the pbox to the collection's internal list.
            this.List.Add(aPBox);
            // Add the pbox to the controls collection of the form 
            // referenced by the HostForm field.
            HostForm.Controls.Add(aPBox);
            // Set intial properties for the pbox object.
            aPBox.Tag = this.Count;
            //aPBox.Click += new System.EventHandler(c_SingleClick);
            //aPBox.DoubleClick += new EventHandler(c_DoubleClick);
            aPBox.MouseClick += new MouseEventHandler(c_SingleClick);
            aPBox.MouseDoubleClick += new MouseEventHandler(c_DoubleClick);
            return aPBox;
        }

        // The constructor hack
        public PBoxArray(Form host)
        {
            HostForm = host;
            //this.NewPBox();
        }

        // The default property
        public PictureBox this[int Index]
        {
            get
            {
                return (PictureBox)this.List[Index];
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
        public void c_SingleClick(object sender, MouseEventArgs e)
        {
            int iClicked = Convert.ToInt32(((PictureBox)sender).Tag.ToString()) - 1;
            if (Ci.lLast + 500 < ClickInfo.Tick() ||
                Ci.iIndex != iClicked ||
                Ci.iCount < 0) Ci.iCount = 0;
            Ci.lLast = ClickInfo.Tick();
            Ci.ptLoc = Cursor.Position;
            Ci.ptRLoc = e.Location;
            Ci.iIndex = iClicked;
            if (e.Button == MouseButtons.Left) Ci.iKey = 1;
            if (e.Button == MouseButtons.Right) Ci.iKey = 2;
            if (e.Button == MouseButtons.Middle) Ci.iKey = 3;
            Ci.iCount += 1;
            Ci.bPoll = true;
        }
        void c_DoubleClick(object sender, MouseEventArgs e)
        {
            int iClicked = Convert.ToInt32(((PictureBox)sender).Tag.ToString()) - 1;
            if (Ci.lLast + 500 < ClickInfo.Tick() ||
                Ci.iIndex != iClicked ||
                Ci.iCount > 0) Ci.iCount = 0;
            Ci.lLast = ClickInfo.Tick();
            Ci.ptLoc = Cursor.Position;
            Ci.ptRLoc = e.Location;
            Ci.iIndex = iClicked;
            if (e.Button == MouseButtons.Left) Ci.iKey = 1;
            if (e.Button == MouseButtons.Right) Ci.iKey = 2;
            if (e.Button == MouseButtons.Middle) Ci.iKey = 3;
            Ci.iCount -= 1;
            Ci.bPoll = true;
        }
    }
    public class PanelArray : System.Collections.CollectionBase
    {
        private readonly Form HostForm;
        public ClickInfo Ci = new ClickInfo();

        /* Declare in main form:
         * PanelArray aPanel;
         * 
         * Execute in Form_Load:
         * aPanel = new PanelArray(this);
         * 
         * Creating a new label:
         * aPanel.NewPanel();
         */

        public Panel NewPanel()
        {
            // Create a new instance of the Panel class.
            Panel aPanel = new Panel();
            // Add the Panel to the collection's internal list.
            this.List.Add(aPanel);
            // Add the Panel to the controls collection of the form 
            // referenced by the HostForm field.
            HostForm.Controls.Add(aPanel);
            // Set intial properties for the Panel object.
            aPanel.Tag = this.Count;
            aPanel.Click += new System.EventHandler(ClickHandler);
            return aPanel;
        }

        // The constructor hack
        public PanelArray(Form host)
        {
            HostForm = host;
            //this.NewPanel();
        }

        // The default property
        public Panel this[int Index]
        {
            get
            {
                return (Panel)this.List[Index];
            }
        }

        // The remove method
        public void Remove()
        {
            // Check to be sure there is a Panel to remove.
            if (this.Count > 0)
            {
                // Remove the last Panel added to the array from the host form 
                // controls collection. Note the use of the indexer in accessing 
                // the array.
                HostForm.Controls.Remove(this[this.Count - 1]);
                this.List.RemoveAt(this.Count - 1);
            }
        }

        // Our goal - the common event handler
        public void ClickHandler(Object sender, System.EventArgs e)
        {
            int iClicked = Convert.ToInt32(((Panel)sender).Tag.ToString())-1;
            if (Ci.iIndex != iClicked) Ci.iCount = 0;
            Ci.iIndex = iClicked;
            Ci.iCount += 1;
            Ci.iKey = 1;
        }
    }
    public class TBoxArray : System.Collections.CollectionBase
    {
        private readonly Form HostForm;
        public ClickInfo Ci = new ClickInfo();
        public TxChgInfo TCi = new TxChgInfo();
        public KeyUpInfo KUi = new KeyUpInfo();

        /* Declare in main form:
         * TBoxArray aTBox;
         * 
         * Execute in Form_Load:
         * aTBox = new TBoxArray(this);
         * 
         * Creating a new textbox:
         * aTBox.NewTBox();
         */

        public TextBox NewTBox()
        {
            // Create a new instance of the TextBox class.
            TextBox aTBox = new TextBox();
            // Add the textbox to the collection's internal list.
            this.List.Add(aTBox);
            // Add the textbox to the controls collection of the form 
            // referenced by the HostForm field.
            HostForm.Controls.Add(aTBox);
            // Set intial properties for the textbox object.
            aTBox.Tag = this.Count;
            aTBox.Text = "TextBox " + this.Count.ToString();
            aTBox.KeyUp += new KeyEventHandler(KeyUpHandler);
            aTBox.TextChanged += new EventHandler(TextChangeHandler);
            aTBox.Click += new System.EventHandler(ClickHandler);
            return aTBox;
        }

        // The constructor hack
        public TBoxArray(Form host)
        {
            HostForm = host;
            this.NewTBox();
        }

        // The default property
        public TextBox this[int Index]
        {
            get
            {
                return (TextBox)this.List[Index];
            }
        }

        // The remove method
        public void Remove()
        {
            // Check to be sure there is a textbox to remove.
            if (this.Count > 0)
            {
                // Remove the last textbox added to the array from the host form 
                // controls collection. Note the use of the indexer in accessing 
                // the array.
                HostForm.Controls.Remove(this[this.Count - 1]);
                this.List.RemoveAt(this.Count - 1);
            }
        }

        // Our goal - the common event handler
        void ClickHandler(object sender, EventArgs e)
        {
            int iClicked = Convert.ToInt32(((TextBox)sender).Tag.ToString()) - 1;
            if (Ci.iIndex != iClicked) Ci.iCount = 0;
            Ci.iIndex = iClicked;
            Ci.iCount += 1;
            Ci.iKey = 1;
        }
        void TextChangeHandler(object sender, EventArgs e)
        {
            TCi.iIndex = Convert.ToInt32
                (((TextBox)sender).Tag.ToString()) - 1;
            TCi.bPoll = true;
        }
        void KeyUpHandler(object sender, KeyEventArgs e)
        {
            KUi.iIndex = Convert.ToInt32
                (((TextBox)sender).Tag.ToString()) - 1;
            KUi.bPoll = true;
            KUi.kCode = e.KeyCode;
        }
    }
    public class ClickInfo
    {
        public bool bPoll = false;
        public int iIndex = -1;
        public int iKey = 0;
        public int iCount = 0;
        public Point ptLoc = new Point(-1, -1);
        public Point ptRLoc = new Point(-1, -1);
        public long lLast = 0;

        public static long Tick()
        {
            return System.DateTime.Now.Ticks / 10000;
        }
    }
    public class TxChgInfo
    {
        public bool bPoll = false;
        public int iIndex = -1;

        public static long Tick()
        {
            return System.DateTime.Now.Ticks / 10000;
        }
    }
    public class KeyUpInfo
    {
        public bool bPoll = false;
        public int iIndex = -1;
        public Keys kCode = Keys.None;

        public static long Tick()
        {
            return System.DateTime.Now.Ticks / 10000;
        }
    }
}
