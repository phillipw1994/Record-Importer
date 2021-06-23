using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RaceGUIObjectsLib
{
    // Declare delegate 
    public delegate void nbClickedEventHandler(NiceButton nbButton);

    [DefaultEvent("Click")]
    public partial class NiceButton : UserControl
    {
        // Declare an event 
        public event nbClickedEventHandler _EvtClicked;

        private Font myFont = null;
        private Rectangle MyRect;
        private StringFormat stringFormatCaption = new StringFormat();
        private StringFormat stringFormatText = new StringFormat();


        private bool bCaptionSet = false;
        private bool bTextSet = false;
        private bool bXOverSet = false;
        private bool bIsSelected = false;
        private bool bPressed = false;
        private string strXOverText = "";
        private string strCaptionText = "";
        private string strButtonText = "";
        private int iFontSizeXOverText = 14;
        private int iFontSizeCaption = 7;
        private int iFontSize = 8;
        private Color cSelected = Color.Transparent;
        private Color cTextColor = Color.Black;
        private Color cTextColorSel = Color.Transparent;
        private Color cTextColorXOver = Color.Red;
        private bool bEnabled = true;
        private bool bShowImage  = true;
        private Image iImgIcon = null;
        private bool bIconSet = false;
        private Point ptImgIconPos = new Point(10, 10);
        private Size szImgIconSize = new Size(20, 20);
        private ContentAlignment caButtonTextAlignment = ContentAlignment.MiddleCenter;
        private bool bNoTextWrapping = false;
        private short sXOverAngle = -30;

        // progress bar
        public class cPBPercentageValue
        {
            private double _Value;
            private string _UserText;

            public double _PValue
            {
                get
                {
                    return _Value;
                }
            }

            public string _PUserText
            {
                get
                {
                    return _UserText;
                }
            }

            public cPBPercentageValue(double byValue, string strUserText)
            {
                _Value = byValue;
                _UserText = strUserText;
                return;
            }
        }
        public enum ePBarPosition
        {
            left,
            right,
            top,
            bottom,
            userdefined
        }
        public enum ePBarTextPosition
        {
            NoText,
            left,
            right,
            center
        }
        private bool bShowProgressBar  = false;
        private ePBarPosition epbPosition = ePBarPosition.bottom;
        private int iPBarSize = 10; // in pixel / width if vertical and height if horizontal
        private string strPBarUserText = ""; // not a property..internal use only
        private ePBarTextPosition epbTextPosition = ePBarTextPosition.center;
        private StringFormat stringFormatPBar = new StringFormat();
        private int iPBarFontSize = 8;
        private Color cPBarTextColor = Color.Black;
        private Color cPBarFillLow = Color.Lime;
        private Color cPBarFillMedium = Color.Lime;
        private Color cPBarFillHigh = Color.Lime;
        private double dPBarTopLowPercentage = 33.0;
        private double dPBarTopMediumPercentage = 66.0;
        private double dPBarPercentageValue = 0.0;
        private bool bPBarDisplayTilde = false;
        private Rectangle rectPBarUserPosition = new Rectangle(0,0,0,0);


        [Browsable(true), DefaultValue(7),Description("Font size of button caption")]
        public int _CaptionSize
        {
            get
            {
                return iFontSizeCaption;
            }

            set
            {
                iFontSizeCaption = value;
                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(""), Description("Button caption text")]
        public string _CaptionText
        {
            get
            {
                return strCaptionText;
            }

            set
            {
                strCaptionText = value;
                bCaptionSet = strCaptionText.Length > 0;
                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(14), Description("Font size of X-Over text")]
        public int _XOverSize
        {
            get
            {
                return iFontSizeXOverText;
            }

            set
            {
                iFontSizeXOverText = value;
                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(""), Description("X-Over text")]
        public string _XOverText
        {
            get
            {
                return strXOverText;
            }

            set
            {
                strXOverText = value;
                bXOverSet = strXOverText.Length > 0;
                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Red"), Description("X-Over text color")]
        public Color _Color_XOver
        {
            get
            {
                return cTextColorXOver;
            }

            set
            {
                cTextColorXOver = value;

                if(strXOverText.Length > 0) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(-30), Description("Angle of X-Over text (angular degree)")]
        public short _XOverAngle
        {
            get
            {
                return sXOverAngle;
            }

            set
            {
                sXOverAngle = value;
                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Transparent"), Description("Color of button when selected (Yellow is a good choise)")]
        public Color _Color_Selected
        {
            get
            {
                return cSelected;
            }

            set
            {
                cSelected = value;

                if(bIsSelected) {
                    _isSelected = true; // just to do the color thing
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Transparent"), Description("Text color when button is selected")]
        public Color _Color_TextSelected
        {
            get
            {
                return cTextColorSel;
            }

            set
            {
                cTextColorSel = value;

                if(bIsSelected) {
                    _isSelected = true; // just to do the color thing
                }
            }
        }

        [Browsable(true), DefaultValue(false), Description("If true then button looks selected/pressed")]
        public bool _isSelected
        {
            get
            {
                return bIsSelected;
            }

            set
            {
                bIsSelected = value;

                if(bIsSelected) {
                    pbNBtn.BackColor = cSelected;
                }
                else{
                    pbNBtn.BackColor = Color.Transparent;
                    bPressed = false; // just in case
                }

                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(8), Description("Font size of button text")]
        public int _TextSize
        {
            get
            {
                return iFontSize;
            }

            set
            {
                iFontSize = value;
                if(strButtonText.Length > 0) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(""), Description("Button text")]
        public string _ButtonText
        {
            get
            {
                return strButtonText;
            }

            set
            {
                strButtonText = value;
                bTextSet = strButtonText.Length > 0;
                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(true), Description("If false it will change cursor and text color (grey)")]
        public bool _Enabled
        {
            get
            {
                return bEnabled;
            }

            set
            {
                bEnabled = value;

                if(bEnabled) {
                    //cTextColor = this.ForeColor;
                    this.Cursor = Cursors.Hand;
                }
                else {
                    //cTextColor = Color.Gray;
                    this.Cursor = Cursors.Default;
                }

                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(true), Description("If false there is no glass-style")]
        public bool _ShowImage
        {
            get
            {
                return bShowImage;
            }

            set
            {
                bShowImage = value;

                if(bShowImage) {
                    pbNBtn.Image = Properties.Resources.field_96;
                }
                else {
                    pbNBtn.Image = null;
                }

                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(null), Description("Sets a small icon/image on the button")]
        public Image _Icon
        {
            get
            {
                return iImgIcon;
            }

            set
            {
                iImgIcon = value;

                bIconSet = iImgIcon != null;

                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(typeof(Point), "10,10"), Description("Position of the small icon/image")]
        public Point _IconPosition
        {
            get
            {
                return ptImgIconPos;
            }

            set
            {
                ptImgIconPos = value;
                if(iImgIcon != null) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Size), "20,20"), Description("Size of the small icon/image")]
        public Size _IconSize
        {
            get
            {
                return szImgIconSize;
            }

            set
            {
                szImgIconSize = value;
                if(iImgIcon != null) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(ContentAlignment.MiddleCenter), Description("Button text Alignment")]
        public ContentAlignment _ButtonTextAlignment
        {
            get
            {
                return caButtonTextAlignment;
            }

            set
            {
                caButtonTextAlignment = value;

                switch(caButtonTextAlignment) {
                    case ContentAlignment.BottomCenter:
                        stringFormatText.Alignment = StringAlignment.Center; // left / right
                        stringFormatText.LineAlignment = StringAlignment.Far; // up / down
                        break;

                    case ContentAlignment.BottomLeft:
                        stringFormatText.Alignment = StringAlignment.Near;
                        stringFormatText.LineAlignment = StringAlignment.Far;
                        break;

                    case ContentAlignment.BottomRight:
                        stringFormatText.Alignment = StringAlignment.Far;
                        stringFormatText.LineAlignment = StringAlignment.Far;
                        break;

                    case ContentAlignment.MiddleCenter:
                        stringFormatText.Alignment = StringAlignment.Center;
                        stringFormatText.LineAlignment = StringAlignment.Center;
                        break;

                    case ContentAlignment.MiddleLeft:
                        stringFormatText.Alignment = StringAlignment.Near;
                        stringFormatText.LineAlignment = StringAlignment.Center;
                        break;

                    case ContentAlignment.MiddleRight:
                        stringFormatText.Alignment = StringAlignment.Far;
                        stringFormatText.LineAlignment = StringAlignment.Center;
                        break;

                    case ContentAlignment.TopCenter:
                        stringFormatText.Alignment = StringAlignment.Center;
                        stringFormatText.LineAlignment = StringAlignment.Near;
                        break;

                    case ContentAlignment.TopLeft:
                        stringFormatText.Alignment = StringAlignment.Near;
                        stringFormatText.LineAlignment = StringAlignment.Near;
                        break;

                    case ContentAlignment.TopRight:
                        stringFormatText.Alignment = StringAlignment.Far;
                        stringFormatText.LineAlignment = StringAlignment.Near;
                        break;
                }

                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(false), Description("no button text wrapping")]
        public bool _ButtonTextNoWrapping
        {
            get
            {
                return bNoTextWrapping;
            }

            set
            {
                if(bNoTextWrapping != value) {
                    bNoTextWrapping = value;

                    stringFormatText.FormatFlags = bNoTextWrapping ? StringFormatFlags.NoWrap : 0;
                    //stringFormatText.Trimming = StringTrimming.;

                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(false), Description("Show progress bar")]
        public bool _PBarShowProgressBar
        {
            get
            {
                return bShowProgressBar;
            }

            set
            {
                bShowProgressBar = value;

                pbNBtn.Refresh();
            }
        }

        [Browsable(true), DefaultValue(ePBarPosition.bottom), Description("progress bar position on button")]
        public ePBarPosition _PBarPosition
        {
            get
            {
                return epbPosition;
            }

            set
            {
                epbPosition = value;

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(10), Description("progress bar size in pixel (width if vertical and height if horizontal)")]
        public int _PBarSize
        {
            get
            {
                return iPBarSize;
            }

            set
            {
                iPBarSize = value;
                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(ePBarTextPosition.center), Description("text position in progress bar")]
        public ePBarTextPosition _PBarTextPosition
        {
            get
            {
                return epbTextPosition;
            }

            set
            {
                epbTextPosition = value;

                switch(epbTextPosition) {
                    case ePBarTextPosition.center:
                        stringFormatPBar.Alignment = StringAlignment.Center; // left / right
                        break;

                    case ePBarTextPosition.left:
                        stringFormatPBar.Alignment = StringAlignment.Near; // left / right
                        break;

                    case ePBarTextPosition.right:
                        stringFormatPBar.Alignment = StringAlignment.Far; // left / right
                        break;
                }

                stringFormatPBar.LineAlignment = StringAlignment.Center; // up / down

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(8), Description("Font size of progress bar text")]
        public int _PBarFontSize
        {
            get
            {
                return iPBarFontSize;
            }

            set
            {
                iPBarFontSize = value;
                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Black"), Description("progress bar text color")]
        public Color _PBarTextColor
        {
            get
            {
                return cPBarTextColor;
            }

            set
            {
                cPBarTextColor = value;

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Lime"), Description("progress bar color for low percentage")]
        public Color _PBarFillLow
        {
            get
            {
                return cPBarFillLow;
            }

            set
            {
                cPBarFillLow = value;

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Lime"), Description("progress bar color for medium percentage")]
        public Color _PBarFillMedium
        {
            get
            {
                return cPBarFillMedium;
            }

            set
            {
                cPBarFillMedium = value;

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Lime"), Description("progress bar color for high percentage")]
        public Color _PBarFillHigh
        {
            get
            {
                return cPBarFillHigh;
            }

            set
            {
                cPBarFillHigh = value;

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(33.0), Description("max value for low percentage range")]
        public double _PBarTopLowPercentage
        {
            get
            {
                return dPBarTopLowPercentage;
            }

            set
            {
                dPBarTopLowPercentage = value;
                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(66.0), Description("max value for medium percentage range")]
        public double _PBarTopMediumPercentage
        {
            get
            {
                return dPBarTopMediumPercentage;
            }

            set
            {
                dPBarTopMediumPercentage = value;
                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(true), DefaultValue(0.0), Description("percentage value")]
        public double _PBarPercentageValue
        {
            get
            {
                return dPBarPercentageValue;
            }

            set
            {
                dPBarPercentageValue = value;
                strPBarUserText = "";
                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        [Browsable(false), DefaultValue(null), Description("percentage value with option for userdefined text")]
        public cPBPercentageValue _PBarPercentageValueEx
        {
            set
            {
                try {
                    cPBPercentageValue cpbPValue = (cPBPercentageValue) value;

                    if(cpbPValue != null) {
                        dPBarPercentageValue = cpbPValue._PValue;
                        strPBarUserText = cpbPValue._PUserText;
                    }

                    if(bShowProgressBar) {
                        pbNBtn.Refresh();
                    }
                }
                catch {
                    // foo
                }
            }

            get
            {
                return new cPBPercentageValue(dPBarPercentageValue, strPBarUserText);
            }
        }

        [Browsable(true), DefaultValue(false), Description("show ~ in front of percentage")]
        public bool _PBarDisplayTilde
        {
            get
            {
                return bPBarDisplayTilde;
            }

            set
            {
                bPBarDisplayTilde = value;

                if(bShowProgressBar) {
                    if(epbTextPosition != ePBarTextPosition.NoText) {
                        pbNBtn.Refresh();
                    }
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Rectangle), "0,0,0,0"), Description("user defined progress bar position")]
        public Rectangle _PBarUserDefinedPosition
        {
            get
            {
                return rectPBarUserPosition;
            }

            set
            {
                rectPBarUserPosition = value;

                if(bShowProgressBar) {
                    pbNBtn.Refresh();
                }
            }
        }

        public NiceButton()
        {
            InitializeComponent();

            pbNBtn.Image = Properties.Resources.field_96;

            stringFormatCaption.Alignment = StringAlignment.Center; // left / right
            stringFormatCaption.LineAlignment = StringAlignment.Center; // up / down
            // default is middle center
            stringFormatText.Alignment = StringAlignment.Center; // left / right
            stringFormatText.LineAlignment = StringAlignment.Center; // up / down

            stringFormatPBar.Alignment = StringAlignment.Center; // left / right
            stringFormatPBar.LineAlignment = StringAlignment.Center; // up / down

            return;
        }

        private void NiceButton_Load(object sender, EventArgs e)
        {
            cTextColor = this.ForeColor;
            return;
        }

        private void pbNBtn_Paint(object sender, PaintEventArgs e)
        {
            int iWidth = pbNBtn.Width;
            int iHeight = pbNBtn.Height;
            int iHeightCaption = 0;
            bool bResize = bPressed || bIsSelected;
            int iOffset = bResize ? 2 : 0;

            //System.Diagnostics.Debug.Print(this.Name);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            if(bEnabled) {
                cTextColor = this.ForeColor;
            }
            else {
                cTextColor = Color.Gray;
            }

            Color cText = ((bIsSelected && bEnabled) && cTextColorSel != Color.Transparent) ? cTextColorSel : cTextColor;
            
            // progress bar
            if(bShowProgressBar) {
                if(iWidth > 30 && iHeight > 30 && (iPBarSize > 1 || epbPosition == ePBarPosition.userdefined)) { // must be big enough
                    Rectangle rectPBar = new Rectangle();
                    bool bDrawPBar = true;
                    bool bButtomUp = false;
                    switch(epbPosition){
                        case ePBarPosition.bottom:
                            rectPBar = new Rectangle(8, (iHeight - 5) - iPBarSize, iWidth - 16, iPBarSize);
                            break;

                        case ePBarPosition.top:
                            rectPBar = new Rectangle(8, 5, iWidth - 16, iPBarSize);
                            break;

                        case ePBarPosition.left:
                            rectPBar = new Rectangle(5, 5, iPBarSize, iHeight - 10);
                            bButtomUp = true;
                            break;

                        case ePBarPosition.right:
                            rectPBar = new Rectangle((iWidth - 8) - iPBarSize, 5, iPBarSize, iHeight - 10);
                            bButtomUp = true;
                            break;

                        case ePBarPosition.userdefined:
                            bDrawPBar = false;
                            if(rectPBarUserPosition.Height > 0 && rectPBarUserPosition.Width > 0) {
                                if(rectPBarUserPosition.X >= 0 && rectPBarUserPosition.Y >= 0 && (rectPBarUserPosition.X + rectPBarUserPosition.Width <= iWidth) && (rectPBarUserPosition.Y + rectPBarUserPosition.Height <= iHeight)) {
                                    rectPBar = rectPBarUserPosition;
                                    bDrawPBar = true;
                                    bButtomUp = rectPBarUserPosition.Height > rectPBarUserPosition.Width;
                                }
                            }
                            break;
                    }
                    if(bDrawPBar) {
                        e.Graphics.DrawRectangle(new Pen(Color.Black, 1), rectPBar);
                        if(dPBarPercentageValue > 0) {
                            // color
                            Color cBar = cPBarFillLow;
                            if(dPBarTopLowPercentage > 0 && dPBarPercentageValue > dPBarTopLowPercentage) {
                                if(dPBarTopMediumPercentage >= dPBarTopLowPercentage && dPBarPercentageValue > dPBarTopMediumPercentage) {
                                    cBar = cPBarFillHigh;
                                }
                                else {
                                    cBar = cPBarFillMedium;
                                }
                            }

                            // position
                            double dValue = Math.Min(dPBarPercentageValue, 100.0d);
                            if(bButtomUp) { // vertical
                                double dPBarHeight = rectPBar.Height - 1;
                                dPBarHeight = (dValue * dPBarHeight) / 100.0d;
                                int iTo = (int) Math.Ceiling(dPBarHeight);
                                e.Graphics.FillRectangle(new SolidBrush(cBar), rectPBar.X + 1, (rectPBar.Y + 1) + ((rectPBar.Height - 1) - iTo), rectPBar.Width - 1, iTo);
                            }
                            else { // horizontal
                                double dPBarWith = rectPBar.Width - 1;
                                dPBarWith = (dValue * dPBarWith) / 100.0d;
                                e.Graphics.FillRectangle(new SolidBrush(cBar), rectPBar.X + 1, rectPBar.Y + 1, (int) Math.Ceiling(dPBarWith), rectPBar.Height - 1);
                            }
                        }

                        // text
                        if(epbTextPosition != ePBarTextPosition.NoText && iPBarFontSize > 1) {
                            string strPBarText = (strPBarUserText.Length > 0 ? strPBarUserText : ((bPBarDisplayTilde ? "~ " : "") + dPBarPercentageValue.ToString("0") + "%"));
                            e.Graphics.DrawString(strPBarText, new Font("Arial", iPBarFontSize, FontStyle.Bold), new System.Drawing.SolidBrush(cPBarTextColor), rectPBar, stringFormatPBar);
                        }
                    }
                }
            }


            // icon
            if(bIconSet){
                if(bResize) {
                    float fFactor = (((iFontSize - 1) * 100) / (float) iFontSize) * 0.01F;
                    int iWidthNew = (int) Math.Round((szImgIconSize.Width * fFactor));
                    e.Graphics.DrawImage(iImgIcon, ptImgIconPos.X + ((szImgIconSize.Width - iWidthNew) / 2), ptImgIconPos.Y, iWidthNew, (int) Math.Round((szImgIconSize.Height * fFactor)));
                }
                else {
                    e.Graphics.DrawImage(iImgIcon, ptImgIconPos.X, ptImgIconPos.Y, szImgIconSize.Width, szImgIconSize.Height);
                }
            }

            // caption
            if(bCaptionSet) {
                myFont = new Font("Arial", /*bResize ? iFontSizeCaption - 1 : */ iFontSizeCaption, FontStyle.Bold);
                SizeF sf = e.Graphics.MeasureString(strCaptionText, myFont);
                if(bResize) { // to prevent caption moving up and down! we always want the caption at the same "spot"
                    myFont = new Font("Arial", iFontSizeCaption - 1, FontStyle.Bold);
                }
                iHeightCaption = (int)(sf.Height); // +2
                MyRect = new Rectangle(0, 6 + (iOffset / 2), iWidth, iHeightCaption);
                e.Graphics.DrawString(strCaptionText, myFont, new System.Drawing.SolidBrush(cText), MyRect, stringFormatCaption);
                myFont.Dispose();
            }

            // button text
            if(bTextSet) {
                int iHeightText = iHeight - iHeightCaption;
                myFont = new Font("Arial", bResize ? iFontSize - 1 : iFontSize, FontStyle.Bold);
                MyRect = new Rectangle(0, iHeightCaption + (iOffset * (stringFormatText.LineAlignment == StringAlignment.Far ? -1 : 1)), iWidth, iHeightText);
                e.Graphics.DrawString(strButtonText, myFont, new System.Drawing.SolidBrush(cText), MyRect, stringFormatText);
                myFont.Dispose();
            }

            // x-over text
            if(bXOverSet) {
                myFont = new Font("Arial", bResize ? iFontSizeXOverText - 2 : iFontSizeXOverText, FontStyle.Bold);
                e.Graphics.TranslateTransform(iWidth / 2, iHeight / 2);
                e.Graphics.RotateTransform((float) sXOverAngle); // angle
                SizeF sf = e.Graphics.MeasureString(strXOverText, myFont);
                e.Graphics.DrawString(strXOverText, myFont, new System.Drawing.SolidBrush(cTextColorXOver), -(sf.Width / 2), -(sf.Height / 2));
                myFont.Dispose();
                e.Graphics.ResetTransform();
            }

            // frame around when pressed
            if(bResize) {
                Pen pPressed = new Pen(Color.Black, 1);
                e.Graphics.DrawRectangle(pPressed, new Rectangle(0, 0, iWidth, iHeight));

                pPressed.Width = 2;
                e.Graphics.DrawRectangle(pPressed, new Rectangle(1, 1, iWidth - 2, iHeight - 2));

                pPressed.Color = Color.DimGray;
                e.Graphics.DrawRectangle(pPressed, new Rectangle(3, 3, iWidth - 6, iHeight - 6));
            }

            return;
        }

        private void pbNBtn_Click(object sender, EventArgs e)
        {
            if(bEnabled) {
                if(_EvtClicked != null) {
                    _EvtClicked(this);
                }


                //call click event on base usercontrol - can eventually get rid of _EvtClicked
                base.OnClick(e);
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
            base.OnMouseDown(e);
            if(bEnabled) {
                bPressed = true;
                pbNBtn.Refresh();
            }
            return;
        }

        private void pbNBtn_MouseUp(object sender, MouseEventArgs e)
        {
            bPressed = false;
            pbNBtn.Refresh();
            return;
        }
    }
}
