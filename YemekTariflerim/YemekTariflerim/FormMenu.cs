using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekTariflerim
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
            Form1 form1 = new Form1();
            form1.Show();
        }

      

        private void btnTarifSil_Click_1(object sender, EventArgs e)
        {
            
            FormTariflerim formTariflerim = new FormTariflerim();

            formTariflerim.Show();

          
            this.Hide();

        }

        private void btnTarifGuncelle_Click(object sender, EventArgs e)
        {
          
            FormTariflerim formTariflerim = new FormTariflerim();
            formTariflerim.from = "update";

            
            formTariflerim.Show();

            this.Hide();
        }

        private void btnTarifEkle_Click(object sender, EventArgs e)
        {

         
            TarifEkleme tarifEkleme = new TarifEkleme();

          
            tarifEkleme.Show();

            
            this.Hide();
        }
    }
}
