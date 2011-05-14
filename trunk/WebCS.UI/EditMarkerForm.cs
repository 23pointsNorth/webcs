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
    public partial class EditMarkerForm : RadForm
    {
        List<ColorMarker> markers = new List<ColorMarker>();
        int index;
        WebCSForm parentForm;

        public EditMarkerForm(WebCSForm mainForm, ref List<ColorMarker> markerList, int markerIndex)
        {
            InitializeComponent();
            parentForm = mainForm;
            EnablingOfEditButton();
            markers = markerList;
            index = markerIndex;
            LoadMarkerInfo();
        }

        public void EnablingOfEditButton()
        {
            if (parentForm.IsVideoRunning && !parentForm.IsTrackingEnabled) 
            { 
                changeColorRadButton.Enabled = true; 
            }
            else 
            { 
                changeColorRadButton.Enabled = false; 
            }
        }

        private void LoadMarkerInfo()
        {
            markerNameRadTextBox.Text = markers[index].Name;
            markerRangeRadTextBox.Text = markers[index].Range.ToString();
            markerPriorityRadTextBox.Text = markers[index].Priority.ToString();
            samplePictureBox.Image = BitmapDraw.FilledRectangle(
                samplePictureBox.Width, samplePictureBox.Height, markers[index].Color);
            //markers[index].FoundMarkerRectC = ColorExtention.RandomColor();
            outliningColorPictureBox.Image = BitmapDraw.FilledRectangle(
                outliningColorPictureBox.Width,outliningColorPictureBox.Height,
                markers[index].FoundMarkerRectC);

            noneRadRadioButton.IsChecked = false;
            if (ColorMarker.IndexMarker.Primary == index) primaryRadRadioButton.IsChecked = true;
            else
            {
                primaryRadRadioButton.IsChecked = false;
                if (ColorMarker.IndexMarker.Secondary == index) secondaryRadRadioButton.IsChecked = true;
                else
                {
                    secondaryRadRadioButton.IsChecked = false;
                    noneRadRadioButton.IsChecked = true;
                }
            }

        }

        private void CloseRadButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveRadButton_Click(object sender, EventArgs e)
        {
            markers[index].ChangeName(markerNameRadTextBox.Text);
            markers[index].ChangeRange(markerRangeRadTextBox.Text);
            int currentPriority = int.Parse(markerPriorityRadTextBox.Text);
            int finalPriority = currentPriority;
            while (markers[index].Priority != finalPriority && 
                MarkerBase.takenPriorities.Contains(currentPriority))
            {
                currentPriority++;
            }
            if (currentPriority != finalPriority && currentPriority != markers[index].Priority)
            {
                MessageBox.Show(
                    "Priority already exists. Suggested: " + currentPriority.ToString(), 
                    "Priority change error");
                return;
            }
            markers[index].ChangePriority(currentPriority);
            parentForm.UpdateMarkersList();
            closeRadButton.PerformClick();
        }

        private void markerPriorityRadTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int.Parse(markerNameRadTextBox.Text);
            }
            catch (FormatException)
            {
                markerNameRadTextBox.Text.Remove(markerNameRadTextBox.Text.Length - 1);
            }
        }

        private void changeColorRadButton_Click(object sender, EventArgs e)
        {
            saveRadButton.PerformClick();
            AddMarkerForm changeColorForm = new AddMarkerForm(
                parentForm, parentForm.ReturnFrame(), ref markers, index);
            changeColorForm.Show();
        }

        private void outliningColorPictureBox_Click(object sender, EventArgs e)
        {
            trackingRadColorDialog.ColorDialogForm.SelectedColor =
                markers[index].FoundMarkerRectC;
            if (trackingRadColorDialog.ShowDialog() == DialogResult.OK)
            {
                markers[index].FoundMarkerRectC = 
                    trackingRadColorDialog.ColorDialogForm.SelectedColor;
            }
            outliningColorPictureBox.Image = BitmapDraw.FilledRectangle(
                outliningColorPictureBox.Width, outliningColorPictureBox.Height,
                markers[index].FoundMarkerRectC);
        }

        private void samplePictureBox_Click(object sender, EventArgs e)
        {
            trackingRadColorDialog.ColorDialogForm.SelectedColor =
                markers[index].Color;
            if (trackingRadColorDialog.ShowDialog() == DialogResult.OK)
            {
                markers[index].ChangeColor( 
                    trackingRadColorDialog.ColorDialogForm.SelectedColor);
            }
            samplePictureBox.Image = BitmapDraw.FilledRectangle(
                samplePictureBox.Width, samplePictureBox.Height, markers[index].Color);
        }


        //public EditMarkerForm(ref List<FeatureMarker> markerList, int index)
        //{
        //    InitializeComponent();
        //}
    }
}
