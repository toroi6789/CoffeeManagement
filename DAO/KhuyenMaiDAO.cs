using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class KhuyenMaiDAO
    {
        // lay tat ca KM
        public static DataTable GetAllKM()
        {
            String query = $"SELECT * FROM coffeemanagement.khuyenmai;";
            return DBConnect.ExecuteQuery(query);
        }
        // lay KM theo ID
        public static DataTable GetKM_ID(int ID)
        {
            String query = $"SELECT * FROM coffeemanagement.khuyenmai where KhuyenMaiID = '{ID}';";
            return DBConnect.ExecuteQuery(query);
        }
        // them KM
        public static void InsertKM(string TenKM, string LoaiKhuyenMai, string MoTa, Decimal giatri, DateTime NgayBatDau, DateTime NgayKetThuc, string TrangThai)
        {
            String query = $"INSERT INTO coffeemanagement.khuyenmai (TenKhuyenMai, LoaiKhuyenMai, MoTa, GiaTri, NgayBatDau, NgayKetThuc, TrangThai) " +
                $"VALUES ('{TenKM}', '{LoaiKhuyenMai}', '{MoTa}', '{giatri}', '{NgayBatDau.ToString("yyyy-MM-dd")}', '{NgayKetThuc.ToString("yyyy-MM-dd")}', '{TrangThai}');";
            DBConnect.ExecuteNonQuery(query);
        }

        // sua KM
        public static void UpdateKM(int KhuyenMaiID, string TenKM, string LoaiKhuyenMai, string MoTa, Decimal giatri, DateTime NgayBatDau, DateTime NgayKetThuc, string TrangThai)
        {
            String query = $"UPDATE coffeemanagement.khuyenmai " +
                $"SET TenKhuyenMai = '{TenKM}', LoaiKhuyenMai = '{LoaiKhuyenMai}', MoTa = '{MoTa}', GiaTri = '{giatri}', NgayBatDau = '{NgayBatDau.ToString("yyyy-MM-dd")}', NgayKetThuc = '{NgayKetThuc.ToString("yyyy-MM-dd")}', TrangThai = '{TrangThai}' " +
                $"WHERE KhuyenMaiID = '{KhuyenMaiID}';";
            DBConnect.ExecuteNonQuery(query);
        }
        // xoa KM
        public static void DeleteKM(int KhuyenMaiID)
        {
            String query = $"DELETE FROM coffeemanagement.khuyenmai WHERE KhuyenMaiID = '{KhuyenMaiID}';";
            DBConnect.ExecuteNonQuery(query);
        }
    }
}
