using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Inspira
{
    public partial class frmCustomersRecord : Form
    {

        DataTable dtable = new DataTable();
        MySqlConnection con = null;
        DataSet ds = new DataSet();
        MySqlCommand cmd = null;
        DataTable dt = new DataTable();
        String cs = Inspira.Properties.Settings.Default.connectionString;
        public frmCustomersRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                con = new MySqlConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT CustomerID, Customername,address,city,zipcode,Phone,email,mobileno,notes from customer", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                dataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomersRecord_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logindataDataSet2.customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter1.Fill(this.logindataDataSet2.customer);
            // TODO: This line of code loads data into the 'logindataDataSet1.customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.logindataDataSet1.customer);
            // TODO: This line of code loads data into the 'loginDataDataSet2.customer' table. You can move, or remove it, as needed.
            //       this.customerTableAdapter.Fill(this.loginDataDataSet2.customer);
            GetData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                frmCustomers frm = new frmCustomers();
                frm.Show();
                frm.txtCustomerID.Text = dr.Cells[0].Value.ToString();
                frm.txtCustomerName.Text = dr.Cells[1].Value.ToString();
                frm.txtAddress.Text = dr.Cells[2].Value.ToString();
                frm.txtCity.Text = dr.Cells[3].Value.ToString();
                frm.txtZipCode.Text = dr.Cells[4].Value.ToString();
                frm.txtPhone.Text = dr.Cells[5].Value.ToString();
                frm.txtEmail.Text = dr.Cells[6].Value.ToString();
                frm.txtMobileNo.Text = dr.Cells[7].Value.ToString();
                frm.txtNotes.Text = dr.Cells[8].Value.ToString();
                frm.btnUpdate.Enabled = true;
                frm.btnDelete.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.txtCustomerName.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }



        private void txtCustomers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(cs);
                //con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT CustomerID, Customername,address,city,zipcode,Phone,email,mobileno,notes from customer where Customername like '" + txtCustomers.Text + "%'", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                //OleDbDataAdapter myDA = new OleDbDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "customer");
                dataGridView1.DataSource = myDataSet.Tables["customer"].DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         private void Button3_Click(object sender, EventArgs e)
         {
             if (dataGridView1.DataSource == null)
             {
                 MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             int rowsTotal = 0;
             int colsTotal = 0;
             int I = 0;
             int j = 0;
             int iC = 0;
             System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
             Excel.Application xlApp = new Excel.Application();

             try
             {
                 Excel.Workbook excelBook = xlApp.Workbooks.Add();
                 Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                 xlApp.Visible = true;

                 rowsTotal = dataGridView1.RowCount - 1;
                 colsTotal = dataGridView1.Columns.Count - 1;
                 var _with1 = excelWorksheet;
                 _with1.Cells.Select();
                 _with1.Cells.Delete();
                 for (iC = 0; iC <= colsTotal; iC++)
                 {
                     _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                 }
                 for (I = 0; I <= rowsTotal - 1; I++)
                 {
                     for (j = 0; j <= colsTotal; j++)
                     {
                         _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                     }
                 }
                 _with1.Rows["1:1"].Font.FontStyle = "Bold";
                 _with1.Rows["1:1"].Font.Size = 12;

                 _with1.Cells.Columns.AutoFit();
                 _with1.Cells.Select();
                 _with1.Cells.EntireColumn.AutoFit();
                 _with1.Cells[1, 1].Select();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 //RELEASE ALLOACTED RESOURCES
                 System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                 xlApp = null;
             }
         }
         
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptCustomers rpt = new rptCustomers();
                //The report you created.
                cmd = new MySqlCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
    //            LoginDataDataSet2 myDS = new LoginDataDataSet2();
                //The DataSet you created.
                con = new MySqlConnection(cs);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from Customer order by Customername";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
         //       myDA.Fill(myDS, "customer");
        //        rpt.SetDataSource(myDS);
                frmCustomersReport frm = new frmCustomersReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
    }
}
