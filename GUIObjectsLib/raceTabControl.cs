using System.Drawing;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    public delegate bool PreRemoveTab(int indx);
    public class raceTabControl : TabControl
    {
        public raceTabControl() : base()
        {
            PreRemoveTabPage = null;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            return;
        }

        public PreRemoveTab PreRemoveTabPage;

        /// <summary>
        /// Draw tab
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //draw close button
            Rectangle r = e.Bounds;
            r = GetTabRect(e.Index);
            r.Offset(2, 2);
            r.Width = 5;
            r.Height = 5;
            Brush b = new SolidBrush(Color.Black);
            Pen p = new Pen(b);
            p.Color = Color.Red;
            e.Graphics.DrawLine(p, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
            e.Graphics.DrawLine(p, r.X + r.Width, r.Y, r.X, r.Y + r.Height);

            //draw tab title
            string titel = this.TabPages[e.Index].Text;
            Font f = this.Font;
            e.Graphics.DrawString(titel, f, b, new PointF(r.X + 5, r.Y));
            return;
        }

        /// <summary>
        /// Look for tab close button click
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            System.Drawing.Rectangle rectMouse = new System.Drawing.Rectangle(e.X, e.Y, 1, 1);
            for(int i = 0; i < TabCount; i++) { // go through all tabs
                Rectangle r = GetTabRect(i);
                r.Offset(2, 2);
                r.Width = 8;
                r.Height = 8;
                if(r.IntersectsWith(rectMouse)){ //if(r.Contains(p)) {
                    CloseTab(i);
                    break;
                }
            }
            return;
        }

        private void CloseTab(int i)
        {
            bool bClose = true;
            if(PreRemoveTabPage != null){
                bClose = PreRemoveTabPage(i);
            }
            if(bClose) {
                TabPages[i].Dispose(); // this has to happen!!!! Will also remove the page! / os - 23/10/2014
                //TabPages.Remove(TabPages[i]);
            }
            return;
        }
    }
}






