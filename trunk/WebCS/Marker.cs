using System;
using System.Drawing;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Math;
using System.Drawing.Imaging;

class Marker
{
    Color color;
    short range;
    bool isColorChange = false;
    bool isFound = false;
    Rectangle rect;
    Rectangle getColorRect;
    Color foundMarkerRectC;
    Color changeColorRectC;

    public static short upperLimit = 255;
    public static short lowerLimit = 0;
    public static Color emptyColor = Color.FromArgb(0, 0, 0);
    public static Rectangle wholeDesktopArea = new Rectangle(0, 0, Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);

    public Color Color { get { return color; } }
    public short Range { get { return range; } }
    public bool IsColorChange { get; set; }
    public bool IsFound { get { return isFound; } }
    public Rectangle Rect { get { return rect; } }
    public Rectangle GetColorRect { get { return getColorRect; } }
    public Color FoundMarkerRectC { get { return foundMarkerRectC; } }
    public Color ChangeColorRectC { get { return changeColorRectC; } }

    public Marker(Color colorValue, short rangeValue, Color foundMarkerRectColor, Rectangle getColorRectValue, Color changeColorRect)
    {
        this.color = colorValue;
        this.range = rangeValue;
        this.foundMarkerRectC = foundMarkerRectColor;
        this.getColorRect = getColorRectValue;
        this.changeColorRectC = changeColorRect;
    }

    public void ChangeColor(Bitmap frame)
    {
        isColorChange = false;

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

    public Bitmap CalculateMarker(Bitmap frame)
    {
        BitmapData ObjectsData = frame.LockBits(
                new Rectangle(0, 0, frame.Width, frame.Height),
                ImageLockMode.ReadOnly, frame.PixelFormat);

        EuclideanColorFiltering filter = new EuclideanColorFiltering();
        // set center color and radius
        filter.CenterColor.Color = color;
        filter.Radius = range;
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

        return frame;
    }
}
