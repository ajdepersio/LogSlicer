namespace LogSlicer
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.logListBox = new System.Windows.Forms.CheckedListBox();
            this.logsLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.logsTextBox = new System.Windows.Forms.TextBox();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.logsFolderBrowseButton = new System.Windows.Forms.Button();
            this.outputFolderBrowseButton = new System.Windows.Forms.Button();
            this.sliceButton = new System.Windows.Forms.Button();
            this.logsBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.outputBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.logSnipOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zipCheckBox = new System.Windows.Forms.CheckBox();
            this.registryCheckBox = new System.Windows.Forms.CheckBox();
            this.ticketLabel = new System.Windows.Forms.Label();
            this.ticketTextBox = new System.Windows.Forms.TextBox();
            this.pbSnipping = new System.Windows.Forms.ProgressBar();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // logListBox
            // 
            this.logListBox.FormattingEnabled = true;
            this.logListBox.Location = new System.Drawing.Point(13, 27);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(278, 349);
            this.logListBox.TabIndex = 0;
            this.logListBox.TabStop = false;
            // 
            // logsLabel
            // 
            this.logsLabel.AutoSize = true;
            this.logsLabel.Location = new System.Drawing.Point(10, 386);
            this.logsLabel.Name = "logsLabel";
            this.logsLabel.Size = new System.Drawing.Size(30, 13);
            this.logsLabel.TabIndex = 1;
            this.logsLabel.Text = "Logs";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(10, 465);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(29, 13);
            this.startLabel.TabIndex = 2;
            this.startLabel.Text = "Start";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(10, 492);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(26, 13);
            this.endLabel.TabIndex = 3;
            this.endLabel.Text = "End";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(10, 415);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(39, 13);
            this.outputLabel.TabIndex = 4;
            this.outputLabel.Text = "Output";
            // 
            // logsTextBox
            // 
            this.logsTextBox.Location = new System.Drawing.Point(55, 383);
            this.logsTextBox.Name = "logsTextBox";
            this.logsTextBox.ReadOnly = true;
            this.logsTextBox.Size = new System.Drawing.Size(148, 20);
            this.logsTextBox.TabIndex = 5;
            this.logsTextBox.TabStop = false;
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.CustomFormat = "  yyyy/MM/dd HH:mm:ss";
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimePicker.Location = new System.Drawing.Point(55, 464);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.ShowUpDown = true;
            this.startDateTimePicker.Size = new System.Drawing.Size(148, 20);
            this.startDateTimePicker.TabIndex = 5;
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "  yyyy/MM/dd HH:mm:ss";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimePicker.Location = new System.Drawing.Point(55, 490);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.ShowUpDown = true;
            this.endDateTimePicker.Size = new System.Drawing.Size(148, 20);
            this.endDateTimePicker.TabIndex = 7;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(55, 411);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(148, 20);
            this.outputTextBox.TabIndex = 8;
            this.outputTextBox.TabStop = false;
            // 
            // logsFolderBrowseButton
            // 
            this.logsFolderBrowseButton.Location = new System.Drawing.Point(216, 383);
            this.logsFolderBrowseButton.Name = "logsFolderBrowseButton";
            this.logsFolderBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.logsFolderBrowseButton.TabIndex = 1;
            this.logsFolderBrowseButton.Text = "Browse";
            this.logsFolderBrowseButton.UseVisualStyleBackColor = true;
            this.logsFolderBrowseButton.Click += new System.EventHandler(this.LogsFolderBrowseButton_Click);
            // 
            // outputFolderBrowseButton
            // 
            this.outputFolderBrowseButton.Location = new System.Drawing.Point(216, 410);
            this.outputFolderBrowseButton.Name = "outputFolderBrowseButton";
            this.outputFolderBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.outputFolderBrowseButton.TabIndex = 2;
            this.outputFolderBrowseButton.Text = "Browse";
            this.outputFolderBrowseButton.UseVisualStyleBackColor = true;
            this.outputFolderBrowseButton.Click += new System.EventHandler(this.OutputFolderBrowseButton_Click);
            // 
            // sliceButton
            // 
            this.sliceButton.Location = new System.Drawing.Point(217, 487);
            this.sliceButton.Name = "sliceButton";
            this.sliceButton.Size = new System.Drawing.Size(75, 23);
            this.sliceButton.TabIndex = 8;
            this.sliceButton.Text = "Slice";
            this.sliceButton.UseVisualStyleBackColor = true;
            this.sliceButton.Click += new System.EventHandler(this.sliceButton_Click);
            // 
            // logSnipOpenFileDialog
            // 
            this.logSnipOpenFileDialog.FileName = "logSnipOpenFileDialog";
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.quickSelectToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(304, 24);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // quickSelectToolStripMenuItem
            // 
            this.quickSelectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCurrentToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripSeparator1});
            this.quickSelectToolStripMenuItem.Name = "quickSelectToolStripMenuItem";
            this.quickSelectToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.quickSelectToolStripMenuItem.Text = "Quick Select";
            // 
            // addCurrentToolStripMenuItem
            // 
            this.addCurrentToolStripMenuItem.Name = "addCurrentToolStripMenuItem";
            this.addCurrentToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addCurrentToolStripMenuItem.Text = "Add Current";
            this.addCurrentToolStripMenuItem.Click += new System.EventHandler(this.addCurrentToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // zipCheckBox
            // 
            this.zipCheckBox.AutoSize = true;
            this.zipCheckBox.Location = new System.Drawing.Point(216, 464);
            this.zipCheckBox.Name = "zipCheckBox";
            this.zipCheckBox.Size = new System.Drawing.Size(41, 17);
            this.zipCheckBox.TabIndex = 6;
            this.zipCheckBox.Text = "Zip";
            this.zipCheckBox.UseVisualStyleBackColor = true;
            // 
            // registryCheckBox
            // 
            this.registryCheckBox.AutoSize = true;
            this.registryCheckBox.Location = new System.Drawing.Point(216, 439);
            this.registryCheckBox.Name = "registryCheckBox";
            this.registryCheckBox.Size = new System.Drawing.Size(64, 17);
            this.registryCheckBox.TabIndex = 4;
            this.registryCheckBox.Text = "Registry";
            this.registryCheckBox.UseVisualStyleBackColor = true;
            // 
            // ticketLabel
            // 
            this.ticketLabel.AutoSize = true;
            this.ticketLabel.Location = new System.Drawing.Point(10, 440);
            this.ticketLabel.Name = "ticketLabel";
            this.ticketLabel.Size = new System.Drawing.Size(37, 13);
            this.ticketLabel.TabIndex = 11;
            this.ticketLabel.Text = "Ticket";
            // 
            // ticketTextBox
            // 
            this.ticketTextBox.Location = new System.Drawing.Point(55, 437);
            this.ticketTextBox.Name = "ticketTextBox";
            this.ticketTextBox.Size = new System.Drawing.Size(148, 20);
            this.ticketTextBox.TabIndex = 3;
            // 
            // pbSnipping
            // 
            this.pbSnipping.Location = new System.Drawing.Point(13, 353);
            this.pbSnipping.Name = "pbSnipping";
            this.pbSnipping.Size = new System.Drawing.Size(278, 23);
            this.pbSnipping.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbSnipping.TabIndex = 13;
            this.pbSnipping.Visible = false;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AcceptButton = this.sliceButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(304, 522);
            this.Controls.Add(this.pbSnipping);
            this.Controls.Add(this.ticketTextBox);
            this.Controls.Add(this.ticketLabel);
            this.Controls.Add(this.registryCheckBox);
            this.Controls.Add(this.zipCheckBox);
            this.Controls.Add(this.sliceButton);
            this.Controls.Add(this.outputFolderBrowseButton);
            this.Controls.Add(this.logsFolderBrowseButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.startDateTimePicker);
            this.Controls.Add(this.logsTextBox);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.logsLabel);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Log Slicer";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckedListBox logListBox;
        private System.Windows.Forms.Label logsLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.TextBox logsTextBox;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button logsFolderBrowseButton;
        private System.Windows.Forms.Button outputFolderBrowseButton;
        private System.Windows.Forms.Button sliceButton;
        private System.Windows.Forms.FolderBrowserDialog logsBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog outputBrowserDialog;
        private System.Windows.Forms.OpenFileDialog logSnipOpenFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox zipCheckBox;
        private System.Windows.Forms.CheckBox registryCheckBox;
        private System.Windows.Forms.Label ticketLabel;
        private System.Windows.Forms.TextBox ticketTextBox;
        private System.Windows.Forms.ProgressBar pbSnipping;
        private System.Windows.Forms.ToolStripMenuItem addCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;


    }
}

