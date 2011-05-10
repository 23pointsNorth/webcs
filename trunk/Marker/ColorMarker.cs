using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;

namespace Marker
{
    public class ColorMarker : MarkerBase
    {
        Color color;
        short range;
        bool isColorChange = false;
        bool isFound = false;
        Rectangle rect;
        Rectangle getColorRect;
        Color foundMarkerRectC;
        Color changeColorRectC;

        const int MIN_BLOB_HEIGHT = 10;
        const int MIN_BLOB_WIDTH = 10;

        public struct _index{
            public ushort Primary;
            public ushort Secondary;
            public _index(ushort p, ushort s) { this.Primary = p; this.Secondary = s; }
        }

        public static _index IndexMarker = new _index(0, 1);

        private short lowerLimit = 0;
        private short upperLimit = 255;
        public static Color emptyColor = Color.FromArgb(0, 0, 0);
        
        public Color Color { get { return color; } }
        public short Range { get { return range; } }
        public bool IsColorChange { get { return isColorChange; } set { isColorChange = value; } }
        public bool IsFound { get { return isFound; } }
        public Rectangle Rect { get { return rect; } }
        public Rectangle GetColorRect { get { return getColorRect; } }
        public Color FoundMarkerRectC
        {
            set { foundMarkerRectC = value; }
            get { return foundMarkerRectC; }
        }
       /*unneeded*/ public Color ChangeColorRectC { get { return changeColorRectC; } }

        public ColorMarker(string name,int priority ,Color colorValue, short rangeValue, Color foundMarkerRectColor, Rectangle getColorRectValue, Color changeColorRect, short lowerLimitValue, short upperLimitValue) : base(name, priority)
        {
            this.color = colorValue;
            this.range = rangeValue;
            this.foundMarkerRectC = foundMarkerRectColor;
            this.getColorRect = getColorRectValue;
            this.changeColorRectC = changeColorRect;
            this.lowerLimit = lowerLimitValue;
            this.upperLimit = upperLimitValue;
        }

        public ColorMarker(string name, int priority) : base(name, priority)
        {

        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        public void ChangeColor(Bitmap frame)
        {
            ChangeColor(frame, this.getColorRect);
        }

        public void ChangeColor(Bitmap frame, Rectangle rect)
        {
            isColorChange = false;

            //get rectangle info; 
            Bitmap sample;
            lock (frame)
            {
                sample = frame.Clone(rect, frame.PixelFormat);
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

            this.color = Color.FromArgb(meanRed, meanGreen, meanBlue);
        }

        public int ChangeRange(string text)
        {
            short cRange;
            try
            {
                cRange = short.Parse(text);
            }
            catch (FormatException)
            {
                cRange = lowerLimit;
            }
            catch (OverflowException)
            {
                cRange = upperLimit;
            }
            if (cRange > upperLimit)
            {
                cRange = upperLimit;
            }
            else if (cRange < lowerLimit)
            {
                cRange = lowerLimit;
            }
            this.range = cRange;
            return this.range;
        }

        override public void CalculateMarker(Bitmap frame)
        {
            BitmapData ObjectsData = frame.LockBits(
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    ImageLockMode.ReadOnly, frame.PixelFormat);

            ColorFiltering filter = new ColorFiltering();
            //set range with (min,max) value
            filter.Blue = new IntRange(color.B - range, color.B + range);
            filter.Red = new IntRange(color.R - range, color.R + range);
            filter.Green = new IntRange(color.G - range, color.G + range);
            filter.ApplyInPlace(ObjectsData);

            try
            {
                ExtractBiggestBlob biggestBlob = new ExtractBiggestBlob();
                Size blobSize = biggestBlob.Apply(ObjectsData).Size; // returns a bitmap - need only size
                if (blobSize.Height < MIN_BLOB_HEIGHT &&
                    blobSize.Width < MIN_BLOB_WIDTH)
                {
                    throw new ArgumentException("Blob too small.");
                }
                rect = new Rectangle(
                    new Point(biggestBlob.BlobPosition.X, biggestBlob.BlobPosition.Y),
                    blobSize);
                isFound = true;
            }
            catch (ArgumentException)
            {
                //no blob found. stay on last known position
                isFound = false;
            }
            frame.UnlockBits(ObjectsData);
            leftOvers[markerNumber] = frame;
        }
    }
}