using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    public static class Util
    {
        /// <summary>
        /// defined constant for background colour of all RACE applications
        /// should be used in Form_Load() function:
        /// this.BackColor = RaceGUIObjectsLib.Util.raceBlue;
        /// All other objects on form should be transperant or set to the same color
        /// 
        /// </summary>
        public static Color raceBlue = (DateTime.Today.Month == 3 && DateTime.Today.Day == 17 ? Color.Green : (DateTime.Today.Month == 4 && DateTime.Today.Day == 1 ? Color.Purple : Color.FromArgb(47, 111, 191)));
        
        public class FormAnimation
        {
            /// <summary>
            /// Animates the window from left to right. 
            /// This flag can be used with roll or slide animation. 
            /// </summary>
            public const int AW_HOR_POSITIVE = 0X1;
            
            /// <summary>
            /// Animates the window from right to left. 
            /// This flag can be used with roll or slide animation.
            /// </summary>
            public const int AW_HOR_NEGATIVE = 0X2;
            
            /// <summary>
            /// Animates the window from top to bottom. 
            /// This flag can be used with roll or slide animation.
            /// </summary>
            public const int AW_VER_POSITIVE = 0X4;
            
            /// <summary>
            /// Animates the window from bottom to top. 
            /// This flag can be used with roll or slide animation.
            /// </summary>
            public const int AW_VER_NEGATIVE = 0X8;

            /// <summary>
            /// Makes the window appear to collapse inward 
            /// if AW_HIDE is used or expand outward if the AW_HIDE is not used.
            /// </summary>
            public const int AW_CENTER = 0X10;

            /// <summary>
            /// Hides the window. By default, the window is shown.
            /// </summary>
            public const int AW_HIDE = 0X10000;

            /// <summary>
            /// Activates the window.
            /// </summary>
            public const int AW_ACTIVATE = 0X20000;
            
            /// <summary>
            /// Uses slide animation. By default, roll animation is used.
            /// </summary>
            public const int AW_SLIDE = 0X40000;

            /// <summary>
            /// Uses a fade effect. 
            /// This flag can be used only if hwnd is a top-level window.
            /// </summary>
            public const int AW_BLEND = 0X80000;

            /// <summary>
            /// Dll function to animates a window.
            /// </summary>
            /// <param name="hwand"></param>
            /// <param name="dwTime"></param>
            /// <param name="dwFlags"></param>
            /// <returns></returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);

            /// <summary>
            /// can go into Form_Load() for animation when form is displayed
            /// </summary>
            /// <param name="frmObject">Form object to animate</param>
            /// <param name="dwTime">duration of the animation</param>
            public static void _FadeIn(Form frmObject, int dwTime = 750)
            {
                if(frmObject != null) {
                    try {
                        if(System.Environment.OSVersion.Version.Major >= 6) { // Vista or higher check (performance)
                            AnimateWindow(frmObject.Handle, dwTime, AW_BLEND | AW_ACTIVATE);
                        }
                    }
                    catch {
                        // foo
                    }
                }
                return;
            }

            /// <summary>
            /// can go in FormClosing() for animation when form is closing
            /// </summary>
            /// <param name="frmObject">Form object to animate</param>
            /// <param name="dwTime">duration of the animation</param>
            public static void _FadeOut(Form frmObject, int dwTime = 400)
            {
                if(frmObject != null) {
                    try {
                        if(System.Environment.OSVersion.Version.Major >= 6) { // Vista or higher check  (performance)
                            AnimateWindow(frmObject.Handle, dwTime, AW_BLEND | AW_HIDE);
                        }
                    }
                    catch {
                        // foo
                    }
                }
                return;
            }
        }
    }
}
