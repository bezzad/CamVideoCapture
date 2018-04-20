namespace Motion_Detection
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.ImageList tbarImageList;
        private System.Windows.Forms.ToolBarButton tbtnStop;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            this.components = new System.ComponentModel.Container();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.tbtnStop = new System.Windows.Forms.ToolBarButton();
            this.tbarImageList = new System.Windows.Forms.ImageList(this.components);
            this.videoPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbtnStop});
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.tbarImageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(398, 42);
            this.toolBar.TabIndex = 0;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // tbtnStop
            // 
            this.tbtnStop.ImageIndex = 0;
            this.tbtnStop.Name = "tbtnStop";
            this.tbtnStop.Text = "Stop";
            this.tbtnStop.ToolTipText = "Stop Recording";
            // 
            // tbarImageList
            // 
            this.tbarImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.tbarImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.tbarImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // videoPanel
            // 
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(0, 42);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(398, 297);
            this.videoPanel.TabIndex = 1;
            this.videoPanel.Resize += new System.EventHandler(this.videoPanel_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(398, 339);
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.toolBar);
            this.Name = "MainForm";
            this.Text = "Motion Detection";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

