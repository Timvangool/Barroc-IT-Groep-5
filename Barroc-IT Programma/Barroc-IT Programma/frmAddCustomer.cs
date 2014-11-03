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
using System.Net.Mail;

namespace Barroc_IT_Programma
{
    public partial class frmAddCustomer : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='D:\Visual Studio 2013\Projects\Barroc-IT Programma\Barroc-IT Programma\Barroc-IT Programma\Barroc-IT.mdf';Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCmd = new SqlCommand("Select top 1* from Tbl_Customer");
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
            try
            {
                sqlCmd.Connection = sqlCon;
                sqlDA.SelectCommand = sqlCmd;
                sqlDA.Fill(sqlDT);
                sqlCon.Open();                
            }
            catch(Exception e)
            {
                MessageBox.Show("The following error has been encountered\n" + e.Message);
                Environment.Exit(0);
            }
            SqlDataReader sqlDR = sqlCmd.ExecuteReader();

            columns = new List<string>();
            for(int i=1;i<sqlDR.FieldCount;i++)
            {
                columns.Add(sqlDR.GetName(i));
            }
            sqlDR.Close();

            lblInfo = new Label[columns.Count];
            txtbInfo = new TextBox[columns.Count - 2];
            chkbInfo = new CheckBox[columns.Count];
            for (int i = 0; i < lblInfo.Length; i++)
            {
                lblInfo[i] = new Label();
                lblInfo[i].Text = columns[i].ToString();
                if(lblInfo[i].Text == "E_Mail")
                { lblInfo[i].Text = lblInfo[i].Text.Replace("_", "-"); }
                else
                { lblInfo[i].Text = lblInfo[i].Text.Replace("_", " "); }
                lblInfo[i].Location = new Point(x, y);
                this.Controls.Add(lblInfo[i]);
                if (i < (columns.Count - 2))
                {
                    txtbInfo[i] = new TextBox();
                    txtbInfo[i].Location = new Point((x + 100), y);
                    this.Controls.Add(txtbInfo[i]);
                }
                else
                {
                    chkbInfo[i] = new CheckBox();
                    chkbInfo[i].Location = new Point((x + 100), y);
                    this.Controls.Add(chkbInfo[i]);
                }
                
                if(y >= 500)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;

            for (int i = 0; i < txtbInfo.Length; i++)
            {
                if ((i >= 0 && i <= 4) || i == 11 || i == 13)
                {
                    if (!IsNotEmpty(txtbInfo[i].Text))
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

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlCon;

                    values.Skip(1);
                    cmd.CommandText = string.Format("INSERT INTO Tbl_Customer ({0}) VALUES('{1}')", string.Join(", ", columns.Where(s => !string.IsNullOrEmpty(s))), string.Join("', '", values));
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        txtbInfo[i].ResetText();
                        chkbInfo[i].ResetText();
                    }
                    MessageBox.Show("Customer succesfully added");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to add customer\nError:" + ex.Message);
                }                
            }
        }

        private void intBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || IsInputKey(Keys.Back))
            {
                e.Handled = true;
            }
        }
        private void txtLimit()
        {
            for (int i = 1; i < txtbInfo.Length; i++)
            {
                if (i == 2 || i == 6 || i == 10)
                {
                    txtbInfo[i].MaxLength = 2;
                }
                else if (i == 3 || i == 7)
                {
                    txtbInfo[i].MaxLength = 4;
                }
                else if (i == 11 || i == 12)
                {
                    txtbInfo[i].MaxLength = 10;
                }
                else if (i == 14)
                {
                    txtbInfo[i].MaxLength = 34;
                }

                if(i == 3|| i == 7 || i == 11 || i == 12 || (i >= 14 && i <= 18))
                {
                    txtbInfo[i].KeyPress += new KeyPressEventHandler(intBox_KeyPress);
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
        private bool IsNotEmpty(string text)
        {
            if (text != "")
            { return true; }
            else
            { return false; }            
        }
    }
}
