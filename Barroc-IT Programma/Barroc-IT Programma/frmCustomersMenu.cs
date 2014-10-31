using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barroc_IT_Programma
{
    public partial class frmCustomersMenu : Form
    {
        public frmMenu frmMenu;
        public frmLogin frmLogin;
        public frmCustomersMenu(frmMenu menu, frmLogin login)
        {
            InitializeComponent();
            frmMenu = menu;
            frmLogin = login;
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
    }
}
