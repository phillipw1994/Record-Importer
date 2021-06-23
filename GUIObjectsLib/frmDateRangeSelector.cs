using System;
using System.Windows.Forms;

namespace GUIObjectsLib
{
    public partial class frmDateRangeSelector : Form
    {
        public frmDateRangeSelector()
        {
            InitializeComponent();
        }

        public DateTime from
        {
            get
            {
                return dtpFrom.Value;
            }
        }
        public DateTime to
        {
            get
            {
                return dtpTo.Value;
            }
        }
        private bool _ok = false;
        /// <summary>
        /// Return whether user clicked OK
        /// </summary>
        public bool ok
        {
            get
            {
                return _ok;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ok = true;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDateRangeSelector_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }
    }
}
