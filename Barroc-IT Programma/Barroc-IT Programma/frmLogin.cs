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

        public frmLogin()
        {
            InitializeComponent();
            dbh = new DatabaseHandler();
            tbPassword.KeyDown += new KeyEventHandler(tbPassword_KeyDown);
            tbUsername.KeyDown += new KeyEventHandler(tbUsername_KeyDown);
        }

        //private bool ExitProgram()
        //{
        //    bool close = false;
        //    DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //    if (result.Equals(DialogResult.OK))
        //    {
        //        if (dbh.Getcon().State == ConnectionState.Open)
        //        {
        //            dbh.CloseCon();
        //        }
        //        close = true;
        //    }
        //    else
        //    {
        //        close = false;
        //    }
        //    return close;
        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
                Application.Exit();
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
                    frmMenu frmmenu = new frmMenu();
                    frmmenu.lbl_user.Text = tbUsername.Text;
                    frmmenu.Show();
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

        //private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if(!ExitProgram())
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //    }
        //}

        void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();
        }

        void tbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbPassword.Focus();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        } 
    }
}
