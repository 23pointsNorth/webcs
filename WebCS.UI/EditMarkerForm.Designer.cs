namespace WebCS
{
    partial class EditMarkerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditMarkerForm));
            this.samplePictureBox = new System.Windows.Forms.PictureBox();
            this.markerNameRadTextBox = new Telerik.WinControls.UI.RadTextBox();
            this.nameRadLabel = new Telerik.WinControls.UI.RadLabel();
            this.markerRangeRadTextBox = new Telerik.WinControls.UI.RadTextBox();
            this.rangeRadLabel = new Telerik.WinControls.UI.RadLabel();
            this.markerPriorityRadTextBox = new Telerik.WinControls.UI.RadTextBox();
            this.priorityRadLabel = new Telerik.WinControls.UI.RadLabel();
            this.workingFrameRadCheckBox = new Telerik.WinControls.UI.RadCheckBox();
            this.saveRadButton = new Telerik.WinControls.UI.RadButton();
            this.vistaTheme = new Telerik.WinControls.Themes.VistaTheme();
            this.closeRadButton = new Telerik.WinControls.UI.RadButton();
            this.changeColorRadButton = new Telerik.WinControls.UI.RadButton();
            this.trackingRadColorDialog = new Telerik.WinControls.RadColorDialog();
            this.outliningColorRadLabel = new Telerik.WinControls.UI.RadLabel();
            this.outliningColorPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.samplePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerNameRadTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerRangeRadTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerPriorityRadTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priorityRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workingFrameRadCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveRadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeRadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeColorRadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outliningColorRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outliningColorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // samplePictureBox
            // 
            this.samplePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.samplePictureBox.Location = new System.Drawing.Point(12, 12);
            this.samplePictureBox.Name = "samplePictureBox";
            this.samplePictureBox.Size = new System.Drawing.Size(75, 75);
            this.samplePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.samplePictureBox.TabIndex = 0;
            this.samplePictureBox.TabStop = false;
            this.samplePictureBox.Click += new System.EventHandler(this.samplePictureBox_Click);
            // 
            // markerNameRadTextBox
            // 
            this.markerNameRadTextBox.Location = new System.Drawing.Point(133, 10);
            this.markerNameRadTextBox.Name = "markerNameRadTextBox";
            this.markerNameRadTextBox.Size = new System.Drawing.Size(115, 20);
            this.markerNameRadTextBox.TabIndex = 1;
            this.markerNameRadTextBox.TabStop = false;
            this.markerNameRadTextBox.Text = "ColorMarker";
            this.markerNameRadTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.markerNameRadTextBox.ThemeName = "Vista";
            // 
            // nameRadLabel
            // 
            this.nameRadLabel.Location = new System.Drawing.Point(93, 12);
            this.nameRadLabel.Name = "nameRadLabel";
            this.nameRadLabel.Size = new System.Drawing.Size(36, 16);
            this.nameRadLabel.TabIndex = 2;
            this.nameRadLabel.Text = "Name";
            // 
            // markerRangeRadTextBox
            // 
            this.markerRangeRadTextBox.Location = new System.Drawing.Point(133, 36);
            this.markerRangeRadTextBox.Name = "markerRangeRadTextBox";
            this.markerRangeRadTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.markerRangeRadTextBox.Size = new System.Drawing.Size(32, 20);
            this.markerRangeRadTextBox.TabIndex = 13;
            this.markerRangeRadTextBox.TabStop = false;
            this.markerRangeRadTextBox.Text = "20";
            this.markerRangeRadTextBox.ThemeName = "Vista";
            // 
            // rangeRadLabel
            // 
            this.rangeRadLabel.Location = new System.Drawing.Point(93, 36);
            this.rangeRadLabel.Name = "rangeRadLabel";
            this.rangeRadLabel.Size = new System.Drawing.Size(40, 16);
            this.rangeRadLabel.TabIndex = 12;
            this.rangeRadLabel.Text = "Range";
            this.rangeRadLabel.ThemeName = "ControlDefault";
            // 
            // markerPriorityRadTextBox
            // 
            this.markerPriorityRadTextBox.Location = new System.Drawing.Point(133, 60);
            this.markerPriorityRadTextBox.Name = "markerPriorityRadTextBox";
            this.markerPriorityRadTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.markerPriorityRadTextBox.Size = new System.Drawing.Size(32, 20);
            this.markerPriorityRadTextBox.TabIndex = 15;
            this.markerPriorityRadTextBox.TabStop = false;
            this.markerPriorityRadTextBox.Text = "1";
            this.markerPriorityRadTextBox.ThemeName = "Vista";
            this.markerPriorityRadTextBox.TextChanged += new System.EventHandler(this.markerPriorityRadTextBox_TextChanged);
            // 
            // priorityRadLabel
            // 
            this.priorityRadLabel.Location = new System.Drawing.Point(93, 60);
            this.priorityRadLabel.Name = "priorityRadLabel";
            this.priorityRadLabel.Size = new System.Drawing.Size(42, 16);
            this.priorityRadLabel.TabIndex = 14;
            this.priorityRadLabel.Text = "Priority";
            this.priorityRadLabel.ThemeName = "ControlDefault";
            // 
            // workingFrameRadCheckBox
            // 
            this.workingFrameRadCheckBox.Location = new System.Drawing.Point(100, 93);
            this.workingFrameRadCheckBox.Name = "workingFrameRadCheckBox";
            this.workingFrameRadCheckBox.Size = new System.Drawing.Size(119, 16);
            this.workingFrameRadCheckBox.TabIndex = 16;
            this.workingFrameRadCheckBox.Text = "Load working frame";
            this.workingFrameRadCheckBox.ThemeName = "Vista";
            // 
            // saveRadButton
            // 
            this.saveRadButton.Location = new System.Drawing.Point(12, 124);
            this.saveRadButton.Name = "saveRadButton";
            this.saveRadButton.Size = new System.Drawing.Size(82, 25);
            this.saveRadButton.TabIndex = 17;
            this.saveRadButton.Text = "Save";
            this.saveRadButton.ThemeName = "Vista";
            this.saveRadButton.Click += new System.EventHandler(this.saveRadButton_Click);
            // 
            // closeRadButton
            // 
            this.closeRadButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeRadButton.Location = new System.Drawing.Point(159, 124);
            this.closeRadButton.Name = "closeRadButton";
            this.closeRadButton.Size = new System.Drawing.Size(82, 25);
            this.closeRadButton.TabIndex = 18;
            this.closeRadButton.Text = "Close";
            this.closeRadButton.ThemeName = "Vista";
            this.closeRadButton.Click += new System.EventHandler(this.CloseRadButton_Click);
            // 
            // changeColorRadButton
            // 
            this.changeColorRadButton.Location = new System.Drawing.Point(12, 93);
            this.changeColorRadButton.Name = "changeColorRadButton";
            this.changeColorRadButton.Size = new System.Drawing.Size(82, 25);
            this.changeColorRadButton.TabIndex = 18;
            this.changeColorRadButton.Text = "Change Color";
            this.changeColorRadButton.ThemeName = "Vista";
            this.changeColorRadButton.Click += new System.EventHandler(this.changeColorRadButton_Click);
            // 
            // trackingRadColorDialog
            // 
            this.trackingRadColorDialog.SelectedColor = System.Drawing.Color.Red;
            this.trackingRadColorDialog.SelectedHslColor = Telerik.WinControls.HslColor.FromAhsl(0, 1, 1);
            // 
            // outliningColorRadLabel
            // 
            this.outliningColorRadLabel.Location = new System.Drawing.Point(171, 36);
            this.outliningColorRadLabel.Name = "outliningColorRadLabel";
            this.outliningColorRadLabel.Size = new System.Drawing.Size(79, 16);
            this.outliningColorRadLabel.TabIndex = 19;
            this.outliningColorRadLabel.Text = "Outlining color";
            // 
            // outliningColorPictureBox
            // 
            this.outliningColorPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outliningColorPictureBox.Location = new System.Drawing.Point(171, 60);
            this.outliningColorPictureBox.Name = "outliningColorPictureBox";
            this.outliningColorPictureBox.Size = new System.Drawing.Size(77, 20);
            this.outliningColorPictureBox.TabIndex = 20;
            this.outliningColorPictureBox.TabStop = false;
            this.outliningColorPictureBox.Click += new System.EventHandler(this.outliningColorPictureBox_Click);
            // 
            // EditMarkerForm
            // 
            this.AcceptButton = this.saveRadButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeRadButton;
            this.ClientSize = new System.Drawing.Size(253, 154);
            this.Controls.Add(this.outliningColorPictureBox);
            this.Controls.Add(this.outliningColorRadLabel);
            this.Controls.Add(this.changeColorRadButton);
            this.Controls.Add(this.closeRadButton);
            this.Controls.Add(this.saveRadButton);
            this.Controls.Add(this.workingFrameRadCheckBox);
            this.Controls.Add(this.markerPriorityRadTextBox);
            this.Controls.Add(this.priorityRadLabel);
            this.Controls.Add(this.markerRangeRadTextBox);
            this.Controls.Add(this.rangeRadLabel);
            this.Controls.Add(this.nameRadLabel);
            this.Controls.Add(this.markerNameRadTextBox);
            this.Controls.Add(this.samplePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EditMarkerForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Marker";
            this.ThemeName = "Vista";
            ((System.ComponentModel.ISupportInitialize)(this.samplePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerNameRadTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerRangeRadTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rangeRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerPriorityRadTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priorityRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workingFrameRadCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveRadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeRadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.changeColorRadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outliningColorRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outliningColorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox samplePictureBox;
        private Telerik.WinControls.UI.RadTextBox markerNameRadTextBox;
        private Telerik.WinControls.UI.RadLabel nameRadLabel;
        private Telerik.WinControls.UI.RadTextBox markerRangeRadTextBox;
        private Telerik.WinControls.UI.RadLabel rangeRadLabel;
        private Telerik.WinControls.UI.RadLabel priorityRadLabel;
        private Telerik.WinControls.UI.RadTextBox markerPriorityRadTextBox;
        private Telerik.WinControls.UI.RadCheckBox workingFrameRadCheckBox;
        private Telerik.WinControls.UI.RadButton saveRadButton;
        private Telerik.WinControls.Themes.VistaTheme vistaTheme;
        private Telerik.WinControls.UI.RadButton closeRadButton;
        private Telerik.WinControls.UI.RadButton changeColorRadButton;
        private Telerik.WinControls.RadColorDialog trackingRadColorDialog;
        private Telerik.WinControls.UI.RadLabel outliningColorRadLabel;
        private System.Windows.Forms.PictureBox outliningColorPictureBox;
    }
}