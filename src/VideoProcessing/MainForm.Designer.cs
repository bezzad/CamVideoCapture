namespace VideoProcessing
{
    partial class MainForm
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
            this.grbConnection = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.camerasCombo = new System.Windows.Forms.ComboBox();
            this.grbFilters = new System.Windows.Forms.GroupBox();
            this.chbRGB2Gray = new System.Windows.Forms.CheckBox();
            this.chbObjDetection = new System.Windows.Forms.CheckBox();
            this.chbNegative = new System.Windows.Forms.CheckBox();
            this.chbMotion = new System.Windows.Forms.CheckBox();
            this.chbHistogramEqualization = new System.Windows.Forms.CheckBox();
            this.chbSaltPepperNoise = new System.Windows.Forms.CheckBox();
            this.chbEuclideanColorFiltering = new System.Windows.Forms.CheckBox();
            this.grbDefineObjects = new System.Windows.Forms.GroupBox();
            this.numMinWidthOfObj = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numMinHeightOfObj = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numRange = new System.Windows.Forms.NumericUpDown();
            this.btnColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grbView = new System.Windows.Forms.GroupBox();
            this.vspOriginalVideo = new AForge.Controls.VideoSourcePlayer();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chbMOTH = new System.Windows.Forms.CheckBox();
            this.grbConnection.SuspendLayout();
            this.grbFilters.SuspendLayout();
            this.grbDefineObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinWidthOfObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHeightOfObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRange)).BeginInit();
            this.grbView.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbConnection
            // 
            this.grbConnection.Controls.Add(this.btnRefresh);
            this.grbConnection.Controls.Add(this.btnDisconnect);
            this.grbConnection.Controls.Add(this.btnStart);
            this.grbConnection.Controls.Add(this.camerasCombo);
            this.grbConnection.Location = new System.Drawing.Point(12, 12);
            this.grbConnection.Name = "grbConnection";
            this.grbConnection.Size = new System.Drawing.Size(332, 84);
            this.grbConnection.TabIndex = 0;
            this.grbConnection.TabStop = false;
            this.grbConnection.Text = "Connection";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisconnect.Location = new System.Drawing.Point(188, 46);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(138, 32);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "&Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Location = new System.Drawing.Point(6, 46);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(138, 32);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "&START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // camerasCombo
            // 
            this.camerasCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camerasCombo.FormattingEnabled = true;
            this.camerasCombo.Location = new System.Drawing.Point(6, 19);
            this.camerasCombo.Name = "camerasCombo";
            this.camerasCombo.Size = new System.Drawing.Size(320, 21);
            this.camerasCombo.TabIndex = 1;
            // 
            // grbFilters
            // 
            this.grbFilters.Controls.Add(this.chbMOTH);
            this.grbFilters.Controls.Add(this.chbRGB2Gray);
            this.grbFilters.Controls.Add(this.chbObjDetection);
            this.grbFilters.Controls.Add(this.chbNegative);
            this.grbFilters.Controls.Add(this.chbMotion);
            this.grbFilters.Controls.Add(this.chbHistogramEqualization);
            this.grbFilters.Controls.Add(this.chbSaltPepperNoise);
            this.grbFilters.Controls.Add(this.chbEuclideanColorFiltering);
            this.grbFilters.Location = new System.Drawing.Point(12, 240);
            this.grbFilters.Name = "grbFilters";
            this.grbFilters.Size = new System.Drawing.Size(332, 115);
            this.grbFilters.TabIndex = 1;
            this.grbFilters.TabStop = false;
            this.grbFilters.Text = "Filter";
            // 
            // chbRGB2Gray
            // 
            this.chbRGB2Gray.AutoSize = true;
            this.chbRGB2Gray.Location = new System.Drawing.Point(6, 88);
            this.chbRGB2Gray.Name = "chbRGB2Gray";
            this.chbRGB2Gray.Size = new System.Drawing.Size(86, 17);
            this.chbRGB2Gray.TabIndex = 1;
            this.chbRGB2Gray.Text = "RGB to Gray";
            this.chbRGB2Gray.UseVisualStyleBackColor = true;
            // 
            // chbObjDetection
            // 
            this.chbObjDetection.AutoSize = true;
            this.chbObjDetection.Location = new System.Drawing.Point(220, 42);
            this.chbObjDetection.Name = "chbObjDetection";
            this.chbObjDetection.Size = new System.Drawing.Size(106, 17);
            this.chbObjDetection.TabIndex = 0;
            this.chbObjDetection.Text = "Object Detection";
            this.chbObjDetection.UseVisualStyleBackColor = true;
            // 
            // chbNegative
            // 
            this.chbNegative.AutoSize = true;
            this.chbNegative.Location = new System.Drawing.Point(220, 65);
            this.chbNegative.Name = "chbNegative";
            this.chbNegative.Size = new System.Drawing.Size(69, 17);
            this.chbNegative.TabIndex = 0;
            this.chbNegative.Text = "Negative";
            this.chbNegative.UseVisualStyleBackColor = true;
            // 
            // chbMotion
            // 
            this.chbMotion.AutoSize = true;
            this.chbMotion.Location = new System.Drawing.Point(220, 19);
            this.chbMotion.Name = "chbMotion";
            this.chbMotion.Size = new System.Drawing.Size(87, 17);
            this.chbMotion.TabIndex = 0;
            this.chbMotion.Text = "Motion Alarm";
            this.chbMotion.UseVisualStyleBackColor = true;
            // 
            // chbHistogramEqualization
            // 
            this.chbHistogramEqualization.AutoSize = true;
            this.chbHistogramEqualization.Location = new System.Drawing.Point(6, 65);
            this.chbHistogramEqualization.Name = "chbHistogramEqualization";
            this.chbHistogramEqualization.Size = new System.Drawing.Size(133, 17);
            this.chbHistogramEqualization.TabIndex = 0;
            this.chbHistogramEqualization.Text = "Histogram Equalization";
            this.chbHistogramEqualization.UseVisualStyleBackColor = true;
            // 
            // chbSaltPepperNoise
            // 
            this.chbSaltPepperNoise.AutoSize = true;
            this.chbSaltPepperNoise.Location = new System.Drawing.Point(6, 42);
            this.chbSaltPepperNoise.Name = "chbSaltPepperNoise";
            this.chbSaltPepperNoise.Size = new System.Drawing.Size(126, 17);
            this.chbSaltPepperNoise.TabIndex = 0;
            this.chbSaltPepperNoise.Text = "Salt an Pepper Noise";
            this.chbSaltPepperNoise.UseVisualStyleBackColor = true;
            // 
            // chbEuclideanColorFiltering
            // 
            this.chbEuclideanColorFiltering.AutoSize = true;
            this.chbEuclideanColorFiltering.Location = new System.Drawing.Point(6, 19);
            this.chbEuclideanColorFiltering.Name = "chbEuclideanColorFiltering";
            this.chbEuclideanColorFiltering.Size = new System.Drawing.Size(139, 17);
            this.chbEuclideanColorFiltering.TabIndex = 0;
            this.chbEuclideanColorFiltering.Text = "Euclidean Color Filtering";
            this.chbEuclideanColorFiltering.UseVisualStyleBackColor = true;
            // 
            // grbDefineObjects
            // 
            this.grbDefineObjects.Controls.Add(this.numMinWidthOfObj);
            this.grbDefineObjects.Controls.Add(this.label3);
            this.grbDefineObjects.Controls.Add(this.numMinHeightOfObj);
            this.grbDefineObjects.Controls.Add(this.label2);
            this.grbDefineObjects.Controls.Add(this.numRange);
            this.grbDefineObjects.Controls.Add(this.btnColor);
            this.grbDefineObjects.Controls.Add(this.label1);
            this.grbDefineObjects.Location = new System.Drawing.Point(12, 102);
            this.grbDefineObjects.Name = "grbDefineObjects";
            this.grbDefineObjects.Size = new System.Drawing.Size(332, 132);
            this.grbDefineObjects.TabIndex = 1;
            this.grbDefineObjects.TabStop = false;
            this.grbDefineObjects.Text = "Define Objects";
            // 
            // numMinWidthOfObj
            // 
            this.numMinWidthOfObj.Location = new System.Drawing.Point(208, 97);
            this.numMinWidthOfObj.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numMinWidthOfObj.Name = "numMinWidthOfObj";
            this.numMinWidthOfObj.Size = new System.Drawing.Size(48, 20);
            this.numMinWidthOfObj.TabIndex = 14;
            this.numMinWidthOfObj.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMinWidthOfObj.ValueChanged += new System.EventHandler(this.numMinWidthOfObj_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Min Width of Object";
            // 
            // numMinHeightOfObj
            // 
            this.numMinHeightOfObj.Location = new System.Drawing.Point(208, 71);
            this.numMinHeightOfObj.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numMinHeightOfObj.Name = "numMinHeightOfObj";
            this.numMinHeightOfObj.Size = new System.Drawing.Size(48, 20);
            this.numMinHeightOfObj.TabIndex = 12;
            this.numMinHeightOfObj.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMinHeightOfObj.ValueChanged += new System.EventHandler(this.numMinHeightOfObj_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Min Height of Object";
            // 
            // numRange
            // 
            this.numRange.Location = new System.Drawing.Point(136, 45);
            this.numRange.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRange.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRange.Name = "numRange";
            this.numRange.Size = new System.Drawing.Size(120, 20);
            this.numRange.TabIndex = 10;
            this.numRange.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.numRange.ValueChanged += new System.EventHandler(this.numRange_ValueChanged);
            // 
            // btnColor
            // 
            this.btnColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnColor.Location = new System.Drawing.Point(77, 16);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(179, 23);
            this.btnColor.TabIndex = 9;
            this.btnColor.Text = "&Press For Select Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Range";
            // 
            // grbView
            // 
            this.grbView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbView.Controls.Add(this.vspOriginalVideo);
            this.grbView.Location = new System.Drawing.Point(12, 361);
            this.grbView.Name = "grbView";
            this.grbView.Size = new System.Drawing.Size(332, 265);
            this.grbView.TabIndex = 15;
            this.grbView.TabStop = false;
            this.grbView.Text = " Original View in (QVGA 320×240) Resulation ";
            // 
            // vspOriginalVideo
            // 
            this.vspOriginalVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vspOriginalVideo.Location = new System.Drawing.Point(6, 19);
            this.vspOriginalVideo.Name = "vspOriginalVideo";
            this.vspOriginalVideo.Size = new System.Drawing.Size(320, 240);
            this.vspOriginalVideo.TabIndex = 0;
            this.vspOriginalVideo.Text = "Original Video Player";
            this.vspOriginalVideo.VideoSource = null;
            this.vspOriginalVideo.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.vspOriginalVideo_NewFrame);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackgroundImage = global::VideoProcessing.Properties.Resources.Refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(150, 46);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chbMOTH
            // 
            this.chbMOTH.AutoSize = true;
            this.chbMOTH.Location = new System.Drawing.Point(220, 88);
            this.chbMOTH.Name = "chbMOTH";
            this.chbMOTH.Size = new System.Drawing.Size(72, 17);
            this.chbMOTH.TabIndex = 2;
            this.chbMOTH.Text = "Add Moth";
            this.chbMOTH.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 638);
            this.Controls.Add(this.grbView);
            this.Controls.Add(this.grbDefineObjects);
            this.Controls.Add(this.grbFilters);
            this.Controls.Add(this.grbConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Video Processing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.grbConnection.ResumeLayout(false);
            this.grbFilters.ResumeLayout(false);
            this.grbFilters.PerformLayout();
            this.grbDefineObjects.ResumeLayout(false);
            this.grbDefineObjects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinWidthOfObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHeightOfObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRange)).EndInit();
            this.grbView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbConnection;
        private System.Windows.Forms.ComboBox camerasCombo;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grbFilters;
        private System.Windows.Forms.CheckBox chbEuclideanColorFiltering;
        private System.Windows.Forms.CheckBox chbObjDetection;
        private System.Windows.Forms.CheckBox chbMotion;
        private System.Windows.Forms.CheckBox chbSaltPepperNoise;
        private System.Windows.Forms.GroupBox grbDefineObjects;
        private System.Windows.Forms.NumericUpDown numMinWidthOfObj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMinHeightOfObj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numRange;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbNegative;
        private System.Windows.Forms.GroupBox grbView;
        private AForge.Controls.VideoSourcePlayer vspOriginalVideo;
        private System.Windows.Forms.CheckBox chbHistogramEqualization;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chbRGB2Gray;
        private System.Windows.Forms.CheckBox chbMOTH;

    }
}

