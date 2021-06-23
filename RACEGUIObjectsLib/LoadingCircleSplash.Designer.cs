namespace RaceGUIObjectsLib
{
    partial class LoadingCircleSplash
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLCSCaption = new System.Windows.Forms.Label();
            this.lcSplash = new RaceGUIObjectsLib.LoadingCircle();
            this.SuspendLayout();
            // 
            // lblLCSCaption
            // 
            this.lblLCSCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLCSCaption.Location = new System.Drawing.Point(12, 23);
            this.lblLCSCaption.Name = "lblLCSCaption";
            this.lblLCSCaption.Size = new System.Drawing.Size(91, 17);
            this.lblLCSCaption.TabIndex = 1;
            this.lblLCSCaption.Text = "Please wait...";
            this.lblLCSCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLCSCaption.UseWaitCursor = true;
            // 
            // lcSplash
            // 
            this.lcSplash.Active = false;
            this.lcSplash.Color = System.Drawing.Color.CornflowerBlue;
            this.lcSplash.InnerCircleRadius = 8;
            this.lcSplash.Location = new System.Drawing.Point(99, 9);
            this.lcSplash.Name = "lcSplash";
            this.lcSplash.NumberSpoke = 22;
            this.lcSplash.OuterCircleRadius = 9;
            this.lcSplash.RotationSpeed = 100;
            this.lcSplash.Size = new System.Drawing.Size(48, 45);
            this.lcSplash.SpokeThickness = 4;
            this.lcSplash.StylePreset = RaceGUIObjectsLib.LoadingCircle.StylePresets.Race;
            this.lcSplash.TabIndex = 2;
            this.lcSplash.Text = "loadingCircle1";
            this.lcSplash.UseWaitCursor = true;
            // 
            // LoadingCircleSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(154, 59);
            this.ControlBox = false;
            this.Controls.Add(this.lcSplash);
            this.Controls.Add(this.lblLCSCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingCircleSplash";
            this.Opacity = 0.97D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.Shown += new System.EventHandler(this.LoadingCircleControlEx_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLCSCaption;
        private RaceGUIObjectsLib.LoadingCircle lcSplash;
    }
}