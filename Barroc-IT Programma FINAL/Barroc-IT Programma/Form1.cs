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

namespace Barroc_IT_Programma
{
    public partial class frmLogin : Form
    {
        private DatabaseHandler dbh;
        public frmMenu frmmenu;

        public frmLogin()
        {
            InitializeComponent();
            dbh = new DatabaseHandler();
            frmmenu = new frmMenu();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                if (dbh.Getcon().State == ConnectionState.Open)
                {
                    dbh.CloseCon();
                }
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
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
            dbh.TestConnection();
            dbh.OpenCon();

            bool exist = false;
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            tbUsername.Text = "";
            tbPassword.Text = "";

            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Table] WHERE Username = @Username AND Password = @Password", dbh.Getcon()))
            {
                cmd.Parameters.AddWithValue("Username", username);
                cmd.Parameters.AddWithValue("Password", password);
                exist = (int)cmd.ExecuteScalar() > 0;
           }

            if (exist)
            {
                bool admin;
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) from [Table] WHERE Username = @Username AND Permisions = 4", dbh.Getcon()))
                {
                    cmd.Parameters.AddWithValue("Username", username);
                    admin = (int)cmd.ExecuteScalar() > 0;
                }
                dbh.CloseCon();

                if (admin)
                {
                    MessageBox.Show("Admin");

                    frmmenu.Show();
                }
                else
                {
                    MessageBox.Show("Normal user");
                }
            }
            else
            {
                dbh.CloseCon();
                MessageBox.Show("Wrong username and/or password");
            }
            }
        }
    }
}
