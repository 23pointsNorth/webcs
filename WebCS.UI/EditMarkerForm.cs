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
        const int SAMPLE_WIDTH = 75;
        const int SAMPLE_HEIGHT = 75;

        public EditMarkerForm(ref List<ColorMarker> markerList, int markerIndex)
        {
            InitializeComponent();

            markers = markerList;
            index = markerIndex;
            LoadMarkerInfo();
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
            while (MarkerBase.takenPriorities.Contains(currentPriority))
            {
                currentPriority++;
            }
            if (currentPriority != finalPriority)
            {
                MessageBox.Show("Priority already exists. Suggested: " + currentPriority.ToString());
                return;
            }
            ((MarkerBase)markers[index]).ChangePriority(currentPriority);
            //WebCS.WebCSForm.
            //call update function from man window 
            CloseRadButton.PerformClick();
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

        //public EditMarkerForm(ref List<FeatureMarker> markerList, int index)
        //{
        //    InitializeComponent();
        //}
    }
}
