namespace WebCS
{
    partial class WebCSForm
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
            this.trackingToggleButton = new Telerik.WinControls.UI.RadToggleButton();
            this.imageContainer = new Telerik.QuickStart.WinControls.ImageContainer();
            this.exitRadButton = new Telerik.WinControls.UI.RadButton();
            this.optionsToggleButton = new Telerik.WinControls.UI.RadToggleButton();
            this.webcamRadPanel = new Telerik.WinControls.UI.RadPanel();
            this.optionsRadPanel = new Telerik.WinControls.UI.RadPanel();
            this.FirstMarkerRadGroupBox = new Telerik.WinControls.UI.RadGroupBox();
            this.firstMarkerRangeRadTextBox = new Telerik.WinControls.UI.RadTextBox();
            this.firstMarkerChangeRadButton = new Telerik.WinControls.UI.RadButton();
            this.firstMarkerRangeRadLabel = new Telerik.WinControls.UI.RadLabel();
            this.firstMarkerSample = new Telerik.QuickStart.WinControls.ImageContainer();
            this.firstMakrerColorLabel = new Telerik.WinControls.UI.RadLabel();
            this.WebcamRadToggleButton = new Telerik.WinControls.UI.RadToggleButton();
            this.applyFilterRadCheckBox = new Telerik.WinControls.UI.RadCheckBox();
            this.userRadLabel = new Telerik.WinControls.UI.RadLabel();
            this.avaliableWebcamsDropDownList = new Telerik.WinControls.UI.RadDropDownList();
            this.startupRadCheckBox = new Telerik.WinControls.UI.RadCheckBox();
            this.SelectDesktopAreaButton = new Telerik.WinControls.UI.RadButton();
            this.SecondMarkerRadGroupBox = new Telerik.WinControls.UI.RadGroupBox();
            this.loadWorkingFrameRadCheckBox = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackingToggleButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitRadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsToggleButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webcamRadPanel)).BeginInit();
            this.webcamRadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsRadPanel)).BeginInit();
            this.optionsRadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstMarkerRadGroupBox)).BeginInit();
            this.FirstMarkerRadGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerRangeRadTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerChangeRadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerRangeRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMakrerColorLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WebcamRadToggleButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applyFilterRadCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avaliableWebcamsDropDownList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startupRadCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectDesktopAreaButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondMarkerRadGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadWorkingFrameRadCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // trackingToggleButton
            // 
            this.trackingToggleButton.Location = new System.Drawing.Point(4, 297);
            this.trackingToggleButton.Name = "trackingToggleButton";
            this.trackingToggleButton.Size = new System.Drawing.Size(100, 25);
            this.trackingToggleButton.TabIndex = 0;
            this.trackingToggleButton.Text = "Activate Tracking";
            this.trackingToggleButton.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.trackingToggleButton_ToggleStateChanged);
            // 
            // imageContainer
            // 
            this.imageContainer.Location = new System.Drawing.Point(4, 3);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(352, 288);
            this.imageContainer.TabIndex = 3;
            this.imageContainer.TabStop = false;
            // 
            // exitRadButton
            // 
            this.exitRadButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitRadButton.Location = new System.Drawing.Point(251, 297);
            this.exitRadButton.Name = "exitRadButton";
            this.exitRadButton.Size = new System.Drawing.Size(105, 25);
            this.exitRadButton.TabIndex = 2;
            this.exitRadButton.Text = "Exit";
            this.exitRadButton.Click += new System.EventHandler(this.exitRadButton_Click);
            // 
            // optionsToggleButton
            // 
            this.optionsToggleButton.Location = new System.Drawing.Point(124, 297);
            this.optionsToggleButton.Name = "optionsToggleButton";
            this.optionsToggleButton.Size = new System.Drawing.Size(105, 25);
            this.optionsToggleButton.TabIndex = 1;
            this.optionsToggleButton.Text = "Options";
            this.optionsToggleButton.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optionsToggleButton_ToggleStateChanged);
            // 
            // webcamRadPanel
            // 
            this.webcamRadPanel.Controls.Add(this.trackingToggleButton);
            this.webcamRadPanel.Controls.Add(this.imageContainer);
            this.webcamRadPanel.Controls.Add(this.optionsToggleButton);
            this.webcamRadPanel.Controls.Add(this.exitRadButton);
            this.webcamRadPanel.Location = new System.Drawing.Point(3, 3);
            this.webcamRadPanel.Name = "webcamRadPanel";
            this.webcamRadPanel.Size = new System.Drawing.Size(360, 325);
            this.webcamRadPanel.TabIndex = 1;
            // 
            // optionsRadPanel
            // 
            this.optionsRadPanel.Controls.Add(this.loadWorkingFrameRadCheckBox);
            this.optionsRadPanel.Controls.Add(this.FirstMarkerRadGroupBox);
            this.optionsRadPanel.Controls.Add(this.WebcamRadToggleButton);
            this.optionsRadPanel.Controls.Add(this.applyFilterRadCheckBox);
            this.optionsRadPanel.Controls.Add(this.userRadLabel);
            this.optionsRadPanel.Controls.Add(this.avaliableWebcamsDropDownList);
            this.optionsRadPanel.Controls.Add(this.startupRadCheckBox);
            this.optionsRadPanel.Controls.Add(this.SelectDesktopAreaButton);
            this.optionsRadPanel.Controls.Add(this.SecondMarkerRadGroupBox);
            this.optionsRadPanel.Location = new System.Drawing.Point(369, 3);
            this.optionsRadPanel.Name = "optionsRadPanel";
            this.optionsRadPanel.Size = new System.Drawing.Size(362, 325);
            this.optionsRadPanel.TabIndex = 2;
            this.optionsRadPanel.Visible = false;
            // 
            // FirstMarkerRadGroupBox
            // 
            this.FirstMarkerRadGroupBox.Controls.Add(this.firstMarkerRangeRadTextBox);
            this.FirstMarkerRadGroupBox.Controls.Add(this.firstMarkerChangeRadButton);
            this.FirstMarkerRadGroupBox.Controls.Add(this.firstMarkerRangeRadLabel);
            this.FirstMarkerRadGroupBox.Controls.Add(this.firstMarkerSample);
            this.FirstMarkerRadGroupBox.Controls.Add(this.firstMakrerColorLabel);
            this.FirstMarkerRadGroupBox.FooterImageIndex = -1;
            this.FirstMarkerRadGroupBox.FooterImageKey = "";
            this.FirstMarkerRadGroupBox.HeaderImageIndex = -1;
            this.FirstMarkerRadGroupBox.HeaderImageKey = "";
            this.FirstMarkerRadGroupBox.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.FirstMarkerRadGroupBox.HeaderText = "First Marker";
            this.FirstMarkerRadGroupBox.Location = new System.Drawing.Point(12, 59);
            this.FirstMarkerRadGroupBox.Name = "FirstMarkerRadGroupBox";
            this.FirstMarkerRadGroupBox.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.FirstMarkerRadGroupBox.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.FirstMarkerRadGroupBox.Size = new System.Drawing.Size(339, 79);
            this.FirstMarkerRadGroupBox.TabIndex = 0;
            this.FirstMarkerRadGroupBox.Text = "First Marker";
            // 
            // firstMarkerRangeRadTextBox
            // 
            this.firstMarkerRangeRadTextBox.Location = new System.Drawing.Point(114, 45);
            this.firstMarkerRangeRadTextBox.Name = "firstMarkerRangeRadTextBox";
            this.firstMarkerRangeRadTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.firstMarkerRangeRadTextBox.Size = new System.Drawing.Size(36, 20);
            this.firstMarkerRangeRadTextBox.TabIndex = 3;
            this.firstMarkerRangeRadTextBox.TabStop = false;
            this.firstMarkerRangeRadTextBox.Text = "100";
            // 
            // firstMarkerChangeRadButton
            // 
            this.firstMarkerChangeRadButton.Location = new System.Drawing.Point(226, 47);
            this.firstMarkerChangeRadButton.Name = "firstMarkerChangeRadButton";
            this.firstMarkerChangeRadButton.Size = new System.Drawing.Size(100, 25);
            this.firstMarkerChangeRadButton.TabIndex = 4;
            this.firstMarkerChangeRadButton.Text = "Change Color";
            // 
            // firstMarkerRangeRadLabel
            // 
            this.firstMarkerRangeRadLabel.Location = new System.Drawing.Point(13, 47);
            this.firstMarkerRangeRadLabel.Name = "firstMarkerRangeRadLabel";
            this.firstMarkerRangeRadLabel.Size = new System.Drawing.Size(76, 18);
            this.firstMarkerRangeRadLabel.TabIndex = 2;
            this.firstMarkerRangeRadLabel.Text = "Marker Range";
            // 
            // firstMarkerSample
            // 
            this.firstMarkerSample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.firstMarkerSample.Location = new System.Drawing.Point(110, 21);
            this.firstMarkerSample.Name = "firstMarkerSample";
            this.firstMarkerSample.Size = new System.Drawing.Size(40, 20);
            this.firstMarkerSample.TabIndex = 1;
            this.firstMarkerSample.TabStop = false;
            // 
            // firstMakrerColorLabel
            // 
            this.firstMakrerColorLabel.Location = new System.Drawing.Point(13, 23);
            this.firstMakrerColorLabel.Name = "firstMakrerColorLabel";
            this.firstMakrerColorLabel.Size = new System.Drawing.Size(72, 18);
            this.firstMakrerColorLabel.TabIndex = 0;
            this.firstMakrerColorLabel.Text = "Marker Color";
            // 
            // WebcamRadToggleButton
            // 
            this.WebcamRadToggleButton.Enabled = false;
            this.WebcamRadToggleButton.Location = new System.Drawing.Point(226, 5);
            this.WebcamRadToggleButton.Name = "WebcamRadToggleButton";
            this.WebcamRadToggleButton.Size = new System.Drawing.Size(125, 25);
            this.WebcamRadToggleButton.TabIndex = 7;
            this.WebcamRadToggleButton.Text = "Start Webcam";
            this.WebcamRadToggleButton.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.WebcamRadToggleButton_ToggleStateChanged);
            // 
            // applyFilterRadCheckBox
            // 
            this.applyFilterRadCheckBox.Location = new System.Drawing.Point(12, 35);
            this.applyFilterRadCheckBox.Name = "applyFilterRadCheckBox";
            this.applyFilterRadCheckBox.Size = new System.Drawing.Size(197, 18);
            this.applyFilterRadCheckBox.TabIndex = 6;
            this.applyFilterRadCheckBox.Text = "Apply mean filter on webcam video";
            this.applyFilterRadCheckBox.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // userRadLabel
            // 
            this.userRadLabel.AllowDrop = true;
            this.userRadLabel.AutoScroll = true;
            this.userRadLabel.Location = new System.Drawing.Point(168, 273);
            this.userRadLabel.Name = "userRadLabel";
            this.userRadLabel.Size = new System.Drawing.Size(78, 18);
            this.userRadLabel.TabIndex = 5;
            this.userRadLabel.Text = "Options Menu";
            // 
            // avaliableWebcamsDropDownList
            // 
            this.avaliableWebcamsDropDownList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.avaliableWebcamsDropDownList.Location = new System.Drawing.Point(12, 9);
            this.avaliableWebcamsDropDownList.Name = "avaliableWebcamsDropDownList";
            this.avaliableWebcamsDropDownList.ShowImageInEditorArea = true;
            this.avaliableWebcamsDropDownList.Size = new System.Drawing.Size(208, 21);
            this.avaliableWebcamsDropDownList.TabIndex = 1;
            this.avaliableWebcamsDropDownList.Text = "Select Webcam";
            this.avaliableWebcamsDropDownList.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.avaliableWebcamsDropDownList_SelectedIndexChanged);
            // 
            // startupRadCheckBox
            // 
            this.startupRadCheckBox.Location = new System.Drawing.Point(12, 304);
            this.startupRadCheckBox.Name = "startupRadCheckBox";
            this.startupRadCheckBox.Size = new System.Drawing.Size(146, 18);
            this.startupRadCheckBox.TabIndex = 3;
            this.startupRadCheckBox.Text = "Start at computer startup";
            // 
            // SelectDesktopAreaButton
            // 
            this.SelectDesktopAreaButton.Location = new System.Drawing.Point(12, 273);
            this.SelectDesktopAreaButton.Name = "SelectDesktopAreaButton";
            this.SelectDesktopAreaButton.Size = new System.Drawing.Size(150, 25);
            this.SelectDesktopAreaButton.TabIndex = 2;
            this.SelectDesktopAreaButton.Text = "Select Desktop Area";
            this.SelectDesktopAreaButton.Click += new System.EventHandler(this.SelectDesktopAreaButton_Click);
            // 
            // SecondMarkerRadGroupBox
            // 
            this.SecondMarkerRadGroupBox.FooterImageIndex = -1;
            this.SecondMarkerRadGroupBox.FooterImageKey = "";
            this.SecondMarkerRadGroupBox.HeaderImageIndex = -1;
            this.SecondMarkerRadGroupBox.HeaderImageKey = "";
            this.SecondMarkerRadGroupBox.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.SecondMarkerRadGroupBox.HeaderText = "Second Marker";
            this.SecondMarkerRadGroupBox.Location = new System.Drawing.Point(12, 144);
            this.SecondMarkerRadGroupBox.Name = "SecondMarkerRadGroupBox";
            this.SecondMarkerRadGroupBox.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.SecondMarkerRadGroupBox.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.SecondMarkerRadGroupBox.Size = new System.Drawing.Size(339, 85);
            this.SecondMarkerRadGroupBox.TabIndex = 4;
            this.SecondMarkerRadGroupBox.Text = "Second Marker";
            // 
            // loadWorkingFrameRadCheckBox
            // 
            this.loadWorkingFrameRadCheckBox.Location = new System.Drawing.Point(231, 35);
            this.loadWorkingFrameRadCheckBox.Name = "loadWorkingFrameRadCheckBox";
            this.loadWorkingFrameRadCheckBox.Size = new System.Drawing.Size(120, 18);
            this.loadWorkingFrameRadCheckBox.TabIndex = 8;
            this.loadWorkingFrameRadCheckBox.Text = "Load working frame";
            // 
            // WebCSForm
            // 
            this.AcceptButton = this.trackingToggleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitRadButton;
            this.ClientSize = new System.Drawing.Size(732, 328);
            this.Controls.Add(this.optionsRadPanel);
            this.Controls.Add(this.webcamRadPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebCSForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "WebCS";
            this.ThemeName = "Vista";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebCSForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackingToggleButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitRadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsToggleButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webcamRadPanel)).EndInit();
            this.webcamRadPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.optionsRadPanel)).EndInit();
            this.optionsRadPanel.ResumeLayout(false);
            this.optionsRadPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstMarkerRadGroupBox)).EndInit();
            this.FirstMarkerRadGroupBox.ResumeLayout(false);
            this.FirstMarkerRadGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerRangeRadTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerChangeRadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerRangeRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMarkerSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstMakrerColorLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WebcamRadToggleButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applyFilterRadCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avaliableWebcamsDropDownList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startupRadCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectDesktopAreaButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondMarkerRadGroupBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadWorkingFrameRadCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadToggleButton trackingToggleButton;
        private Telerik.WinControls.UI.RadToggleButton optionsToggleButton;
        private Telerik.WinControls.UI.RadPanel webcamRadPanel;
        private Telerik.WinControls.UI.RadButton exitRadButton;
        private Telerik.QuickStart.WinControls.ImageContainer imageContainer;
        private Telerik.WinControls.UI.RadPanel optionsRadPanel;
        private Telerik.WinControls.UI.RadGroupBox SecondMarkerRadGroupBox;
        private Telerik.WinControls.UI.RadGroupBox FirstMarkerRadGroupBox;
        private Telerik.WinControls.UI.RadDropDownList avaliableWebcamsDropDownList;
        private Telerik.WinControls.UI.RadCheckBox startupRadCheckBox;
        private Telerik.WinControls.UI.RadButton SelectDesktopAreaButton;
        private Telerik.WinControls.UI.RadLabel userRadLabel;
        private Telerik.WinControls.UI.RadCheckBox applyFilterRadCheckBox;
        private Telerik.WinControls.UI.RadToggleButton WebcamRadToggleButton;
        private Telerik.QuickStart.WinControls.ImageContainer firstMarkerSample;
        private Telerik.WinControls.UI.RadLabel firstMakrerColorLabel;
        private Telerik.WinControls.UI.RadButton firstMarkerChangeRadButton;
        private Telerik.WinControls.UI.RadTextBox firstMarkerRangeRadTextBox;
        private Telerik.WinControls.UI.RadLabel firstMarkerRangeRadLabel;
        private Telerik.WinControls.UI.RadCheckBox loadWorkingFrameRadCheckBox;
    }
}

