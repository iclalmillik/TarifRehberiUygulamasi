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
    public partial class TarifGuncelle : Form
    {
        public TarifGuncelle(string tarifAdi, string kategoriAdi, string hazirlamaSuresi, string maliyet, string talimatlar, string tarifID, string resim)
        {
            InitializeComponent();
            // Verileri güncelleme formuna aktar
            textBoxAd.Text = tarifAdi;
            textBoxKatAd.Text = kategoriAdi;
            textBoxSure.Text = hazirlamaSuresi;
            textBoxMaliyet.Text = maliyet;
            textBoxTalimat.Text = talimatlar;
            labelTarifId.Text = tarifID;
            pBResimGüncelle.Text = resim;
            
        }

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVPVCU9; Initial Catalog=Db_YemekTarifleri; Integrated Security=True");

        

       private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
          
            TarifListem tarifListem = new TarifListem();
            tarifListem.Show();
        }

        private void buttonOnay_Click(object sender, EventArgs e)
        {
            // Kategori ID'yi alabilmek için kategori adını al
            string kategoriAdi = textBoxKatAd.Text; 
            int kategoriID = 0;

         
            string queryKategoriID = "SELECT KategoriID FROM Kategoriler WHERE KategoriAdi = @kategoriAdi";

            SqlCommand cmdKategoriID = new SqlCommand(queryKategoriID, conn);
            cmdKategoriID.Parameters.AddWithValue("@kategoriAdi", kategoriAdi);

            conn.Open();
            SqlDataReader reader = cmdKategoriID.ExecuteReader();

            if (reader.Read()) 
            {
                kategoriID = Convert.ToInt32(reader["KategoriID"]); 
            }
            conn.Close();

            
            if (kategoriID > 0)
            {
                
                int tarifID;
                if (!int.TryParse(labelTarifId.Text, out tarifID))
                {
                    MessageBox.Show("Geçerli bir tarif ID'si giriniz.");
                    return; 
                }

              
                string queryGuncelle = "UPDATE Tarifler SET TarifAdi = @p1, HazirlamaSuresi = @p2, Talimatlar = @p3, KategoriID = @kategoriID ,Resim=@resim WHERE TarifID = @tarifID";

                SqlCommand cmdGuncelle = new SqlCommand(queryGuncelle, conn);
                cmdGuncelle.Parameters.AddWithValue("@p1", textBoxAd.Text);
                cmdGuncelle.Parameters.AddWithValue("@p2", textBoxSure.Text);
                cmdGuncelle.Parameters.AddWithValue("@p3", textBoxTalimat.Text);
                cmdGuncelle.Parameters.AddWithValue("@kategoriID", kategoriID);
                cmdGuncelle.Parameters.AddWithValue("@tarifID", tarifID); // Mevcut tarifin ID'sini değiştirilmeden kullan

                

              cmdGuncelle.Parameters.AddWithValue("@resim",resimYolu);

                conn.Open();
                cmdGuncelle.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Tarif başarıyla güncellendi!");

              
            }
            else
            {
                MessageBox.Show("Girilen kategori bulunamadı. Lütfen geçerli bir kategori adı giriniz.");
            }







        }


      

        public string resimYolu = "";
        private void btnResimGüncelle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "RESİM SEÇME EKRANI";
            ofd.Filter = "PNG | *.png | JPG | *.jpg | JPEG | *.jpeg | All Files | *.*";
            ofd.FilterIndex = 4;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                

                pBResimGüncelle.Image = new Bitmap(ofd.FileName);
                resimYolu = (ofd.FileName.ToString());
            }
        }
        private void ListeleMalzemeler(int tarifID)
        {
            StringBuilder sb = new StringBuilder();


            try
            {
                conn.Open();

              
                string query = @"SELECT m.MalzemeAdi, tm.MalzemeMiktar, m.MalzemeBirim
                             FROM TarifMalzeme tm
                             JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID
                             WHERE tm.TarifID = @TarifID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    
                    command.Parameters.AddWithValue("@TarifID", tarifID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            string malzemeAdi = reader["MalzemeAdi"].ToString();
                            string malzemeMiktar = reader["MalzemeMiktar"].ToString();
                            string malzemeBirim = reader["MalzemeBirim"].ToString();

                            // Her malzemeyi ayrı satırda yazdırma
                            sb.AppendLine($"{malzemeAdi}: {malzemeMiktar} {malzemeBirim}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                sb.AppendLine("Hata: " + ex.Message);
            }
            finally
            {
                
                conn.Close();
            }

          
            richBoxMalzemeler.Text = sb.ToString();
        }

        private void TarifGuncelle_Load(object sender, EventArgs e)
        {

            // labelTarifId’den tarifID’yi al ve int’e çevir
            int tarifID;
            if (int.TryParse(labelTarifId.Text, out tarifID))
            {
               
                ListeleMalzemeler(tarifID);
            }
            else
            {
                MessageBox.Show("Geçersiz Tarif ID. Lütfen geçerli bir ID ile formu açın.");
            }
        }

      

      

      
    }
}

