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
using System.Net.Mail;


namespace Barroc_IT_Programma
{
    public partial class frmAddCustomer : Form
    {
        DatabaseHandler dbh = new DatabaseHandler();
        AccountAcces ac = new AccountAcces();

        SqlCommand sqlCmd;
        SqlDataAdapter sqlDA = new SqlDataAdapter();
        DataTable sqlDT = new DataTable();
        Label[] lblInfo;
        TextBox[] txtbInfo;
        CheckBox[] chkbInfo;
        Button btnAdd;
        List<string> columns;
        List<string> values;
        int y = 15;
        int x = 15;
        
        public frmAddCustomer()
        {
            InitializeComponent();
            DBConnect();

            SqlDataReader sqlDR = sqlCmd.ExecuteReader();
            columns = new List<string>();
            for(int i=1;i<sqlDR.FieldCount;i++)
            {
                columns.Add(sqlDR.GetName(i));
            }
            sqlDR.Close();
            
            CreateLayout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;

            for (int i = 0; i < txtbInfo.Length; i++)
            {
                if (lblInfo[i].Text.Contains("Name") || lblInfo[i].Text.Contains("1") || lblInfo[i].Text.Contains("Phone") || lblInfo[i].Text.Contains("Mail"))
                {
                    if (txtbInfo[i].Text == "")
                    {
                        MessageBox.Show("Field '" + lblInfo[i].Text + "' is required");
                        isEmpty = true;
                        break;
                    }
                }
            }

            if (isEmpty)
            {
            }
            else if (!IsValidEmail(txtbInfo[13].Text))
            {
                MessageBox.Show("E-Mail is invalid");
            }

            else
            {
                ConvertValues();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = dbh.Getcon();

                    values.Skip(1);
                    cmd.CommandText = string.Format("INSERT INTO Tbl_Customer ({0}) VALUES('{1}')", string.Join(", ", columns.Where(s => !string.IsNullOrEmpty(s))), string.Join("', '", values));
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (i < columns.Count - 2)
                        { txtbInfo[i].ResetText(); } 
                        else
                        { chkbInfo[i].ResetText(); }
                    }
                    MessageBox.Show("Customer succesfully added");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to add customer\nError: " + ex.Message);
                }                
            }
        }

        //---methods---//
        private void intBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || IsInputKey(Keys.Back))
            {
                e.Handled = true;
            }
        }
        private void decimalBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            TextBox txtb = (TextBox)sender;
            if(!char.IsControl(e.KeyChar))
            {
                if (txtb.Text.IndexOf('.') > -1 && txtb.Text.Substring(txtb.Text.IndexOf('.')).Length >= 3)
                {
                    e.Handled = true;
                }
            }
            
        }
        private void txtLimit()
        {
            for (int i = 0; i < txtbInfo.Length; i++)
            {
                if (lblInfo[i].Text.Contains("Area Code"))
                {
                    txtbInfo[i].MaxLength = 2;
                }
                else if (lblInfo[i].Text.Contains("Area Number") || lblInfo[i].Text.Contains("Ledger"))
                {
                    txtbInfo[i].MaxLength = 4;
                }
                else if (lblInfo[i].Text.Equals("Initials") || lblInfo[i].Text.Contains("Invoices"))
                {
                    txtbInfo[i].MaxLength = 3;
                }
                else if (lblInfo[i].Text.Contains("Phone") || lblInfo[i].Text.Contains("Fax"))
                {
                    txtbInfo[i].MaxLength = 10;
                }
                else if (lblInfo[i].Text.Contains("Bank"))
                {
                    txtbInfo[i].MaxLength = 34;
                }
                else if (lblInfo[i].Text.Contains(" Amount"))
                {
                    txtbInfo[i].MaxLength = 21;
                }
                else
                {
                    txtbInfo[i].MaxLength = 50;
                }

                if (lblInfo[i].Text.Contains("Number")|| lblInfo[i].Text.Contains("Invoices") || lblInfo[i].Text.Contains("Account"))
                {
                    txtbInfo[i].ShortcutsEnabled = false;
                    txtbInfo[i].KeyPress += new KeyPressEventHandler(intBox_KeyPress);
                }

                if (lblInfo[i].Text.Contains(" Amount"))
                {
                    txtbInfo[i].ShortcutsEnabled = false;
                    txtbInfo[i].KeyPress += new KeyPressEventHandler(decimalBox_KeyPress);
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void DBConnect()
        {
            try
            {
                dbh.OpenCon();
                sqlCmd = new SqlCommand("Select top 1* from Tbl_Customer", dbh.Getcon());
            }
            catch (Exception e)
            {
                MessageBox.Show("The following error has been encountered\n" + e.Message);
                dbh.CloseCon();
                Environment.Exit(0);
            }
        }
        private void CreateLayout()
        {
            lblInfo = new Label[columns.Count];
            txtbInfo = new TextBox[columns.Count - 2];
            chkbInfo = new CheckBox[columns.Count];
            for (int i = 0; i < lblInfo.Length; i++)
            {
                lblInfo[i] = new Label();
                lblInfo[i].Text = columns[i].ToString();
                if (lblInfo[i].Text == "E_Mail")
                { lblInfo[i].Text = lblInfo[i].Text.Replace("_", "-"); }
                else
                { lblInfo[i].Text = lblInfo[i].Text.Replace("_", " "); }
                lblInfo[i].Location = new Point(x, y);
                this.Controls.Add(lblInfo[i]);
                if (i < (columns.Count - 2))
                {
                    txtbInfo[i] = new TextBox();
                    txtbInfo[i].Name = columns[i].ToString();
                    txtbInfo[i].Location = new Point((x + 100), y);
                    this.Controls.Add(txtbInfo[i]);
                }
                else
                {
                    chkbInfo[i] = new CheckBox();
                    chkbInfo[i].Location = new Point((x + 100), y);
                    this.Controls.Add(chkbInfo[i]);
                }

                if (y >= 400)
                {
                    x += 250;
                    y = 15;
                }
                else
                { y += 30; }

            }
            txtLimit();

            btnAdd = new Button();
            btnAdd.Text = "Add";
            btnAdd.Location = new Point((x + 100), y);
            this.Controls.Add(btnAdd);
            this.AcceptButton = btnAdd;
            btnAdd.Click += new EventHandler(btnAdd_Click);
        }
        private void ConvertValues()
        {
            values = new List<string>();
            for (int i = 0; i < columns.Count; i++)
            {
                if (i < columns.Count - 2)
                {
                    if (txtbInfo[i].Text != "")
                    { values.Add(txtbInfo[i].Text); }
                    else
                    { values.Add(string.Empty); }
                }
                else
                {
                    values.Add(chkbInfo[i].Checked.ToString());
                }
            }
        }
    }
}
