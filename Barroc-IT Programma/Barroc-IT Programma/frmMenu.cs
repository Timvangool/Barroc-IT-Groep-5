using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.SqlServerCe;

namespace Barroc_IT_Programma
{
    public partial class frmMenu : Form
    {
        private DatabaseHandler dbh;
        public frmLogin frmloginpanel;
        public frmMenu(frmLogin login)
        {
            InitializeComponent();
            frmloginpanel = login;
            dbh = new DatabaseHandler();
            lblMenuVisible.Visible = false;
        }
        public bool permissions()
        {
            dbh.TestConnection();
            dbh.OpenCon();
            bool admin = false;
            string user = lbl_user.Text;
            int permissionValue = 0;
            string sql = "SELECT * FROM  tbl_Account WHERE Username = @user";
            SqlCommand cmd = new SqlCommand(sql, dbh.Getcon());
            cmd.Parameters.Add(new SqlParameter("user", user));
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                permissionValue = rdr.GetInt32(3);
            }
            if (permissionValue == 4)
            {
                admin = true;
            }
            dbh.CloseCon();
            return admin;
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
           bool admin = permissions();

            if(admin)
            {
                // Open admin panel
            }
            else
            {
                MessageBox.Show("You don't have permissions to open the Admin Panel");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            
            frmloginpanel.lblLoginVisible.Text = "Show";
            lblMenuVisible.Text = "Hide";
            ToggleForm();
        }

        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomersMenu frmCustomer = new frmCustomersMenu(this, frmloginpanel);
            frmCustomer.Show();
            frmCustomer.lblCustomerVisible.Text = "Show";
            frmCustomer.ToggleForm();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }

        public void ToggleForm()
        {
            if(lblMenuVisible.Text =="Show")
            {
                this.Opacity = 1;
                this.ShowInTaskbar = true;
            }
            else if(lblMenuVisible.Text =="Hide")
            {
                dbh.CloseCon();
                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }
            
        }

        private void lblMenuVisible_TextChanged(object sender, EventArgs e)
        {
            ToggleForm();
        }
    }
}
