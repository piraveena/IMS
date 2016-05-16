using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inspira
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            progressBar1.Visible = true;

            this.progressBar1.Value = this.progressBar1.Value + 2;
            if (this.progressBar1.Value == 1)
            {
                label3.Text = "Reading modules..";
            }
            else if (this.progressBar1.Value == 2)
            {
                label3.Text = "Turning on modules.";
            }
            else if (this.progressBar1.Value == 4)
            {
                label3.Text = "Starting modules..";
            }
            else if (this.progressBar1.Value == 6)
            {
                label3.Text = "Loading modules..";
            }
            else if (this.progressBar1.Value == 8)
            {
                label3.Text = "Done Loading modules..";
            }
            else if (this.progressBar1.Value == 10)
            {
                frm.Show();
                timer1.Enabled = false;
                this.Hide();
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            progressBar1.Width = this.Width;
        }
    }
}
