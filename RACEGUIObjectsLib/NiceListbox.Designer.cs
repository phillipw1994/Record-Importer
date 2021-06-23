namespace RaceGUIObjectsLib
{
    partial class NiceListbox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flpNLB_Items = new System.Windows.Forms.FlowLayoutPanel();
            this.tmAutoScroll = new System.Windows.Forms.Timer(this.components);
            this.pbNLB_Slider = new System.Windows.Forms.PictureBox();
            this.nbtnNLB_DOWN = new RaceGUIObjectsLib.NiceButton();
            this.nbtnNLB_UP = new RaceGUIObjectsLib.NiceButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbNLB_Slider)).BeginInit();
            this.SuspendLayout();
            // 
            // flpNLB_Items
            // 
            this.flpNLB_Items.AutoScroll = true;
            this.flpNLB_Items.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpNLB_Items.Location = new System.Drawing.Point(3, 3);
            this.flpNLB_Items.Name = "flpNLB_Items";
            this.flpNLB_Items.Size = new System.Drawing.Size(244, 208);
            this.flpNLB_Items.TabIndex = 2;
            this.flpNLB_Items.Resize += new System.EventHandler(this.flpNLB_Items_Resize);
            // 
            // tmAutoScroll
            // 
            this.tmAutoScroll.Interval = 1500;
            this.tmAutoScroll.Tick += new System.EventHandler(this.tmAutoScroll_Tick);
            // 
            // pbNLB_Slider
            // 
            this.pbNLB_Slider.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbNLB_Slider.BackgroundImage = global::RaceGUIObjectsLib.Properties.Resources.ScrollSlider;
            this.pbNLB_Slider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbNLB_Slider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbNLB_Slider.Location = new System.Drawing.Point(251, 44);
            this.pbNLB_Slider.Name = "pbNLB_Slider";
            this.pbNLB_Slider.Size = new System.Drawing.Size(49, 28);
            this.pbNLB_Slider.TabIndex = 3;
            this.pbNLB_Slider.TabStop = false;
            this.pbNLB_Slider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbNLB_Slider_MouseDown);
            this.pbNLB_Slider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbNLB_Slider_MouseMove);
            // 
            // nbtnNLB_DOWN
            // 
            this.nbtnNLB_DOWN._ButtonText = "DOWN";
            this.nbtnNLB_DOWN._ButtonTextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.nbtnNLB_DOWN._IconPosition = new System.Drawing.Point(14, 2);
            this.nbtnNLB_DOWN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nbtnNLB_DOWN.BackColor = System.Drawing.Color.OldLace;
            this.nbtnNLB_DOWN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nbtnNLB_DOWN.Location = new System.Drawing.Point(251, 174);
            this.nbtnNLB_DOWN.Margin = new System.Windows.Forms.Padding(1);
            this.nbtnNLB_DOWN.Name = "nbtnNLB_DOWN";
            this.nbtnNLB_DOWN.Size = new System.Drawing.Size(49, 37);
            this.nbtnNLB_DOWN.TabIndex = 1;
            this.nbtnNLB_DOWN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nbtnNLB_DOWN_MouseDown);
            // 
            // nbtnNLB_UP
            // 
            this.nbtnNLB_UP._ButtonText = "UP";
            this.nbtnNLB_UP._ButtonTextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.nbtnNLB_UP._IconPosition = new System.Drawing.Point(14, 15);
            this.nbtnNLB_UP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nbtnNLB_UP.BackColor = System.Drawing.Color.OldLace;
            this.nbtnNLB_UP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nbtnNLB_UP.Location = new System.Drawing.Point(251, 3);
            this.nbtnNLB_UP.Margin = new System.Windows.Forms.Padding(1);
            this.nbtnNLB_UP.Name = "nbtnNLB_UP";
            this.nbtnNLB_UP.Size = new System.Drawing.Size(49, 37);
            this.nbtnNLB_UP.TabIndex = 0;
            this.nbtnNLB_UP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nbtnNLB_UP_MouseDown);
            // 
            // NiceListbox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pbNLB_Slider);
            this.Controls.Add(this.flpNLB_Items);
            this.Controls.Add(this.nbtnNLB_DOWN);
            this.Controls.Add(this.nbtnNLB_UP);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MinimumSize = new System.Drawing.Size(80, 140);
            this.Name = "NiceListbox";
            this.Size = new System.Drawing.Size(300, 214);
            this.Load += new System.EventHandler(this.NiceListbox_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NiceListbox_MouseClick);
            this.Resize += new System.EventHandler(this.NiceListbox_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbNLB_Slider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private NiceButton nbtnNLB_UP;
        private NiceButton nbtnNLB_DOWN;
        private System.Windows.Forms.FlowLayoutPanel flpNLB_Items;
        private System.Windows.Forms.Timer tmAutoScroll;
        private System.Windows.Forms.PictureBox pbNLB_Slider;
    }
}
