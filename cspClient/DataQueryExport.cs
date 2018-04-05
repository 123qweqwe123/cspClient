using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cspClient
{
    public partial class DataQueryExport : Form
    {
        public bool retVal = false;
        public DataQueryExport()
        {
            InitializeComponent();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if ((!this.cb_l.Checked) && (!this.cb_v.Checked) && (!this.cb_w.Checked))
            {
                MessageBox.Show("至少勾选一个导出类型");
                return;
            }
            this.retVal = true;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.retVal = false;
            this.Hide();
        }
    }
}
