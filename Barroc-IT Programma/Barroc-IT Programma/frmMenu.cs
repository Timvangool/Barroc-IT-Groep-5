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
    public partial class frmMenu : Form
    {
        private DatabaseHandler dbh;
        public frmMenu()
        {
            InitializeComponent();
            dbh = new DatabaseHandler();
            permissions();

            
        }
        public void permissions()
        {
            bool admin = false;
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Table] WHERE Permisions = 4", dbh.Getcon()))
            {
                admin = (int)cmd.ExecuteScalar() > 0;
            }
          
            if (admin)
            {
                
            }
            else
            {
            }
        }
    }
}
