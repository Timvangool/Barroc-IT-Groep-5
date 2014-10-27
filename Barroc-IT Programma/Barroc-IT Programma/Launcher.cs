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
        public Launcher()
        {
            InitializeComponent();
            
            frmLogin frmlogin = new frmLogin();
            frmlogin.Show();
            this.Opacity = 0;
            this.ShowInTaskbar = false;
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ExitProgram())
            {
                this.Close();
            }
            else
            {
                e.Cancel = false;
            }
        }

        private bool ExitProgram()
        {
            bool close = false;
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                close = true;
            }
            else
            {
                close = false;
            }
            return close;
        }
    }
}
