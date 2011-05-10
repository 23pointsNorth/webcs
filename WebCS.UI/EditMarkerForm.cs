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
    public partial class EditMarkerForm : RadForm
    {
        List<ColorMarker> markers = new List<ColorMarker>();
        int index;
        WebCSForm parentForm;

        const int SAMPLE_WIDTH = 75;
        const int SAMPLE_HEIGHT = 75;

        public EditMarkerForm(WebCSForm mainForm, ref List<ColorMarker> markerList, int markerIndex)
        {
            InitializeComponent();
            parentForm = mainForm;
            if (parentForm.isVideoRunning) { changeColorRadButton.Enabled = true; }
            else { changeColorRadButton.Enabled = false; }
            markers = markerList;
            index = markerIndex;
            LoadMarkerInfo();
            //trackingRadColorDialog.Container.
        }

        private void LoadMarkerInfo()
        {
            markerNameRadTextBox.Text = markers[index].Name;
            markerRangeRadTextBox.Text = markers[index].Range.ToString();
            markerPriorityRadTextBox.Text = markers[index].Priority.ToString();
            samplePictureBox.Image = BitmapDraw.FilledRectangle(
                SAMPLE_WIDTH, SAMPLE_HEIGHT, markers[index].Color);
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
            while (markers[index].Priority != finalPriority && MarkerBase.takenPriorities.Contains(currentPriority))
            {
                currentPriority++;
            }
            if (currentPriority != finalPriority && currentPriority != markers[index].Priority)
            {
                MessageBox.Show("Priority already exists. Suggested: " + currentPriority.ToString(), "Priority change error");
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
            if (trackingRadColorDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(trackingRadColorDialog.ColorDialogForm.SelectedColor.ToString());
            }
            saveRadButton.PerformClick();
            AddMarkerForm changeColorForm = new AddMarkerForm(parentForm, parentForm.ReturnFrame(), ref markers, index);
            changeColorForm.Show();
        }


        //public EditMarkerForm(ref List<FeatureMarker> markerList, int index)
        //{
        //    InitializeComponent();
        //}
    }
}
