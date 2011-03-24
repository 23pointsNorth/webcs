using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
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

            //loading the saved user settings
            applyFilterRadCheckBox.Checked = User.Default.applyMedianFilter;
            desktopBoundries = User.Default.desktopAreaBoundriesRectangle;
            areDesktopBounriesVisible = User.Default.areDesktopAreaBoundriesVisible;
            softwareCursor.DesktopArea = desktopBoundries;
            enableMouseRadCheckBox.Checked = User.Default.isMouseEnabled;
            centerLineRadCheckBox.Checked = User.Default.showCenterLine;
            connectCenters = User.Default.showCenterLine;
            proximityClick = User.Default.proximityClick;
            softwareCursor.DeltaPosition = proximityClick;

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
                firstMarkerRange = User.Default.firstMarkerRangeUser;
                secondMarkerRange = User.Default.secondMarkerRangeUser;
            }
            catch
            {
                //default 
                firstMarkerColor = emptyColor;
                secondMarkerColor = emptyColor;
                firstMarkerRangeRadTextBox.Text = "20";
                secondMarkerRangeRadTextBox.Text = "20";
                firstMarkerRange = 20;
                secondMarkerRange = 20;
            }
            firstMarkerSample.Image = DrawFilledRectangle(
                firstMarkerSample.Width, firstMarkerSample.Height, firstMarkerColor);
            secondMarkerSample.Image = DrawFilledRectangle(
                secondMarkerSample.Width, secondMarkerSample.Height, secondMarkerColor);
        }
        public static Bitmap DrawFilledRectangle(int width, int height, Color fillColour)
        {
            Bitmap filledBitmap = new Bitmap(width, height);

            using (Graphics graphicsOnImage = Graphics.FromImage(filledBitmap))
            using (SolidBrush brush = new SolidBrush(fillColour))
            {
                graphicsOnImage.FillRectangle(brush, 0, 0, width, height);
            }
            return filledBitmap;
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
            if (videoCaptureDevices.Count > 0)
            {
                webcamsMenuStrip.Enabled = true;
            }
            else
            { 
                //show that there are not webcams
                webcamsMenuStrip.Items.Add("No webcams");
                webcamsMenuStrip.Enabled = false;
            }

            foreach (FilterInfo videoCaptureDevice in videoCaptureDevices)
            {
                //add all webcams to the drop down list
                avaliableWebcamsDropDownList.Items.Add(videoCaptureDevice.Name);
                webcamsMenuStrip.Items.Add(videoCaptureDevice.Name.ToString());
            }

            try
            {
                avaliableWebcamsDropDownList.SelectedText = User.Default.loadWebcamName;
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
            catch (NullReferenceException e)
            {
                userRadLabel.Text = "Video not started. " + e.Message;
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
            //Resize control corresponding to the case 
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

        bool isTrackingEnabled = false;
        private void trackingToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                trackingToggleButton.Text = "Disable &Tracking";
                isTrackingEnabled = true;
            }
            else
            {
                trackingToggleButton.Text = "Enable &Tracking";
                isTrackingEnabled = false;
            }
        }

        bool areDesktopBounriesVisible = false;
        bool changeFirstClick = false;
        bool changeSecondClick = false;

        private void SelectDesktopAreaButton_Click(object sender, EventArgs e)
        {
            //select the new boundries of the desktop, so that the whole screen can be accessed
            this.imageContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageContainer_MouseDown);
            areDesktopBounriesVisible = true;

            changeFirstClick = true;
            changeSecondClick = true;
        }

        Bitmap newFrame;
        Bitmap firstFrameClone;
        Bitmap secondFrameClone;
        static Color emptyColor = Color.FromArgb(0, 0, 0);
        Color firstMarkerColor = emptyColor;
        Color secondMarkerColor = emptyColor;
        Rectangle firstMarkerRect;
        Rectangle secondMarkerRect;
        Mouse softwareCursor = new Mouse(Cursor.Position, new Point(0, 0), 0);
        bool foundFirstMarker = false;
        bool foundSecondMarker = false;

        private void FinalVideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //When a new frame is recieved, all the alogrithms should be run
            //update image 
            newFrame = (Bitmap)eventArgs.Frame.Clone();
            newFrame = ResizeBitmap(newFrame, Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);

            newFrame.RotateFlip(RotateFlipType.Rotate180FlipY); //miror image

            //should the median fileter be applied
            if (applyFilterRadCheckBox.Checked)
            {
                BitmapData objectsData = newFrame.LockBits(
                    new Rectangle(0, 0, newFrame.Width, newFrame.Height),
                    ImageLockMode.ReadOnly, newFrame.PixelFormat);
                Median filer = new Median();
                filer.ApplyInPlace(objectsData);
                newFrame.UnlockBits(objectsData);
            }

            //is tracking enabled?
            if (isTrackingEnabled)
            {
                //make sure both clones contain starting bitmap
                firstFrameClone = new Bitmap(newFrame);
                secondFrameClone = new Bitmap(newFrame);

                //find marker positions
                CalculateMarker(
                    firstFrameClone, firstMarkerColor, Color.Green, firstMarkerRange, 
                    firstMarkerLoadRadRadioButton.IsChecked, out firstMarkerRect, out foundFirstMarker);
                CalculateMarker(
                    secondFrameClone, secondMarkerColor, Color.Blue, secondMarkerRange, 
                    secondMarkerLoadRadRadioButton.IsChecked, out secondMarkerRect, out foundSecondMarker);
                
                if (foundFirstMarker)
                {
                    //when the position of the marker is known, the curson can be moved, otherwise do nothing
                    softwareCursor.SetNewPosition(firstMarkerRect, secondMarkerRect);

                    if (isMouseEnabled && !secondMarkerRect.Equals(wholeDesktopArea) && foundSecondMarker)
                    {
                        //You only click when the mouse is enabled and when both markers are found
                        softwareCursor.Click();
                    }
                }
            }
            try
            {
                //under-the-hood options
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

                if (areDesktopBounriesVisible)
                {
                    newFrame = drawRectangleOnBitmap(
                        (Bitmap)newFrame.Clone(),
                        desktopBoundries,
                        new Pen(Color.Gray, 2));
                }

                //drawing a line connecting the centers of both markers
                if (connectCenters && foundFirstMarker && foundSecondMarker && 
                    (trackingToggleButton.ToggleState == ToggleState.On))
                {
                    Color drawColor = (softwareCursor.IsMouseDown)?Color.Firebrick:Color.ForestGreen;
                    Point firstCenter = new Point(
                    firstMarkerRect.X + firstMarkerRect.Width / 2, firstMarkerRect.Y + firstMarkerRect.Height / 2);
                    Point secondCenter = new Point(
                    secondMarkerRect.X + secondMarkerRect.Width / 2, secondMarkerRect.Y + secondMarkerRect.Height / 2);
                    int diff = (int)Math.Sqrt(
                        Math.Pow(Math.Abs(firstCenter.X - secondCenter.X), 2) +
                        Math.Pow(Math.Abs(firstCenter.Y - secondCenter.Y), 2));
                    PointF lineCenter = new Point(
                        (firstCenter.X + secondCenter.X) / 2,
                        (firstCenter.Y + secondCenter.Y) / 2);

                    using (Graphics g = Graphics.FromImage(newFrame))
                    {
                        g.DrawLine(new Pen(drawColor, 2), firstCenter, secondCenter);
                        g.DrawString(diff.ToString(), new Font("Arial", 10), 
                            new SolidBrush(drawColor), lineCenter);
                    }
                }
            }
            finally
            {
                if (showFrames)
                {
                    imageContainer.Image = newFrame;    //update image to container
                }
            }
            
        }

        static Rectangle wholeDesktopArea = new Rectangle(0,0, Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);
        private void CalculateMarker(Bitmap frame, Color markerColor, Color rectangleColor, int colorRange, bool loadWorkingFrame, out Rectangle markerRect, out bool found)
        {
            markerRect = wholeDesktopArea;
            BitmapData ObjectsData = frame.LockBits(
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    ImageLockMode.ReadOnly, frame.PixelFormat);

            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            // set center color and radius
            filter.CenterColor.Color = markerColor;
            filter.Radius = (short)colorRange;
            filter.ApplyInPlace(ObjectsData);
            
            try
            {
                ExtractBiggestBlob biggestBlob = new ExtractBiggestBlob();
                Size blobSize = biggestBlob.Apply(ObjectsData).Size; // returns a bitmap - need only size
                if (blobSize.Height < Constants.MIN_BLOB_HEIGHT &&
                    blobSize.Width < Constants.MIN_BLOB_WIDTH)
                {
                    throw new ArgumentException("Blob too small.");
                }
                markerRect = new Rectangle(
                    new Point (biggestBlob.BlobPosition.X, biggestBlob.BlobPosition.Y), 
                    blobSize);
                if (loadWorkingFrame)
                {
                    newFrame = (Bitmap)frame.Clone();
                }
                found = true;
                newFrame = drawRectangleOnBitmap(
                    newFrame, markerRect, new Pen(rectangleColor, 2));
            }
            catch (ArgumentException)
            {
                //no blob found. stay on last known position
                found = false;
            }

            frame.UnlockBits(ObjectsData);
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
                timeOut.Enabled = true;
                timeOut.Start();
                DrawOnEmptyFrame("Starting...");
                webcamRadToggleButton.Text = "Stop &Webcam";
                avaliableWebcamsDropDownList.Enabled = false;
                firstMarkerChangeRadButton.Enabled = true;
                secondMarkerChangeRadButton.Enabled = true;

                try
                {
                    //stat selected webcam
                    finalVideoSource = new VideoCaptureDevice(
                        videoCaptureDevices[avaliableWebcamsDropDownList.SelectedIndex - 1].MonikerString);
                    //-1 because [0] in avaliableWebcams = "Select Webcam"
                    finalVideoSource.NewFrame += new NewFrameEventHandler(
                        FinalVideoSource_NewFrame);
                    finalVideoSource.DesiredFrameSize = new Size(
                        Constants.DESIRED_FRAME_WIDTH, Constants.DESIRED_FRAME_HEIGHT);
                    finalVideoSource.DesiredFrameRate = Constants.DESIRED_FRAME_RATE;
                    finalVideoSource.Start();
                }
                finally
                {
                    isVideoRunning = finalVideoSource.IsRunning;
                }
            }
            else
            {
                StopWebcam();
                if (trackingToggleButton.ToggleState == ToggleState.On)
                {
                    trackingToggleButton.PerformClick(); // stop tracking
                }
                isVideoRunning = finalVideoSource.IsRunning;
                webcamRadToggleButton.Text = "Start &Webcam";
                avaliableWebcamsDropDownList.Enabled = true;
                avaliableWebcamsDropDownList_SelectedIndexChanged(null, null);

                firstMarkerChangeRadButton.Enabled = false;
                secondMarkerChangeRadButton.Enabled = false;

                firstMarkerChangeColor = false;
                secondMarkerChangeColor = false;

                timeOut.Stop();
                timeOut.Enabled = false;
            }
            CheckEnabledTracking();
        }
        private void avaliableWebcamsDropDownList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (avaliableWebcamsDropDownList.SelectedIndex > 0)
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
                noLoadRadRadioButton.IsChecked = true;
                firstMarkerChangeColor = true;
            }
            else
            {
                if (finalVideoSource.FramesReceived > 0) // if not running - no frame - no color
                {
                    try
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
                    catch
                    {
                        firstMarkerChangeRadButton.PerformClick();
                    }
                }
            }

            CheckEnabledTracking();
        }
        private void secondMarkerChangeRadButton_Click(object sender, EventArgs e)
        {
            if (!secondMarkerChangeColor)
            {
                noLoadRadRadioButton.IsChecked = true;
                secondMarkerChangeColor = true;
            }
            else
            {
                if (finalVideoSource.FramesReceived > 0)
                {
                    try
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
                    catch
                    {
                        secondMarkerChangeRadButton.PerformClick();
                    }
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
            User.Default.firstMarkerRangeUser = firstMarkerRange;
            User.Default.secondMarkerColorUser = secondMarkerColor;
            User.Default.secondMarkerRangeUser = secondMarkerRange;
            User.Default.applyMedianFilter = applyFilterRadCheckBox.Checked;
            User.Default.desktopAreaBoundriesRectangle = desktopBoundries;
            User.Default.areDesktopAreaBoundriesVisible = areDesktopBounriesVisible;
            User.Default.isMouseEnabled = isMouseEnabled;
            User.Default.showCenterLine = connectCenters;
            User.Default.proximityClick = proximityClick;
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
        private void webcamsMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            avaliableWebcamsDropDownList.SelectedText = e.ClickedItem.ToString();
            avaliableWebcamsDropDownList_SelectedIndexChanged(null, null);
            webcamRadToggleButton.PerformClick();
        }
        private void timeOut_Tick(object sender, EventArgs e)
        {
            //if no webcam is started after timer ticks, the webcam canot be loaded;
            if (finalVideoSource.FramesReceived <= 0)
            {
                webcamRadToggleButton.PerformClick();
                timeOut.Enabled = false;
                DrawOnEmptyFrame("Unable to \nload webcam.\nPlease, try again.");
            }
        }

        Point firstClick;
        Point secondClick;
        Rectangle desktopBoundries;

        private void imageContainer_MouseDown(object sender, MouseEventArgs e)
        {
            if (changeFirstClick)
            {
                firstClick = e.Location;
                changeFirstClick = false;
            }
            else
            {
                if (changeSecondClick)
                {
                    secondClick = e.Location;
                    changeSecondClick = false;
                    desktopBoundries = new Rectangle(
                        Math.Min(firstClick.X,secondClick.X), 
                        Math.Min(firstClick.Y,secondClick.Y), 
                        Math.Abs(firstClick.X - secondClick.X), 
                        Math.Abs(firstClick.Y - secondClick.Y));
                    areDesktopBounriesVisible = true;
                    softwareCursor.DesktopArea = desktopBoundries;
                    this.imageContainer.MouseDown -= 
                        new System.Windows.Forms.MouseEventHandler(this.imageContainer_MouseDown);
                }
            }
        }

        bool showFrames = true;
        private void noFramesRadRadioButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (noFramesRadRadioButton.IsChecked)
            {
                DrawOnEmptyFrame("No Frames\nmode.");
                showFrames = false;
            }
            else
            {
                DrawOnEmptyFrame("Webcam \nnot selected.");
                showFrames = true;
            }
        }

        bool isMouseEnabled = false;
        private void enableMouseRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            isMouseEnabled = enableMouseRadCheckBox.Checked;
        }

        int firstMarkerRange;
        int secondMarkerRange;

        private void firstMarkerRangeRadTextBox_TextChanged(object sender, EventArgs e)
        {
            int range;
            try
            {
                range = int.Parse(firstMarkerRangeRadTextBox.Text);
            }
            catch (FormatException)
            {
                range = 0;
                firstMarkerRangeRadTextBox.Text = "0";
            }

            if (range > 255)
            {
                range = 255;
                firstMarkerRangeRadTextBox.Text = range.ToString();
            }
            else if (range < 0)
            {
                range = 0;
                firstMarkerRangeRadTextBox.Text = range.ToString();
            }
            firstMarkerRange = range;
        }
        private void secondMarkerRangeRadTextBox_TextChanged(object sender, EventArgs e)
        {
            int range;
            try
            {
                range = int.Parse(secondMarkerRangeRadTextBox.Text);
            }
            catch (FormatException)
            {
                range = 0;
                secondMarkerRangeRadTextBox.Text = "0";
            }

            if (range > 255)
            {
                range = 255;
                secondMarkerRangeRadTextBox.Text = range.ToString();
            }
            else if (range < 0)
            {
                range = 0;
                secondMarkerRangeRadTextBox.Text = range.ToString();
            }
            secondMarkerRange = range;

        }

        bool connectCenters;
        private void centerLineRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            connectCenters = centerLineRadCheckBox.Checked;
        }

        int proximityClick;
        private void deltaPositionRadTextBox_TextChanged(object sender, EventArgs e)
        {
            int delta;
            try
            {
                delta = int.Parse(deltaPositionRadTextBox.Text);
            }
            catch (FormatException)
            {
                delta = 0;
                deltaPositionRadTextBox.Text = "0";
            }

            if (delta > Constants.DESIRED_FRAME_WIDTH)
            {
                delta = Constants.DESIRED_FRAME_WIDTH;
                deltaPositionRadTextBox.Text = delta.ToString();
            }
            else if (delta < 0)
            {
                delta = 0;
                deltaPositionRadTextBox.Text = delta.ToString();
            }
            softwareCursor.DeltaPosition = delta;
            proximityClick = delta;
        }
    }
}
