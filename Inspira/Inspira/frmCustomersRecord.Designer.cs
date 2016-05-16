namespace Inspira
{
    partial class frmCustomersRecord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomersRecord));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCustomers = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.customerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.logindataDataSet1 = new Inspira.logindataDataSet1();
            this.logindataDataSet9 = new Inspira.logindataDataSet9();
            this.customerTableAdapter = new Inspira.logindataDataSet1TableAdapters.customerTableAdapter();
            this.logindataDataSet2 = new Inspira.logindataDataSet2();
            this.customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customerTableAdapter1 = new Inspira.logindataDataSet2TableAdapters.customerTableAdapter();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zipcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobilenoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logindataDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logindataDataSet9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logindataDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCustomers);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 82);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Customers";
            // 
            // txtCustomers
            // 
            this.txtCustomers.Location = new System.Drawing.Point(16, 40);
            this.txtCustomers.Name = "txtCustomers";
            this.txtCustomers.Size = new System.Drawing.Size(218, 24);
            this.txtCustomers.TabIndex = 0;
            this.txtCustomers.TextChanged += new System.EventHandler(this.txtCustomers_TextChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.button1);
            this.GroupBox2.Controls.Add(this.Button3);
            this.GroupBox2.Location = new System.Drawing.Point(274, 3);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(230, 82);
            this.GroupBox2.TabIndex = 18;
            this.GroupBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(119, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "&View Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Button3
            // 
            this.Button3.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button3.Location = new System.Drawing.Point(19, 23);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(94, 40);
            this.Button3.TabIndex = 2;
            this.Button3.Text = "&Export Excel";
            this.Button3.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerIDDataGridViewTextBoxColumn,
            this.customernameDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn,
            this.zipcode,
            this.phoneDataGridViewTextBoxColumn,
            this.email,
            this.mobilenoDataGridViewTextBoxColumn,
            this.notes});
            this.dataGridView1.DataSource = this.customerBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(23, 117);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(947, 517);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // customerBindingSource1
            // 
            this.customerBindingSource1.DataMember = "customer";
            this.customerBindingSource1.DataSource = this.logindataDataSet1;
            // 
            // logindataDataSet1
            // 
            this.logindataDataSet1.DataSetName = "logindataDataSet1";
            this.logindataDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // logindataDataSet9
            // 
            this.logindataDataSet9.DataSetName = "logindataDataSet9";
            this.logindataDataSet9.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customerTableAdapter
            // 
            this.customerTableAdapter.ClearBeforeFill = true;
            // 
            // logindataDataSet2
            // 
            this.logindataDataSet2.DataSetName = "logindataDataSet2";
            this.logindataDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customerBindingSource
            // 
            this.customerBindingSource.DataMember = "customer";
            this.customerBindingSource.DataSource = this.logindataDataSet2;
            // 
            // customerTableAdapter1
            // 
            this.customerTableAdapter1.ClearBeforeFill = true;
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            this.customerIDDataGridViewTextBoxColumn.HeaderText = "CustomerID";
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            // 
            // customernameDataGridViewTextBoxColumn
            // 
            this.customernameDataGridViewTextBoxColumn.DataPropertyName = "Customername";
            this.customernameDataGridViewTextBoxColumn.HeaderText = "Customername";
            this.customernameDataGridViewTextBoxColumn.Name = "customernameDataGridViewTextBoxColumn";
            // 
            // addressDataGridViewTextBoxColumn
            // 
            this.addressDataGridViewTextBoxColumn.DataPropertyName = "address";
            this.addressDataGridViewTextBoxColumn.HeaderText = "address";
            this.addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "city";
            this.cityDataGridViewTextBoxColumn.HeaderText = "city";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            // 
            // zipcode
            // 
            this.zipcode.DataPropertyName = "zipcode";
            this.zipcode.HeaderText = "zipcode";
            this.zipcode.Name = "zipcode";
            // 
            // phoneDataGridViewTextBoxColumn
            // 
            this.phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
            this.phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
            this.phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
            // 
            // email
            // 
            this.email.DataPropertyName = "email";
            this.email.HeaderText = "email";
            this.email.Name = "email";
            // 
            // mobilenoDataGridViewTextBoxColumn
            // 
            this.mobilenoDataGridViewTextBoxColumn.DataPropertyName = "mobileno";
            this.mobilenoDataGridViewTextBoxColumn.HeaderText = "mobileno";
            this.mobilenoDataGridViewTextBoxColumn.Name = "mobilenoDataGridViewTextBoxColumn";
            // 
            // notes
            // 
            this.notes.DataPropertyName = "notes";
            this.notes.HeaderText = "notes";
            this.notes.Name = "notes";
            // 
            // frmCustomersRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(974, 646);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmCustomersRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customers Record";
            this.Load += new System.EventHandler(this.frmCustomersRecord_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logindataDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logindataDataSet9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logindataDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCustomers;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    //    private LoginDataDataSet2 loginDataDataSet2;
   //     private System.Windows.Forms.BindingSource customerBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
   //     private System.Windows.Forms.BindingSource logindataDataSet9BindingSource;
        private logindataDataSet9 logindataDataSet9;
        private logindataDataSet1 logindataDataSet1;
        private System.Windows.Forms.BindingSource customerBindingSource1;
        private logindataDataSet1TableAdapters.customerTableAdapter customerTableAdapter;
        private logindataDataSet2 logindataDataSet2;
        private System.Windows.Forms.BindingSource customerBindingSource;
        private logindataDataSet2TableAdapters.customerTableAdapter customerTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zipcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn mobilenoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notes;
    }
}