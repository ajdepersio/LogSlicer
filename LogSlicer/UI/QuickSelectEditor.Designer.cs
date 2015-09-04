namespace LogSlicer.UI
{
    partial class QuickSelectEditor
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
            this.lbQuickSelects = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblQuickSelects = new System.Windows.Forms.Label();
            this.lblLogSet = new System.Windows.Forms.Label();
            this.lbLogs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbQuickSelects
            // 
            this.lbQuickSelects.DisplayMember = "Name";
            this.lbQuickSelects.FormattingEnabled = true;
            this.lbQuickSelects.Location = new System.Drawing.Point(12, 26);
            this.lbQuickSelects.Name = "lbQuickSelects";
            this.lbQuickSelects.Size = new System.Drawing.Size(117, 186);
            this.lbQuickSelects.TabIndex = 0;
            this.lbQuickSelects.SelectedIndexChanged += new System.EventHandler(this.lbQuickSelects_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(174, 218);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblQuickSelects
            // 
            this.lblQuickSelects.AutoSize = true;
            this.lblQuickSelects.Location = new System.Drawing.Point(12, 6);
            this.lblQuickSelects.Name = "lblQuickSelects";
            this.lblQuickSelects.Size = new System.Drawing.Size(73, 13);
            this.lblQuickSelects.TabIndex = 6;
            this.lblQuickSelects.Text = "Quick Selects";
            // 
            // lblLogSet
            // 
            this.lblLogSet.AutoSize = true;
            this.lblLogSet.Location = new System.Drawing.Point(136, 6);
            this.lblLogSet.Name = "lblLogSet";
            this.lblLogSet.Size = new System.Drawing.Size(44, 13);
            this.lblLogSet.TabIndex = 7;
            this.lblLogSet.Text = "Log Set";
            // 
            // lbLogs
            // 
            this.lbLogs.FormattingEnabled = true;
            this.lbLogs.Location = new System.Drawing.Point(139, 26);
            this.lbLogs.Name = "lbLogs";
            this.lbLogs.Size = new System.Drawing.Size(110, 186);
            this.lbLogs.TabIndex = 8;
            // 
            // QuickSelectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 253);
            this.Controls.Add(this.lbLogs);
            this.Controls.Add(this.lblLogSet);
            this.Controls.Add(this.lblQuickSelects);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lbQuickSelects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickSelectEditor";
            this.ShowIcon = false;
            this.Text = "QuickSelectEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbQuickSelects;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblQuickSelects;
        private System.Windows.Forms.Label lblLogSet;
        private System.Windows.Forms.ListBox lbLogs;
    }
}