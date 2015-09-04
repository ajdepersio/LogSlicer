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
            if (lbLogs.Items.Count > 0)
            {

                List<String> logTypes = lbLogs.Items.Cast<String>().ToList();

                string quickSelectName = "";

                MessageTextBox popup = new MessageTextBox();
                popup.Text = "Enter Name For Log Set";
                popup.inputLabel.Text = "Name";
                popup.ShowDialog();

                quickSelectName = popup.Results;

                if (quickSelectName != "")
                {
                    QuickSelect qs = new QuickSelect(quickSelectName, logTypes);
                    qs.WriteToConfig();
                    _mainForm.AddQuickSelect(qs);
                }
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

                    _mainForm.RemoveQuickSelect(qs);
                    this.Close();
                }
            }
        }

        private void lbQuickSelects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbLogs.Items.Clear();
                QuickSelect selectedQuickSelect = (QuickSelect)lbQuickSelects.SelectedItem;

                ListViewItem lvi;
                foreach(string logName in selectedQuickSelect.Types)
                {
                    lbLogs.Items.Add(logName);
                }

                //ListViewItem[] lvItems = string.Join(",", selectedQuickSelect.Types.ToArray());

                //lvLogSet.Items.AddRange
                //lvLogSet.Items = string.Join(",", selectedQuickSelect.Types.ToArray());
                //txtLogSets.Text = string.Join(",", selectedQuickSelect.Types.ToArray());
            }
            catch(NullReferenceException)
            {
                //txtLogSets.Text = "";
            }
            
        }
    }
}
