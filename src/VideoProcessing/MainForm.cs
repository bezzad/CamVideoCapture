using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
//using System.Threading.Tasks;

namespace VideoProcessing
{
    public partial class MainForm : Form
    {
        private List<CameraFigure> cameraFigures = new List<CameraFigure>();
        private FilterInfoCollection videoDevices;
        public MainForm()
        {
            InitializeComponent();
            //
            // Set Process Priority to High Processing
            //
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;

            InitializeVideoDevice();
        }

        private void CreateVideoFigure(AForge.Video.DirectShow.VideoCaptureDevice videoSource)
        {
            try
            {
                ViewOriginalVideo(videoSource);
                ViewAppliedFilterVideo(videoSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
            #region AggregateException catch for .NET 4 or higher
            /*
            catch (AggregateException ex)
            {
                foreach (var v in ex.InnerExceptions)
                    MessageBox.Show(ex.Message + " " + v.Message);
            }
            */
            #endregion
        }

        private void InitializeVideoDevice()
        {
            camerasCombo.Items.Clear();
            //
            // Find any video device and add to combo box
            //
            try
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                // add all devices to combo
                foreach (FilterInfo device in videoDevices)
                {
                    camerasCombo.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                camerasCombo.Items.Add("No local capture devices");
                videoDevices = null;
            }
            finally
            {
                camerasCombo.SelectedIndex = 0;
            }
        }

        private void ViewAppliedFilterVideo(VideoCaptureDevice videoSource)
        {
            CameraFigure AppliedFilterCameraFigureForm = new CameraFigure(videoSource, "Applied Video Filters");
            //
            // Add and show applied filters...
            //
            // Motion Alarm
            if (chbMotion.Checked) AppliedFilterCameraFigureForm.MotionAlarm = true;
            //
            // Salt and Pepper Noise
            if (chbSaltPepperNoise.Checked) AppliedFilterCameraFigureForm.SaltAndPepperNoise = true;
            //
            // Euclidean Color Filtering
            if (chbEuclideanColorFiltering.Checked) AppliedFilterCameraFigureForm.EuclideanColorFiltering = true;
            //
            // Object Detection
            if (chbObjDetection.Checked) AppliedFilterCameraFigureForm.ObjectDetection = true;
            //
            // Negative
            if (chbNegative.Checked) AppliedFilterCameraFigureForm.Negative = true;
            //
            // Histogram Equalization
            if (chbHistogramEqualization.Checked) AppliedFilterCameraFigureForm.HistogramEqualization = true;
            //
            // RGB to Gray
            if (chbRGB2Gray.Checked) AppliedFilterCameraFigureForm.RGB2Gray = true;
            //
            // Add Moth
            if (chbMOTH.Checked) AppliedFilterCameraFigureForm.Moth = true;
            //
            // Show by top features...
            //
            AppliedFilterCameraFigureForm.Show();
            cameraFigures.Add(AppliedFilterCameraFigureForm);
        }
        
        private void ViewOriginalVideo(VideoCaptureDevice videoSource)
        {
            this.vspOriginalVideo.VideoSource = videoSource;

            this.vspOriginalVideo.SignalToStop();
            this.vspOriginalVideo.WaitForStop();
            this.vspOriginalVideo.Start();
        }
        
        private void vspOriginalVideo_NewFrame(object sender, ref Bitmap image)
        {
            grbView.BeginInvoke(new Action(delegate
            {
                grbView.Text = string.Format(" Original View in (QVGA 320×240) Resulation ----- FPS={0} ", CalculateFrameRate().ToString());
            }));
        }
        
        
        #region Basic Frame Counter (FPS)
        private int frameRate = 0;
        private int lastFrameRate = 1;
        private int lastTick = System.Environment.TickCount;
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

        #region Controls Events
        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ImageFilters.FilterColor = colorDialog.Color;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            foreach (var anyForm in cameraFigures)
                anyForm.Close();

            this.vspOriginalVideo.SignalToStop();
            this.vspOriginalVideo.WaitForStop();
            this.vspOriginalVideo.Stop();

            cameraFigures.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitializeVideoDevice();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //
            // Check the video capture device connected to PC or no?
            //
            if (camerasCombo.Text == "No local capture devices")
            {
                MessageBox.Show("Please connect a capture devices to your PC!", "No local capture devices");
                return;
            }
            //
            // Connect to selected video capture device...
            //
            AForge.Video.DirectShow.VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[camerasCombo.SelectedIndex].MonikerString);
            //
            // Create new video figure
            //
            CreateVideoFigure(videoSource);
            //
            //
            //
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDisconnect_Click(sender, e);
        }

        private void numMinHeightOfObj_ValueChanged(object sender, EventArgs e)
        {
            ImageFilters.BlobCounterFilter.MinHeight = Convert.ToInt32(numMinHeightOfObj.Value);
        }

        private void numMinWidthOfObj_ValueChanged(object sender, EventArgs e)
        {
            ImageFilters.BlobCounterFilter.MinWidth = Convert.ToInt32(numMinWidthOfObj.Value);
        }

        private void numRange_ValueChanged(object sender, EventArgs e)
        {
            ImageFilters.Range = Convert.ToInt32(numRange.Value);
        }
        #endregion
    }
}
