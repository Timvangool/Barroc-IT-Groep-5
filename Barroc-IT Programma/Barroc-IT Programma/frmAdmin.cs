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
    public partial class frmAdmin : Form
    {
        frmLogin frmlogin;
        frmAddEditUser frmaddedituser;
        public frmAdmin()
        {
            InitializeComponent();
        }

        public string AddEdit;

        public void btnAddUser_Click(object sender, EventArgs e)
        {
            frmaddedituser = new frmAddEditUser();
            AddEdit = "Add";
            frmaddedituser.Show();
        }

        public void btnEditUser_Click(object sender, EventArgs e)
        {
            frmaddedituser = new frmAddEditUser();
            AddEdit = "Edit";
            frmaddedituser.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmlogin = new frmLogin();
            this.Hide();
            frmlogin.Show();
        }
    }
}