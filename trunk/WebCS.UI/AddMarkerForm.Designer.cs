namespace WebCS
{
    partial class AddMarkerForm
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
            this.components = new System.ComponentModel.Container();
            this.vistaTheme = new Telerik.WinControls.Themes.VistaTheme();
            this.frameImageContainer = new System.Windows.Forms.PictureBox();
            this.closeRadButton = new Telerik.WinControls.UI.RadButton();
            this.markerTypeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorMarkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadDropDownMenu();
            this.radMenuButtonItem1 = new Telerik.WinControls.UI.RadDropDownMenu();
            this.extractRadButton = new Telerik.WinControls.UI.RadButton();
            this.userHelpRadLabel = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.frameImageContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeRadButton)).BeginInit();
            this.markerTypeContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuButtonItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.extractRadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userHelpRadLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // frameImageContainer
            // 
            this.frameImageContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frameImageContainer.Location = new System.Drawing.Point(12, 12);
            this.frameImageContainer.Name = "frameImageContainer";
            this.frameImageContainer.Size = new System.Drawing.Size(352, 288);
            this.frameImageContainer.TabIndex = 5;
            this.frameImageContainer.TabStop = false;
            this.frameImageContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frameImageContainer_MouseMove);
            this.frameImageContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frameImageContainer_MouseDown);
            this.frameImageContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frameImageContainer_MouseUp);
            // 
            // closeRadButton
            // 
            this.closeRadButton.Location = new System.Drawing.Point(98, 306);
            this.closeRadButton.Name = "closeRadButton";
            this.closeRadButton.Size = new System.Drawing.Size(80, 25);
            this.closeRadButton.TabIndex = 8;
            this.closeRadButton.Text = "Close";
            this.closeRadButton.ThemeName = "Vista";
            this.closeRadButton.Click += new System.EventHandler(this.closeRadButton_Click);
            // 
            // markerTypeContextMenuStrip
            // 
            this.markerTypeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorMarkerToolStripMenuItem});
            this.markerTypeContextMenuStrip.Name = "iconContextMenuStrip";
            this.markerTypeContextMenuStrip.ShowImageMargin = false;
            this.markerTypeContextMenuStrip.Size = new System.Drawing.Size(122, 26);
            // 
            // colorMarkerToolStripMenuItem
            // 
            this.colorMarkerToolStripMenuItem.Name = "colorMarkerToolStripMenuItem";
            this.colorMarkerToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.colorMarkerToolStripMenuItem.Text = "Color Marker";
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.AnimationEnabled = false;
            this.radMenuItem1.AnimationFrames = 1;
            this.radMenuItem1.AutoSize = true;
            this.radMenuItem1.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.radMenuItem1.DropShadow = true;
            this.radMenuItem1.EasingType = Telerik.WinControls.RadEasingType.InQuad;
            this.radMenuItem1.EnableAeroEffects = false;
            this.radMenuItem1.FadeAnimationFrames = 10;
            this.radMenuItem1.FadeAnimationSpeed = 10;
            this.radMenuItem1.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn;
            this.radMenuItem1.FitToScreenMode = ((Telerik.WinControls.UI.FitToScreenModes)((Telerik.WinControls.UI.FitToScreenModes.FitWidth | Telerik.WinControls.UI.FitToScreenModes.FitHeight)));
            this.radMenuItem1.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges;
            this.radMenuItem1.Location = new System.Drawing.Point(0, 0);
            this.radMenuItem1.Maximum = new System.Drawing.Size(0, 0);
            this.radMenuItem1.Minimum = new System.Drawing.Size(0, 0);
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Opacity = 1F;
            this.radMenuItem1.ProcessKeyboard = false;
            this.radMenuItem1.RollOverItemSelection = true;
            // 
            // 
            // 
            this.radMenuItem1.RootElement.StretchHorizontally = false;
            this.radMenuItem1.RootElement.StretchVertically = false;
            this.radMenuItem1.Size = new System.Drawing.Size(0, 0);
            this.radMenuItem1.TabIndex = 0;
            this.radMenuItem1.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges;
            this.radMenuItem1.Visible = false;
            // 
            // radMenuButtonItem1
            // 
            this.radMenuButtonItem1.AnimationEnabled = false;
            this.radMenuButtonItem1.AnimationFrames = 1;
            this.radMenuButtonItem1.AutoSize = true;
            this.radMenuButtonItem1.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down;
            this.radMenuButtonItem1.DropShadow = true;
            this.radMenuButtonItem1.EasingType = Telerik.WinControls.RadEasingType.InQuad;
            this.radMenuButtonItem1.EnableAeroEffects = false;
            this.radMenuButtonItem1.FadeAnimationFrames = 10;
            this.radMenuButtonItem1.FadeAnimationSpeed = 10;
            this.radMenuButtonItem1.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn;
            this.radMenuButtonItem1.FitToScreenMode = ((Telerik.WinControls.UI.FitToScreenModes)((Telerik.WinControls.UI.FitToScreenModes.FitWidth | Telerik.WinControls.UI.FitToScreenModes.FitHeight)));
            this.radMenuButtonItem1.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges;
            this.radMenuButtonItem1.Location = new System.Drawing.Point(0, 0);
            this.radMenuButtonItem1.Maximum = new System.Drawing.Size(0, 0);
            this.radMenuButtonItem1.Minimum = new System.Drawing.Size(0, 0);
            this.radMenuButtonItem1.Name = "radMenuButtonItem1";
            this.radMenuButtonItem1.Opacity = 1F;
            this.radMenuButtonItem1.ProcessKeyboard = false;
            this.radMenuButtonItem1.RollOverItemSelection = true;
            // 
            // 
            // 
            this.radMenuButtonItem1.RootElement.StretchHorizontally = false;
            this.radMenuButtonItem1.RootElement.StretchVertically = false;
            this.radMenuButtonItem1.Size = new System.Drawing.Size(0, 0);
            this.radMenuButtonItem1.TabIndex = 0;
            this.radMenuButtonItem1.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges;
            this.radMenuButtonItem1.Visible = false;
            // 
            // extractRadButton
            // 
            this.extractRadButton.Enabled = false;
            this.extractRadButton.Location = new System.Drawing.Point(12, 306);
            this.extractRadButton.Name = "extractRadButton";
            this.extractRadButton.Size = new System.Drawing.Size(80, 25);
            this.extractRadButton.TabIndex = 9;
            this.extractRadButton.Text = "Extract";
            this.extractRadButton.ThemeName = "Vista";
            this.extractRadButton.Click += new System.EventHandler(this.extractRadButton_Click);
            // 
            // userHelpRadLabel
            // 
            this.userHelpRadLabel.Location = new System.Drawing.Point(184, 306);
            this.userHelpRadLabel.Name = "userHelpRadLabel";
            this.userHelpRadLabel.Size = new System.Drawing.Size(197, 16);
            this.userHelpRadLabel.TabIndex = 11;
            this.userHelpRadLabel.Text = "Click upper-left and lower right corner.";
            // 
            // AddMarkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 334);
            this.Controls.Add(this.extractRadButton);
            this.Controls.Add(this.frameImageContainer);
            this.Controls.Add(this.userHelpRadLabel);
            this.Controls.Add(this.closeRadButton);
            this.MaximizeBox = false;
            this.Name = "AddMarkerForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Add Marker";
            this.ThemeName = "Vista";
            ((System.ComponentModel.ISupportInitialize)(this.frameImageContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeRadButton)).EndInit();
            this.markerTypeContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radMenuItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuButtonItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.extractRadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userHelpRadLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.VistaTheme vistaTheme;
        private System.Windows.Forms.PictureBox frameImageContainer;
        private Telerik.WinControls.UI.RadButton closeRadButton;
        private System.Windows.Forms.ContextMenuStrip markerTypeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem colorMarkerToolStripMenuItem;
        private Telerik.WinControls.UI.RadDropDownMenu radMenuItem1;
        private Telerik.WinControls.UI.RadDropDownMenu radMenuButtonItem1;
        private Telerik.WinControls.UI.RadButton extractRadButton;
        private Telerik.WinControls.UI.RadLabel userHelpRadLabel;
    }
}