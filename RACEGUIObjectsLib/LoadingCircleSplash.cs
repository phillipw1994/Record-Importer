using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RaceGUIObjectsLib
{
    public partial class LoadingCircleSplash : Form
    {
        private static Thread tSplash = null;
        public static bool bSplash = false;
        public static bool bSplashRunning = false;

        internal struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        private LoadingCircleSplash(LoadingCircle.StylePresets lcsStyle, Color cCircleColor, string strCaption)
        {
            InitializeComponent();
            lcSplash.StylePreset = lcsStyle;
            lcSplash.Color = cCircleColor;
            lblLCSCaption.Text = strCaption;
            if(lblLCSCaption.Text.Length == 0) {
                lblLCSCaption.Visible = false;
                lcSplash.Left = lblLCSCaption.Left;
                this.Width = lcSplash.Width + 12;
            }
            return;
        }

        private void LoadingCircleControlEx_Shown(object sender, EventArgs e)
        {
            lcSplash.Visible = true;
            lcSplash.Active = true;

            bSplashRunning = true;

            while(bSplash) {
                Application.DoEvents();
                Application.DoEvents();
            }

            lcSplash.Active = false;
            lcSplash.Visible = false;
            this.Close();
            return;
        }

        // statics

        private static void ShowSplashWait(POINT pPos, LoadingCircle.StylePresets lcsStyle, Color cCircleColor, string strCaption)
        {
            LoadingCircleSplash swHandle = new LoadingCircleSplash(lcsStyle, cCircleColor, strCaption); // only valid as long as the thread is alife
            swHandle.Top = pPos.Y;
            swHandle.Left = pPos.X;
            bSplash = true;
            // show the frame
            swHandle.ShowDialog();
            swHandle.Dispose();
            swHandle = null;
            return;
        }

        public static void StartSplashWait(int iPosLeft, int iPosTop, LoadingCircle.StylePresets lcsStyle = LoadingCircle.StylePresets.Race, string strColorName = "CornflowerBlue", string strCaption = "Please wait...")
        {
            POINT pPos = new POINT(iPosLeft, iPosTop);
            Color cCircleColor = Color.FromName(strColorName);
            if(strCaption == "none") {
                strCaption = "";
            }
            bSplashRunning = false;
            // check if its running...if yes, we stop it!
            if(tSplash != null) StopSplashWait();
            tSplash = new Thread(() => { ShowSplashWait(pPos, lcsStyle, cCircleColor, strCaption); }); // set up the thread (call)
            tSplash.IsBackground = true;
            tSplash.SetApartmentState(ApartmentState.STA);
            tSplash.Start();
            while(!bSplashRunning) { // we give the "handle" back after splash screen is shown
                Application.DoEvents();
            }
            return;
        }

        public static void StopSplashWait()
        {
            // we are done...clean up!
            if(tSplash != null) {
                if(bSplash) {
                    bSplash = false;
                    tSplash.Join();
                }
                tSplash = null;
            }
            return;
        }
    }
}