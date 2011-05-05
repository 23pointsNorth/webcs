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

namespace WebCS
{
    public partial class EditMarkerForm : RadForm
    {
        public EditMarkerForm(ref List<ColorMarker> markerList)
        {
            InitializeComponent();
        }

        private void CloseRadButton_Click(object sender, EventArgs e)
        {
            //this.Hide();
            this.Close();
        }

        //public EditMarkerForm(ref List<FeatureMarker> markerList)
        //{
        //    InitializeComponent();
        //}
    }
}
