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
    public partial class frmAddEditUser : Form
    {
        frmAdmin frmadmin = new frmAdmin();
        public frmAddEditUser()
        {
            InitializeComponent();
            if (frmadmin.AddEdit == "Add")
            {
                this.Text = "Add";
            }
        }
    }
}
