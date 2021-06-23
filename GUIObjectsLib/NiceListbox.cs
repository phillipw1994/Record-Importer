using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    // Declare delegate 
    public delegate void nlbItemSelectedEventHandler(NiceButton nbItem, string strID);

    [DefaultEvent("_EvtItemSelected")]
    public partial class NiceListbox : UserControl
    {
        // Declare an event 
        public event nlbItemSelectedEventHandler _EvtItemSelected;

        private int iAutoScrollDelay = 800; // ms (should be >= 800)
        private int iVScrollMax = 0;
        private int iVScrollMin = 0;
        private int iVScrollValue = 0;
        private int iVScrollSmallVal = 0;
        private int iSliderTopPos = 0;
        private int iSliderBottomPos = 0;
        private int iSliderMouseDownPos = 0;
        private int iSliderHeight = 0;
        private int iHalfSliderHeight = 0;
        private Size szObjSize;
        private Padding padMargin;
        private int iItemHeight = 32;
        private int iFontSize = 9;
        private ContentAlignment caTextVert = ContentAlignment.MiddleLeft;

        private NiceButton _nbtnSelected = null;

        [Browsable(true), DefaultValue(9), Description("Font size of item text")]
        public int _TextSize
        {
            get
            {
                return iFontSize;
            }

            set
            {
                iFontSize = value;
                if(flpNLB_Items.Controls.Count > 0) {
                    flpNLB_Items.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(32), Description("height of an item")]
        public int _ItemHeight
        {
            get
            {
                return iItemHeight;
            }

            set
            {
                iItemHeight = value;
                szObjSize.Height = iItemHeight;
                if(flpNLB_Items.Controls.Count > 0) {
                    flpNLB_Items.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(ContentAlignment.MiddleLeft), Description("Item text alignment")]
        public ContentAlignment _ItemTextAlignment
        {
            get
            {
                return caTextVert;
            }

            set
            {
                caTextVert = value;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_CLIPCHILDREN
                return cp;
            }
        }
        
        public NiceListbox()
        {
            InitializeComponent();

            // slider init
            iSliderHeight = pbNLB_Slider.Height;
            iHalfSliderHeight = Convert.ToInt32(iSliderHeight / 2); // the middle of the Slider
            iSliderTopPos = nbtnNLB_UP.Bottom + 5;
            
            padMargin = new System.Windows.Forms.Padding(0);
            szObjSize = new Size(0, iItemHeight);
            return;
        }

        private void NiceListbox_Load(object sender, EventArgs e)
        {
            nbtnNLB_DOWN._Icon = Properties.Resources.arrow_down;
            nbtnNLB_UP._Icon = Properties.Resources.arrow_up;

            pbNLB_Slider.Top = iSliderTopPos;

            flpNLB_Items.AutoScroll = false;

            ManageScrolling();

            return;
        }

        public void ClearItems()
        {
            flpNLB_Items.Controls.Clear();
            ManageScrolling();
            MoveSlider(iSliderTopPos);
            return;
        }

        public void RemoveSelection()
        {
            if(_nbtnSelected != null) {
                _nbtnSelected._isSelected = false;
                _nbtnSelected = null;
            }
            return;
        }

        private NiceButton _AddItem(string strID, string strText, bool bButtonTextNoWrapping = true)
        {
            NiceButton nbtnObj = new NiceButton();
            nbtnObj.Name = "nbtnNLBItem_" + strID;
            nbtnObj.Size = szObjSize;
            nbtnObj._ButtonText = strText;
            nbtnObj._TextSize = _TextSize;
            nbtnObj._ButtonTextAlignment = caTextVert;
            nbtnObj._ButtonTextNoWrapping = bButtonTextNoWrapping;
            nbtnObj.BackColor = Color.Wheat;
            nbtnObj._Color_Selected = Color.Yellow;
            nbtnObj.Margin = padMargin;

            // events
            nbtnObj._EvtClicked += new nbClickedEventHandler(NLBItem__EvtClicked);

            // add to flowpanel
            flpNLB_Items.Controls.Add(nbtnObj);

            ManageScrolling();

            return nbtnObj;
        }
        
        public NiceButton AddItem(string strID, string strText)
        {
            return _AddItem(strID, strText);
        }

        public NiceButton AddItem(string strID, string strText, bool bTextWrapping)
        {
            return _AddItem(strID, strText, !bTextWrapping);
        }

        public int GetSelectedItem_Index()
        {
            int iIndex = -1; // -1 = nothing selected
            int iIdx = 0;
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(nbtn._isSelected) {
                    iIndex = iIdx;
                    break;
                }
                iIdx++;
            }
            return iIndex;
        }

        private string ExtractIDFromItemName(string strItemName)
        {
            return strItemName.Substring(12);
        }

        public string GetSelectedItem_ID()
        {
            string strID = "";
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(nbtn._isSelected) {
                    strID = ExtractIDFromItemName(nbtn.Name);
                    break;
                }
            }
            return strID;
        }

        public string GetSelectedItem_Text()
        {
            string strText = "";
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(nbtn._isSelected) {
                    strText = nbtn._ButtonText;
                    break;
                }
            }
            return strText;
        }

        public string GetSelectedItem_Caption()
        {
            string strText = "";
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(nbtn._isSelected) {
                    strText = nbtn._CaptionText;
                    break;
                }
            }
            return strText;
        }

        public object GetSelectedItem_Tag()
        {
            object oTag = null;
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(nbtn._isSelected) {
                    oTag = nbtn.Tag;
                    break;
                }
            }
            return oTag;
        }

        public bool Contains_ID(string strID)
        {
            bool bYes = false;
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(strID == ExtractIDFromItemName(nbtn.Name)) {
                    bYes = true;
                    break;
                }
            }
            return bYes;
        }

        public bool Contains_Text(string strText)
        {
            bool bYes = false;
            foreach(NiceButton nbtn in flpNLB_Items.Controls) {
                if(strText == nbtn._ButtonText) {
                    bYes = true;
                    break;
                }
            }
            return bYes;
        }

        public long Count()
        {
            return flpNLB_Items.Controls.Count;
        }

        private void EnableScrollItems(bool bEnabled)
        {
            nbtnNLB_UP.Visible = bEnabled;
            nbtnNLB_DOWN.Visible = bEnabled;
            pbNLB_Slider.Visible = bEnabled;
            return;
        }

        private void ManageScrolling()
        {
            flpNLB_Items.AutoScroll = true;

            iVScrollMin = flpNLB_Items.VerticalScroll.Minimum;
            iVScrollMax = flpNLB_Items.VerticalScroll.Maximum - this.Height; // minus the height I can see
            iVScrollSmallVal = iItemHeight;

            flpNLB_Items.AutoScroll = false;
            
            EnableScrollItems(iVScrollMax > iVScrollMin);
            return;
        }

        private void NLBItem__EvtClicked(NiceButton nbButton)
        {
            if(_nbtnSelected != null && _nbtnSelected != nbButton) {
                _nbtnSelected._isSelected = false;
            }
            _nbtnSelected = nbButton;
            _nbtnSelected._isSelected = !_nbtnSelected._isSelected;

            if(!_nbtnSelected._isSelected) { // we don't want a button as "selected" when we unselect it ;)
                _nbtnSelected = null;
            }

            // give the event through for selecting an item
            if(_EvtItemSelected != null) {
                _EvtItemSelected(_nbtnSelected, _nbtnSelected != null ? ExtractIDFromItemName(_nbtnSelected.Name) : "");
            }
            return;
        }

        private void nbtnNLB_UP_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left) {
                ScrollVertical(iVScrollValue - iVScrollSmallVal);
                if(e.Clicks == 1) {
                    StartAutoScroll();
                }
            }
            return;
        }

        private void nbtnNLB_DOWN_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left){
                ScrollVertical(iVScrollValue + iVScrollSmallVal);
                if(e.Clicks == 1) {
                    StartAutoScroll();
                }
            }
            return;
        }

        private void StartAutoScroll()
        {
            tmAutoScroll.Interval = iAutoScrollDelay;
            tmAutoScroll.Enabled = true;
            return;
        }

        private void tmAutoScroll_Tick(object sender, EventArgs e)
        {
            bool bStop = true;
            if(MouseButtons == System.Windows.Forms.MouseButtons.Left) {
                Point pt = this.PointToClient(MousePosition);
                Control objHit = this.GetChildAtPoint(pt);
                if(objHit == nbtnNLB_DOWN) {
                    ScrollVertical(iVScrollValue + iVScrollSmallVal);
                    bStop = false;
                }
                else {
                    if(objHit == nbtnNLB_UP) {
                        ScrollVertical(iVScrollValue - iVScrollSmallVal);
                        bStop = false;
                    }
                }
            }

            if(bStop) {
                tmAutoScroll.Enabled = false;
            }
            else { // if still going...increase speed with every cycle
                if(tmAutoScroll.Interval == iAutoScrollDelay) {
                    tmAutoScroll.Interval = 200;
                }
                else {
                    if(tmAutoScroll.Interval >= 20) {
                        tmAutoScroll.Interval -= 10;
                    }
                }
            }
            return;
        }

        private void MoveSlider(int iPos)
        {
            if(iPos < iSliderTopPos) {
                iPos = iSliderTopPos;
            }
            else {
                if((iPos + iSliderHeight) > iSliderBottomPos) {
                    iPos = iSliderBottomPos - iSliderHeight;
                }
            }
            pbNLB_Slider.Top = iPos;
            float fPercentage = CalcSliderPosPercentage(iPos);
            ScrollVertical(Convert.ToInt32(iVScrollMax * (fPercentage / 100.0f)), false);
            return;
        }

        private void pbNLB_Slider_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left) {
                int iPos = this.PointToClient(MousePosition).Y - iSliderMouseDownPos;
                MoveSlider(iPos);
            }
            return;
        }

        private void pbNLB_Slider_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left) {
                iSliderMouseDownPos = e.Y;
            }
            return;
        }

        private int CalcSliderRangePx()
        {
            int iTopPos = iSliderTopPos + iHalfSliderHeight;
            int iBottomPos = iSliderBottomPos - iHalfSliderHeight;
            int iSliderRangePx = iBottomPos - iTopPos;
            return iSliderRangePx;
        }

        private float CalcSliderPosPercentage(int iPosTopOfSlider)
        {
            float fSliderRangePx = (float) CalcSliderRangePx();
            int iTopPos = iSliderTopPos + iHalfSliderHeight;
            float fValue = (((iPosTopOfSlider + iHalfSliderHeight) - iTopPos) * 100.0f) / fSliderRangePx;
            return fValue;
        }

        private int CalcSliderPos(int iVScrollValue)
        {
            float fSliderRangePx = (float) CalcSliderRangePx();
            int iPos = (Convert.ToInt32(fSliderRangePx) * iVScrollValue) / iVScrollMax;
            return iPos + iSliderTopPos;
        }

        private void ScrollVertical(int iScrollTo, bool bMoveSlider = true)
        {
            int iAdditional = 0;
            iVScrollValue = iScrollTo;
            if(iVScrollValue <= iVScrollMin) {
                iVScrollValue = (iVScrollMin == 0 ? 1 : iVScrollMin); // 0 doesn't work...so we set it to 1
                tmAutoScroll.Enabled = false; // stop autoscroll in case it was active
            }
            else {
                if(iVScrollValue > iVScrollMax) {
                    iVScrollValue = iVScrollMax;
                    iAdditional = (iItemHeight / 2);
                    tmAutoScroll.Enabled = false; // stop autoscroll in case it was active
                }
            }
            flpNLB_Items.VerticalScroll.Value = iVScrollValue + iAdditional;
            flpNLB_Items.Update();

            if(bMoveSlider) {
                // here the code to move the slider
                int iPos = CalcSliderPos(iVScrollValue);
                pbNLB_Slider.Top = iPos;
            }
            return;
        }

        private void NiceListbox_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left) {
                if(e.X > pbNLB_Slider.Left && e.X < pbNLB_Slider.Right) {
                    if(e.Y >= iSliderTopPos && e.Y <= iSliderBottomPos) {
                        MoveSlider(e.Y - iHalfSliderHeight);
                    }
                }
            }
            return;
        }

        private void flpNLB_Items_Resize(object sender, EventArgs e)
        {
            szObjSize = new Size(flpNLB_Items.Width - 4, iItemHeight);
            return;
        }

        private void NiceListbox_Resize(object sender, EventArgs e)
        {
            flpNLB_Items.Width = this.Width - 3 - nbtnNLB_UP.Width - 4 - 1;
            flpNLB_Items.Height = this.Height - 3 - 3;
            nbtnNLB_UP.Top = 3;
            nbtnNLB_UP.Left = flpNLB_Items.Right + 4;
            nbtnNLB_DOWN.Top = flpNLB_Items.Bottom - nbtnNLB_DOWN.Height;
            nbtnNLB_DOWN.Left = nbtnNLB_UP.Left;
            pbNLB_Slider.Left = nbtnNLB_UP.Left;
            // slider init
            iSliderBottomPos = nbtnNLB_DOWN.Top - 5;
            return;
        }
    }
}
