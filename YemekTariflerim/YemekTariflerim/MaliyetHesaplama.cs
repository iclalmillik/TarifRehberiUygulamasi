using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemekTariflerim
{
    public class MaliyetHesaplama
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVPVCU9; Initial Catalog=Db_YemekTarifleri; Integrated Security=True");
        private void CalculateRecipeCost()
        {

            conn.Open();
            SqlCommand command = new SqlCommand(
                "SELECT t.TarifID, t.TarifAdi, SUM(tm.MalzemeMiktar * m.BirimFiyat) AS ToplamMaliyet " +
                "FROM Tarifler t " +
                "JOIN TarifMalzeme tm ON t.TarifID = tm.TarifID " +
                "JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID " +
                "GROUP BY t.TarifID, t.TarifAdi",
                conn);


            conn.Close();


        }
    }
}