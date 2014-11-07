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
    public partial class frmEditBasicCustomer : Form
    {
        frmCustomersMenu custMenu;
        string username;
        DatabaseHandler dbh = new DatabaseHandler();
        AccountAcces acces;
        int[] Acces;
        string[] columns;

        TextBox[] tb = new TextBox[25];
        int tbX = 120;
        int tbY = 13;
        string customerSelected;
        int[] customerId;
        int selectedCustomerID;

        public frmEditBasicCustomer(string user, frmCustomersMenu menu)
        {
            InitializeComponent();
            custMenu = menu;
            username = user;
            acces = new AccountAcces(username);
            columns = acces.GetColumnsCustomer();
            LoadCustomers();
            cbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.Opacity = 0;
            //this.Enabled = false;
            //this.ShowInTaskbar = false;
            //CheckUser();  
            Acces = acces.GetCustomerAcces();
            btnSave.Visible = false;
        }

        protected void CheckUser()
        {
            if (username == "Sales")
            {
                Acces = acces.GetCustomerAcces();
                this.Opacity = 1;
                this.Enabled = true;
                this.ShowInTaskbar = true;
            }
            else
            {
                MessageBox.Show("There went something wrong.");
            }
        }

        protected string[] ConvertToStringArray()
        {
            string[] contentTextbox = new string[Acces.Length];
            for (int i = 0; i < Acces.Length; i++)
            {
                if (tb[i].Text == "")
                    contentTextbox[i] = null;
                else
                    contentTextbox[i] = tb[i].Text;
            }
            return contentTextbox;
        }

        protected void UpdateCustomer()
        {
            string[] data = ConvertToStringArray();
            for (int i = 0; i < Acces.Length; i++)
            {
                if (data[i] == null)
                {
                    data[i] = "";
                }
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + Environment.CurrentDirectory + "\\Barroc-IT.mdf';Integrated Security=True;Connect Timeout=50");
                int accesNumber = Acces[i];
                string temp = columns[accesNumber];
                var value = data[i];
                string sql = "UPDATE Tbl_Customer SET " + temp + " = @Value" + " WHERE Company_Id = @Customer";
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                sqlCommand.Parameters.Add(new SqlParameter("Value", value));
                sqlCommand.Parameters.Add(new SqlParameter("Customer", selectedCustomerID));
                sqlCommand.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void LoadCustomers()
        {
            int i = 0;
            customerId = new int[255];
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + Environment.CurrentDirectory + "\\Barroc-IT.mdf';Integrated Security=True;Connect Timeout=50"))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "select Company_Id, Company_Name from Tbl_customer";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbCustomer.Items.Add(reader.GetString(1));
                        customerId[i] = reader.GetInt32(0);
                        i++;
                    }
                }
                connection.Close();
            }
        }

        protected void AddLabels()
        {
            int lenght = Acces.Length;
            int x = 13;
            int y = 13;
            for (int i = 0; i < lenght; i++)
            {
                int accesNumber = Acces[i];
                string text = columns[accesNumber];
                if (text.Contains("_"))
                {
                    text = text.Replace("_", " ");
                }
                else if (text == "E_Mail")
                {
                    text = "E-Mail";
                }
                Label[] lbl = new Label[columns.Length];
                lbl[i] = new Label();
                lbl[i].Text = text;
                lbl[i].Location = new Point(x, y);
                lbl[i].Name = text;
                this.Controls.Add(lbl[i]);
                y += 30;
                if (y >= 500)
                {
                    x += 250;
                    y = 13;
                }
            }
        }

        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            do
            {
                this.Height += 5;
                this.CenterToScreen();
            } while (this.Height < 600);
            this.CenterToScreen();
            customerSelected = ((string)cbCustomer.SelectedItem).ToString();
            selectedCustomerID = cbCustomer.SelectedIndex + 1;
            cbCustomer.Visible = false;
            cbCustomer.Enabled = false;
            lblSelect.Visible = false;
            btnSelectCustomer.Visible = false;
            btnSelectCustomer.Enabled = false;
            btnSave.Visible = true;
            btnSave.Enabled = true;
            AddLabels();
            AddTextbox();
        }

        private void AddTextbox()
        {
            string[] contentTextbox = new string[25];
            for (int i = 0; i < Acces.Length; i++)
            {
                //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\sven\Documents\Visual Studio 2013\Projects\Barroc-IT Programma work everything\Barroc-IT Programma\Barroc-IT.mdf;Integrated Security=True;Connect Timeout=30");
                SqlConnection con = dbh.Getcon();
                int accesNumber = Acces[i];
                string sql = "select * from Tbl_Customer where Company_Id = @Customer";
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                sqlCommand.Parameters.Add(new SqlParameter("Column", columns[accesNumber]));
                sqlCommand.Parameters.Add(new SqlParameter("Customer", selectedCustomerID));
                SqlDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    contentTextbox[i] = rdr.GetValue(accesNumber).ToString();
                }
                con.Close();
            }


            for (int i = 0; i < Acces.Length; i++)
            {
                int accesNumber = Acces[i];
                string name = columns[accesNumber];
                if (name.Contains("_"))
                {
                    name = name.Replace("_", " ");
                }
                else if (name.Contains("E_Mail"))
                {
                    name = name.Replace("_", "-");
                }
                tb[i] = new TextBox();
                tb[i].Location = new Point(tbX, tbY);
                tb[i].Text = contentTextbox[i];
                tb[i].Name = name;
                tb[i].KeyPress += new KeyPressEventHandler(Check_Input);
                this.Controls.Add(tb[i]);
                tbY += 30;
                if (tbY >= 500)
                {
                    tbX += 250;
                    tbY = 13;
                }

                if (tb[i].Name.Contains("Areacode"))
                {
                    tb[i].MaxLength = 2;
                }
                else if (tb[i].Name.Contains("Areanumber") || tb[i].Name.Contains("Ledger"))
                {
                    tb[i].MaxLength = 4;
                }
                else if (tb[i].Name.Contains("Initials") || tb[i].Name.Contains("Invoices"))
                {
                    tb[i].MaxLength = 3;
                }
                else if (tb[i].Name.Contains("Phone") || tb[i].Name.Contains("Fax"))
                {
                    tb[i].MaxLength = 10;
                }
                else if (tb[i].Name.Contains("Bank"))
                {
                    tb[i].MaxLength = 34;
                }
                else if (tb[i].Name.Contains("Amount"))
                {
                    tb[i].MaxLength = 21;
                }

                if (tb[i].Name.Contains("Number") || tb[i].Name.Contains("Invoices") || tb[i].Name.Contains("Account"))
                {
                    tb[i].ShortcutsEnabled = false;
                    tb[i].KeyPress += new KeyPressEventHandler(intBox_KeyPress);
                }

                if (tb[i].Name.Contains(" Amount"))
                {
                    tb[i].KeyPress += new KeyPressEventHandler(intBox_KeyPress);
                }
            }
        }

        private void Check_Input(Object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateCustomer();
                MessageBox.Show("You succesfully update the company " + customerSelected);
                this.Opacity = 0;
                this.ShowInTaskbar = false;
                custMenu.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update customer\nError: " + ex.Message);
            }
        }

        private void intBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || IsInputKey(Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            custMenu.Enabled = true;
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }

        private void frmEditBasicCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
