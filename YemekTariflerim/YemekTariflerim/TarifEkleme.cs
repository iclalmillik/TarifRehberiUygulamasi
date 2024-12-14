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
    public partial class TarifEkleme : Form
    {
        public TarifEkleme()
        {
            InitializeComponent();
            LoadMalzemeler();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVPVCU9; Initial Catalog=Db_YemekTarifleri; Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
         
            TarifListem tarifListem = new TarifListem();
            tarifListem.Show();
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            // İlk olarak girilen kategori adına göre KategoriID'yi bul
            string kategoriAdi = textKatgEkle.Text; // Textbox'tan kategori adını al
            int kategoriID = 0;

            // Kategoriler tablosundan KategoriID'yi alacak sorgu
            string queryKategoriID = "SELECT KategoriID FROM Kategoriler WHERE KategoriAdi = @kategoriAdi";

            SqlCommand cmdKategoriID = new SqlCommand(queryKategoriID, conn);
            cmdKategoriID.Parameters.AddWithValue("@kategoriAdi", kategoriAdi);

            conn.Open();
            SqlDataReader reader = cmdKategoriID.ExecuteReader();

            if (reader.Read()) 
            {
                kategoriID = Convert.ToInt32(reader["KategoriID"]); // KategoriID'yi al
            }
            conn.Close();

            // Eğer kategori ID bulunduysa tarifler tablosuna ekleme işlemi yapılacak
            if (kategoriID > 0)
            {
                string queryTarifEkle = "INSERT INTO Tarifler(TarifAdi, HazirlamaSuresi, Talimatlar, KategoriID,Resim) VALUES (@p1, @p2, @p3, @kategoriID,@resim)";

                SqlCommand cmdTarifEkle = new SqlCommand(queryTarifEkle, conn);
                cmdTarifEkle.Parameters.AddWithValue("@p1", textAdEkle.Text);
                cmdTarifEkle.Parameters.AddWithValue("@p2", textSureEkle.Text);
                cmdTarifEkle.Parameters.AddWithValue("@p3", textTalimatEkle.Text);
                cmdTarifEkle.Parameters.AddWithValue("@kategoriID", kategoriID); // Bulunan kategori ID'yi ekliyoruz
                cmdTarifEkle.Parameters.AddWithValue("@resim", resimYolu);

                conn.Open();
                cmdTarifEkle.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Tarif başarıyla eklendi!");
            }
            else
            {
                MessageBox.Show("Girilen kategori bulunamadı. Lütfen geçerli bir kategori adı giriniz.");
            }
        }

        public string resimYolu = "";
        private void buttonResimYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "RESİM SEÇME EKRANI";
            ofd.Filter = "PNG | *.png | JPG | *.jpg | JPEG | *.jpeg | All Files | *.*";
            ofd.FilterIndex = 4;
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                pictureBoxResim.Image = new Bitmap(ofd.FileName);
                resimYolu = (ofd.FileName.ToString());
            }
        }

        private void comboBoxMalzeme_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadMalzemeler()
        {
            conn.Open();
            string query = "SELECT MalzemeAdi,MalzemeBirim FROM Malzemeler";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            comboBoxMalzeme.Items.Clear();
            while (reader.Read())
            {
                comboBoxMalzeme.Items.Add(reader["MalzemeAdi"].ToString() + " -> " + reader["MalzemeBirim"].ToString());


            }
            conn.Close();

        }



        private void buttonmalzemeEkle_Click(object sender, EventArgs e)
        {

            if (comboBoxMalzeme.SelectedItem != null)
            {
                string selectedMalzeme = comboBoxMalzeme.SelectedItem.ToString();
                conn.Open();
                string checkQuery = "select ToplamMiktar from Malzemeler Where MalzemeAdi=@malzemeAdi";
                SqlCommand command = new SqlCommand(checkQuery, conn);
                command.Parameters.AddWithValue("@malzemeAdi", selectedMalzeme);
                object result = command.ExecuteScalar();
               
                
                    int mevcutmiktar = Convert.ToInt32(result);
                    mevcutmiktar += 1;
                    string updateQuery = "Update Malzemeler Set ToplamMiktar=@yenimiktar where MalzemeAdi=@malzemeAdi";
                    SqlCommand updateComm = new SqlCommand(updateQuery, conn);
                    updateComm.Parameters.AddWithValue("@yenimiktar", mevcutmiktar);
                    updateComm.Parameters.AddWithValue("@malzemeAdi", selectedMalzeme);
                    MessageBox.Show("Malzeme mevcut. Miktar 1 artırıldı.");


                

            }

        }

        private void buttonYmalzemeEkle_Click(object sender, EventArgs e)
        {

            // TextBox'tan malzeme adını al
            string malzemeAdi = textBoxYeniMAlzeme.Text.Trim(); // textBoxMalzemeAdi, kendi TextBox kontrol adınız

            if (string.IsNullOrEmpty(malzemeAdi))
            {
                MessageBox.Show("Lütfen bir malzeme adı girin.");
                return; // Malzeme adı boş ise işlemi sonlandır
            }

            try
            {
                conn.Open();

                // Malzeme veritabanında mevcut mu kontrol et
                string checkQuery = "SELECT ToplamMiktar FROM Malzemeler WHERE MalzemeAdi = @malzemeAdi";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);
                    object result = checkCommand.ExecuteScalar();

                    if (result != null)
                    {
                        // Malzeme mevcut, miktarını artır
                        int mevcutMiktar = Convert.ToInt32(result);
                        mevcutMiktar += 1; 

                        string updateQuery = "UPDATE Malzemeler SET ToplamMiktar = @yeniMiktar WHERE MalzemeAdi = @malzemeAdi";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, conn))
                        {
                            updateCommand.Parameters.AddWithValue("@yeniMiktar", mevcutMiktar);
                            updateCommand.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);
                            updateCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Malzeme mevcut. Miktar 1 artırıldı.");
                    }
                    else
                    {
                        // Malzeme mevcut değil, yeni bir malzeme ekle
                        string insertQuery = "INSERT INTO Malzemeler (MalzemeAdi, ToplamMiktar) VALUES (@malzemeAdi, @miktar)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                        {
                            insertCommand.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);
                            insertCommand.Parameters.AddWithValue("@miktar", 1); 
                            insertCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Yeni malzeme eklendi.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}