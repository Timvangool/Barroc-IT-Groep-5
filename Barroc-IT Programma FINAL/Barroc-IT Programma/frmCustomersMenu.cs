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
    public partial class frmCustomersMenu : Form
    {
        public frmMenu frmMenu;
        public frmLogin frmLogin;
        DatabaseHandler dbh = new DatabaseHandler();
        Form frmShowCustomers = new Form();
        DataGridView dgvCustomers = new DataGridView();
        Button btnCancel = new Button();
        Label lbShowHide = new Label();
        string username;
        frmAddCustomer add;
        public frmCustomersMenu(frmMenu menu, frmLogin login, string username)
        {
            InitializeComponent();
            frmMenu = menu;
            frmLogin = login;
            this.username = username;
            if(username != "Sales")
            {
                btnAddCustomer.Enabled = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmMenu.lblMenuVisible.Text = "Show";
            frmMenu.ToggleForm();
            lblCustomerVisible.Text = "Hide";
            ToggleForm();
        }

        public void ToggleForm()
        {
            if (lblCustomerVisible.Text == "Show")
            {
                this.Opacity = 1;
                this.ShowInTaskbar = true;
            }
            else if (lblCustomerVisible.Text == "Hide")
            {
                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }

        }

        private void lblCustomerVisible_TextChanged(object sender, EventArgs e)
        {
            ToggleForm();
        }

        private void frmCustomersMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Please go back and log-out first before you close the program");
            e.Cancel = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            lblCustomerVisible.Text = "Hide";
            ToggleForm();
            frmLogin.lblLoginVisible.Text = "Show";
            frmLogin.ToggleForm();
        }

        private void btnShowCustomer_Click(object sender, EventArgs e)
        {
            if (lbShowHide.Text != "")
            {
                frmShowCustomers.ShowInTaskbar = true;
                frmShowCustomers.Opacity = 1;
                frmShowCustomers.Enabled = true;
                this.ShowInTaskbar = false;
                this.Opacity = 0;
                this.Enabled = false;
            }
            else
            {
                frmShowCustomers.Height = 350;
                frmShowCustomers.Width = 500;
                frmShowCustomers.Text = "Customers.";
                frmShowCustomers.FormClosing += new FormClosingEventHandler(frmShowCustomers_FormClosing);
                frmShowCustomers.Show();

                dgvCustomers.Height = 200;
                dgvCustomers.Width = 450;
                dgvCustomers.Location = new Point(20, 50);
                frmShowCustomers.Controls.Add(dgvCustomers);

                btnCancel.Width = 75;
                btnCancel.Height = 23;
                btnCancel.Text = "Close";
                btnCancel.Location = new Point(20, 255);
                btnCancel.Click += new EventHandler(btnCancel_Click);
                frmShowCustomers.Controls.Add(btnCancel);

                lbShowHide.Text = "";
                lbShowHide.Location = new Point(500, 700);
                frmShowCustomers.Controls.Add(lbShowHide);
                lbShowHide.Text = "Show";
                LoadDgv(dgvCustomers);

                this.ShowInTaskbar = false;
                this.Opacity = 0;
                this.Enabled = false;
            }
        }

        public void LoadDgv(DataGridView dgv)
        {
            string query = "SELECT * FROM Tbl_Customer";
            using (SqlConnection conn = dbh.Getcon())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dgv.DataSource = ds.Tables[0];
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
            this.ShowInTaskbar = true;
            this.Enabled = true;
            frmShowCustomers.Opacity = 0;
            frmShowCustomers.ShowInTaskbar = false;
            frmShowCustomers.Enabled = false;
            lbShowHide.Text = "Hide";
        }

        private void frmShowCustomers_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Please use the 'Close' button to exit this window..");
            e.Cancel = true;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            add = new frmAddCustomer(username);
            add.Show();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            frmEditBasicCustomer frmedit = new frmEditBasicCustomer(username, this);
            this.Enabled = false;
            frmedit.Show();
        }
    }
}