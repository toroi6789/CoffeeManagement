using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ThanhToanBUS
    {
        public static void TaoThanhToan(int hoaDonID, int nhanVienID, decimal SoTien, string PhuongThuc, DateTime NgayThanhToan, string TrangThai)
        {
            DAO.ThanhToanDAO.TaoThanhToan(hoaDonID, nhanVienID, SoTien, PhuongThuc, NgayThanhToan, TrangThai);
        }
    }
}


