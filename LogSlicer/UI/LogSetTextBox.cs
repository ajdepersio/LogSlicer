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
    public partial class LogSetTextBox : Form
    {
        private Main _mainForm;

        public LogSetTextBox(Main mainForm)
        {
            InitializeComponent();
            this._mainForm = mainForm;
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Create new QuickSelect item and add it to the menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            //TODO Figure out a better way to do this so we don't have to pass _mainForm to this class
            QuickSelect qs = new QuickSelect(txtName.Text, _mainForm.logListBox.CheckedItems.Cast<string>().ToList());
            qs.WriteToConfig();
            _mainForm.AddQuickSelect(qs);
            this.Close();
        }
    }
}
