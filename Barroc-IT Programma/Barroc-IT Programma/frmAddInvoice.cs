﻿using System;
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
    public partial class frmAddInvoice : Form
    {
        DatabaseHandler dbh = new DatabaseHandler();
        AccountAcces ac = new AccountAcces();

        SqlCommand sqlCmd;
        SqlDataAdapter sqlDA = new SqlDataAdapter();
        DataTable sqlDT = new DataTable();
        Label[] lblInfo;
        TextBox[] txtbInfo;
        Button btnAdd;
        List<string> columns;
        List<string> values;
        int y = 55;
        int x = 15;
        int[] customerId;
        int custID;

        public frmAddInvoice()
        {
            InitializeComponent();
            CreateLayout();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dbh.CloseCon();
            custID = cmbBox.SelectedIndex + 1;
            if (custID == 0)
            {
                MessageBox.Show("Please select a customer");
            }
            else
            {
                InvoiceLayout();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isEmpty = false;

            for (int i = 0; i < txtbInfo.Length; i++)
            {
                if (txtbInfo[i].Text == "")
                {
                    MessageBox.Show("Field '" + lblInfo[i].Text + "' is required");
                    isEmpty = true;
                    break;
                }
            }

            if (isEmpty)
            {
            }
            else
            {
                ConvertValues();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = dbh.Getcon();

                    values.Skip(1);
                    cmd.CommandText = string.Format("INSERT INTO Tbl_Invoices ({0}) VALUES('{1}')", string.Join(", ", columns.Where(s => !string.IsNullOrEmpty(s))), string.Join("', '", values));
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (lblInfo[i].Text.Contains("Invoice"))
                        {
                            txtbInfo[i].ResetText();
                        }
                    }
                    MessageBox.Show("Invoice succesfully added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add Invoice\nError: " + ex.Message);
                }
            }
        }

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
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
            for (int i = 0; i < txtbInfo.Length; i++)
            {
                if (lblInfo[i].Text.Contains("Code"))
                {
                    txtbInfo[i].MaxLength = 2;
                    txtbInfo[i].Text = "21";
                }
                else
                {
                    txtbInfo[i].MaxLength = 255;
                    txtbInfo[i].KeyPress += new KeyPressEventHandler(txtBox_KeyPress);
                }

                if (lblInfo[i].Text.Contains("Code"))
                {
                    txtbInfo[i].ShortcutsEnabled = false;
                    txtbInfo[i].KeyPress += new KeyPressEventHandler(intBox_KeyPress);
                }

                if (lblInfo[i].Text.Contains("Id"))
                {
                    txtbInfo[i].Text = custID.ToString();
                    txtbInfo[i].ReadOnly = true;
                }
            }
        }
        private void DBConnect(string cmd)
        {
            try
            {
                dbh.OpenCon();
                sqlCmd = new SqlCommand(cmd, dbh.Getcon());
            }
            catch (Exception e)
            {
                MessageBox.Show("The following error has been encountered\n" + e.Message);
                dbh.CloseCon();
                Environment.Exit(0);
            }
        }
        private void FillColumns()
        {
            SqlDataReader sqlDR = sqlCmd.ExecuteReader();
            columns = new List<string>();
            for (int i = 1; i < sqlDR.FieldCount; i++)
            {
                columns.Add(sqlDR.GetName(i));
            }
            sqlDR.Close();
        }
        private void CreateLayout()
        {
            cmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            customerId = new int[255];
            DBConnect("SELECT Company_Id, Company_Name from Tbl_Customer");
            using (SqlDataReader cmbDR = sqlCmd.ExecuteReader())
            {
                int i = 0;
                while (cmbDR.Read())
                {
                    cmbBox.Items.Add(cmbDR.GetString(1));
                    customerId[i] = cmbDR.GetInt32(0);
                    i++;
                }
            }
        }
        private void InvoiceLayout()
        {
            DBConnect("SELECT * FROM Tbl_Invoices Where Customer_ID = " + custID);
            FillColumns();

            lblInfo = new Label[columns.Count];
            txtbInfo = new TextBox[columns.Count];
            for (int i = 0; i < lblInfo.Length; i++)
            {
                if (y >= 100)
                {
                    x += 250;
                    y = 85;
                }
                else
                { y += 30; }

                lblInfo[i] = new Label();
                lblInfo[i].Text = columns[i].ToString();
                if (lblInfo[i].Text.Contains("BTW"))
                { lblInfo[i].Text = lblInfo[i].Text.Replace("_", "-"); }
                else
                { lblInfo[i].Text = lblInfo[i].Text.Replace("_", " "); }
                lblInfo[i].Location = new Point(x, y);
                this.Controls.Add(lblInfo[i]);
                
                txtbInfo[i] = new TextBox();
                txtbInfo[i].Name = columns[i].ToString();
                txtbInfo[i].Location = new Point((x + 100), y);
                this.Controls.Add(txtbInfo[i]);
            }
            txtLimit();

            btnAdd = new Button();
            btnAdd.Text = "Add";
            btnAdd.Location = new Point((x + 100), y += 30);
            this.Controls.Add(btnAdd);
            this.AcceptButton = btnAdd;
            btnAdd.Click += new EventHandler(btnAdd_Click);
        }
        private void ConvertValues()
        {
            values = new List<string>();
            for (int i = 0; i < columns.Count; i++)
            {
                if (txtbInfo[i].Text != "")
                { values.Add(txtbInfo[i].Text); }
                else
                { values.Add(string.Empty); }
            }
        }
    }
}
