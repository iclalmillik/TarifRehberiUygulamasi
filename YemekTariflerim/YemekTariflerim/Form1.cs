using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekTariflerim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          

        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVPVCU9; Initial Catalog=Db_YemekTarifleri; Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       


      

        private void buttonMenu_Click(object sender, EventArgs e)
        {

            FormMenu formMenu = new FormMenu();

            formMenu.Show();

            this.Hide();

           
        }

        private void buttonTarifler_Click(object sender, EventArgs e)
        {
            FormTariflerim formTariflerim = new FormTariflerim();
            formTariflerim.from = "mainpage";
           
          
            formTariflerim.Show();
            this.Hide();
           


        }
    }
}
