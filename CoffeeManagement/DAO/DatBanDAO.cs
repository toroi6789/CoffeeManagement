using CoffeeManagement.DTO;
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

        public static DatBanDTO GetDatBanByID(int datBanID)
        {
            string query = $@"
        SELECT DatBanID, BanID, Ngay, GioBatDau, GioKetThuc
        FROM DatBan
        WHERE DatBanID = {datBanID};
    ";

            DataTable dt = DBConnect.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new DatBanDTO(
                Convert.ToInt32(row["DatBanID"]),
                Convert.ToInt32(row["BanID"]),
                Convert.ToDateTime(row["Ngay"]),
                (TimeSpan)row["GioBatDau"],
                (TimeSpan)row["GioKetThuc"]
            );
        }
        public static bool UpdateDatBanByID(DatBanDTO dto)
        {
            string query = $@"
                UPDATE DatBan
                SET 
                    BanID = {dto.BanID},
                    Ngay = '{dto.Ngay:yyyy-MM-dd}',
                    GioBatDau = '{dto.GioBatDau}',
                    GioKetThuc = '{dto.GioKetThuc}'
                WHERE DatBanID = {dto.DatBanID};
            ";

            DataTable rs = DBConnect.ExecuteQuery(query);
            return rs.Rows.Count > 0;
        }

    }
}
