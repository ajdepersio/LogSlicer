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
        private List<String> _logTypes;

        public LogSetTextBox(Main mainForm, List<String> logTypes)
        {
            InitializeComponent();
            this._mainForm = mainForm;
            this._logTypes = logTypes;
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
            QuickSelect qs = new QuickSelect(txtName.Text, _logTypes);
            qs.WriteToConfig();
            _mainForm.AddQuickSelect(qs);
            this.Close();
        }
    }
}