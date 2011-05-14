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
        bool isFound = false;
        Rectangle rect;
        Color foundMarkerRectC;

        const int MIN_BLOB_HEIGHT = 10;
        const int MIN_BLOB_WIDTH = 10;

        private short lowerLimit = 0;
        private short upperLimit = 255;
        public static Color emptyColor = Color.FromArgb(0, 0, 0);
        
        public Color Color { get { return color; } }
        public short Range { get { return range; } }
        public bool IsFound { get { return isFound; } }
        public Rectangle Rect { get { return rect; } }
        public Color FoundMarkerRectC
        {
            set { foundMarkerRectC = value; }
            get { return foundMarkerRectC; }
        }

        public ColorMarker(string name,int priority ,Color colorValue, short rangeValue, Color foundMarkerRectColor, short lowerLimitValue, short upperLimitValue) : base(name, priority)
        {
            this.color = colorValue;
            this.range = rangeValue;
            this.foundMarkerRectC = foundMarkerRectColor;
            this.lowerLimit = lowerLimitValue;
            this.upperLimit = upperLimitValue;
        }

        public ColorMarker(string name, int priority) : base(name, priority)
        {

        }
        public ColorMarker() { }

        public static bool TryParse(string fullStringInfo, out ColorMarker convertedMarker)
        {
            int numberOfParameters = 7;
            bool isSuccessful = false;
            convertedMarker = new ColorMarker();
            if (fullStringInfo == null) { 
                //throw new ArgumentException("Parsed string is null");
                return isSuccessful;
            }
            try
            {
                if ((!fullStringInfo.StartsWith("{")) || (!fullStringInfo.EndsWith("}"))) 
                { 
                    throw new FormatException("No {} brackets were found."); 
                }
                string stringInfo = fullStringInfo.Substring(1, fullStringInfo.Length - 2);
                string[] parameters = stringInfo.Split(';');
                if (parameters.Length != numberOfParameters)
                {
                    throw new FormatException("Not all parameters found."); 
                }
                string newMarkerName = parameters[0];
                int newMarkerPriority = int.Parse(parameters[1]);
                convertedMarker = new ColorMarker(newMarkerName, newMarkerPriority);
                convertedMarker.color = Color.FromArgb(int.Parse(parameters[2]));
                convertedMarker.range = short.Parse(parameters[3]);
                convertedMarker.foundMarkerRectC = Color.FromArgb(int.Parse(parameters[4]));
                convertedMarker.lowerLimit = short.Parse(parameters[5]);
                convertedMarker.upperLimit = short.Parse(parameters[6]);
                //add other params
                isSuccessful = true;
            }
            catch (FormatException)
            {
                return isSuccessful;
            }
            return isSuccessful;
        }
        public void ChangeColor(Color newColor)
        {
            color = newColor;
        }

        public void ChangeColor(Bitmap frame, Rectangle rect)
        {
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

        public override string ToString()
        {
            return
                @"{" + this.markerName + ";" + this.priority.ToString() + ";" +
                this.color.ToArgb().ToString()+ ";" + this.range.ToString() + ";" +
                this.foundMarkerRectC.ToArgb().ToString()+ ";" + 
                this.lowerLimit.ToString() + ";" +
                this.upperLimit.ToString() + "}";
        }
    }
}