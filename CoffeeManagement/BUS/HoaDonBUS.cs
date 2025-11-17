using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagement.DAO;

namespace CoffeeManagement.BUS
{
    public class HoaDonBUS
    {
        public static DataTable ChiTietHoaDonID(int ID)
        {
            return HoaDonDAO.LaySanPhamCuaHoaDon(ID);
        }

        public static DataTable HoaDonID(int ID)
        {
            return HoaDonDAO.HoaDonTheoID(ID);
        }

        //tao hoa don
        public static void TaoHoaDon(int NhanVienID, int BanID, DateTime NgayLap, decimal TongTien, string trangthai)
        {
            HoaDonDAO.TaoHoaDon(NhanVienID, BanID, NgayLap, TongTien, trangthai);
        }
        //tao chi tiet hoa don
        public static void TaoChiTietHoaDon(int SoLuong, decimal DonGia, int HoaDonID, int SanPhamID)
        {
            HoaDonDAO.TaoChiTietHoaDon(SoLuong, DonGia, HoaDonID, SanPhamID);
        }
        //lay tat ca hoa don
        public static DataTable TatCaHoaDon()
        {
            return HoaDonDAO.LayTatCaHoaDon();
        }

    }
}
