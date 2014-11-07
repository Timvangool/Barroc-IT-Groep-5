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
    public partial class Launcher : Form
    {
        public frmLogin frmLoginPanel;
        DatabaseHandler dhb;
        public Launcher()
        {
            InitializeComponent();
            dhb = new DatabaseHandler();
            frmLoginPanel = new frmLogin(this);
            frmLoginPanel.Show();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }

        public void ExitProgram()
        {
            dhb.CloseCon();
            this.Close();
        }
    }
}
