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
    public partial class frmLogin : Form
    {
        private DatabaseHandler dbh;
        public frmMenu frmMenu;
        public Launcher launcher;
        public frmLogin(Launcher form)
        {
            InitializeComponent();
            launcher = form;
            dbh = new DatabaseHandler();
            tbPassword.KeyDown += new KeyEventHandler(tbPassword_KeyDown);
            frmMenu = new frmMenu(this);
            frmMenu.Show();
            frmMenu.Opacity = 0;
            frmMenu.ShowInTaskbar = false;
            frmMenu.Enabled = false;
            lblLoginVisible.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login()
        {
            if (tbUsername.Text == "")
            {
                MessageBox.Show("Field 'Username' is required.");
            }
            if (tbPassword.Text == "")
            {
                MessageBox.Show("Field 'Password' is required.");
            }
            if (tbPassword.Text != "" && tbUsername.Text != "")
            {
                if (CheckLogin(tbUsername.Text, tbPassword.Text))
                {
                    frmMenu.lbl_user.Text = tbUsername.Text;
                    MenuShow();
                    this.Opacity = 0;
                    this.ShowInTaskbar = false;
                    this.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Your Username and Password does not match. Contact the program manager.");
                }
            }
        }

        public void MenuShow()
        {
            lblLoginVisible.Text = "Hide";
            frmMenu.Opacity = 1;
            frmMenu.ShowInTaskbar = true;
            frmMenu.Enabled = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private bool CheckLogin(string userName, string passWord)
        {
            bool loginSuccessful = false;
            string username = null;
            string password = null;
            string sql = "SELECT * FROM  tbl_Account WHERE Username = @UserName AND Password = @Password";
            dbh.OpenCon();
            SqlCommand sqlCommand = new SqlCommand(sql, dbh.Getcon());
            sqlCommand.Parameters.Add(new SqlParameter("UserName", userName));
            sqlCommand.Parameters.Add(new SqlParameter("Password", passWord));
            SqlDataReader rdr = sqlCommand.ExecuteReader();
            while (rdr.Read())
            {
                username = rdr.GetString(1);
                password = rdr.GetString(2);
            }
            if (username == tbUsername.Text && password == tbPassword.Text)
            {
                    loginSuccessful = true;
            }
            dbh.CloseCon();
            return loginSuccessful;
        }

        void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
                launcher.ExitProgram();
            else
                e.Cancel = true;
        }

        public void ToggleForm()
        {
            if (lblLoginVisible.Text == "Show")
            {
                this.Opacity = 1;
                this.ShowInTaskbar = true;
                this.Enabled = true;
            }
            else if (lblLoginVisible.Text == "Hide")
            {
                this.Opacity = 0;
                this.ShowInTaskbar = false;
                this.Enabled = false;
            }

        }

        private void lblVisible_TextChanged(object sender, EventArgs e)
        {
            ToggleForm();
        }
    }
}
