using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ThanhToanDAO
    {
        public static void TaoThanhToan(int hoaDonID, int nhanVienID, decimal SoTien, string PhuongThuc, DateTime NgayThanhToan, string TrangThai)
        {
            string query = $"INSERT INTO ThanhToan (HoaDonID, NhanVienID, SoTien, PhuongThuc, NgayThanhToan, TrangThai) " +
                           $"VALUES ({hoaDonID}, {nhanVienID}, {SoTien}, '{PhuongThuc}', '{NgayThanhToan.ToString("yyyy-MM-dd HH:mm:ss")}', '{TrangThai}')";
            DAO.DBConnect.ExecuteNonQuery(query);
        }
    }
}
