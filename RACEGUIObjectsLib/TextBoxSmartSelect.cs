using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;

namespace RaceGUIObjectsLib
{
    /// <summary>
    ///     AutoComplete only works on a prefix.  ie String.StartsWith(x)  
    ///     This changes this to find within all text for an item.   ie String.Contains(x)
    /// </summary>

    public class TextBoxSmartSelect : TextBox, IMessageFilter
    {
        private Control ComboParentForm = null; // Or use type "Form

        //used to show options to user
        private ListBox listBoxChild = null;
        private bool selectionMade = false;         //set to true when a selection has been made from list
        private bool MsgFilterActive = false;


        //array of strings used to smart select from - used instead of source if just one field
        private ArrayList _Items = new ArrayList();
        public ArrayList Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        private bool _IgnoreTextChange = false;
        public bool IgnoreTextChange
        {
            get { return _IgnoreTextChange; }
            set { _IgnoreTextChange = value; }
        }

        private string _Key = "";
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }


        //private string _Text = "";
        public override string Text
        {
            get
            {
                return base.Text; // _Text;
            }

            set
            {
                if (value.Length == 0)
                    Console.Write("?");

                base.Text = value;

                if (!DesignMode)
                    SelectionMade(this, null);
            }
        }

        /// <summary>
        /// Sets text to control without popping up selection list
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            this.IgnoreTextChange = true;
            base.Text = text;         
            this.IgnoreTextChange = false;
        }


        public TextBoxSmartSelect()
        {
            // Set up all the events we need to handle
            TextChanged += TextBoxSmartSelect_TextChanged;
            LostFocus += TextBoxSmartSelect_LostFocus;
            MouseDown += TextBoxSmartSelect_MouseDown;
            MouseClick += TextBoxSmartSelect_MouseClick;
            HandleDestroyed += TextBoxSmartSelect_HandleDestroyed;
        }

        void TextBoxSmartSelect_HandleDestroyed(object sender, EventArgs e)
        {
            if (MsgFilterActive)
                Application.RemoveMessageFilter(this);
        }

        ~TextBoxSmartSelect()
        {
        }

        /// <summary>
        /// Set a DataTable to the list of Items to auto-select from
        /// Note that the datatable should only contain 1 field
        /// </summary>
        /// <param name="rst">Assunes data for display is in ordinal field position 0.  A key can be provided in ordinal key position 1</param>
        public void SetDataSource(System.Data.DataTable rst)
        {
            try
            {
                //convert DataTable to array

                //can be used if only 1 field
                //this.Items = new ArrayList(rst.Rows.OfType<System.Data.DataRow>().Select(k => k[0].ToString()).ToArray());
                
                this.Items = new ArrayList();
                foreach (DataRow row in rst.Rows)
                {
                    DataPair pair = new DataPair();
                    pair.Name = row[0].ToString();
                    if (row.Table.Columns.Count > 1)
                        pair.Key = row[1].ToString();
                    else
                        pair.Key = pair.Name;

                    this.Items.Add(pair);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TextBoxSmartSelect_MouseDown(object sender, MouseEventArgs e)
        {
            //HideTheList();
        }

        private void TextBoxSmartSelect_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IgnoreTextChange)
            {
                SetText("");
                ShowList();
            }
        }

        void TextBoxSmartSelect_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (this.ComboParentForm != null)
                {
                    //if (listBoxChild != null && !listBoxChild.Focused)
                    //    HideTheList();

                    if (!selectionMade && this.Tag != null && this.Tag.ToString().Length > 0)
                    {
                        Console.WriteLine(this.Tag.ToString());
                        //this.SetText(this.Tag.ToString());
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        void InitListControl()
        {
            if (listBoxChild == null)
            {
                // Find parent - or keep going up until you find the parent form
                //ComboParentForm = this.Parent;
                ComboParentForm = this.FindForm();

                if (ComboParentForm != null)
                {
                    // Setup a messaage filter so we can listen to the keyboard
                    if (!MsgFilterActive)
                    {
                        Application.AddMessageFilter(this);
                        MsgFilterActive = true;
                    }


                    listBoxChild = listBoxChild = new ListBox();
                    listBoxChild.Height = 400;
                    listBoxChild.Visible = false;
                    listBoxChild.Click += listBoxChild_Click;
                    listBoxChild.MouseWheel += listBoxChild_MouseWheel;
                    ComboParentForm.Controls.Add(listBoxChild);
                    ComboParentForm.Controls.SetChildIndex(listBoxChild, 0); // Put it at the front
                }
            }
        }


        void TextBoxSmartSelect_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreTextChange)
                ShowList();
        }

        /// <summary>
        /// SelectionMade event fires when user selects an item from the list
        /// </summary>
        public event EventHandler SelectionMade = null;
        protected virtual void OnSelectionMade(EventArgs e)
        {
            selectionMade = true;
            EventHandler handler = SelectionMade;
            if (handler != null)
                handler(this, e);
        }


        /// <summary>
        /// Copy the selection from the list-box into the combo box
        /// </summary>
        private void CopySelection()
        {
            if (listBoxChild.SelectedItem != null)
            {
                DataPair pair = (DataPair)listBoxChild.SelectedItem;

                this.Key = pair.Key;
                this.Text = pair.Name;       //use list               
                this.Tag = pair.Name;

                HideList();
                this.SelectAll();

                //raise event that a selection has been made
                SelectionMade(this, null);
            }
        }

        private void listBoxChild_Click(object sender, EventArgs e)
        {
            var ThisList = sender as ListBox;

            if (ThisList != null)
            {
                // Copy selection to the combo box
                CopySelection();
            }
        }

        /// <summary>
        /// Disable mousewheel scrolling on comboboxes, as per Andrew Lock's request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listBoxChild_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        public void SaveCurrentSelection()
        {
            this.Tag = this.Text;
        }

        void ShowList()
        {
            selectionMade = false;
            string SearchText = this.Text.ToUpper();

            InitListControl();

            if (listBoxChild == null)
                return;

            if (listBoxChild.DataSource == null)
                listBoxChild.Items.Clear();     //clear Item list
            else
                listBoxChild.DataSource = null; //clear data source

            //populate from list
            string formattedItem = "";
            foreach (DataPair Item in this.Items)
            {
                formattedItem = Item.Name.ToUpper();
                if (Item != null && (SearchText.Length == 0 || formattedItem.Contains(SearchText)))
                    listBoxChild.Items.Add(Item);
            }


            switch (listBoxChild.Items.Count)
            {
                case 0:
                    //nothing to show
                    HideList();
                    break;
                default:
                    //show list
                    Point PutItHere = new Point(this.Left, this.Bottom);
                    Control TheControlToMove = this;

                    PutItHere = this.Parent.PointToScreen(PutItHere);

                    TheControlToMove = listBoxChild;
                    PutItHere = ComboParentForm.PointToClient(PutItHere);

                    TheControlToMove.Show();
                    TheControlToMove.Left = PutItHere.X;
                    TheControlToMove.Top = PutItHere.Y;
                    TheControlToMove.Width = this.Width;

                    int TotalItemHeight = listBoxChild.ItemHeight * (listBoxChild.Items.Count + 1);
                    //TheControlToMove.Height = 400;
                    TheControlToMove.Height = Math.Min(400, TotalItemHeight);
                    break;

            }
        }

        private void HideList()
        {
            if (listBoxChild != null)
                listBoxChild.Hide();

            //try to reset selection before list was shown
            if (!selectionMade && this.Tag != null && this.Tag.ToString().Length > 0)
            {
                Console.WriteLine(this.Tag.ToString());
                this.SetText(this.Tag.ToString());
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            try
            {
                var Ctrl = Control.FromHandle(m.HWnd);

                //check message is from the form that TextBoxSmartSelect is on
                
                if (ComboParentForm != null && Ctrl != null)
                {
                    Form controlForm = Ctrl.FindForm();
                    if (controlForm != null && controlForm.Equals(ComboParentForm))
                    {
                        if (m.Msg == 0x201) // Mouse click: WM_LBUTTONDOWN
                        {
                            //check if we need to hide the listBox, because it has been clicked away from
                            //var Pos = new Point((int)(m.LParam.ToInt32() & 0xFFFF), (int)(m.LParam.ToInt32() >> 16));

                            //check message is from a control on the form TextBoxSmartSelect is on, and list box is visible

                            if (Ctrl != null && listBoxChild.Visible)
                            {
                                // Convert the point into our parent control's coordinates ...
                                //Pos = ComboParentForm.PointToClient(Ctrl.PointToScreen(Pos));

                                // ... because we need to hide the list if user clicks on something other than the list-box
                                if (ComboParentForm != null)
                                {
                                    if (listBoxChild != null &&
                                        !Ctrl.Equals(listBoxChild))
                                    {
                                        this.HideList();

                                        //do we need to reset last selected?
                                        if (!selectionMade && this.Tag != null && this.Tag.ToString().Length > 0)
                                            this.SetText(this.Tag.ToString());
                                    }
                                }
                            }
                        }
                        else if (m.Msg == 0x100) // WM_KEYDOWN
                        {
                            int keyCode = m.WParam.ToInt32();

                            if (listBoxChild != null && listBoxChild.Visible)
                            {
                                //in listbox
                                switch (keyCode)
                                {
                                    case 0x1B: // Escape key
                                        if (this.Tag != null)
                                            this.Text = this.Tag.ToString();
                                        this.HideList();
                                        return true;

                                    case 0x26: // up key
                                    case 0x28: // right key
                                        // Change selection
                                        int NewIx = listBoxChild.SelectedIndex + ((m.WParam.ToInt32() == 0x26) ? -1 : 1);

                                        // Keep the index valid!
                                        if (NewIx >= 0 && NewIx < listBoxChild.Items.Count)
                                            listBoxChild.SelectedIndex = NewIx;
                                        return true;

                                    case 0x0D: // return (use the currently selected item)
                                        //check if we are down to one item
                                        if (listBoxChild.Items.Count == 1)
                                            listBoxChild.SelectedIndex = 0;
                                        //this.Text = listBoxChild.Items[0].ToString();  //select this

                                        CopySelection();
                                        return true;
                                }
                            }
                            else if (this.Focused)
                            {
                                //in textbox
                                if (keyCode == 13)
                                {
                                    this.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}

