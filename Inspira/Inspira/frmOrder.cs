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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Inspira
{
    public partial class frmOrder : Form
    {
        MySqlCommand cmd;
        MySqlConnection con;
        MySqlDataReader rdr;
        String cs = Inspira.Properties.Settings.Default.connectionString;
        //OleDbCommand cmd;
        //         OleDbConnection con;
        //         OleDbDataReader rdr;
        //         String cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SIS_DB.accdb;";

        public frmOrder()
        {
            InitializeComponent();
        }
        private void auto()
        {
            txtInvoiceNo.Text = "INV-" + GetUniqueKey(8);

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

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerID.Text == "")
                {
                    MessageBox.Show("Please retrieve Customer ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerID.Focus();
                    return;
                }

                if (txtTaxPer.Text == "")
                {
                    MessageBox.Show("Please enter tax percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTaxPer.Focus();
                    return;
                }

                if (txtTotalPayment.Text == "")
                {
                    MessageBox.Show("Please enter total payment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalPayment.Focus();
                    return;
                }
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no product added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                auto();


                con = new MySqlConnection(cs);
               // con = new OleDbConnection(cs);
                con.Open();
                string ct = "SELECT orderID from sales where orderID=@find" ;
                cmd = new MySqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new MySqlParameter("@find", MySqlDbType.VarChar, 20, "orderID"));
               // cmd.Parameters.Add(new OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 20, "invoiceno"));
                cmd.Parameters["@find"].Value = txtInvoiceNo.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read() == true)
                {
                    MessageBox.Show("orderID Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }

                con = new MySqlConnection(cs);
                //con = new OleDbConnection(cs);
                con.Open();
               // MessageBox.Show("ffvnjnnv");
                string cib = "insert into sales(orderID,orderDate,CustomerID,Customername,GrandTotal,TotalPayment,PaymentDue) VALUES ('" + txtInvoiceNo.Text + "',#" + dtpInvoiceDate.Text + "#,'" + txtCustomerID.Text + "','" + txtCustomerName.Text + "'," + txtTotal.Text + "," + txtTotalPayment.Text + "," + txtPaymentDue.Text + ")";

                //                 string cb = "insert into Customer(CustomerID,Customername,address,city,zipcode,Phone,email,mobileno,notes) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)";
                //                 cmd = new MySqlCommand(cb);
                //                 cmd.Connection = con;
                //                 cmd.Parameters.Add(new MySqlParameter("@d1", MySqlDbType.VarChar, 20, "CustomerID"));

                // 
                //                 cmd.Parameters["@d1"].Value = txtCustomerID.Text;
                //                 cmd.Parameters["@d2"].Value = txtCustomerName.Text;
                //                 cmd.Parameters["@d3"].Value = txtAddress.Text;
                //                 cmd.Parameters["@d4"].Value = txtCity.Text;
                // txtCustomerID.Text + "'," + txtTotal.Text + "," + txtTotalPayment.Text + "," + txtPaymentDue.Text + ")";

                string cb = "insert into sales(orderID,orderDate,CustomerID,Customername,GrandTotal,TotalPayment,PaymentDue) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7)";

                cmd = new MySqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new MySqlParameter("@d1", MySqlDbType.VarChar, 20, "orderID"));
                cmd.Parameters.Add(new MySqlParameter("@d2", MySqlDbType.VarChar, 100, "orderDate"));
                cmd.Parameters.Add(new MySqlParameter("@d3", MySqlDbType.VarChar, 20, "CustomerID"));
                cmd.Parameters.Add(new MySqlParameter("@d4", MySqlDbType.VarChar, 10, "Customername"));
                cmd.Parameters.Add(new MySqlParameter("@d5", MySqlDbType.VarChar, 10, "GrandTotal"));
                cmd.Parameters.Add(new MySqlParameter("@d6", MySqlDbType.VarChar, 10, "TotalPayment"));
                cmd.Parameters.Add(new MySqlParameter("@d7", MySqlDbType.VarChar, 10, "PaymentDue"));


                cmd.Parameters["@d1"].Value = txtInvoiceNo.Text;
                cmd.Parameters["@d2"].Value = dtpInvoiceDate.Text;
                cmd.Parameters["@d3"].Value = txtCustomerID.Text;
                cmd.Parameters["@d5"].Value = txtTotal.Text;
                cmd.Parameters["@d6"].Value = txtTotalPayment.Text;
                cmd.Parameters["@d7"].Value = txtPaymentDue.Text;
                cmd.Parameters["@d4"].Value = txtCustomerName.Text;

                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Close();


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {

                    con = new MySqlConnection(cs);
                    // con = new OleDbConnection(cs);
                    con.Open();
                    // string cd ="INSERT INTO order(orderID,productID,Quantity,Price,TotalAmount) VALUES(@orderID,@productID,@Quantity,@Price,@TotalAmount)";
                    string cd = "INSERT INTO orders (orderID,productID,quantity,price,totalAmount) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new MySqlCommand(cd);
                    // cmd = new OleDbCommand(cd);
                    cmd.Connection = con;

                    cmd.Parameters.Add(new MySqlParameter("@d1", MySqlDbType.VarChar, 20, "orderID"));
                    cmd.Parameters.Add(new MySqlParameter("@d2", MySqlDbType.VarChar, 20, "productID"));
                    cmd.Parameters.Add(new MySqlParameter("@d3", MySqlDbType.VarChar, 20, "quantity"));
                    cmd.Parameters.Add(new MySqlParameter("@d4", MySqlDbType.VarChar, 20, "price"));
                    cmd.Parameters.Add(new MySqlParameter("@d5", MySqlDbType.VarChar, 20, "totalAmount"));
                    cmd.Parameters["@d1"].Value = txtInvoiceNo.Text;
                    cmd.Parameters["@d2"].Value = ListView1.Items[i].SubItems[1].Text;
                    cmd.Parameters["@d3"].Value = ListView1.Items[i].SubItems[4].Text;
                    cmd.Parameters["@d4"].Value = ListView1.Items[i].SubItems[3].Text;
                    cmd.Parameters["@d5"].Value = ListView1.Items[i].SubItems[5].Text;
                    //cmd.ExecuteReader();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                   
                    con = new MySqlConnection(cs);
                   // con = new OleDbConnection(cs);
                    con.Open();
                    string cb1 = "update stock set Quantity = Quantity - " + ListView1.Items[i].SubItems[4].Text + " where productID= " + ListView1.Items[i].SubItems[1].Text + "";
                    cmd = new MySqlCommand(cb1);
                   // cmd = new OleDbCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
//                 for (int i = 0; i <= ListView1.Items.Count - 1; i++)
//                 {
//                     con = new MySqlConnection(cs);
//                     //con = new OleDbConnection(cs);
//                     con.Open();
// 
//                     string cb2 = "update stock set TotalPrice = Totalprice - '" + ListView1.Items[i].SubItems[5].Text + "' where ConfigID= " + ListView1.Items[i].SubItems[1].Text + "";
//                     cmd = new MySqlCommand(cb2);
//                   //  cmd = new OleDbCommand(cb2);
//                     cmd.Connection = con;
//                     cmd.ExecuteReader();
//                     con.Close();
//                 }

                Save.Enabled = false;
                btnPrint.Enabled = true;
                dataGridView1.Rows.Clear();
                GetData();
               // dataGridView1.Rows.Clear();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logindataDataSet.stock' table. You can move, or remove it, as needed.
            this.stockTableAdapter.Fill(this.logindataDataSet.stock);
            GetData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord1 frm = new frmCustomersRecord1();
          //  frm.label1.Text = label6.Text;
            frm.Visible = true;
        }


        private void txtSaleQty_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtPrice.Text, out val1);
            int.TryParse(txtSaleQty.Text, out val2);
            int I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();
        }

        public double subtot()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            i = 0;
            j = 0;
            k = 0;


            try
            {

                j = ListView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToInt32(ListView1.Items[i].SubItems[5].Text);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerID.Text == "")
                {
                    MessageBox.Show("Please retrieve Customer ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerID.Focus();
                    return;
                }

                if (txtProductName.Text == "")
                {
                    MessageBox.Show("Please retrieve product name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtSaleQty.Text == "")
                {
                    MessageBox.Show("Please enter no. of sale quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaleQty.Focus();
                    return;
                }
                int SaleQty = Convert.ToInt32(txtSaleQty.Text);
                if (SaleQty == 0)
                {
                    MessageBox.Show("no. of sale quantity can not be zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSaleQty.Focus();
                    return;
                }

                if (ListView1.Items.Count == 0)
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(txtConfigID.Text);
                    lst.SubItems.Add(txtProductName.Text);
                    lst.SubItems.Add(txtPrice.Text);
                    lst.SubItems.Add(txtSaleQty.Text);
                    lst.SubItems.Add(txtTotalAmount.Text);
                    ListView1.Items.Add(lst);
                    txtSubTotal.Text = subtot().ToString();
                    if (txtTaxPer.Text != "")
                    {
                        txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                        txtTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
                    }
                    int val1 = 0;
                    int val2 = 0;
                    int.TryParse(txtTotal.Text, out val1);
                    int.TryParse(txtTotalPayment.Text, out val2);
                    int I = (val1 - val2);
                    txtPaymentDue.Text = I.ToString();
                    txtProductName.Text = "";
                    txtConfigID.Text = "";
                    txtPrice.Text = "";
                    txtAvailableQty.Text = "";
                    txtSaleQty.Text = "";
                    txtTotalAmount.Text = "";
                    textBox1.Text = "";
                    return;
                }

                for (int j = 0; j <= ListView1.Items.Count - 1; j++)
                {
                    if (ListView1.Items[j].SubItems[1].Text == txtConfigID.Text)
                    {
                        ListView1.Items[j].SubItems[1].Text = txtConfigID.Text;
                        ListView1.Items[j].SubItems[2].Text = txtProductName.Text;
                        ListView1.Items[j].SubItems[3].Text = txtPrice.Text;
                        ListView1.Items[j].SubItems[4].Text = (Convert.ToInt32(ListView1.Items[j].SubItems[4].Text) + Convert.ToInt32(txtSaleQty.Text)).ToString();
                        ListView1.Items[j].SubItems[5].Text = (Convert.ToInt32(ListView1.Items[j].SubItems[5].Text) + Convert.ToInt32(txtTotalAmount.Text)).ToString();
                        txtSubTotal.Text = subtot().ToString();
                        if (txtTaxPer.Text != "")
                        {
                            txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                            txtTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
                        }
                        int val1 = 0;
                        int val2 = 0;
                        int.TryParse(txtTotal.Text, out val1);
                        int.TryParse(txtTotalPayment.Text, out val2);
                        int I = (val1 - val2);
                        txtPaymentDue.Text = I.ToString();
                        txtProductName.Text = "";
                        txtConfigID.Text = "";
                        txtPrice.Text = "";
                        txtAvailableQty.Text = "";
                        txtSaleQty.Text = "";
                        txtTotalAmount.Text = "";
                        return;

                    }
                }

                ListViewItem lst1 = new ListViewItem();

                lst1.SubItems.Add(txtConfigID.Text);
                lst1.SubItems.Add(txtProductName.Text);
                lst1.SubItems.Add(txtPrice.Text);
                lst1.SubItems.Add(txtSaleQty.Text);
                lst1.SubItems.Add(txtTotalAmount.Text);
                ListView1.Items.Add(lst1);
                txtSubTotal.Text = subtot().ToString();
                if (txtTaxPer.Text != "")
                {
                    txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                    txtTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
                }
                int val3 = 0;
                int val4 = 0;
                int.TryParse(txtTotal.Text, out val3);
                int.TryParse(txtTotalPayment.Text, out val4);
                int I1 = (val3 - val4);
                txtPaymentDue.Text = I1.ToString();
                txtProductName.Text = "";
                txtConfigID.Text = "";
                txtPrice.Text = "";
                txtAvailableQty.Text = "";
                txtSaleQty.Text = "";
                txtTotalAmount.Text = "";
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    ListView1.FocusedItem.Remove();
                    itmCnt = ListView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        //Dim lst1 As New ListViewItem(i)
                        //ListView1.Items(i).SubItems(0).Text = t
                        t = t + 1;

                    }
                    txtSubTotal.Text = subtot().ToString();
                    if (txtTaxPer.Text != "")
                    {
                        txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                        txtTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
                    }
                    int val1 = 0;
                    int val2 = 0;
                    int.TryParse(txtTotal.Text, out val1);
                    int.TryParse(txtTotalPayment.Text, out val2);
                    int I = (val1 - val2);
                    txtPaymentDue.Text = I.ToString();
                }

                btnRemove.Enabled = false;
                if (ListView1.Items.Count == 0)
                {
                    txtSubTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTaxPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }
                txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();
                txtTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(cs);
                //con = new OleDbConnection(cs);
                con.Open();

                String sql = "SELECT productID,DrugName,unitPrice,Quantity from Stock where DrugName like '" + textBox1.Text + "%' group by productID,DrugName,unitPrice having sum(quantity > 0) order by DrugName";
                cmd = new MySqlCommand(sql, con);
                //cmd = new OleDbCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                }
                con.Close();
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
               // MessageBox.Show("kjhgfd");
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtConfigID.Text = dr.Cells[0].Value.ToString();
                txtProductName.Text = dr.Cells[1].Value.ToString();
                txtPrice.Text = dr.Cells[3].Value.ToString();
                txtAvailableQty.Text = dr.Cells[4].Value.ToString();
                txtSaleQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetData()
        {
            try
            {
                con = new MySqlConnection(cs);
              //  con = new OleDbConnection(cs);
                con.Open();
                //String sql = "SELECT StockID,Config.ConfigID,ProductName,Features,Price,sum(Quantity) from Stock,Config where Stock.ConfigID=Config.ConfigID group by StockID,productname,Price,Features,Config.ConfigID having sum(quantity > 0) order by ProductName";
                String sql = "SELECT productID,DrugName,ChemicalName,unitPrice,Quantity,expiryDate from stock group by productID,DrugName,unitPrice having sum(quantity > 0) order by DrugName";
                cmd = new MySqlCommand(sql, con);
                //cmd = new OleDbCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              //  dataGridView1.Rows.Clear();
//                 DataTable DT = (DataTable)dataGridView1.DataSource;
//                 if (DT != null)
//                     DT.Clear();
                dataGridView1.DataSource = null;
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3],rdr[4],rdr[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            txtInvoiceNo.Text = "";
            dtpInvoiceDate.Text = DateTime.Today.ToString();
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtProductName.Text = "";
            txtConfigID.Text = "";
            txtPrice.Text = "";
            txtAvailableQty.Text = "";
            txtSaleQty.Text = "";
            txtTotalAmount.Text = "";
            ListView1.Items.Clear();
            txtSubTotal.Text = "";
            txtTaxPer.Text = "";
            txtTaxAmt.Text = "";
            txtTotal.Text = "";
            txtTotalPayment.Text = "";
            txtPaymentDue.Text = "";
            textBox1.Text = "";
            txtRemarks.Text = "";
            Save.Enabled = true;
            Delete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnPrint.Enabled = false;
            ListView1.Enabled = true;
            Button7.Enabled = true;

        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                con = new MySqlConnection(cs);
                //con = new OleDbConnection(cs);
                con.Open();
                string cq1 = "delete from order where orderID='" + txtInvoiceNo.Text + "'";
                cmd = new MySqlCommand(cq1);
               // cmd = new OleDbCommand(cq1);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                con = new MySqlConnection(cs);
                //con = new OleDbConnection(cs);
                con.Open();
                string cq = "delete from sales where orderID='" + txtInvoiceNo.Text + "'";
                cmd = new MySqlCommand(cq);
                //cmd = new OleDbCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.lblUser.Text = label6.Text;
            frm.Show();
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
            int I = (val1 - val2);
            txtPaymentDue.Text = I.ToString();
        }

        private void txtTotalPayment_Validating(object sender, CancelEventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Total Payment can't be more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPayment.Text = "";
                txtPaymentDue.Text = "";
                txtTotalPayment.Focus();
                return;
            }
        }

        private void txtSaleQty_Validating(object sender, CancelEventArgs e)
        {

            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtAvailableQty.Text, out val1);
            int.TryParse(txtSaleQty.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Selling quantities are more than available quantities", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSaleQty.Text = "";
                txtTotalAmount.Text = "";
                txtSaleQty.Focus();
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                rptInvoice rpt = new rptInvoice();
                //The report you created.
                cmd = new MySqlCommand();
                //cmd = new OleDbCommand();
                MySqlDataAdapter myDA = new MySqlDataAdapter();
                //OleDbDataAdapter myDA = new OleDbDataAdapter();
                DataSet myDS = new DataSet();
                //The DataSet you created.
                con = new MySqlConnection(cs);
              //  con = new OleDbConnection(cs);
                cmd.Connection = con;
                cmd.CommandText = "SELECT stock.productID, stock.DrugName,stock.unitPrice, sales.orderID, sales.orderDate, sales.CustomerID,sales.GrandTotal, sales.TotalPayment, sales.PaymentDue, order.orderID AS Expr1, order.productID AS Expr2, order.Quantity, order.Price AS Expr3, sales.TotalAmount,customer.CustomerID AS Expr4, customer.Customername, customer.address,customer.City,customer.zipcode,customer.Phone,customer.mobileno, customer.email, customer.notes FROM (((customer INNER JOIN sales ON customer.CustomerID = sales.CustomerID) INNER JOIN order ON sales.orderID = order.orderID) INNER JOIN stock ON order.productID = stock.productID) where sales.orderID='" + txtInvoiceNo.Text + "'";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "stock");
                myDA.Fill(myDS, "sales");
                myDA.Fill(myDS, "order");
                myDA.Fill(myDS, "customer");
                rpt.SetDataSource(myDS);
                frmInvoiceReport frm = new frmInvoiceReport();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con = new MySqlConnection(cs);
               // con = new OleDbConnection(cs);
                con.Open();
                String cb = "update sales set GrandTotal= " + txtTotal.Text + ",TotalPayment= " + txtTotalPayment.Text + ",PaymentDue= " + txtPaymentDue.Text + "' where orderID= '" + txtInvoiceNo.Text + "'";
                cmd = new MySqlCommand(cb);
             //   cmd = new OleDbCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new MySqlConnection(cs);
                  //  con = new OleDbConnection(cs);
                    string cd = "update order set Quantity=" + ListView1.Items[i].SubItems[4].Text + ",Price= " + ListView1.Items[i].SubItems[3].Text + ",TotalAmount= " + ListView1.Items[i].SubItems[5].Text + " where orderID='" + txtInvoiceNo.Text + "' and productID= " + ListView1.Items[i].SubItems[1].Text + "";
                    cmd = new MySqlCommand(cd);
                    //cmd = new OleDbCommand(cd);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new MySqlConnection(cs);
                    //con = new OleDbConnection(cs);
                    con.Open();
                    string cb1 = "update stock set Quantity = Quantity - " + ListView1.Items[i].SubItems[4].Text + " where productID= " + ListView1.Items[i].SubItems[1].Text + "";
                    cmd = new MySqlCommand(cb1);
                    //       cmd = new OleDbCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new MySqlConnection(cs);
                    //con = new OleDbConnection(cs);
                    con.Open();

                    string cb2 = "update stock set TotalPrice = TotalPrice - '" + ListView1.Items[i].SubItems[5].Text + "' where productID= " + ListView1.Items[i].SubItems[1].Text + "";
                    cmd = new MySqlCommand(cb2);
                    // cmd = new OleDbCommand(cb2);
                    cmd.Connection = con;
                    //cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }
                GetData();
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSalesRecord1 frm = new frmSalesRecord1();
            frm.DataGridView1.DataSource = null;
            frm.dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            frm.dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            frm.GroupBox3.Visible = false;
            frm.DataGridView3.DataSource = null;
            frm.cmbCustomerName.Text = "";
            frm.GroupBox4.Visible = false;
            frm.DateTimePicker1.Text = DateTime.Today.ToString();
            frm.DateTimePicker2.Text = DateTime.Today.ToString();
            frm.DataGridView2.DataSource = null;
            frm.GroupBox10.Visible = false;
            frm.FillCombo();
            frm.label9.Text = label6.Text;
            frm.Show();
        }

        private void txtSaleQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotalPayment_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}