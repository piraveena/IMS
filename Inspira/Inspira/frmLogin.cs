using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
namespace Inspira
{
    public partial class frmLogin : Form
    {



        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {
                MySqlConnection myConnection = new MySqlConnection();

                myConnection.ConnectionString = Inspira.Properties.Settings.Default.connectionString;
                MySqlDataAdapter adapter;
                myConnection.Open();
                MessageBox.Show("COnnected");
                DataTable table = new DataTable();
                adapter = new MySqlDataAdapter("select username,password from userdata where username = '" + txtUserName.Text + "' and password = '" + txtPassword.Text + "'", myConnection);
                adapter.Fill(table);
                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show("Login is Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
                else
                {

                    myConnection.Close();
                    /* int i;
                     ProgressBar1.Visible = true;
                     ProgressBar1.Maximum = 5000;
                     ProgressBar1.Minimum = 0;
                     ProgressBar1.Value = 4;
                     ProgressBar1.Step = 1;

                     for (i = 0; i <= 5000; i++)
                     {
                         ProgressBar1.PerformStep();
                     }
                     */
                    this.Hide();
                    frmMainMenu frm = new frmMainMenu();
                    frm.Show();
                    frm.lblUser.Text = txtUserName.Text;
                }

                
                  
                  if (myConnection.State == ConnectionState.Open)
                  {
                      myConnection.Dispose();
                  }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // ProgressBar1.Visible = false;
            txtUserName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmChangePassword frm = new frmChangePassword();
            frm.Show();
            frm.txtUserName.Text = "";
            frm.txtNewPassword.Text = "";
            frm.txtOldPassword.Text = "";
            frm.txtConfirmPassword.Text = "";
        }


         /*private void checkBox1_CheckedChanged(object sender, EventArgs e)
         {
             if (checkBox1.Checked)
             {
                 txtPassword.UseSystemPasswordChar = false;
             }
             else
             {
                 txtPassword.UseSystemPasswordChar = true;
             }
         }*/


        /* private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             this.Hide();
             frmRecoveryPassword frm = new frmRecoveryPassword();
             frm.txtEmail.Focus();
             frm.Show();
         }*/
    }
}
