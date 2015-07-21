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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblQuickSelects = new System.Windows.Forms.Label();
            this.lblLogSet = new System.Windows.Forms.Label();
            this.lvLogSet = new System.Windows.Forms.ListView();
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(93, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
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
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 218);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            // lvLogSet
            // 
            this.lvLogSet.LabelEdit = true;
            this.lvLogSet.Location = new System.Drawing.Point(135, 26);
            this.lvLogSet.Name = "lvLogSet";
            this.lvLogSet.Size = new System.Drawing.Size(114, 186);
            this.lvLogSet.TabIndex = 8;
            this.lvLogSet.UseCompatibleStateImageBehavior = false;
            // 
            // QuickSelectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 253);
            this.Controls.Add(this.lvLogSet);
            this.Controls.Add(this.lblLogSet);
            this.Controls.Add(this.lblQuickSelects);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblQuickSelects;
        private System.Windows.Forms.Label lblLogSet;
        private System.Windows.Forms.ListView lvLogSet;
    }
}