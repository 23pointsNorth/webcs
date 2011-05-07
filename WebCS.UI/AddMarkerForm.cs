using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Marker;
using BitmapProcessing;

namespace WebCS
{
    public partial class AddMarkerForm : RadForm
    {
        ColorMarker clrMarker;
        List<ColorMarker> colorMarkerList = new List<ColorMarker>();

        Bitmap originalFrame;
        enum type
        {
            color,
            feature
        };

        type markerType;

        public AddMarkerForm(Bitmap frame, ref List<ColorMarker> markerList)
        {
            InitializeComponent();
            markerType = type.color;
            originalFrame = new Bitmap(frame);
            frameImageContainer.Image = frame;

            colorMarkerList = markerList;
            int addedPriority = 0;
            do
            {
                clrMarker = new ColorMarker(
                    "Color Marker " + (MarkerBase.NextMarkerNumber + addedPriority).ToString(),
                    MarkerBase.NextMarkerNumber);
                addedPriority++;
            } while (clrMarker == null);
            
        }

        //public AddMarkerForm(Bitmap frame, ref FeatureMarker marker, string name)
        //{
        //    InitializeComponent();
        
        //    markerType = type.feture;
        //}

        private void closeRadButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void extractRadButton_Click(object sender, EventArgs e)
        {
            if (markerType == type.color)
            {
                //create clrMarker stuff such as color;
                clrMarker.ChangeColor(originalFrame, markerRectangle);
                colorMarkerList.Add(clrMarker);
                EditMarkerForm editMarker = new EditMarkerForm(
                    ref colorMarkerList, colorMarkerList.IndexOf(clrMarker));
                editMarker.Show();
            }

            closeRadButton.PerformClick();
        }

        Point upperLeftCorner;
        bool mouseDown = false;

        private void frameImageContainer_MouseDown(object sender, MouseEventArgs e)
        {
            upperLeftCorner = e.Location;
            mouseDown = true;
        }

        Rectangle markerRectangle;

        private void frameImageContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                frameImageContainer.Image = new Bitmap(originalFrame);
                markerRectangle = new Rectangle(
                        upperLeftCorner.X, upperLeftCorner.Y,
                        Math.Abs(upperLeftCorner.X - e.X),
                        Math.Abs(upperLeftCorner.Y - e.Y));
                BitmapDraw.Rectangle(
                    (Bitmap)frameImageContainer.Image,
                    markerRectangle,
                    new Pen(Color.Red)
                );
            }
        }

        private void frameImageContainer_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            extractRadButton.Enabled = true;
            markerRectangle = new Rectangle(
                upperLeftCorner.X, upperLeftCorner.Y,
                Math.Abs(upperLeftCorner.X - e.X),
                Math.Abs(upperLeftCorner.Y - e.Y));
        }
    }
}
