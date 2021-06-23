using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    /// <summary>
    /// This override of the combobox provides an advantage over the vanilla combobox:
    /// 1.  There is a glitch if you are using a ComboBox either in a DataGridView or standalone.
    ///     If you open the ComboBox dropdown but then type a value and press ENTER, the value is lost.
    ///     This error does not appear if you press TAB.
    ///     This is the workaround.
    /// </summary>
    public class SmartComboBox : ComboBox
    {
        private int initialDropDownWidth = 0;
        private const UInt32 WM_CTLCOLORLISTBOX = 0x0134;
        private const int SWP_NOSIZE = 0x1;
        private bool _isLoading;

        public SmartComboBox()
        {
            base.AutoCompleteMode = AutoCompleteMode.Suggest;
            base.AutoCompleteSource = AutoCompleteSource.ListItems;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            initialDropDownWidth = this.DropDownWidth;
        }
        
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int 
         X, int Y, int cx, int cy, uint uFlags);


        [DefaultValue(AutoCompleteMode.Suggest)]
        public new AutoCompleteMode AutoCompleteMode
        {
            set { base.AutoCompleteMode = value; Invalidate(); }
            get { return base.AutoCompleteMode; }
        }

        [DefaultValue(AutoCompleteSource.ListItems)]
        public new AutoCompleteSource AutoCompleteSource
        {
            set { base.AutoCompleteSource = value; Invalidate(); }
            get { return base.AutoCompleteSource; }
        }

        [DefaultValue(ComboBoxStyle.DropDownList)]
        public new ComboBoxStyle DropDownStyle
        {
            set { base.DropDownStyle = value; Invalidate(); }
            get { return base.DropDownStyle; }
        }

        public Boolean IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
            }
        }
        /// <summary>
        ///  There is a glitch if you are using a ComboBox either in a DataGridView or standalone.
        /// If you open the ComboBox dropdown but then type a value and press ENTER, the value is lost.
        /// This error does not appear if you press TAB.
        /// This is the workaround.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (this.DroppedDown)
                this.DroppedDown = false;

            // Allow the ESC key to cancel changes even if AutoComplete mode has been active.
            if ((e.KeyCode == Keys.Escape))
            {
                this.Text = "";
                this.SelectedItem = null;
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //capture arrow keys, enter
            switch (e.KeyValue)
            {
                case 13:
                    if (this.Tag != null && this.Tag.ToString().Length > 0)
                        this.Text = this.Tag.ToString();
                    else
                        this.OnLeave(e);

                    break;
                case 38:
                case 40:
                    //up and down arrow keys
                    this.Tag = this.Text;
                    break;
                default:
                    //this.Tag = "";
                    break;
            }

            base.OnKeyUp(e);
        }

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            if (this.SelectedItem != null)
            {
                this.Tag = this.SelectedItem.ToString();

                base.OnSelectedItemChanged(e);
            }
        }

        public void UpdateDropDownWidth()
        {
            //Create a GDI+ drawing surface to measure string widths
            System.Drawing.Graphics ds = this.CreateGraphics();

            //Float to hold largest single item width
            float maxWidth = 0;

            //Iterate over each item, measuring the maximum width
            //of the DisplayMember strings
            foreach (object item in this.Items)
            {
                maxWidth = Math.Max(maxWidth, ds.MeasureString(item.ToString(), this.Font).Width);
            }

            //Add a buffer for some white space
            //around the text
            maxWidth += 30;

            //round maxWidth and cast to an int
            int newWidth = (int)Decimal.Round((decimal)maxWidth, 0);

            //If the width is bigger than the screen, ensure
            //we stay within the bounds of the screen
            if (newWidth > Screen.GetWorkingArea(this).Width)
            {
                newWidth = Screen.GetWorkingArea(this).Width;
            }

            //Only change the default width if it's smaller
            //than the newly calculated width
            if (newWidth > initialDropDownWidth)
            {
                this.DropDownWidth = newWidth;
            }

            //Clean up the drawing surface
            ds.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            if(!_isLoading)
            {
                if (m.Msg == WM_CTLCOLORLISTBOX)
                {
                    // Make sure we are inbounds of the screen
                    int left = this.PointToScreen(new Point(0, 0)).X;

                    //Only do this if the dropdown is going off right edge of screen
                    if (this.DropDownWidth > Screen.PrimaryScreen.WorkingArea.Width - left)
                    {
                        // Get the current combo position and size
                        Rectangle comboRect = this.RectangleToScreen(this.ClientRectangle);

                        int dropHeight = 0;
                        int topOfDropDown = 0;
                        int leftOfDropDown = 0;

                        //Calculate dropped list height
                        for (int i = 0; (i < this.Items.Count && i < this.MaxDropDownItems); i++)
                        {
                            dropHeight += this.ItemHeight;
                        }

                        //Set top position of the dropped list if 
                        //it goes off the bottom of the screen
                        if (dropHeight > Screen.PrimaryScreen.WorkingArea.Height -
                           this.PointToScreen(new Point(0, 0)).Y)
                        {
                            topOfDropDown = comboRect.Top - dropHeight - 2;
                        }
                        else
                        {
                            topOfDropDown = comboRect.Bottom;
                        }

                        //Calculate shifted left position
                        leftOfDropDown = comboRect.Left - (this.DropDownWidth -
                           (Screen.PrimaryScreen.WorkingArea.Width - left));

                        // Postioning/sizing the drop-down
                        //SetWindowPos(HWND hWnd,
                        //      HWND hWndInsertAfter,
                        //      int X,
                        //      int Y,
                        //      int cx,
                        //      int cy,
                        //      UINT uFlags);
                        //when using the SWP_NOSIZE flag, cx and cy params are ignored
                        SetWindowPos(m.LParam,
                           IntPtr.Zero,
                           leftOfDropDown,
                           topOfDropDown,
                           0,
                           0,
                           SWP_NOSIZE);
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SmartComboBox
            // 
            this.ResumeLayout(false);

        }

    }
}
