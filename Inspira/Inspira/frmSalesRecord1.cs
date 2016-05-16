using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace Inspira
{
    public partial class frmSalesRecord1 : Form
    {

        DataTable dTable;
        MySqlConnection con = null;
        //OleDbConnection con = null;
        MySqlDataAdapter adp;
        //OleDbDataAdapter adp;
        DataSet ds;
        MySqlCommand cmd = null;
        //OleDbCommand cmd = null;
        DataTable dt = new DataTable();
        MySqlDataReader rdr;
      //  OleDbDataReader rdr;
        String cs = Inspira.Properties.Settings.Default.connectionString;
        public frmSalesRecord1()
        {
            InitializeComponent();
        }



        public void GetData()
        {
            try
            {
                con = new MySqlConnection(cs);
                // con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT orderID,orderDate,CustomerID,Customername,GrandTotal,TotalPayment,PaymentDue,orderStatus,orderSendDate from sales order by orderDate", con);
                // cmd = new OleDbCommand("SELECT (CustomerID)as [Customer ID],(Customername) as [Customer Name],(address) as [Address],(landmark) as [Landmark],(city) as [City],(state) as [State],(zipcode) as [Zip/Post Code],(Phone) as [Phone],(email) as [Email],(mobileno) as [Mobile No],(faxno) as [Fax No],(notes) as [Notes] from Customer order by CustomerName", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                //OleDbDataAdapter myDA = new OleDbDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "sales");
                DataGridView1.DataSource = myDataSet.Tables["sales"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSalesRecord_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logindataDataSet5.sales' table. You can move, or remove it, as needed.
            this.salesTableAdapter.Fill(this.logindataDataSet5.sales);
            //             // TODO: This line of code loads data into the 'logindataDataSet4.orders' table. You can move, or remove it, as needed.
            //             this.ordersTableAdapter1.Fill(this.logindataDataSet4.orders);
            //             // TODO: This line of code loads data into the 'logindataDataSet3.sales' table. You can move, or remove it, as needed.
            //             this.salesTableAdapter.Fill(this.logindataDataSet3.sales);
            //             // TODO: This line of code loads data into the 'logindataDataSet3.orders' table. You can move, or remove it, as needed.
            //            this.ordersTableAdapter.Fill(this.logindataDataSet3.orders);
            FillCombo();
            GetData();

        }

          private void Button3_Click(object sender, EventArgs e)
          {
              if (DataGridView1.DataSource == null)
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

                  rowsTotal = DataGridView1.RowCount - 1;
                  colsTotal = DataGridView1.Columns.Count - 1;
                  var _with1 = excelWorksheet;
                  _with1.Cells.Select();
                  _with1.Cells.Delete();
                  for (iC = 0; iC <= colsTotal; iC++)
                  {
                      _with1.Cells[1, iC + 1].Value = DataGridView1.Columns[iC].HeaderText;
                  }
                  for (I = 0; I <= rowsTotal - 1; I++)
                  {
                      for (j = 0; j <= colsTotal; j++)
                      {
                          _with1.Cells[I + 2, j + 1].value = DataGridView1.Rows[I].Cells[j].Value;
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
          
         private void Button4_Click(object sender, EventArgs e)
         {
             if (DataGridView2.DataSource == null)
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

                 rowsTotal = DataGridView2.RowCount - 1;
                 colsTotal = DataGridView2.Columns.Count - 1;
                 var _with1 = excelWorksheet;
                 _with1.Cells.Select();
                 _with1.Cells.Delete();
                 for (iC = 0; iC <= colsTotal; iC++)
                 {
                     _with1.Cells[1, iC + 1].Value = DataGridView2.Columns[iC].HeaderText;
                 }
                 for (I = 0; I <= rowsTotal - 1; I++)
                 {
                     for (j = 0; j <= colsTotal; j++)
                     {
                         _with1.Cells[I + 2, j + 1].value = DataGridView2.Rows[I].Cells[j].Value;
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

         private void Button7_Click(object sender, EventArgs e)
         {
             if (DataGridView3.DataSource == null)
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

                 rowsTotal = DataGridView3.RowCount - 1;
                 colsTotal = DataGridView3.Columns.Count - 1;
                 var _with1 = excelWorksheet;
                 _with1.Cells.Select();
                 _with1.Cells.Delete();
                 for (iC = 0; iC <= colsTotal; iC++)
                 {
                     _with1.Cells[1, iC + 1].Value = DataGridView3.Columns[iC].HeaderText;
                 }
                 for (I = 0; I <= rowsTotal - 1; I++)
                 {
                     for (j = 0; j <= colsTotal; j++)
                     {
                         _with1.Cells[I + 2, j + 1].value = DataGridView3.Rows[I].Cells[j].Value;
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
 
        private void Button9_Click(object sender, EventArgs e)
        {
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;
        }
        public void FillCombo()
        {

            try
            {
                con = new MySqlConnection(cs);
               // con = new OleDbConnection(cs);
                con.Open();
                
               // adp = new OleDbDataAdapter();

                adp = new  MySqlDataAdapter();

                adp.SelectCommand = new MySqlCommand("SELECT  sales.Customername FROM sales,customer where sales.CustomerID=customer.CustomerID",con);
               // adp.SelectCommand = new OleDbCommand("SELECT distinct CustomerName FROM Sales,Customer where Sales.CustomerID=Customer.CustomerID", con);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dTable = ds.Tables[0];
                cmbCustomerName.Items.Clear();
                foreach (DataRow drow in dTable.Rows)
                {
                    cmbCustomerName.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox3.Visible = true;
                con = new MySqlConnection(cs);
             //   con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT orderID,orderDate,sales.CustomerID,sales.Customername,GrandTotal,TotalPayment,paymentDue,orderStatus,orderSendDate from sales,customer where sales.CustomerID=customer.CustomerID and orderDate between #" + dtpInvoiceDateFrom.Text+ "# And #" + dtpInvoiceDateTo.Text + "#", con); 
                //    cmd = new OleDbCommand("SELECT (invoicName],SubTotal as [SubTotal],VATPerTotal],(TotalPayment) as [Total Payment],(PaymentDue) as [Payment Due],Remarks from Sales,Customer where Sales.CustomerID=Customer.CustomerID and InvoiceDate between #" + dtpInvoiceDateFrom.Text + "# And #" + dtpInvoiceDateTo.Text + "# order by InvoiceDate desc", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
           //     OleDbDataAdapter myDA = new OleDbDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Sales");
            //    myDA.Fill(myDataSet, "Customer");
              //  DataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView1.DataSource = myDataSet.Tables["Sales"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView1.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[4].Value);
                    Int64 j = Convert.ToInt64(r.Cells[5].Value);
                    Int64 k = Convert.ToInt64(r.Cells[6].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;

                }
                TextBox1.Text = sum.ToString();
                TextBox2.Text = sum1.ToString();
                TextBox3.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ErrorgetData", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GroupBox4.Visible = true;
                con = new MySqlConnection(cs);
               // con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT orderID,orderDate,sales.CustomerID,Customername,grandTotal,TotalPayment,PaymentDue from sales WHERE sales.CustomerID=customer.CustomerID and Customername='" + cmbCustomerName.Text + "' order by Customername,InvoiceDate", con);
                // cmd = new OleDbCommand("SELECT (invoiceNo) as [Invoice No],(s from Sales,Customer where Sales.CustomerID=Customer.CustomerID and Customername='" + cmbCustomerName.Text + "' order by CustomerName,InvoiceDate", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
            //    OleDbDataAdapter myDA = new OleDbDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "sales");
             //   myDA.Fill(myDataSet, "customer");
           //     DataGridView3.DataSource = myDataSet.Tables["customer"].DefaultView;
                DataGridView3.DataSource = myDataSet.Tables["sales"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView3.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[7].Value);
                    Int64 j = Convert.ToInt64(r.Cells[8].Value);
                    Int64 k = Convert.ToInt64(r.Cells[9].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox6.Text = sum.ToString();
                TextBox5.Text = sum1.ToString();
                TextBox4.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox10.Visible = true;
                con = new MySqlConnection(cs);
               // con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT orderID, orderDate, sales.CustomerID, Customername, grandTotal, TotalPayment, PaymentDue from sales,customer WHERE sales.CustomerID = customer.CustomerID and orderDate between #" + DateTimePicker2.Text + "# And #" + DateTimePicker1.Text + "# and PaymentDue > 0 order by orderDate desc", con);
                //  cmd = new OleDbCommand("SELECT (invoibetween #" + DateTimePicker2.Text + "# And #" + DateTimePicker1.Text + "# and PaymentDue > 0 order by InvoiceDate desc", con);
                MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
           //     OleDbDataAdapter myDA = new OleDbDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Sales");
                myDA.Fill(myDataSet, "Customer");
                DataGridView2.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView2.DataSource = myDataSet.Tables["Sales"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView2.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells[7].Value);
                    Int64 j = Convert.ToInt64(r.Cells[8].Value);
                    Int64 k = Convert.ToInt64(r.Cells[9].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox12.Text = sum.ToString();
                TextBox11.Text = sum1.ToString();
                TextBox10.Text = sum2.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSalesRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmOrder frm = new frmOrder();
            frm.label6.Text = label9.Text;
            frm.Show();
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView1.SelectedRows[0];
                this.Hide();
                frmOrder frmSales = new frmOrder();
                // or simply use column name instead of index
                // dr.Cells["id"].Value.ToString();
                frmSales.Show();
                frmSales.txtInvoiceNo.Text = dr.Cells[0].Value.ToString();
                frmSales.dtpInvoiceDate.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerID.Text = dr.Cells[2].Value.ToString();
                frmSales.txtCustomerName.Text = dr.Cells[3].Value.ToString();
              //  frmSales.txtSubTotal.Text = dr.Cells[4].Value.ToString();
             //   frmSales.txtTaxPer.Text = dr.Cells[5].Value.ToString();
             //   frmSales.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                frmSales.txtTotal.Text = dr.Cells[4].Value.ToString();
                frmSales.txtTotalPayment.Text = dr.Cells[5].Value.ToString();
                frmSales.txtPaymentDue.Text = dr.Cells[6].Value.ToString();
          //      frmSales.txtRemarks.Text = dr.Cells[10].Value.ToString();
                frmSales.btnUpdate.Enabled = true;
                frmSales.Delete.Enabled = true;
                frmSales.btnPrint.Enabled = true;
                frmSales.Save.Enabled = false;
                frmSales.label6.Text = label9.Text;
                con = new MySqlConnection(cs);
            //    con = new OleDbConnection(cs);
                con.Open();
                //cmd = new OleDbCommand("SELECT Config.ConfigID,Config.Productname,ProductSold.Price,ProductSold.Quantity,ProductSold.TotalAmount from Sales,ProductSold,Config where Sales.InvoiceNo=ProductSold.InvoiceNo and Config.ConfigID=ProductSold.ConfigID and Sales.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                cmd = new MySqlCommand("SELECT stock.productID,stock.DrugName,orders.price,orders.quantity,orders.totalAmount from sales,orders,stock where sales.orderID=orders.orderID and stock.productID=orders.productID and sales.orderID='" + dr.Cells[0].Value.ToString() + "'", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read() == true)
                {
                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(rdr[0].ToString().Trim());
                    lst.SubItems.Add(rdr[1].ToString().Trim());
                    lst.SubItems.Add(rdr[2].ToString().Trim());
                    lst.SubItems.Add(rdr[3].ToString().Trim());
                    lst.SubItems.Add(rdr[4].ToString().Trim());
                    frmSales.ListView1.Items.Add(lst);
                }
                frmSales.ListView1.Enabled = false;
                frmSales.Button7.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView3.SelectedRows[0];
                this.Hide();
                frmOrder frmSales = new frmOrder();
                // or simply use column name instead of index
                // dr.Cells["id"].Value.ToString();
                frmSales.Show();
                frmSales.txtInvoiceNo.Text = dr.Cells[0].Value.ToString();
                frmSales.dtpInvoiceDate.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerID.Text = dr.Cells[2].Value.ToString();
                frmSales.txtCustomerName.Text = dr.Cells[3].Value.ToString();
                frmSales.txtSubTotal.Text = dr.Cells[4].Value.ToString();
                frmSales.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                frmSales.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                frmSales.txtTotal.Text = dr.Cells[7].Value.ToString();
                frmSales.txtTotalPayment.Text = dr.Cells[8].Value.ToString();
                frmSales.txtPaymentDue.Text = dr.Cells[9].Value.ToString();
                frmSales.txtRemarks.Text = dr.Cells[10].Value.ToString();
                frmSales.btnUpdate.Enabled = true;
                frmSales.Delete.Enabled = true;
                frmSales.btnPrint.Enabled = true;
                frmSales.Save.Enabled = false;
                frmSales.label6.Text = label9.Text;
                con = new MySqlConnection(cs);
              //  con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand("SELECT stock.productID,stock.DrugName,orders.price,orders.quantity,sales.totalAmount from sales,orders,stock where sales.orderID=orders.orderID and stock.productID=orders.productID and sales.orderID='" + dr.Cells[0].Value.ToString() + "'", con);
          //      cmd = new OleDbCommand("SELECT Config.ConfigID,Config.Productname,ProductSold.Price,ProductSold.Quantity,ProductSold.TotalAmount from Sales,ProductSold,Config where Sales.InvoiceNo=ProductSold.InvoiceNo and Config.ConfigID=ProductSold.ConfigID and Sales.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(rdr[0].ToString().Trim());
                    lst.SubItems.Add(rdr[1].ToString().Trim());
                    lst.SubItems.Add(rdr[2].ToString().Trim());
                    lst.SubItems.Add(rdr[3].ToString().Trim());
                    lst.SubItems.Add(rdr[4].ToString().Trim());
                    frmSales.ListView1.Items.Add(lst);

                }
                frmSales.ListView1.Enabled = false;
                frmSales.Button7.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = DataGridView2.SelectedRows[0];
                this.Hide();
                frmOrder frmSales = new frmOrder();
                // or simply use column name instead of index
                // dr.Cells["id"].Value.ToString();
                frmSales.Show();
                frmSales.txtInvoiceNo.Text = dr.Cells[0].Value.ToString();
                frmSales.dtpInvoiceDate.Text = dr.Cells[1].Value.ToString();
                frmSales.txtCustomerID.Text = dr.Cells[2].Value.ToString();
                frmSales.txtCustomerName.Text = dr.Cells[3].Value.ToString();
                frmSales.txtSubTotal.Text = dr.Cells[4].Value.ToString();
                frmSales.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                frmSales.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                frmSales.txtTotal.Text = dr.Cells[7].Value.ToString();
                frmSales.txtTotalPayment.Text = dr.Cells[8].Value.ToString();
                frmSales.txtPaymentDue.Text = dr.Cells[9].Value.ToString();
                frmSales.txtRemarks.Text = dr.Cells[10].Value.ToString();
                frmSales.btnUpdate.Enabled = true;
                frmSales.Delete.Enabled = true;
                frmSales.btnPrint.Enabled = true;
                frmSales.Save.Enabled = false;
                frmSales.label6.Text = label9.Text;
                con = new MySqlConnection(cs);
               // con = new OleDbConnection(cs);
                con.Open();
                cmd = new MySqlCommand();
                cmd = new MySqlCommand("SELECT stock.productID,stock.DrugName,orders.price,orders.quantity,sales.totalAmount from sales,orders,stock where sales.orderID=orders.orderID and stock.productID=orders.productID and sales.orderID='" + dr.Cells[0].Value.ToString() + "'", con);
                //cmd = new OleDbCommand("SELECT Config.ConfigID,Config.Productname,ProductSold.Price,ProductSold.Quantity,ProductSold.TotalAmount from Sales,ProductSold,Config where Sales.InvoiceNo=ProductSold.InvoiceNo and Config.ConfigID=ProductSold.ConfigID and Sales.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(rdr[0].ToString().Trim());
                    lst.SubItems.Add(rdr[1].ToString().Trim());
                    lst.SubItems.Add(rdr[2].ToString().Trim());
                    lst.SubItems.Add(rdr[3].ToString().Trim());
                    lst.SubItems.Add(rdr[4].ToString().Trim());
                    frmSales.ListView1.Items.Add(lst);

                }
                frmSales.ListView1.Enabled = false;
                frmSales.Button7.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;
            

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
