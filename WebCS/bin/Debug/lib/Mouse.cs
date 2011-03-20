﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Mouse
{
    Point mouse;
    Point pressure;
    private int deltaPosition;
    private const int acuracyInPixels = 1;
    Rectangle desktopAreaBoundries;

    public int DeltaPosition
    {
        set { this.deltaPosition = value; }
    }
    public Point MousePoint
    {
        get { return mouse; }
    }

    public Point PressurePoint
    {
        get { return pressure; }
    }

    public Rectangle DesktopArea
    {
        set { this.desktopAreaBoundries = value; }
    }

    public Mouse(Point fisrtCoordinates, Point secondCoordinates, int positionDifference)
    {
        SetNewPosition(fisrtCoordinates, secondCoordinates);
        this.deltaPosition = positionDifference;
    }

    public void SetNewPosition(Point fisrtCoordinates, Point secondCoordinates)
    {
        this.mouse = fisrtCoordinates;
        this.pressure = secondCoordinates;
    }

    public void SetNewPosition(Rectangle firstMarker, Rectangle secondMarker)
    {
        Point uncheckedMouse = new Point(
            firstMarker.X + firstMarker.Width / 2, firstMarker.Y + firstMarker.Height / 2);
        Point uncheckedPressure = new Point(
            secondMarker.X + secondMarker.Width / 2, secondMarker.Y + secondMarker.Height / 2);
        this.mouse = new Point(
            Math.Min(desktopAreaBoundries.X+desktopAreaBoundries.Width,Math.Max(uncheckedMouse.X,desktopAreaBoundries.X)),
            Math.Min(desktopAreaBoundries.Y+desktopAreaBoundries.Height,Math.Max(uncheckedMouse.Y,desktopAreaBoundries.Y)));
        this.pressure = new Point(
            Math.Min(desktopAreaBoundries.X + desktopAreaBoundries.Width, Math.Max(uncheckedPressure.X, desktopAreaBoundries.X)),
            Math.Min(desktopAreaBoundries.Y + desktopAreaBoundries.Height, Math.Max(uncheckedPressure.Y, desktopAreaBoundries.Y)));

        Cursor.Position = newPostionInScreenPixels();
    }

    public void Click()
    {
        if (mouse.X >= 0 && mouse.Y >= 0)
        {
            if (Math.Abs(mouse.X - pressure.X) < deltaPosition &&
                Math.Abs(mouse.Y - pressure.Y) < deltaPosition)
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
        int screenX = (int)(Cursor.Clip.Width * 1.0 / desktopAreaBoundries.Width) * (mouse.X - desktopAreaBoundries.X);
        int screenY = (int)(Cursor.Clip.Height * 1.0 / desktopAreaBoundries.Height) * (mouse.Y - desktopAreaBoundries.Y);
       
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
