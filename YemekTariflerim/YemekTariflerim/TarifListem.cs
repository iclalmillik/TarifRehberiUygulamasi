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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YemekTariflerim
{
    public partial class TarifListem : UserControl
    {
        public TarifListem()
        {
            InitializeComponent();
           

        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVPVCU9; Initial Catalog=Db_YemekTarifleri; Integrated Security=True");


       

       

        private void linkLabelTalimat_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            conn.Open();

            string query = "SELECT Talimatlar,TarifAdi FROM Tarifler WHERE TarifID = @tarifID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@tarifID", labelID.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            


            if (reader.Read())
            {
                MessageBox.Show("Talimatlar:\n " + reader["Talimatlar"].ToString(), reader["TarifAdi"].ToString());
                
              

            }
            else
            {
                
                MessageBox.Show("Talimat bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            conn.Close();
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            conn.Open();
           
            string deleteTarifMalzemeQuery = "DELETE FROM TarifMalzeme WHERE TarifID = @tarifID";
            SqlCommand deleteTarifMalzeme = new SqlCommand(deleteTarifMalzemeQuery, conn);
            deleteTarifMalzeme.Parameters.AddWithValue("@tarifID", labelID.Text);
            deleteTarifMalzeme.ExecuteNonQuery();

            string deleteTarifQuery = "DELETE FROM Tarifler WHERE TarifID = @tarifID";
            SqlCommand deleteTarif = new SqlCommand(deleteTarifQuery, conn);
            deleteTarif.Parameters.AddWithValue("@tarifID", labelID.Text);
            deleteTarif.ExecuteNonQuery();

          



            conn.Close();
            this.Hide();
            MessageBox.Show(labelTarifAdi.Text + " adlı tarife ait bilgiler başarıyla silindi.");
        }

        private void buttonGuncele_Click(object sender, EventArgs e)
        {

            string query = "SELECT * FROM Tarifler WHERE TarifID = @TarifID";

            conn.Open();

            SqlCommand readTarifQuery = new SqlCommand(query, conn);
            readTarifQuery.Parameters.AddWithValue("@TarifID", labelID.Text);

            SqlDataReader reader = readTarifQuery.ExecuteReader();

            string talimatlar = "";

            while (reader.Read())
            {
                talimatlar = reader["Talimatlar"].ToString();
            } 

            reader.Close();
            conn.Close();

            // Label'lardan verileri al


            string tarifAdi = labelTarifAdi.Text;
            string kategoriAdi = labelKategori.Text;
            string hazirlamaSuresi = labelHazirlaSure.Text;
            string maliyet = labelMaliyet.Text;
            string tarifID = labelID.Text;
            string resim=pictureBoxResim.Text;




        
            TarifGuncelle tarifGuncelle = new TarifGuncelle(tarifAdi,kategoriAdi,hazirlamaSuresi,maliyet,talimatlar,tarifID,resim);
            
          
          
            tarifGuncelle.Show();
            conn.Close();
        }

       
    }
}
