using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    // Declare delegate 
    public delegate void ntbClickedEventHandler(NiceTextbox ntbObj);

    public partial class NiceTextbox : UserControl
    {
        // Declare an event 
        public event ntbClickedEventHandler _EvtClicked;

        private Color cTextColor = Color.Black;
        private Font myFont = null;
        private Rectangle MyRect;
        private StringFormat stringFormat = new StringFormat();
                
        private bool bPressed = false;
        private bool bReadOnly = false;
        private string strCaptionText = "Caption";
        private string strText = "Text";
        private string strText2 = "";
        private int iFontSizeCaption = 7;
        private int iFontSizeText = 10;

        [Browsable(true), DefaultValue(7), Description("Caption font size")]
        public int _CaptionSize
        {
            get
            {
                return iFontSizeCaption;
            }

            set
            {
                iFontSizeCaption = value;
                pbNTBox.Refresh();
            }
        }

        [Browsable(true), DefaultValue(10), Description("Text font size")]
        public int _TextSize
        {
            get
            {
                return iFontSizeText;
            }

            set
            {
                iFontSizeText = value;
                pbNTBox.Refresh();
            }
        }

        [Browsable(true), DefaultValue("Caption"), Description("Caption")]
        public string _CaptionText
        {
            get
            {
                return strCaptionText;
            }

            set
            {
                strCaptionText = value;
                pbNTBox.Refresh();
            }
        }

        [Browsable(true), DefaultValue("Text"), Description("Text")]
        public string _Text
        {
            get
            {
                return strText;
            }

            set
            {
                strText = value;
                pbNTBox.Refresh();
            }
        }

        [Browsable(true), DefaultValue(""), Description("Second text")]
        public string _Text2
        {
            get
            {
                return strText2;
            }

            set
            {
                strText2 = value;
                pbNTBox.Refresh();
            }
        }

        [Browsable(true), DefaultValue(false), Description("Readonly")]
        public bool _ReadOnly
        {
            get
            {
                return bReadOnly;
            }

            set
            {
                bReadOnly = value;
                if(bReadOnly){
                    pbNTBox.BackColor = Color.LightGray;
                    this.Cursor = Cursors.Default;
                }
                else{
                    pbNTBox.BackColor = Color.Transparent;
                    this.Cursor = Cursors.Hand;
                }
                pbNTBox.Refresh();
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Black"), Description("Color of text and second text")]
        public Color _TextColor
        {
            get
            {
                return cTextColor;
            }

            set
            {
                cTextColor = value;
                if(strText.Length > 0 || strText2.Length > 0) {
                    pbNTBox.Refresh();
                }
            }
        }

        public NiceTextbox()
        {
            InitializeComponent();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            pbNTBox.Image = Properties.Resources.field_96;
            return;
        }

        public void SetDateTimeValue(DateTime dtValue, string strFormatDate, string strFormatTime)
        {
            string strDate = "";
            string strTime = "";
            try {
                if(dtValue != null) {
                    if(strFormatDate.Length == 0) strFormatDate = "dd/MM/yyyy";
                    if(strFormatTime.Length == 0) strFormatTime = "HH:mm";
                    strDate = dtValue.ToString(strFormatDate, CultureInfo.CurrentCulture);
                    strTime = dtValue.ToString(strFormatTime, CultureInfo.CurrentCulture);
                }
            }
            catch { }
            strText = strDate;
            strText2 = strTime;
            pbNTBox.Refresh();
            return;
        }

        public bool GetDateTimeValue(ref DateTime dtValue)
        {
            bool bOk = false;
            try {
                if(strText.Length >= 8) {
                    try {
                        dtValue = Convert.ToDateTime(strText + " " + strText2);
                        bOk = true;
                    }
                    catch { }
                }
            }
            catch { }
            return bOk;
        }

        public void Reset_Text()
        {
            strText = "";
            strText2 = "";
            pbNTBox.Refresh();
            return;
        }

        private void pbNBtn_Paint(object sender, PaintEventArgs e)
        {
            int iWidth = pbNTBox.Width;
            int iHeight = pbNTBox.Height;
            int iHeightCaption = 0;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            if(strCaptionText.Length > 0) {
                myFont = new Font("Arial", bPressed ? iFontSizeCaption - 2 : iFontSizeCaption, FontStyle.Bold);
                SizeF sf = e.Graphics.MeasureString(strCaptionText, myFont);
                iHeightCaption = (int)(sf.Height) + 5;
                MyRect = new Rectangle(0, 3, iWidth, iHeightCaption);
                e.Graphics.DrawString(strCaptionText, myFont, new System.Drawing.SolidBrush(Color.Black), MyRect, stringFormat);
                myFont.Dispose();
            }

            if(strText.Length > 0) {
                bool bText2 = strText2.Length > 0;
                int iHeightText = (iHeight - (iHeightCaption + 3)) / (bText2 ? 2 : 1);
                myFont = new Font("Arial", bPressed ? iFontSizeText - 2 : iFontSizeText, FontStyle.Bold);
                MyRect = new Rectangle(0, 3 + iHeightCaption, iWidth, iHeightText);
                e.Graphics.DrawString(strText, myFont, new System.Drawing.SolidBrush(cTextColor), MyRect, stringFormat);

                if(bText2) {
                    MyRect = new Rectangle(0, 3 + iHeightCaption + iHeightText, iWidth, iHeightText);
                    e.Graphics.DrawString(strText2, myFont, new System.Drawing.SolidBrush(cTextColor), MyRect, stringFormat);
                }
                myFont.Dispose();
            }
            return;
        }

        private void pbNBtn_Click(object sender, EventArgs e)
        {
            if(!bReadOnly){
                if(_EvtClicked != null) {
                    _EvtClicked(this);
                }
            }
            return;
        }

        private void pbNBtn_DoubleClick(object sender, EventArgs e)
        {
            pbNBtn_Click(sender, e);
            return;
        }

        private void pbNBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if(!bReadOnly){
                bPressed = true;
                pbNTBox.Refresh();
            }
            return;
        }

        private void pbNBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if(!bReadOnly){
                bPressed = false;
                pbNTBox.Refresh();
            }
            return;
        }
    }
}
