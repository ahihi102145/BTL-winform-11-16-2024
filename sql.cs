using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class sql : Form
    {
       

        public sql()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dethitracnghiem dt = new Dethitracnghiem();
            dt.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            
        }
    }
}
