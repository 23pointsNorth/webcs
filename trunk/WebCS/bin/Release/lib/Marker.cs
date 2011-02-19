using System;
using System.Collections.Generic;
using System.Drawing;

public class Marker
{
    //const
    public static Point FIRSTMARKER = new Point(150, 100);
    public static Size FIRSTMARKER_SIZE = new Size(20, 20);
    public static Color FIRSTMARKER_COLOUR = Color.Blue;

    public static Point SECONDMARKER = new Point(225, 200);
    public static Size SECONDMARKER_SIZE = new Size(20, 20);
    public static Color SECONDMARKER_COLOUR = Color.Green;

    public static int NULL_RED = 10;
    public static int FULL_RED = 245;
    public static int NULL_GREEN = 10;
    public static int FULL_GREEN = 245;
    public static int NULL_BLUE = 10;
    public static int FULL_BLUE = 245;
    
    private Point center;

    public Point Center
    {
        set { center = value; }
        get { return center; }
    }

    List<Pixel> OkPixels = new List<Pixel>();

    public struct Pixel
    {
        private Color pixelColour;
        private Point pixelPoint;

        public Color PixelColour
        {
            set{pixelColour=value;} 
            get {return pixelColour;}
        }

        public Point PixelPoint
        {
            set { pixelPoint = value; }
            get { return pixelPoint; }
        }

        public Pixel(Point position, Color pixColour)
        {
            this.pixelPoint = position;
            this.pixelColour = pixColour;
        }

    }

    private Color avarageColour;
    public Color AvarageColour
    {
        set { avarageColour = value; }
        get { return avarageColour; }
    }

    public Marker(Bitmap frame, RectangleShape area)
    {
        int rectanglePixelWidth=
            area.RectangleUpperLeftAngle.X + area.RectangleSize.Width;
        int rectanglePixelHeight =
            area.RectangleUpperLeftAngle.Y + area.RectangleSize.Height;
        for (int x = area.RectangleUpperLeftAngle.X; x < rectanglePixelWidth ; x++)
        {
            for (int y = area.RectangleUpperLeftAngle.Y; y < rectanglePixelHeight; y++)
            {
                Color currentPixel;
                lock (frame)
                {
                    currentPixel = frame.GetPixel(x, y);
                }
                if ((currentPixel.R <= FULL_RED && currentPixel.R >= NULL_RED)&&
                    (currentPixel.B <= FULL_BLUE && currentPixel.B >= NULL_BLUE) &&
                    (currentPixel.G <= FULL_GREEN && currentPixel.G >= NULL_GREEN))
                {
                    OkPixels.Add(new Pixel(new Point(x, y), currentPixel));
                }
            }
        }
        CalculateAvarageColour(OkPixels);
        CalculateRange(OkPixels);
        CalculatePosition(OkPixels);
    }

    public Point CalculatePosition(List<Pixel> OkPxls)
    {
        long sumOfX = 0;
        long sumOfY = 0;

        foreach (var pixel in OkPxls)
        {
            sumOfX += pixel.PixelPoint.X;
            sumOfY += pixel.PixelPoint.Y;
        }
        
        int pixelCount = OkPxls.Count;

        if (pixelCount != 0)
        {
            int avarageX = (int)sumOfX / pixelCount;
            int avarageY = (int)sumOfY / pixelCount;
            center = new Point(avarageX, avarageY);
            return center;
        }
        else
        {
            return new Point(-1, -1);
        }
    }

    public void CalculateAvarageColour(List<Pixel> OkPixels)
    {
        long sumOfRed=0;
        long sumOfBlue=0;
        long sumOfGreen=0;

        foreach (var pixel in OkPixels)
        {
            sumOfRed += pixel.PixelColour.R;
            sumOfBlue += pixel.PixelColour.B;
            sumOfGreen += pixel.PixelColour.G;
        }

        int pixelCount = OkPixels.Count;
        if (pixelCount != 0)
        {
            int avarageRed = (int)sumOfRed / pixelCount;
            int avarageBlue = (int)sumOfBlue / pixelCount;
            int avaageGreen = (int)sumOfGreen / pixelCount;
            avarageColour = Color.FromArgb(avarageRed, avarageBlue, avaageGreen);
        }
    }

    private int range;
    public int Range
    {
        set { range = value; }
        get { return range; }
    }

    private void CalculateRange(List<Pixel> OkPixels)
    {
        int minColour = 255;
        int maxColour = 0;

        foreach (var pixel in OkPixels)
        {
            int minCurrentPixelColour = Math.Min(pixel.PixelColour.B, 
                Math.Min(pixel.PixelColour.R, pixel.PixelColour.G));
            minColour = Math.Min(minColour, minCurrentPixelColour);
            int maxCurrentPixelColour = Math.Max(pixel.PixelColour.R,
                Math.Max(pixel.PixelColour.B, pixel.PixelColour.G));
            maxColour = Math.Max(maxColour, maxCurrentPixelColour);
        }
        int toMaxRange= maxColour - Math.Max(
            avarageColour.R,Math.Max(avarageColour.B,avarageColour.G));
        int toMinRange = Math.Max(avarageColour.R,
            Math.Max(avarageColour.B, avarageColour.G)) - minColour;

        this.Range = Math.Max(toMaxRange, toMinRange) / 2; //(int)(minColour + maxColour)/2;
    }

    public Marker(int x, int y)
    {
        this.center.X = x;
        this.center.Y = y;
    }

    public bool AreColoursEqual(Color targetColour)
    {
        //if equal would return true
        return (
            Math.Abs(targetColour.R - avarageColour.R) <= range &&
            Math.Abs(targetColour.G - avarageColour.G) <= range &&
            Math.Abs(targetColour.B - avarageColour.B) <= range);
        
    }

}
