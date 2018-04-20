using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace VideoProcessing
{
    public partial class CameraFigure : Form
    {
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;

        public bool SaltAndPepperNoise = false;
        public bool EuclideanColorFiltering = false;
        public bool ObjectDetection = false;
        public bool Negative = false;
        public bool HistogramEqualization = false;
        public bool RGB2Gray = false;
        public bool MotionAlarm = false;
        public bool Moth = false;

        private Point MouseLocationOnImage = new Point(0, 0);

        private bool isRunning = false;
        public bool IsRunning { get { return isRunning; } } // ReadOnly Field

        private Size imageSize = new Size(0, 0);
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;

                this.BeginInvoke(new Action(delegate { this.Size = new Size(imageSize.Width + 17, imageSize.Height + 39); }));
            }
        }


        public CameraFigure(AForge.Video.DirectShow.VideoCaptureDevice videoSource, string formText)
        {
            /// Set current Process Affinity: (I want this process work only of the first 1 available processors)
            ///
            /// Bitmask                  Binary value               Eligible processors
            /// 0x0001                   00000000 00000001          1
            /// 0x0003                   00000000 00000011          1 and 2
            /// 0x0007                   00000000 00000111          1, 2 and 3
            /// 0x0009                   00000000 00001001          1 and 4
            /// 0x007F                   00000000 01111111          1, 2, 3, 4, 5, 6 and 7
            /// 
            Process Proc = Process.GetCurrentProcess();
            long AffinityMask = (long)Proc.ProcessorAffinity;
            AffinityMask &= 0x0001; // use only any of the first 1 available processors
            Proc.ProcessorAffinity = (IntPtr)AffinityMask;
            //
            InitializeComponent();
            InitializeVideoSourcePlayer(videoSource);
            //
            this.Text = formText;
            this.SizeChanged += CameraFigure_SizeChanged;
            this.FormClosing += CameraFigure_FormClosing;

            this.videoSourcePlayer.SignalToStop();
            this.videoSourcePlayer.WaitForStop();
            this.videoSourcePlayer.Start();
            this.isRunning = true;



            this.videoSourcePlayer.MouseClick += videoSourcePlayer_MouseClick;
        }

        void CameraFigure_SizeChanged(object sender, EventArgs e)
        {
            this.Text = string.Format("Applied Video Filters {0}", this.Size.ToString());
        }

        void videoSourcePlayer_MouseClick(object sender, MouseEventArgs e)
        {
            MouseLocationOnImage = getMouseLocationOnImage(videoSourcePlayer.Size, ImageSize, e.Location);
        }

        public Point getMouseLocationOnImage(Size imagePanelSize, Size imageOriginalSize, Point MouseLocationOnPanel)
        {
            double RatioX = (double)imagePanelSize.Width / (double)imageOriginalSize.Width;
            double RatioY = (double)imagePanelSize.Height / (double)imageOriginalSize.Height;

            return new Point((int)(MouseLocationOnPanel.X / RatioX), (int)(MouseLocationOnPanel.Y / RatioY));
        }

        void CameraFigure_FormClosing(object sender, FormClosingEventArgs e)
        {
        repeatDispose:
            this.videoSourcePlayer.Dispose();

            if (this.videoSourcePlayer.IsRunning)
            {
                this.isRunning = false;
                //this.videoSourcePlayer.SignalToStop();
                //this.videoSourcePlayer.WaitForStop();
                //this.videoSourcePlayer.Stop();
            }

            if (!this.videoSourcePlayer.IsDisposed)
                goto repeatDispose;
        }

        void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            if (!IsRunning) return;

            Bitmap mImage = (Bitmap)image.Clone();
            List<string> filtersName = new List<string>();
            ///
            /// Note: Priority for implementation of filters is important,
            ///       because the original frame changes have negative effects on the performance of the another filters.
            //
            if(MotionAlarm)
            {
                mImage = mImage.MotionAlarm();
                filtersName.Add("Motion Alarm");
            }
            if (ObjectDetection)
            {
                ImageFilters.ObjectDetection(ref mImage);
                filtersName.Add("Object Detection");
            }
            if (SaltAndPepperNoise)
            {
                mImage.SaltAndPepperNoise(5);
                filtersName.Add("Salt and Pepper Noise");
            }
            if (EuclideanColorFiltering)
            {
                mImage.EuclideanColorFiltering();
                filtersName.Add("Euclidean Color Filtering");
            }
            if (Negative)
            {
                mImage.Negative();
                filtersName.Add("Negative");
            }
            if (HistogramEqualization)
            {
                mImage.HistogramEqualization();
                filtersName.Add("Histogram Equalization");
            }
            if(RGB2Gray)
            {
                ImageFilters.RGB2Gray(ref mImage);
                filtersName.Add("RGB to Gray");
            }
            if(Moth)
            {
                ImageFilters.AddMoth(ref mImage, MouseLocationOnImage);
                filtersName.Add("Add Moth");
            }

            ImageDrawStrings(ref mImage, filtersName);

            image = mImage;

            if (ImageSize.Height == 0 || ImageSize.Width == 0) ImageSize = image.Size;
        }
        void ImageDrawStrings(ref Bitmap image, List<string> lstString)
        {
            float stepSpace = 22;
            PointF textLocation = new PointF(0, 0);
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            Font font = new Font(FontFamily.Families[0], 15, FontStyle.Bold);
            Size drawSize = image.Size;

            using (Graphics graphic = Graphics.FromImage(image))
            {
                
                // Draw Image Size like this:    {Width=800, Height=600}
                graphic.DrawString(drawSize.ToString(), font, brush, textLocation); textLocation.Y += stepSpace;

                // Draw Image Resulation Standards Name like this:   QVGA
                string[] displayStandardNames = drawSize.getDisplayStandards().Select(x => x.Name).ToArray();
                for (int i = 0; i < displayStandardNames.Count(); i++)
                {
                    if (i == displayStandardNames.Count() - 1) // if the element is last object
                        graphic.DrawString(displayStandardNames[i], font, brush, textLocation);
                    else // if have other element after this object
                        graphic.DrawString(displayStandardNames[i] + Environment.NewLine, font, brush, textLocation);
                 
                    textLocation.Y += stepSpace;
                }
                

                // Draw Frame Per Second rates like this: FPS:   25
                graphic.DrawString("FPS: " + CalculateFrameRate().ToString(), font, brush, textLocation); textLocation.Y += stepSpace;

                // if used from filters, draw filter name...
                if (lstString.Count > 0)
                {
                    // Draw this:   'Applied Filters:'
                    graphic.DrawString("Applied Filters:", font, brush, textLocation); textLocation.X += 120;

                    // Draw any used filters name like this:       Salt and Pepper Noise
                    //                                             Euclidean Color Filtering
                    //                                             ...
                    foreach (string filterName in lstString)
                    { graphic.DrawString(filterName, font, brush, textLocation); textLocation.Y += stepSpace; }
                }
            }
        }
        
        void InitializeVideoSourcePlayer(AForge.Video.DirectShow.VideoCaptureDevice videoSource)
        {
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoSourcePlayer.Location = new System.Drawing.Point(0, 0);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(400, 400);
            this.videoSourcePlayer.TabIndex = 0;
            this.videoSourcePlayer.Text = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = videoSource;
            this.videoSourcePlayer.NewFrame += videoSourcePlayer_NewFrame;
            //
            // Add to client form
            //
            this.Controls.Add(this.videoSourcePlayer);
        }

        #region Basic Frame Counter (FPS)
        private int lastTick = System.Environment.TickCount;
        private int lastFrameRate = 1;
        private int frameRate = 0;

        public int CalculateFrameRate()
        {
            if (System.Environment.TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = System.Environment.TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        #endregion
    }

    
}
