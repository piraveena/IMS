using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using MySql.Data;
namespace Inspira
{
    public partial class frmCustomers : Form
    {
        // OleDbDataReader rdr = null;
        MySqlDataReader rdr = null;
        MySqlConnection con = null;
        MySqlCommand cmd = null;
       String cs = Inspira.Properties.Settings.Default.connectionString;

        public frmCustomers()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtCustomerName.Text = "";
            txtMobileNo.Text = "";
            txtNotes.Text = "";
            txtPhone.Text = "";
            txtCustomerID.Text = "";
            txtZipCode.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtCustomerName.Focus();

        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {

        }
        private void auto()
        {
            txtCustomerID.Text = "C-" + GetUniqueKey(6);
        }
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        private void txtZipCode_Validating(object sender, CancelEventArgs e)
        {
            if (txtZipCode.TextLength > 6)
            {
                MessageBox.Show("Only 6 digits are allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZipCode.Focus();
            }
        }

        private void txtZipCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (txtMobileNo.TextLength > 10)
            {
                MessageBox.Show("Only 10 digits are allowed", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Focus();
                return;
            }

            if (txtZipCode.Text == "")
            {
                MessageBox.Show("Please enter zip/post code", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZipCode.Focus();
                return;
            }


            if (txtMobileNo.Text == "")
            {
                MessageBox.Show("Please enter mobile no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
                return;
            }

            try {

                auto();
                con = new MySqlConnection(cs);
                con.Open();
                string ct = "select CustomerID from customer where CustomerID=@find";
                cmd = new MySqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new MySqlParameter("@find",MySqlDbType.VarChar, 20, "CustomerID"));
                cmd.Parameters["@find"].Value = txtCustomerID.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("Customer ID Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                }
                else
                {
                    con = new MySqlConnection(cs);
                    con.Open();
                    string cb = "insert into customer(CustomerID,Customername,address,city,zipcode,Phone,email,mobileno,notes) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)";
                    cmd = new MySqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new MySqlParameter("@d1", MySqlDbType.VarChar, 20, "CustomerID"));
                    cmd.Parameters.Add(new MySqlParameter("@d2", MySqlDbType.VarChar, 100, "Customername"));
                    cmd.Parameters.Add(new MySqlParameter("@d3", MySqlDbType.VarChar, 250, "address"));
                    cmd.Parameters.Add(new MySqlParameter("@d4", MySqlDbType.VarChar, 50, "city"));
                    cmd.Parameters.Add(new MySqlParameter("@d5", MySqlDbType.VarChar, 10, "zipcode"));
                    cmd.Parameters.Add(new MySqlParameter("@d6", MySqlDbType.VarChar, 15, "phone"));
                    cmd.Parameters.Add(new MySqlParameter("@d7", MySqlDbType.VarChar, 150, "email"));
                    cmd.Parameters.Add(new MySqlParameter("@d8", MySqlDbType.VarChar, 15, "mobileno"));
                    cmd.Parameters.Add(new MySqlParameter("@d9", MySqlDbType.VarChar, 250, "notes"));

                    cmd.Parameters["@d1"].Value = txtCustomerID.Text;
                    cmd.Parameters["@d2"].Value = txtCustomerName.Text;
                    cmd.Parameters["@d3"].Value = txtAddress.Text;
                    cmd.Parameters["@d4"].Value = txtCity.Text;
                    cmd.Parameters["@d5"].Value = txtZipCode.Text;
                    cmd.Parameters["@d6"].Value = txtPhone.Text;
                    cmd.Parameters["@d7"].Value = txtEmail.Text;
                    cmd.Parameters["@d8"].Value = txtMobileNo.Text;
                    cmd.Parameters["@d9"].Value = txtNotes.Text;

                    cmd.ExecuteReader();
                    MessageBox.Show("Successfully saved", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Close();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   
        
        private void delete_records()
        {
            
            try
            {

                int RowsAffected = 0;
                con = new MySqlConnection(cs);
                con.Open();
                string cq = "delete from customer where CustomerID=@DELETE1;";
                cmd = new MySqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new MySqlParameter("@DELETE1", MySqlDbType.VarChar, 20, "CustomerID"));
                cmd.Parameters["@DELETE1"].Value = txtCustomerID.Text;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you really want to delete the record?", "Customer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(cs);
                //con = new OleDbConnection(cs);
                con.Open();

                string cb = "update customer set Customername = '" + txtCustomerName.Text + "',address= '" + txtAddress.Text + "',city= '" + txtCity.Text + "',zipcode= '" + txtZipCode.Text + "',Phone= '" + txtPhone.Text + "',email= '" + txtEmail.Text + "',mobileno= '" + txtMobileNo.Text + "',notes= '" + txtNotes.Text + "' where CustomerID= '" + txtCustomerID.Text + "'";
                cmd = new MySqlCommand(cb);
               // cmd = new OleDbCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                MessageBox.Show("Successfully updated", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord2 frm = new frmCustomersRecord2();
            frm.Show();
            frm.GetData();
        }
    }
}
