using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using AForge.Math;
using AForge;

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
            avaliableWebcamsDropDownList.SelectedIndex = 0;
            LoadAvaliableWebcams();
            LoadMarkers();
        }

        private void LoadMarkers()
        {
            firstMarkerSample.Image = RectangleShape.DrawFilledRectangle(
                firstMarkerSample.Width, firstMarkerSample.Height, firstMarkerColor);
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
            if (args.ToggleState.ToString().Equals("On"))
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
            if (args.ToggleState.ToString().Equals("On"))
            {
                trackingToggleButton.Text = "Disable Tracking";
            }
            else
            {
                trackingToggleButton.Text = "Activate Tracking";
            }
        }

        private void SelectDesktopAreaButton_Click(object sender, EventArgs e)
        {
            userRadLabel.Refresh();
        }

        Bitmap newFrame;
        Bitmap frameClone;
        //public static readonly Grayscale BT709 = new Grayscale(0.2125, 0.7154, 0.0721);
        Color firstMarkerColor = Color.FromArgb(215, 50, 50);

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

            if (trackingToggleButton.ToggleState.ToString().Equals("On"))
            {
                frameClone = (Bitmap)newFrame.Clone();

                // create filter
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                // set center color and radius
                filter.CenterColor.Color = firstMarkerColor;

                filter.Radius = getRange();
                // apply the filter
                filter.ApplyInPlace(frameClone);

                BitmapData objectsData = frameClone.LockBits(
                    new Rectangle(0, 0, frameClone.Width, frameClone.Height),
                    ImageLockMode.ReadOnly, frameClone.PixelFormat);
                // grayscaling
                //UnmanagedImage grayImage = new Grayscale.CommonAlgorithms.BT709.Apply(new UnmanagedImage(objectsData));
                UnmanagedImage grayImage = new GrayscaleBT709().Apply(
                    new UnmanagedImage(objectsData));
                // unlock image
                frameClone.UnlockBits(objectsData);

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
                    Rectangle objectRect = rects[0];
                    userRadLabel.Text =
                        "Center at: (" + (objectRect.X + objectRect.Width / 2).ToString() +
                        "; " + (objectRect.Y + objectRect.Height / 2).ToString() + ")";

                    if (loadWorkingFrameRadCheckBox.Checked)
                    {
                        newFrame = (Bitmap)frameClone.Clone();
                    }

                    newFrame = drawRectangleOnBitmap(
                        newFrame, objectRect, new Pen(Color.FromArgb(160, 255, 160), 2));
                }
            }

            if (firstMarkerChangeColor) // or secondMarkerColorChange
            {
                newFrame = drawRectangleOnBitmap(
                    (Bitmap)newFrame.Clone(),
                    firstMakrerRect,
                    new Pen(Color.Red, 2));
            }

            imageContainer.Image = newFrame;
        }

        private short getRange()
        {
            int range=0;
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

        private void WebCSForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            StopWebcam();
        }

        private void WebcamRadToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState.ToString().Equals("On"))
            {
                WebcamRadToggleButton.Text = "Stop Webcam";
                avaliableWebcamsDropDownList.Enabled = false;
                firstMarkerChangeRadButton.Enabled = true;

                //stat selected webcam
                finalVideoSource = new VideoCaptureDevice(
                    videoCaptureDevices[avaliableWebcamsDropDownList.SelectedIndex - 1].MonikerString);
                //-1 because [0] in avaliableWebcams = "Select Webcam"
                finalVideoSource.NewFrame += new NewFrameEventHandler(
                    FinalVideoSource_NewFrame);
                finalVideoSource.DesiredFrameSize = new Size(
                    Constants.DESIRED_FRAME_WIDTH, Constants.DESIRED_FRAME_HEIGHT);
                finalVideoSource.Start();

                //place focus on next item;
                applyFilterRadCheckBox.Focus();
            }
            else
            {
                StopWebcam();

                WebcamRadToggleButton.Text = "Start Webcam";
                avaliableWebcamsDropDownList.Enabled = true;
                avaliableWebcamsDropDownList_SelectedIndexChanged(null, null);

                firstMarkerChangeRadButton.Enabled = false;
            }
        }

        private void avaliableWebcamsDropDownList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (avaliableWebcamsDropDownList.SelectedIndex != 0)
            {
                WebcamRadToggleButton.Enabled = true;
                DrawOnEmptyFrame("Click to \nstart webcam.");
            }
            else
            {
                WebcamRadToggleButton.Enabled = false;
                DrawOnEmptyFrame("Webcam \nnot selected.");
            }
        }

        bool firstMarkerChangeColor = false;
        Rectangle firstMakrerRect = new Rectangle(
            Constants.IMAGE_WIDTH / 2 - 15,
            Constants.IMAGE_HEIGHT / 2 - 15, 30, 30);

        private void firstMarkerChangeRadButton_Click(object sender, EventArgs e)
        {
            if (!firstMarkerChangeColor)
            {
                loadWorkingFrameRadCheckBox.Checked = false;
                firstMarkerChangeColor = true;
            }
            else
            {
                firstMarkerChangeColor = false;

                //get rectangle info; crop first pixel - red line
                Bitmap fSample = newFrame.Clone(firstMakrerRect, newFrame.PixelFormat);
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
                    firstMarkerSample.Width,firstMarkerSample.Height);
                using (Graphics g = Graphics.FromImage(fSampleBitmap))
                {
                    using (SolidBrush brush = new SolidBrush(firstMarkerColor))
                    {
                        g.FillRectangle(brush,0,0,firstMarkerSample.Width,firstMarkerSample.Height);
                    }
                }

                firstMarkerSample.Image=fSampleBitmap;
            }
        }





    }
}
