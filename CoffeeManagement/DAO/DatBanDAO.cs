using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CoffeeManagement.DAO
{
    public class DatBanDAO
    {
        public static void DatBan(int banID, DateTime ngay, TimeSpan gioBD, TimeSpan gioKT)
        {
            string query =
                $"INSERT INTO DatBan (BanID, Ngay, GioBatDau, GioKetThuc) " +
                $"VALUES ({banID}, '{ngay:yyyy-MM-dd}', '{gioBD}', '{gioKT}');";

            DBConnect.ExecuteNonQuery(query);
        }

        public static bool KiemTraTrung(int banID, DateTime ngay, TimeSpan gioBD, TimeSpan gioKT)
        {
            string query =
                $"SELECT * FROM DatBan " +
                $"WHERE BanID = {banID} AND Ngay = '{ngay:yyyy-MM-dd}' " +
                $"AND ( " +
                $"      (GioBatDau <= '{gioBD}' AND GioKetThuc > '{gioBD}') OR " +
                $"      (GioBatDau < '{gioKT}' AND GioKetThuc >= '{gioKT}') OR " +
                $"      ('{gioBD}' <= GioBatDau AND '{gioKT}' >= GioKetThuc) " +
                $"    );";

            DataTable dt = DBConnect.ExecuteQuery(query);
            return dt.Rows.Count > 0;
        }
        public static DataTable LayDatBanTheoBan(int banID)
        {
            string q = $@"
        SELECT DatBanID, BanID, Ngay, GioBatDau, GioKetThuc
        FROM DatBan
        WHERE BanID = {banID}
        AND Ngay >= CURDATE()
        ORDER BY Ngay DESC, GioBatDau DESC;
    ";

            return DBConnect.ExecuteQuery(q);
        }


    }
}
