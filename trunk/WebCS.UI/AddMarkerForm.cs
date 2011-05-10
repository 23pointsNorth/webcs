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
using WebCS.Utilities;

namespace WebCS
{
    public partial class AddMarkerForm : RadForm
    {
        ColorMarker clrMarker;
        List<ColorMarker> colorMarkerList = new List<ColorMarker>();
        WebCSForm parentForm;
        Bitmap originalFrame;

        enum type
        {
            newColor,
            editColor,
            feature
        };

        type markerType;

        public AddMarkerForm(WebCSForm mainForm, Bitmap frame, ref List<ColorMarker> markerList)
        {
            InitializeComponent();
            parentForm = mainForm;
            markerType = type.newColor;
            originalFrame = new Bitmap(frame);
            frameImageContainer.Image = frame;

            colorMarkerList = markerList;
            int addedPriority = 0;
            while (MarkerBase.takenPriorities.Contains(MarkerBase.NextMarkerNumber + addedPriority))
            {
                addedPriority++;
            }
            clrMarker = new ColorMarker(
                "ColorMarker" + (MarkerBase.NextMarkerNumber + addedPriority).ToString(),
                MarkerBase.NextMarkerNumber + addedPriority);
        }

        ColorMarker changeMarker;
        public AddMarkerForm(WebCSForm mainForm, Bitmap frame, ref List<ColorMarker> markerList, int markerIndex)
        {
            InitializeComponent();
            parentForm = mainForm;
            markerType = type.editColor;
            originalFrame = new Bitmap(frame);
            frameImageContainer.Image = frame;
            colorMarkerList = markerList;
            changeMarker = markerList[markerIndex];
        }

        //public AddMarkerForm(Bitmap frame, ref FeatureMarker marker, string name)
        //{
        //    InitializeComponent();
        
        //    markerType = type.feture;
        //}

        private void closeRadButton_Click(object sender, EventArgs e)
        {
            clrMarker.RemoveMarker();
            this.Close();
        }

        private void extractRadButton_Click(object sender, EventArgs e)
        {
            if (markerType == type.newColor)
            {
                //create clrMarker stuff such as color;
                clrMarker.ChangeColor(originalFrame, markerRectangle);
                clrMarker.FoundMarkerRectC = ColorExtention.RandomColor();
                colorMarkerList.Add(clrMarker);
                EditMarkerForm editMarker = new EditMarkerForm(
                    parentForm, ref colorMarkerList, colorMarkerList.IndexOf(clrMarker));
                editMarker.Show();
            }
            else if (markerType == type.editColor)
            {
                changeMarker.ChangeColor(originalFrame, markerRectangle);
                EditMarkerForm editMarker = new EditMarkerForm(
                    parentForm, ref colorMarkerList, colorMarkerList.IndexOf(changeMarker));
                editMarker.Show();
            }

            this.Close();
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
