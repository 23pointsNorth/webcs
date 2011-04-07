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
using System.Collections.Generic;
using Counter;
using BitmapProcessing;
using AForge;
using Marker;
using CursorMovement;
using System.Threading;

namespace WebCS
{
    public partial class WebCSForm : RadForm
    {
        public WebCSForm()
        {
            InitializeComponent();
            this.Size = new Size(
                Constants.WEBCAM_ONLY_WIDTH, Constants.WEBCAM_ONLY_HEIGHT);
            imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "Webcam \nnot selected.");
            avaliableWebcamsDropDownList.Items.Add("Select Webcam");

            //loading the saved user settings
            applyMedianFilterRadCheckBox.Checked = User.Default.applyMedianFilter;
            applyMedianFilter = applyMedianFilterRadCheckBox.Checked;
            applyMeanFilterRadCheckBox.Checked = User.Default.applyMeanFilter;
            applyMeanFilter = applyMeanFilterRadCheckBox.Checked;
            desktopBoundries = User.Default.desktopAreaBoundriesRectangle;
            areDesktopBounriesVisible = User.Default.areDesktopAreaBoundriesVisible;
            softwareCursor.DesktopArea = desktopBoundries;
            enableMovingRadCheckBox.Checked = User.Default.isMovingEnabled;
            enableClickingRadCheckBox.Checked = User.Default.isClickingEnabled;
            centerLineRadCheckBox.Checked = User.Default.showCenterLine;
            connectCenters = User.Default.showCenterLine;
            proximityClick = User.Default.proximityClick;
            softwareCursor.DeltaPosition = proximityClick;
            deltaPositionRadTextBox.Text = proximityClick.ToString();

            LoadAvaliableWebcams();
            LoadMarkers();
            LoadAtStartup();
            CheckEnabledTracking();
        }


        List<ColorMarker> markersList = new List<ColorMarker>();

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
            ColorMarker firstMarker;
            ColorMarker secondMarker;
            try
            {
                firstMarker = new ColorMarker(
                    User.Default.firstMarkerColorUser,
                    User.Default.firstMarkerRangeUser,
                    Color.Green, new Rectangle(
                        Constants.IMAGE_WIDTH / 2 - 25,
                        Constants.IMAGE_HEIGHT / 2 - 25, 30, 30),
                    Color.LightGreen, 0, 255);

                secondMarker = new ColorMarker(
                    User.Default.secondMarkerColorUser,
                    User.Default.secondMarkerRangeUser,
                    Color.Blue, new Rectangle(
                        Constants.IMAGE_WIDTH / 2 + 15,
                        Constants.IMAGE_HEIGHT / 2 + 15, 30, 30),
                    Color.LightBlue, 0, 255);
                firstMarkerRangeRadTextBox.Text = User.Default.firstMarkerRangeUser.ToString();
                secondMarkerRangeRadTextBox.Text = User.Default.secondMarkerRangeUser.ToString();
            }
            catch
            {
                //default 
                firstMarker = new ColorMarker(
                    ColorMarker.emptyColor,
                    20,
                    Color.Green, new Rectangle(
                        Constants.IMAGE_WIDTH / 2 - 25,
                        Constants.IMAGE_HEIGHT / 2 - 25, 30, 30),
                    Color.LightBlue, 0, 255);
                secondMarker = new ColorMarker(
                    ColorMarker.emptyColor,
                    20,
                    Color.Blue, new Rectangle(
                        Constants.IMAGE_WIDTH / 2 + 15,
                        Constants.IMAGE_HEIGHT / 2 + 15, 30, 30),
                    Color.LightBlue, 0, 255);
                firstMarkerRangeRadTextBox.Text = "20";
                secondMarkerRangeRadTextBox.Text = "20";
            }

            markersList.Add(firstMarker);
            markersList.Add(secondMarker);

            ColorMarker.IndexMarker = new ColorMarker._index((ushort)markersList.IndexOf(firstMarker), (ushort)markersList.IndexOf(secondMarker));
            firstMarkerSample.Image = BitmapDraw.FilledRectangle(
                firstMarkerSample.Width, firstMarkerSample.Height, firstMarker.Color);
            secondMarkerSample.Image = BitmapDraw.FilledRectangle(
                secondMarkerSample.Width, secondMarkerSample.Height, secondMarker.Color);
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
        Mouse softwareCursor = new Mouse(Cursor.Position, Cursor.Position, 0);
        bool applyMedianFilter = false;
        bool applyMeanFilter = false;
        QueryPerfCounter fpsTimer = new QueryPerfCounter();

        private void FinalVideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            fpsTimer.Start();
            //When a new frame is recieved, all the alogrithms should be run
            //update image 
            newFrame = (Bitmap)eventArgs.Frame.Clone();
            newFrame = BitmapDraw.Resize(newFrame, Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);

            newFrame.RotateFlip(RotateFlipType.Rotate180FlipY); //miror image

            //what  fileter should be applied
            FiltersSequence filters = new FiltersSequence();
            if (applyMedianFilter)
            {
                filters.Add(new Median());
            }

            if (applyMeanFilter)
            {
                filters.Add(new Mean());
            }

            if (filters.Count > 0)
            {
                BitmapData objectsData = newFrame.LockBits(
                    new Rectangle(0, 0, newFrame.Width, newFrame.Height),
                    ImageLockMode.ReadOnly, newFrame.PixelFormat);
                filters.Apply(objectsData);
                newFrame.UnlockBits(objectsData);
            }

            Dictionary<Rectangle, Color> rectDictionary = new Dictionary<Rectangle, Color>();
            if (isTrackingEnabled)
            {
                //make sure all methods contain starting bitmap
                for (int m = 0; m < markersList.Count; m++)
                {
                    //Add next line if Threaded Calculation is enabled(remove normal);
                    markersList[m].ThreadCalculateMarker(new Bitmap(newFrame));
                    //markersList[m].CalculateMarker(new Bitmap(newFrame));
                }

                //Add next line if Threaded Calculation is enabled;
                WaitHandle.WaitAll(ColorMarker.doneEvents.ToArray());

                for (int m = 0; m < markersList.Count; m++)
                {
                    if (markersList[m].IsFound)
                    {
                        rectDictionary.Add(markersList[m].Rect,
                            markersList[m].FoundMarkerRectC);
                    }
                }

                softwareCursor.CalculateNewPosition(markersList[ColorMarker.IndexMarker.Primary].Rect, markersList[ColorMarker.IndexMarker.Secondary].Rect);
                if (markersList[ColorMarker.IndexMarker.Primary].IsFound)
                {
                    if (firstMarkerLoadRadRadioButton.IsChecked)
                    {
                        newFrame = MarkerBase.leftOvers[ColorMarker.IndexMarker.Primary];
                    }
                    //when the position of the marker is known, 
                    //the curson can be moved to the pre calc position, otherwise do nothing
                    if (isMovingEnabled)
                    {
                        softwareCursor.SetNewPosition();
                    }
                    if (isClickingEnabled && markersList[ColorMarker.IndexMarker.Secondary].IsFound)
                    {
                        //You only click when the mouse is enabled and when both markers are found
                        softwareCursor.Click();
                    }
                }
                if (markersList[ColorMarker.IndexMarker.Secondary].IsFound && 
                    secondMarkerLoadRadRadioButton.IsChecked)
                {
                    newFrame = MarkerBase.leftOvers[ColorMarker.IndexMarker.Secondary];
                }
            }
            //under-the-hood options
            if (markersList[ColorMarker.IndexMarker.Primary].IsColorChange)
            {
                rectDictionary.Add(markersList[ColorMarker.IndexMarker.Primary].GetColorRect, markersList[ColorMarker.IndexMarker.Primary].ChangeColorRectC);
            }

            if (markersList[ColorMarker.IndexMarker.Secondary].IsColorChange)
            {
                rectDictionary.Add(markersList[ColorMarker.IndexMarker.Secondary].GetColorRect, markersList[ColorMarker.IndexMarker.Secondary].ChangeColorRectC);
            }

            if (areDesktopBounriesVisible)
            {
                rectDictionary.Add(desktopBoundries, Color.Gray);
            }

            BitmapDraw.Rectangle(newFrame, rectDictionary);
            //drawing a line connecting the centers of both markers
            if (connectCenters && markersList[ColorMarker.IndexMarker.Primary].IsFound && markersList[ColorMarker.IndexMarker.Secondary].IsFound && isTrackingEnabled)
            {
                Color drawColor = (softwareCursor.IsMouseDown) ? Color.Firebrick : Color.DarkGreen;
                
                Point lineCenter = new Point(
                    (softwareCursor.MousePoint.X + softwareCursor.PressurePoint.X) / 2,
                    (softwareCursor.MousePoint.Y + softwareCursor.PressurePoint.Y) / 2);
                
                using (Graphics g = Graphics.FromImage(newFrame))
                {
                    g.DrawLine(new Pen(drawColor, 2), 
                        softwareCursor.MousePoint, softwareCursor.PressurePoint);
                    g.DrawString(softwareCursor.Proximity.ToString(), new Font("Arial", 10), 
                        new SolidBrush(drawColor), lineCenter);
                }
            }
            fpsTimer.Stop();
            fpsTimer.DurationPerIteration();
            if (showFrames)
            {
                DrawFrameRate();
                imageContainer.Image = newFrame;    //update image to container
            }
        }

        private void DrawFrameRate()
        {
            if (fpsTimer.FPS < maxFrameRate)
            {
                BitmapDraw.WriteString(newFrame,
                    fpsTimer.FPSstring + " " + fpsTimer.DurationInMS.ToString("0.0") + " ms",
                    Color.DarkRed, new Point(5, 5));
            }
            else
            {
                BitmapDraw.WriteString(newFrame,
                    maxFrameRate.ToString("0.0") + " FPS " + fpsTimer.DurationInMS.ToString("0.0") + " ms",
                    Color.DarkRed, new Point(5, 5));
            }
        }

        private void WebCSForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            StopWebcam();
            systemTrayIcon.Dispose();
        }

        double maxFrameRate = 72;
        bool isVideoRunning = false;
        private void WebcamRadToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                timeOut.Enabled = true;
                timeOut.Start();
                imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "Starting...");
                webcamRadToggleButton.Text = "Stop &Webcam";
                avaliableWebcamsDropDownList.Enabled = false;
                webcamOptionsRadButton.Enabled = true;
                firstMarkerChangeRadButton.Enabled = true;
                secondMarkerChangeRadButton.Enabled = true;

                try
                {
                    int index = avaliableWebcamsDropDownList.SelectedIndex;
                    //-1 because [0] in avaliableWebcams = "Select Webcam"
                    //stat selected webcam
                    finalVideoSource = new VideoCaptureDevice(
                        videoCaptureDevices[index-1].MonikerString);
                    finalVideoSource.NewFrame += new NewFrameEventHandler(
                        FinalVideoSource_NewFrame);
                    finalVideoSource.DesiredFrameSize = new Size(
                        Constants.DESIRED_FRAME_WIDTH, Constants.DESIRED_FRAME_HEIGHT);
                    finalVideoSource.DesiredFrameRate = Constants.DESIRED_FRAME_RATE;
                    finalVideoSource.Start();

                    //finalVideoSource.DisplayPropertyPage(new IntPtr());
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
                webcamOptionsRadButton.Enabled = false;

                markersList[ColorMarker.IndexMarker.Primary].IsColorChange = false;
                markersList[ColorMarker.IndexMarker.Secondary].IsColorChange = false;

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
                imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "Click to \nstart webcam.");
            }
            else
            {
                webcamRadToggleButton.Enabled = false;
                imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "Webcam \nnot selected.");
            }
        }

        private void firstMarkerChangeRadButton_Click(object sender, EventArgs e)
        {
            if (!markersList[ColorMarker.IndexMarker.Primary].IsColorChange)
            {
                noLoadRadRadioButton.IsChecked = true;
                markersList[ColorMarker.IndexMarker.Primary].IsColorChange = true;
            }
            else
            {
                if (finalVideoSource.FramesReceived > 0) // if not running - no frame - no color
                {
                    try
                    {

                        markersList[ColorMarker.IndexMarker.Primary].IsColorChange = false;
                        markersList[ColorMarker.IndexMarker.Primary].ChangeColor(newFrame);
                        firstMarkerSample.Image = BitmapDraw.FilledRectangle(
                            firstMarkerSample.Width, 
                            firstMarkerSample.Height,
                            markersList[ColorMarker.IndexMarker.Primary].Color);
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
            if (!markersList[ColorMarker.IndexMarker.Secondary].IsColorChange)
            {
                noLoadRadRadioButton.IsChecked = true;
                markersList[ColorMarker.IndexMarker.Secondary].IsColorChange = true;
            }
            else
            {
                if (finalVideoSource.FramesReceived > 0)
                {
                    try
                    {
                        markersList[ColorMarker.IndexMarker.Secondary].IsColorChange = false;
                        markersList[ColorMarker.IndexMarker.Secondary].ChangeColor(newFrame);
                        secondMarkerSample.Image = BitmapDraw.FilledRectangle(
                            secondMarkerSample.Width, 
                            secondMarkerSample.Height,
                            markersList[ColorMarker.IndexMarker.Secondary].Color);
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
                !markersList[ColorMarker.IndexMarker.Primary].Color.Equals(ColorMarker.emptyColor) &&
                !markersList[ColorMarker.IndexMarker.Secondary].Color.Equals(ColorMarker.emptyColor) &&
                isVideoRunning);
        }
        private void saveOptionsRadButton_Click(object sender, EventArgs e)
        {
            User.Default.loadWebcamName = avaliableWebcamsDropDownList.SelectedText;
            User.Default.firstMarkerColorUser = markersList[ColorMarker.IndexMarker.Primary].Color;
            User.Default.firstMarkerRangeUser = markersList[ColorMarker.IndexMarker.Primary].Range;
            User.Default.secondMarkerColorUser = markersList[ColorMarker.IndexMarker.Secondary].Color;
            User.Default.secondMarkerRangeUser = markersList[ColorMarker.IndexMarker.Secondary].Range;
            User.Default.applyMedianFilter = applyMedianFilter;
            User.Default.applyMeanFilter = applyMeanFilter;
            User.Default.desktopAreaBoundriesRectangle = desktopBoundries;
            User.Default.areDesktopAreaBoundriesVisible = areDesktopBounriesVisible;
            User.Default.isMovingEnabled = isMovingEnabled;
            User.Default.isClickingEnabled = isClickingEnabled;
            User.Default.showCenterLine = connectCenters;
            User.Default.proximityClick = proximityClick;
            User.Default.Save();
        }
        private void cancelFirstMarkerRadButton_Click(object sender, EventArgs e)
        {
            markersList[ColorMarker.IndexMarker.Primary].IsColorChange = false;
        }
        private void cancelSecondMarkerRadButton_Click(object sender, EventArgs e)
        {
            markersList[ColorMarker.IndexMarker.Secondary].IsColorChange = false;
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
                imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "Unable to \nload webcam.\nPlease, try again.");
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
                imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "No Frames\nmode.");
                showFrames = false;
            }
            else
            {
                imageContainer.Image = BitmapDraw.WriteString(
                new Bitmap(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT), "Webcam \nnot selected.");
                showFrames = true;
            }
        }

        bool isClickingEnabled = false;
        private void enableClickingRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            isClickingEnabled = enableClickingRadCheckBox.Checked;
        }

        private void firstMarkerRangeRadTextBox_TextChanged(object sender, EventArgs e)
        {
            if (markersList.Count > 0)
            {
                markersList[ColorMarker.IndexMarker.Primary].ChangeRange(firstMarkerRangeRadTextBox.Text);
                firstMarkerRangeRadTextBox.Text = markersList[ColorMarker.IndexMarker.Primary].Range.ToString();
            }
        }
        private void secondMarkerRangeRadTextBox_TextChanged(object sender, EventArgs e)
        {
            if (markersList.Count > 0)
            {
                markersList[ColorMarker.IndexMarker.Secondary].ChangeRange(secondMarkerRangeRadTextBox.Text);
                secondMarkerRangeRadTextBox.Text = markersList[ColorMarker.IndexMarker.Secondary].Range.ToString();
            }
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
            softwareCursor.IsMouseDown = false;
            proximityClick = delta;
        }

        private void applyMedianFilterRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            applyMedianFilter = applyMedianFilterRadCheckBox.Checked;
        }

        private void applyMeanFilterRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            applyMeanFilter = applyMeanFilterRadCheckBox.Checked;
        }

        bool isMovingEnabled;
        private void enableMovingRadCheckBox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            isMovingEnabled = enableMovingRadCheckBox.Checked;
        }

        private void webcamOptionsRadButton_Click(object sender, EventArgs e)
        {
            try
            {
                finalVideoSource.DisplayPropertyPage(new IntPtr());
            }
            catch { }
        }


    }
}
