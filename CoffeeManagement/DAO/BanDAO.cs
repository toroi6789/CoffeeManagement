using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class BanDAO
    {
        public static void CapNhatTrangThaiBan(int BanID, string TrangThai)
        {
            string query = $"UPDATE ban SET TrangThai = '{TrangThai}' WHERE BanID = {BanID};";
            DBConnect.ExecuteNonQuery(query);
        }
        public static DataTable LayTatCaBanHoatDong()
        {
            string query = "SELECT BanID,TenBan FROM ban WHERE TrangThai = 'Trống';";
            return DBConnect.ExecuteQuery(query);
        }

        // Lấy tất cả các bàn
        public static DataTable LayTatCaBan()
        {
            string query = "SELECT BanID, TenBan, SucChua, TrangThai FROM Ban;";
            return DBConnect.ExecuteQuery(query);
        }

        // Thêm bàn
        public static void ThemBan(string tenBan, int sucChua, string trangThai)
        {
            string query =
                $"INSERT INTO Ban (TenBan, SucChua, TrangThai) VALUES ('{tenBan}', {sucChua}, '{trangThai}');";
            DBConnect.ExecuteNonQuery(query);
        }

        // Cập nhật bàn
        public static void SuaBan(int banID, string tenBan, int sucChua, string trangThai)
        {
            string query =
                $"UPDATE Ban SET TenBan = '{tenBan}', SucChua = {sucChua}, TrangThai = '{trangThai}' WHERE BanID = {banID};";
            DBConnect.ExecuteNonQuery(query);
        }

        // Xóa bàn
        public static void XoaBan(int banID)
        {
            string query = $"DELETE FROM Ban WHERE BanID = {banID};";
            DBConnect.ExecuteNonQuery(query);
        }

        // Tìm kiếm bàn theo tên hoặc trạng thái
        public static DataTable TimKiemBan(string keyword)
        {
            string query =
                $"SELECT BanID, TenBan, SucChua, TrangThai FROM Ban WHERE TenBan LIKE '%{keyword}%' OR TrangThai LIKE '%{keyword}%';";
            return DBConnect.ExecuteQuery(query);
        }

        // Reset AutoIncrement
        public static void ResetAutoIncrement()
        {
            string query = "ALTER TABLE Ban AUTO_INCREMENT = 1;";
            DBConnect.ExecuteNonQuery(query);
        }

        // Kiểm tra xem bàn có người hay không
        public static bool IsBanAvailable(int banID)
        {
            string query = $"SELECT TrangThai FROM Ban WHERE BanID = {banID};";
            DataTable dt = DBConnect.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                string trangThai = dt.Rows[0]["TrangThai"].ToString();
                // Kiểm tra trạng thái, nếu có người thì không thể đặt
                if (trangThai == "Có người")
                {
                    return false; // Bàn không có sẵn
                }
            }
            return true; // Bàn có sẵn
        }
    }
}
