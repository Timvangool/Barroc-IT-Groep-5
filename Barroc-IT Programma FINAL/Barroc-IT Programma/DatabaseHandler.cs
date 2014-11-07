using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Data;


namespace Barroc_IT_Programma
{
    class DatabaseHandler
    {
        private SqlConnection con;

        public DatabaseHandler()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + Environment.CurrentDirectory + "\\Barroc-IT.mdf';Integrated Security=True;Connect Timeout=50");
        }

        public void TestConnection()
        {
            bool open = false;

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    open = true;
                }
                con.Close();
            }

            if (!open)
            {
                Application.Exit();
            }
        }

        public void OpenCon()
        {
            con.Open();
        }
        
        public void CloseCon()
        {
            con.Close();
        }

        public SqlConnection Getcon()
        {
            return con;
        }

        public System.Data.DataTable FillDT(string query)
        {
            TestConnection();
            OpenCon();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, Getcon());
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            CloseCon();

            return dt;
        }

    }
}