using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekTariflerim
{
    public partial class FormTariflerim : Form
    {
        public FormTariflerim()
        {
            InitializeComponent();
        }

        public string from = "";

     
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVPVCU9; Initial Catalog=Db_YemekTarifleri; Integrated Security=True");
      

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
   
            Form1 form1 = new Form1();
            form1.Show();
        }


       

        private void FormTariflerim_Load(object sender, EventArgs e)
        {


            
            ListePanelim.Controls.Clear();
            conn.Open();

            string query = "SELECT t.TarifAdi, t.TarifID, t.HazirlamaSuresi, t.Talimatlar, t.Resim, k.KategoriAdi, " +
                   "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
                   " FROM TarifMalzeme tm " +
                   " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                   " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
                   "FROM Tarifler t " +
                   "JOIN Kategoriler k ON t.KategoriID = k.KategoriID";
            SqlCommand cmd = new SqlCommand( query, conn) ;
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                TarifListem arac = new TarifListem();
                arac.labelID.Text = reader["TarifID"].ToString();
                arac.labelTarifAdi.Text=reader["TarifAdi"].ToString();
                arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString();
                arac.labelKategori.Text = reader["KategoriAdi"].ToString() ;
                arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString()+" TL";
              

                if (from.Equals("mainpage"))
                {
                    arac.buttonSil.Visible = false;
                    arac.buttonGuncele.Visible = false;
                }

               
                string resimYolu = reader["Resim"].ToString();
                if (!string.IsNullOrEmpty(resimYolu))
                {
                    
                    arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                }
                else
                {
                   
                    arac.pictureBoxResim.Image = null;
                }

                ListePanelim.Controls.Add(arac);

            }
            conn.Close();
        }

       private void textTarifAdiArama_TextChanged(object sender, EventArgs e)
        {

            ListePanelim.Controls.Clear();
            conn.Open();
            string orderBy = "ASC";
            
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() == "Artan Sıraya Göre")
                {
                    orderBy = "ASC";
                }
                else if (comboBox1.SelectedItem.ToString() == "Azalan Sıraya Göre")
                {
                    orderBy = "DESC";
                }
            }

            SqlCommand search = new SqlCommand(
                                "SELECT t.TarifAdi, t.TarifID, t.HazirlamaSuresi, t.Talimatlar, t.Resim, k.KategoriAdi, " +
                                "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
                                " FROM TarifMalzeme tm " +
                                " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                                " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
                                "FROM Tarifler t " +
                                "JOIN Kategoriler k ON t.KategoriID = k.KategoriID " +
                                "WHERE t.TarifAdi LIKE @TarifAdi " +
                                "ORDER BY t.HazirlamaSuresi "+ orderBy, conn);

            search.Parameters.AddWithValue("@TarifAdi", "%" + textTarifAdiArama.Text + "%");

            SqlDataReader reader = search.ExecuteReader();
            while (reader.Read())
            {
                TarifListem arac = new TarifListem();
                arac.labelID.Text = reader["TarifID"].ToString();
                arac.labelTarifAdi.Text = reader["TarifAdi"].ToString();
                arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString() + " dakika";
                arac.labelKategori.Text = reader["KategoriAdi"].ToString();
                arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString() + " TL";
                if (from.Equals("mainpage"))
                {
                    arac.buttonSil.Visible = false;
                    arac.buttonGuncele.Visible = false;
                }
               
                string resimYolu = reader["Resim"].ToString();
                if (!string.IsNullOrEmpty(resimYolu))
                {
                   
                    arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                }
                else
                {

                    arac.pictureBoxResim.Image = null;
                }



                ListePanelim.Controls.Add(arac);

            }
            conn.Close ();

        }

        

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            textTarifAdiArama_TextChanged(sender, e);
            if (comboBox1.SelectedItem.ToString() == "Artan Sıraya Göre")
            {
                ListePanelim.Controls.Clear();
                conn.Open();
                SqlCommand search = new SqlCommand(
                     "SELECT t.TarifID, t.TarifAdi, t.HazirlamaSuresi, t.Resim, k.KategoriAdi, " +
                     "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
                     " FROM TarifMalzeme tm " +
                     " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                     " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
                     "FROM Tarifler t " +
                     "JOIN Kategoriler k ON t.KategoriID = k.KategoriID " +
                     "ORDER BY t.HazirlamaSuresi ASC", conn);




                SqlDataReader reader = search.ExecuteReader();
                while (reader.Read())
                {
                    TarifListem arac = new TarifListem();
                    arac.labelID.Text = reader["TarifID"].ToString();
                    arac.labelTarifAdi.Text = reader["TarifAdi"].ToString();
                    arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString() + " dakika";
                    arac.labelKategori.Text = reader["KategoriAdi"].ToString();
                    arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString() + " TL";
                    if (from.Equals("mainpage"))
                    {
                        arac.buttonSil.Visible = false;
                        arac.buttonGuncele.Visible = false;
                    }
                    // Resim yolunu al ve PictureBox'a ata
                    string resimYolu = reader["Resim"].ToString();
                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                        // Resmi yükle
                        arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                    }
                    else
                    {

                        arac.pictureBoxResim.Image = null;
                    }
                    ListePanelim.Controls.Add(arac);
                }
                conn.Close();


            }

            else if (comboBox1.SelectedItem.ToString() == "Azalan Sıraya Göre")
            {
                ListePanelim.Controls.Clear();
                conn.Open();
                SqlCommand search = new SqlCommand(
                   "SELECT t.TarifID, t.TarifAdi, t.HazirlamaSuresi, t.Resim, k.KategoriAdi, " +
                   "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
                   " FROM TarifMalzeme tm " +
                   " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                   " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
                   "FROM Tarifler t " +
                   "JOIN Kategoriler k ON t.KategoriID = k.KategoriID " +
                    "ORDER BY t.HazirlamaSuresi DESC", conn); 




                SqlDataReader reader = search.ExecuteReader();
                while (reader.Read())
                {
                    TarifListem arac = new TarifListem();
                    arac.labelID.Text = reader["TarifID"].ToString();
                    arac.labelTarifAdi.Text = reader["TarifAdi"].ToString();
                    arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString() + " dakika";
                    arac.labelKategori.Text = reader["KategoriAdi"].ToString();
                    arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString() + " TL";
                    if (from.Equals("mainpage"))
                    {
                        arac.buttonSil.Visible = false;
                        arac.buttonGuncele.Visible = false;
                    }
                   
                    string resimYolu = reader["Resim"].ToString();
                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                    
                        arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                    }
                    else
                    {

                        arac.pictureBoxResim.Image = null;
                    }
                    ListePanelim.Controls.Add(arac);
                }
                conn.Close();


            }
        }

        private void comboBoxKAtegori_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListePanelim.Controls.Clear();
            conn.Open();
            // Seçilen kategori adı
            string selectedCategory = comboBoxKAtegori.SelectedItem.ToString(); // Eğer kategoriler listesinde kategori adı varsa
            SqlCommand search = new SqlCommand(
               "SELECT t.TarifID, t.TarifAdi, t.HazirlamaSuresi, t.Resim, k.KategoriAdi, " +
               "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
               " FROM TarifMalzeme tm " +
               " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
               " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
               "FROM Tarifler t " +
               "JOIN Kategoriler k ON t.KategoriID = k.KategoriID " +
               "WHERE k.KategoriAdi = @KategoriAdi", conn);


            search.Parameters.AddWithValue("@KategoriAdi", selectedCategory); 

            SqlDataReader reader = search.ExecuteReader();
            while (reader.Read())
            {
                TarifListem arac = new TarifListem();
                arac.labelID.Text = reader["TarifID"].ToString();
                arac.labelTarifAdi.Text = reader["TarifAdi"].ToString();
                arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString() + " dakika";
                arac.labelKategori.Text = reader["KategoriAdi"].ToString();
                arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString() + " TL";
                if (from.Equals("mainpage"))
                {
                    arac.buttonSil.Visible = false;
                    arac.buttonGuncele.Visible = false;
                }
               
                string resimYolu = reader["Resim"].ToString();
                if (!string.IsNullOrEmpty(resimYolu))
                {
                   
                    arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                }
                else
                {

                    arac.pictureBoxResim.Image = null;
                }
                ListePanelim.Controls.Add(arac);

            }
            conn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Azalan Sıraya Göre")
            {
                ListePanelim.Controls.Clear();
                conn.Open();
                SqlCommand search = new SqlCommand(
                    "SELECT t.TarifID, t.TarifAdi, t.HazirlamaSuresi, t.Resim, k.KategoriAdi, " +
                    "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
                    " FROM TarifMalzeme tm " +
                    " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                    " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
                    "FROM Tarifler t " +
                    "JOIN Kategoriler k ON t.KategoriID = k.KategoriID " +
                    "ORDER BY ToplamMaliyet DESC", conn); 
                SqlDataReader reader = search.ExecuteReader();
                while (reader.Read())
                {
                    TarifListem arac = new TarifListem();
                    arac.labelID.Text = reader["TarifID"].ToString();
                    arac.labelTarifAdi.Text = reader["TarifAdi"].ToString();
                    arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString() + " dakika";
                    arac.labelKategori.Text = reader["KategoriAdi"].ToString();
                    arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString() + " TL";
                    if (from.Equals("mainpage"))
                    {
                        arac.buttonSil.Visible = false;
                        arac.buttonGuncele.Visible = false;
                    }

                   
                    string resimYolu = reader["Resim"].ToString();
                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                        
                        arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                    }
                    else
                    {
                        arac.pictureBoxResim.Image = null;
                    }
                    ListePanelim.Controls.Add(arac);
                }
                conn.Close();
            }
            else if (comboBox2.SelectedItem.ToString() == "Artan Sıraya Göre")
            {
                ListePanelim.Controls.Clear();
                conn.Open();
                SqlCommand search = new SqlCommand(
                    "SELECT t.TarifID, t.TarifAdi, t.HazirlamaSuresi, t.Resim, k.KategoriAdi, " +
                    "(SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat) " +
                    " FROM TarifMalzeme tm " +
                    " JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                    " WHERE tm.TarifID = t.TarifID) AS ToplamMaliyet " +
                    "FROM Tarifler t " +
                    "JOIN Kategoriler k ON t.KategoriID = k.KategoriID " +
                    "ORDER BY ToplamMaliyet ASC", conn); 
                SqlDataReader reader = search.ExecuteReader();
                while (reader.Read())
                {
                    TarifListem arac = new TarifListem();
                    arac.labelID.Text = reader["TarifID"].ToString();
                    arac.labelTarifAdi.Text = reader["TarifAdi"].ToString();
                    arac.labelHazirlaSure.Text = reader["HazirlamaSuresi"].ToString() + " dakika";
                    arac.labelKategori.Text = reader["KategoriAdi"].ToString();
                    arac.labelMaliyet.Text = reader["ToplamMaliyet"].ToString() + " TL";
                    if (from.Equals("mainpage"))
                    {
                        arac.buttonSil.Visible = false;
                        arac.buttonGuncele.Visible = false;
                    }

                  
                    string resimYolu = reader["Resim"].ToString();
                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                        
                        arac.pictureBoxResim.Image = Image.FromFile(resimYolu);
                    }
                    else
                    {
                        arac.pictureBoxResim.Image = null;
                    }
                    ListePanelim.Controls.Add(arac);
                }
                conn.Close();
            }
        }

       
    }
}
