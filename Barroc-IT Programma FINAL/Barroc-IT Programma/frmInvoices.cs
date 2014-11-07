using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlServerCe;

namespace Barroc_IT_Programma
{
            
    public partial class frmInvoices : Form
    {
        frmLogin frmLogin;
        DatabaseHandler dbh = new DatabaseHandler();
        frmMenu frmmenu;
        Form frmShowInvoices = new Form();
        DataGridView dgvInvoices = new DataGridView();
        Button btnCancel = new Button(); 
        Label lbShowHide = new Label();

        public frmInvoices(frmLogin login, frmMenu menu)
        {
            InitializeComponent();
            frmLogin = login;
            frmmenu = menu;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin.Opacity = 1;
            frmLogin.ShowInTaskbar = true;
            frmLogin.Enabled = true;
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Enabled = false;
        }

        private void btnShowInvoices_Click(object sender, EventArgs e)
        {
            if (lbShowHide.Text != "")
            {
                frmShowInvoices.ShowInTaskbar = true;
                frmShowInvoices.Opacity = 1;
                frmShowInvoices.Enabled = true;
                this.ShowInTaskbar = false;
                this.Opacity = 0;
                this.Enabled = false;
            }
            else
            {
                frmShowInvoices.Height = 350;
                frmShowInvoices.Width = 500;
                frmShowInvoices.Text = "Invoices.";
                frmShowInvoices.FormClosing += new FormClosingEventHandler(frmShowInvoices_FormClosing);
                frmShowInvoices.Show();

                dgvInvoices.Height = 200;
                dgvInvoices.Width = 450;
                dgvInvoices.Location = new Point(20, 50);
                frmShowInvoices.Controls.Add(dgvInvoices);

                btnCancel.Width = 75;
                btnCancel.Height = 23;
                btnCancel.Text = "Close";
                btnCancel.Location = new Point(20, 255);
                btnCancel.Click += new EventHandler(btnCancel_Click);
                frmShowInvoices.Controls.Add(btnCancel);

                lbShowHide.Text = "";
                lbShowHide.Location = new Point(500, 700);
                frmShowInvoices.Controls.Add(lbShowHide);
                lbShowHide.Text = "Show"; 
                LoadDgv(dgvInvoices);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
                this.Opacity = 1;
                this.ShowInTaskbar = true;
                this.Enabled = true;
                frmShowInvoices.Opacity = 0;
                frmShowInvoices.ShowInTaskbar = false;
                frmShowInvoices.Enabled = false;
                lbShowHide.Text = "Hide";
        }

        public void LoadDgv(DataGridView dgv)
        {
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + Environment.CurrentDirectory + "\\Barroc-IT.mdf';Integrated Security=True;Connect Timeout=50";
            string query = "SELECT * FROM Tbl_Invoices";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dgv.DataSource = ds.Tables[0];
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Enabled = false;
            frmmenu.Opacity = 1;
            frmmenu.ShowInTaskbar = true;
            frmmenu.Enabled = true;
        }

        private void frmInvoices_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Please go back and log-out first before you close the program");
            e.Cancel = true;
        }

        private void frmShowInvoices_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Please use the 'Close' button to exit this window.");
            e.Cancel = true;
        }

        private void btnEditInvoices_Click(object sender, EventArgs e)
        {

        }

        private void btnAddInvoices_Click(object sender, EventArgs e)
        {
            frmAddInvoice frmaddinvoice = new frmAddInvoice(this);
            frmaddinvoice.Show();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }
    }
}