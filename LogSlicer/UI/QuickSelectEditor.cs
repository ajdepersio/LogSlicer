using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogSlicer.UI
{
    public partial class QuickSelectEditor : Form
    {
        private Main _mainForm;

        public QuickSelectEditor(Main mainForm)
        {
            this._mainForm = mainForm;
            InitializeComponent();

            this.initQuickSelectListBox(QuickSelect.QuickSelects);
        }

        private void initQuickSelectListBox(List<QuickSelect> quickSelects)
        {
            foreach(QuickSelect quickSelect in quickSelects)
            {
                this.lbQuickSelects.Items.Add(quickSelect);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //TODO  Make it so that ListBox updates itself with the new logs
            if (txtLogSets.Text.Length > 0)
            {
                List<String> logTypes = txtLogSets.Text.Split(',').ToList<String>();

                LogSetTextBox popup = new LogSetTextBox(_mainForm, logTypes);
                popup.Text = "Enter Name For Log Set";
                popup.inputLabel.Text = "Name";
                popup.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please enter some log types for the set separated by commas.\r\nFor example: ip,notifier,SIPEngine");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbQuickSelects.SelectedItem == null)
            {
                MessageBox.Show("Please select a Quick Select item to delete.");
            }
            else
            {
                DialogResult result = MessageBox.Show(String.Format("Are you sure you want to delete the Quick Select item {0}?", lbQuickSelects.SelectedItem), 
                    "Confirm Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string qsToRemove = lbQuickSelects.SelectedItem.ToString();

                    QuickSelect qs = QuickSelect.QuickSelects.Find(x => x.Name == qsToRemove);
                    qs.Delete();
                    //Update QuickSelectEditor and Main UI
                    lbQuickSelects.ClearSelected();
                    lbQuickSelects.Items.Remove(lbQuickSelects.SelectedItem);

                    _mainForm.RemoteQuickSelect(qs);
                }
            }
        }

        private void lbQuickSelects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                QuickSelect selectedQuickSelect = (QuickSelect)lbQuickSelects.SelectedItem;
                txtLogSets.Text = string.Join(",", selectedQuickSelect.Types.ToArray());
            }
            catch(NullReferenceException)
            {
                txtLogSets.Text = "";
            }
            
        }
    }
}
