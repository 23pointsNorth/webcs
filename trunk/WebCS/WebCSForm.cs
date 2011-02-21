using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;
using WebCS.Properties;

namespace WebCS
{
    public partial class WebCSForm : RadForm
    {
        public WebCSForm()
        {
            InitializeComponent();
            this.Size = new Size(
                Constants.WEBCAM_ONLY_WIDTH, Constants.WEBCAM_ONLY_HEIGHT);
            DrawOnEmptyFrame("Webcam \nnot selected.");
            avaliableWebcamsDropDownList.Items.Add("Select Webcam");
            applyFilterRadCheckBox.Checked = User.Default.applyMeanFilter;
            LoadAvaliableWebcams();
            LoadMarkers();
            LoadAtStartup();
            CheckEnabledTracking();
        }

        RegistryKey regKeyApp = Registry.CurrentUser.OpenSubKey(
            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        string applicationName = "WebCS";

        private void LoadAtStartup()
        {
            // Check to see the current state (running at startup or not)
            if (regKeyApp.GetValue(applicationName) == null)
            {
                startupRadCheckBox.Checked = false; //not set to run at startup
            }
            else
            {
                startupRadCheckBox.Checked = true; //run at startup
            }
        }

        private void startupRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (startupRadCheckBox.Checked)
            {
                regKeyApp.SetValue(applicationName, Application.ExecutablePath.ToString()); //add
            }
            else
            {
                regKeyApp.DeleteValue(applicationName, false); // remove from registry
            }
        }

        private void LoadMarkers()
        {
            try
            {
                firstMarkerColor = User.Default.firstMarkerColorUser;
                secondMarkerColor = User.Default.secondMarkerColorUser;
                firstMarkerRangeRadTextBox.Text = User.Default.firstMarkerRangeUser.ToString();
                secondMarkerRangeRadTextBox.Text = User.Default.secondMarkerRangeUser.ToString();
            }
            catch
            {
                firstMarkerColor = emptyColor;
                secondMarkerColor = emptyColor;
                firstMarkerRangeRadTextBox.Text = "20";
                secondMarkerRangeRadTextBox.Text = "20";
            }
            firstMarkerSample.Image = RectangleShape.DrawFilledRectangle(
                firstMarkerSample.Width, firstMarkerSample.Height, firstMarkerColor);
            secondMarkerSample.Image = RectangleShape.DrawFilledRectangle(
                secondMarkerSample.Width, secondMarkerSample.Height, secondMarkerColor);
        }

        private void DrawOnEmptyFrame(string text)
        {
            Bitmap emptyBitmap = new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);
            using (Graphics g = Graphics.FromImage(emptyBitmap))
            {
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Center;
                strFormat.LineAlignment = StringAlignment.Center;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(text, new Font("Arial", 20), Brushes.Black,
                    new RectangleF(0, 0, 352, 288), strFormat);
            }
            imageContainer.Image = emptyBitmap;
        }

        private Bitmap drawRectangleOnBitmap(Bitmap image, Rectangle rect, Pen pen)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawRectangle(pen, rect);
            }
            return image;
        }

        private FilterInfoCollection videoCaptureDevices;
        public VideoCaptureDevice finalVideoSource;

        private void LoadAvaliableWebcams()
        {
            //find all avaliable webcams
            videoCaptureDevices = new FilterInfoCollection(
                FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoCaptureDevice in videoCaptureDevices)
            {
                avaliableWebcamsDropDownList.Items.Add(videoCaptureDevice.Name);
            }

            try
            {
                avaliableWebcamsDropDownList.SelectedText = User.Default.loadWebcamName;
                avaliableWebcamsDropDownList_SelectedIndexChanged(null, null);
            }
            catch
            {
                avaliableWebcamsDropDownList.SelectedIndex = 0;
            }
        }

        private void StopWebcam()
        {
            try
            {
                if (finalVideoSource.IsRunning)
                {
                    finalVideoSource.Stop();
                }
            }
            catch (NullReferenceException e1)
            {
                userRadLabel.Text = "Video not started. " + e1.Message;
            }
        }

        public Bitmap ResizeBitmap(Bitmap toResize, int newWidth, int newHeight)
        {
            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics graphic = Graphics.FromImage((System.Drawing.Image)result))
                graphic.DrawImage(toResize, 0, 0, newWidth, newHeight);
            return result;
        }

        private void exitRadButton_Click(object sender, EventArgs e)
        {
            StopWebcam();
            Environment.Exit(1);
        }

        private void optionsToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                optionsRadPanel.Show();
                this.Size = new Size(
                    Constants.OPTIONS_ADDED_WIDTH, Constants.OPTIONS_ADDED_HEIGHT);            
            }
            else
            {
                optionsRadPanel.Hide();
                this.Size = new Size(
                    Constants.WEBCAM_ONLY_WIDTH, Constants.WEBCAM_ONLY_HEIGHT);
            }
        }

        private void trackingToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                trackingToggleButton.Text = "Disable Tracking";
            }
            else
            {
                trackingToggleButton.Text = "Enable Tracking";
                userRadLabel.ResetText();
            }
        }

        private void SelectDesktopAreaButton_Click(object sender, EventArgs e)
        {

        }

        Bitmap newFrame;
        Bitmap firstFrameClone;
        Bitmap secondFrameClone;
        //public static readonly Grayscale BT709 = new Grayscale(0.2125, 0.7154, 0.0721);
        static Color emptyColor = Color.FromArgb(0, 0, 0);
        Color firstMarkerColor = emptyColor;
        Color secondMarkerColor = emptyColor;
        Rectangle firstMarkerRect;
        Rectangle secondMarkerRect;

        private void FinalVideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //update image 
            newFrame = (Bitmap)eventArgs.Frame.Clone();
            newFrame = ResizeBitmap(newFrame, Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);

            if (applyFilterRadCheckBox.Checked)
            {
                Mean filer = new Mean();
                BitmapData objectsData = newFrame.LockBits(
                    new Rectangle(0, 0, newFrame.Width, newFrame.Height),
                    ImageLockMode.ReadOnly, newFrame.PixelFormat);
                filer.ApplyInPlace(objectsData);
                newFrame.UnlockBits(objectsData);
            }

            if (trackingToggleButton.ToggleState == ToggleState.On)
            {
                //make sure both clones contain starting bitmap
                firstFrameClone = new Bitmap(newFrame);
                secondFrameClone = new Bitmap(newFrame);
                CalculateMarker(
                    firstFrameClone, firstMarkerColor, Color.Green, 1, 
                    firstMarkerLoadRadRadioButton.IsChecked, out firstMarkerRect);
                CalculateMarker(
                    secondFrameClone, secondMarkerColor, Color.Blue, 2, 
                    secondMarkerLoadRadRadioButton.IsChecked, out secondMarkerRect);
            }

            if (firstMarkerChangeColor)
            {
                newFrame = drawRectangleOnBitmap(
                    (Bitmap)newFrame.Clone(),
                    firstMarkerGetColorRect,
                    new Pen(Color.LightGreen, 2));
            }

            if (secondMarkerChangeColor)
            {
                newFrame = drawRectangleOnBitmap(
                    (Bitmap)newFrame.Clone(),
                    secondMarkerGetColorRect,
                    new Pen(Color.LightBlue, 2));
            }

            imageContainer.Image = newFrame;
        }

        private void CalculateMarker(Bitmap frame, Color markerColor, Color rectangleColor, int rangeNum, bool loadWorkingFrame, out Rectangle markerRect)
        {
            BitmapData ObjectsData = frame.LockBits(
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    ImageLockMode.ReadOnly, frame.PixelFormat);

            // create filter
            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            // set center color and radius
            filter.CenterColor.Color = markerColor;
            filter.Radius = getRange(rangeNum);
            // apply the filter
            filter.ApplyInPlace(ObjectsData);

            // grayscaling
            //UnmanagedImage grayImage = new Grayscale.CommonAlgorithms.BT709.Apply(new UnmanagedImage(objectsData));
            UnmanagedImage grayImage = new GrayscaleBT709().Apply(
                new UnmanagedImage(ObjectsData));
            // unlock image
            frame.UnlockBits(ObjectsData);
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = 5;
            blobCounter.MinHeight = 5;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;
            blobCounter.ProcessImage(grayImage);
            //blobCounter.ExtractBlobsImage(grayImage);
            Rectangle[] rects = blobCounter.GetObjectsRectangles();
            if (rects.Length > 0)
            {
                markerRect = rects[0];
                userRadLabel.Text =
                    "(" + (markerRect.X + markerRect.Width / 2).ToString() +
                    "; " + (markerRect.Y + markerRect.Height / 2).ToString() + ") ";

                if (loadWorkingFrame)
                {
                    newFrame = (Bitmap)frame.Clone();
                }

                newFrame = drawRectangleOnBitmap(
                    newFrame, markerRect, new Pen(rectangleColor, 2));
            }
            else
            {
                markerRect = new Rectangle(new Point(0, 0), frame.Size);
            }
        }

        private short getRange(int marker)
        {
            int range = 0;
            if (marker == 1)
            {
                try
                {
                    range = int.Parse(firstMarkerRangeRadTextBox.Text);
                    range = Math.Max(0, range);
                    range = Math.Min(255, range);
                    firstMarkerRangeRadTextBox.Text = range.ToString();
                }
                catch
                {
                    firstMarkerRangeRadTextBox.Text = "0";
                }
                return (short)range;
            }
            else if (marker == 2)
            {
                try
                {
                    range = int.Parse(secondMarkerRangeRadTextBox.Text);
                    range = Math.Max(0, range);
                    range = Math.Min(255, range);
                    secondMarkerRangeRadTextBox.Text = range.ToString();
                }
                catch
                {
                    secondMarkerRangeRadTextBox.Text = "0";
                }
                return (short)range;
            }
            else return (short)range;
        }

        private void WebCSForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            StopWebcam();
            systemTrayIcon.Dispose();
        }

        bool isVideoRunning = false;
        private void WebcamRadToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                DrawOnEmptyFrame("Starting...");
                webcamRadToggleButton.Text = "Stop Webcam";
                avaliableWebcamsDropDownList.Enabled = false;
                firstMarkerChangeRadButton.Enabled = true;
                secondMarkerChangeRadButton.Enabled = true;

                //stat selected webcam
                finalVideoSource = new VideoCaptureDevice(
                    videoCaptureDevices[avaliableWebcamsDropDownList.SelectedIndex - 1].MonikerString);
                //-1 because [0] in avaliableWebcams = "Select Webcam"
                finalVideoSource.NewFrame += new NewFrameEventHandler(
                    FinalVideoSource_NewFrame);
                finalVideoSource.DesiredFrameSize = new Size(
                    Constants.DESIRED_FRAME_WIDTH, Constants.DESIRED_FRAME_HEIGHT);
                finalVideoSource.Start();
                isVideoRunning = finalVideoSource.IsRunning;
                //place focus on next item;
                applyFilterRadCheckBox.Focus();
            }
            else
            {
                StopWebcam();
                if (trackingToggleButton.ToggleState == ToggleState.On)
                {
                    trackingToggleButton.PerformClick(); // stop tracking
                }
                isVideoRunning = finalVideoSource.IsRunning;
                webcamRadToggleButton.Text = "Start Webcam";
                avaliableWebcamsDropDownList.Enabled = true;
                avaliableWebcamsDropDownList_SelectedIndexChanged(null, null);

                firstMarkerChangeRadButton.Enabled = false;
                secondMarkerChangeRadButton.Enabled = false;

                firstMarkerChangeColor = false;
                secondMarkerChangeColor = false;
            }

            CheckEnabledTracking();
        }

        private void avaliableWebcamsDropDownList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (avaliableWebcamsDropDownList.SelectedIndex != 0)
            {
                webcamRadToggleButton.Enabled = true;
                DrawOnEmptyFrame("Click to \nstart webcam.");
            }
            else
            {
                webcamRadToggleButton.Enabled = false;
                DrawOnEmptyFrame("Webcam \nnot selected.");
            }
        }

        bool firstMarkerChangeColor = false;
        Rectangle firstMarkerGetColorRect = new Rectangle(
            Constants.IMAGE_WIDTH / 2 - 25,
            Constants.IMAGE_HEIGHT / 2 - 25, 30, 30);

        bool secondMarkerChangeColor = false;
        Rectangle secondMarkerGetColorRect = new Rectangle(
            Constants.IMAGE_WIDTH / 2 + 15,
            Constants.IMAGE_HEIGHT / 2 + 15, 30, 30);

        private void firstMarkerChangeRadButton_Click(object sender, EventArgs e)
        {
            if (!firstMarkerChangeColor)
            {
                firstMarkerLoadRadRadioButton.IsChecked = false;
                secondMarkerLoadRadRadioButton.IsChecked = false;
                firstMarkerChangeColor = true;
            }
            else
            {
                if (finalVideoSource.FramesReceived>0) // if not running - no frame - no color
                {
                    firstMarkerChangeColor = false;

                    //get rectangle info; crop first pixel - red line
                    Bitmap fSample;
                    lock (newFrame)
                    {
                        fSample = newFrame.Clone(firstMarkerGetColorRect, newFrame.PixelFormat);
                    }
                    new Mean().Apply(fSample);

                    ImageStatistics statistics = new ImageStatistics(fSample);

                    Histogram histogramRed = statistics.RedWithoutBlack;
                    Histogram histogramGreen = statistics.GreenWithoutBlack;
                    Histogram histogramBlue = statistics.BlueWithoutBlack;

                    // get the values
                    int meanRed = (int)histogramRed.Mean;     // mean red value
                    int meanGreen = (int)histogramGreen.Mean;
                    int meanBlue = (int)histogramBlue.Mean;

                    firstMarkerColor = Color.FromArgb(meanRed, meanGreen, meanBlue);
                    //firstMarkerRangeRadTextBox.Text = 
                    //    ((histogramRed.GetRange(0.7).Min + 
                    //    histogramBlue.GetRange(0.7).Min + 
                    //    histogramGreen.GetRange(0.7).Min)/3).ToString();
                    // returns the range in [min,max];

                    Bitmap fSampleBitmap = new Bitmap(
                        firstMarkerSample.Width, firstMarkerSample.Height);
                    using (Graphics g = Graphics.FromImage(fSampleBitmap))
                    {
                        using (SolidBrush brush = new SolidBrush(firstMarkerColor))
                        {
                            g.FillRectangle(brush, 0, 0, firstMarkerSample.Width, firstMarkerSample.Height);
                        }
                    }

                    firstMarkerSample.Image = fSampleBitmap;
                }
            }

            CheckEnabledTracking();
        }

        private void secondMarkerChangeRadButton_Click(object sender, EventArgs e)
        {
            if (!secondMarkerChangeColor)
            {
                firstMarkerLoadRadRadioButton.IsChecked = false;
                secondMarkerLoadRadRadioButton.IsChecked = false;
                secondMarkerChangeColor = true;
            }
            else
            {
                if (finalVideoSource.FramesReceived>0)
                {
                    secondMarkerChangeColor = false;

                    //get rectangle info; crop first pixel - red line
                    Bitmap sSample;
                    lock (newFrame)
                    {
                        sSample = newFrame.Clone(secondMarkerGetColorRect, newFrame.PixelFormat);
                    }
                    new Mean().Apply(sSample);

                    ImageStatistics statistics = new ImageStatistics(sSample);

                    Histogram histogramRed = statistics.RedWithoutBlack;
                    Histogram histogramGreen = statistics.GreenWithoutBlack;
                    Histogram histogramBlue = statistics.BlueWithoutBlack;

                    // get the values
                    int meanRed = (int)histogramRed.Mean;     // mean red value
                    int meanGreen = (int)histogramGreen.Mean;
                    int meanBlue = (int)histogramBlue.Mean;

                    secondMarkerColor = Color.FromArgb(meanRed, meanGreen, meanBlue);
                    //MarkerRangeRadTextBox.Text = 
                    //    ((histogramRed.GetRange(0.7).Min + 
                    //    histogramBlue.GetRange(0.7).Min + 
                    //    histogramGreen.GetRange(0.7).Min)/3).ToString();
                    // returns the range in [min,max];

                    Bitmap sSampleBitmap = new Bitmap(
                        secondMarkerSample.Width, secondMarkerSample.Height);
                    using (Graphics g = Graphics.FromImage(sSampleBitmap))
                    {
                        using (SolidBrush brush = new SolidBrush(secondMarkerColor))
                        {
                            g.FillRectangle(brush, 0, 0, secondMarkerSample.Width, secondMarkerSample.Height);
                        }
                    }

                    secondMarkerSample.Image = sSampleBitmap;
                }
            }

            CheckEnabledTracking();
        }

        private void systemTrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            this.Show();
            this.WindowState = FormWindowState.Normal;
            systemTrayIcon.Visible = false;
        }

        private void WebCSForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                this.Hide();
                systemTrayIcon.Visible = true;
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            systemTrayIcon_MouseDoubleClick(null, null);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            systemTrayIcon.Visible = false;
            systemTrayIcon.Dispose();
            exitRadButton_Click(null, null);
        }

        private void startStopWebcamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webcamRadToggleButton.PerformClick();
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackingToggleButton.PerformClick();
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveOptionsRadButton.PerformClick();
        }

        private void CheckEnabledTracking()
        {
            this.trackingToggleButton.Enabled = (
                !firstMarkerColor.Equals(emptyColor) &&
                !secondMarkerColor.Equals(emptyColor) &&
                isVideoRunning);
        }

        private void saveOptionsRadButton_Click(object sender, EventArgs e)
        {
            User.Default.loadWebcamName = avaliableWebcamsDropDownList.SelectedText;
            User.Default.firstMarkerColorUser = firstMarkerColor;
            User.Default.firstMarkerRangeUser = getRange(1);
            User.Default.secondMarkerColorUser = secondMarkerColor;
            User.Default.secondMarkerRangeUser = getRange(2);
            User.Default.applyMeanFilter = applyFilterRadCheckBox.Checked;
            User.Default.Save();
        }

        private void cancelFirstMarkerRadButton_Click(object sender, EventArgs e)
        {
            firstMarkerChangeColor = false;
        }

        private void cancelSecondMarkerRadButton_Click(object sender, EventArgs e)
        {
            secondMarkerChangeColor = false;
        }

    }
}
