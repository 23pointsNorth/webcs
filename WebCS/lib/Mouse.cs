using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BitmapProcessing;

public class Mouse
{
    Point mouse;
    Point pressure;
    private const int deltaX = 50;
    private const int deltaY = 50;
    private const int acuracyInPixels = 1;

    public Mouse(Point fisrtCoordinates, Point secondCoordinates)
    {
        SetNewPosition(fisrtCoordinates, secondCoordinates);
    }

    public Mouse(Bitmap frame, Marker firstMarker, Marker secondMarker)
    {
        CalculateNewPosition(frame,firstMarker,secondMarker);
    }

    public void SetNewPosition(Point fisrtCoordinates, Point secondCoordinates)
    {
        mouse = fisrtCoordinates;
        pressure = secondCoordinates;
    }

    public void SetNewPosition(Rectangle firstMarker, Rectangle secondMarker, Rectangle desktopArea)
    {
        mouse = new Point(
            firstMarker.X + firstMarker.Width / 2, firstMarker.Y + firstMarker.Height / 2);
        pressure = new Point(
            secondMarker.X + secondMarker.Width / 2, secondMarker.Y + secondMarker.Height / 2);
    }

    public void CalculateNewPosition(Bitmap frame, Marker firstMarker, Marker secondMarker)
    {
        List<Marker.Pixel> foundPixelsFirst = new List<Marker.Pixel>();
        List<Marker.Pixel> foundPixelsSecond = new List<Marker.Pixel>();

        FastBitmap frameLocked = new FastBitmap(frame);
        frameLocked.LockImage();

        int frameWidth = frame.Width;
        int frameHeight = frame.Height;

        for (int x = 0; x < frameWidth; x += acuracyInPixels)
        {
            for (int y = 0; y < frameHeight; y += acuracyInPixels)
            {
                Color currentPixel = frameLocked.GetPixel(x, y);
                if (firstMarker.AreColoursEqual(currentPixel))
                {
                    foundPixelsFirst.Add(new Marker.Pixel(
                        new Point(x, y), currentPixel));
                }
                if (secondMarker.AreColoursEqual(currentPixel))
                {
                    foundPixelsSecond.Add(new Marker.Pixel(
                        new Point(x, y), currentPixel));
                }
            }
        }

        frameLocked.UnlockImage();

        mouse = firstMarker.CalculatePosition(foundPixelsFirst);
        pressure = secondMarker.CalculatePosition(foundPixelsSecond);

    }

    public void MoveMouseAndClick()
    {
        if (mouse.X >= 0 && mouse.Y >= 0)
        {
            Cursor.Position = newPostionInScreenPixels();

            if (Math.Abs(mouse.X - pressure.X) < deltaX &&
                Math.Abs(mouse.Y - pressure.Y) < deltaY)
            {
                //isMouseDown = true;
                doMouseClick(); 
            }
            else
            {
                //isMouseDown = false;
            }
        }
    }

    private Point newPostionInScreenPixels()
    {
        int screenX = (int)(Cursor.Clip.Width * 1.0 / Constants.IMAGE_WIDTH * mouse.X);
        int screenY = (int)(Cursor.Clip.Height * 1.0 / Constants.IMAGE_HEIGHT * mouse.Y);
        
        return new Point(screenX, screenY);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;

    private void doMouseClickOnTarget(Point position)
    {
        mouse_event(
            MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP,
            position.X, position.Y, 0, 0);
    }

    private void doMouseClick()
    {
        mouse_event(
            MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP,
            Cursor.Position.X, Cursor.Position.Y, 0, 0);
    }
}
