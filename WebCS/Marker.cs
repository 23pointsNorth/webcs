using System;
using System.Drawing;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Math;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WebCS
{
    class Marker
    {
        Color color;
        int range;
        bool isColorChange = false;
        bool isFound;
        Rectangle rect;
        Rectangle getColorRect;
        Color foundMarkerRectC;
        Color changeColorRectC;


        public Color Color { get; set; }
        public int Range { get; set; }
        public bool IsColorChange { get; set; }
        public bool IsFound { get; set; }
        public Rectangle Rect { get; set; }
        public Rectangle GetColorRect { get; set; }
        public Color FoundMarkerRectC { get; set; }
        public Color ChangeColorRectC { get; set; }

        public Marker(Color colorValue, int rangeValue, bool found, Color foundMarkerRectColor ,bool changeColor, Rectangle getColorRectValue, Color changeColorRect)
        {
            this.Color = colorValue;
            this.Range = rangeValue;
            this.IsFound = found;
            this.FoundMarkerRectC = foundMarkerRectColor;
            this.IsColorChange = changeColor;
            this.GetColorRect = getColorRectValue;
            this.ChangeColorRectC = changeColorRect;
        }

        public void ChangeColor(Bitmap frame)
        {
            this.IsColorChange = false;

            //get rectangle info; crop first pixel - red line
            Bitmap sample;
            lock (frame)
            {
                sample = frame.Clone(this.GetColorRect, frame.PixelFormat);
            }
            new Mean().Apply(sample);

            ImageStatistics statistics = new ImageStatistics(sample);

            Histogram histogramRed = statistics.RedWithoutBlack;
            Histogram histogramGreen = statistics.GreenWithoutBlack;
            Histogram histogramBlue = statistics.BlueWithoutBlack;

            // get the values
            int meanRed = (int)histogramRed.Mean;     // mean red value
            int meanGreen = (int)histogramGreen.Mean;
            int meanBlue = (int)histogramBlue.Mean;

            this.Color = Color.FromArgb(meanRed, meanGreen, meanBlue);
        }

        public int ChangeRange(string text)
        {
            int cRange;
            try
            {
                cRange = int.Parse(text);
            }
            catch (FormatException)
            {
                cRange = 0;
            }
            if (cRange > 255)
            {
                cRange = 255;
            }
            else if (cRange < 0)
            {
                cRange = 0;
            }
            this.Range = cRange;
            return this.Range;
        }


        static Rectangle wholeDesktopArea = new Rectangle(0, 0, Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);
        public Bitmap CalculateMarker(Bitmap frame, bool loadWorkingFrame)
        {
            this.Rect = wholeDesktopArea;
            Bitmap uneditedFrame = new Bitmap(frame);
            BitmapData ObjectsData = frame.LockBits(
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    ImageLockMode.ReadOnly, frame.PixelFormat);

            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            // set center color and radius
            filter.CenterColor.Color = this.Color;
            filter.Radius = (short)this.Range;
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
                this.Rect = new Rectangle(
                    new Point(biggestBlob.BlobPosition.X, biggestBlob.BlobPosition.Y),
                    blobSize);
                this.IsFound = true;
            }
            catch (ArgumentException)
            {
                //no blob found. stay on last known position
                this.IsFound = false;
            }
            frame.UnlockBits(ObjectsData);
            if (loadWorkingFrame)
            {
                MessageBox.Show("Load Working Frame");
                uneditedFrame = (Bitmap)frame.Clone();
            }
            return uneditedFrame;
        }
    }
}
