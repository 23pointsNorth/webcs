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
            MessageBox.Show(currentPriority.ToString());
            MessageBox.Show("Initial " + markers[index].Priority.ToString());
            while (markers[index].Priority != finalPriority && MarkerBase.takenPriorities.Contains(currentPriority))
            {
                currentPriority++;
            }
            if (currentPriority != finalPriority && currentPriority != markers[index].Priority)
            {
                MessageBox.Show(markers[index].Priority.ToString());
                MessageBox.Show("Priority already exists. Suggested: " + currentPriority.ToString(), "Priority Change");
                foreach (var p in MarkerBase.takenPriorities)
                {
                    MessageBox.Show(p.ToString());
                }
                return;
            }
            markers[index].ChangePriority(currentPriority);
            MessageBox.Show("Last " + markers[index].Priority.ToString());
            parentForm.UpdateMarkersList();
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
