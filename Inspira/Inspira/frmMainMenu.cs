using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
namespace Inspira
{
    public partial class frmMainMenu : Form
    {
        //OleDbDataReader rdr = null;
        // OleDbConnection con = null;
        //OleDbCommand cmd = null;
        //  String cs = Inspira.Properties.Settings.Default.connectionString;
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.Show();
        }

        private void registrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmUserRegistration frm = new frmUserRegistration();
            frm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.Show();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserRegistration frm = new frmUserRegistration();
            frm.Show();
        }

        private void profileEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmProduct frm = new frmProduct();
            frm.Show();
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void wordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Wordpad.exe");
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TaskMgr.exe");
        }

        private void mSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Winword.exe");
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            frm.Show();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompany frm = new frmCompany();
            frm.Show();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomersRecord frm = new frmCustomersRecord();
            frm.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.Show();
            frm.txtUserName.Text = "";
            frm.txtPassword.Text = "";
            frm.txtUserName.Focus();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logindataDataSet.stock' table. You can move, or remove it, as needed.
            this.stockTableAdapter.Fill(this.logindataDataSet.stock);
            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();
            GetData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();
        }

        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct();
            frm.Show();
        }

        private void productsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmProductsRecord frm = new frmProductsRecord();
            frm.Show();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.Show();

        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStock frm = new frmStock();
            frm.label8.Text = lblUser.Text;
            frm.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStock frm = new frmStock();
            frm.label8.Text = lblUser.Text;
            frm.Show();
        }

        public void GetData()
        {
            
            try {
                MySqlConnection myConnection1 = new MySqlConnection();

                myConnection1.ConnectionString = Inspira.Properties.Settings.Default.connectionString;
                String Query = "SELECT productID,DrugName,ChemicalName,batchNo,companyName,unitPrice,Quantity from stock";
                MySqlCommand myCommand1 = new MySqlCommand(Query, myConnection1);
                MySqlDataAdapter myAdapter1 = new MySqlDataAdapter();
                myAdapter1.SelectCommand = myCommand1;
               // myConnection1.Open();
                DataTable table = new DataTable();
                myAdapter1.Fill(table);
                //adapter = new MySqlDataAdapter("SELECT ID,DrugName,ChemicalName from stock");
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stockToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmStockRecord frm = new frmStockRecord();
            frm.Show();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmOrder frm = new frmOrder();
            frm.label6.Text = lblUser.Text;
            frm.Show();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmOrder frm = new frmOrder();
            frm.label6.Text = lblUser.Text;
            frm.Show();
        }

        private void salesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSalesRecord frm = new frmSalesRecord();
            frm.Show();
        }

        private void loginDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginDetails frm = new frmLoginDetails();
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try { 
            MySqlConnection myConnection1 = new MySqlConnection();
            myConnection1.ConnectionString = Inspira.Properties.Settings.Default.connectionString;
                String Query = "SELECT productID,DrugName,ChemicalName,batchNo,companyName,unitPrice,Quantity from stock WHERE DrugName like '" + txtProductName.Text + "%' ";
            MySqlCommand myCommand1 = new MySqlCommand(Query, myConnection1);
            MySqlDataAdapter myAdapter1 = new MySqlDataAdapter();
            myAdapter1.SelectCommand = myCommand1;
            // myConnection1.Open();
            DataTable table = new DataTable();
            myAdapter1.Fill(table);
            //adapter = new MySqlDataAdapter("SELECT ID,DrugName,ChemicalName from stock");
            dataGridView1.DataSource = table;
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();

        }


    }
}
