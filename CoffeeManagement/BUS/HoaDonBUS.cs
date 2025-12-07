using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;

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
        public static void TaoHoaDon(int NhanVienID, int BanID, DateTime NgayLap, decimal TongTien, string trangthai, int IDKM)
        {
            HoaDonDAO.TaoHoaDon(NhanVienID, BanID, NgayLap, TongTien, trangthai, IDKM);
        }
        //tao chi tiet hoa don
        public static void TaoChiTietHoaDon(int SoLuong, decimal DonGia, int HoaDonID, int SanPhamID, int ThanhTien)
        {
            HoaDonDAO.TaoChiTietHoaDon(SoLuong, DonGia, HoaDonID, SanPhamID, ThanhTien);


        }
        //lay tat ca hoa don
        public static DataTable TatCaHoaDon()
        {
            return HoaDonDAO.LayTatCaHoaDon();
        }
        // sua trang thai hoa don
        public static void SuaTrangThai(int ID, string trangthai)
        {
            HoaDonDAO.SuaTrangThai(ID, trangthai);
        }
        // Cap nhat phuong thuc thanh toan
        public static void Capnhatphuongthuc(int ID, string PhuongThuc)
        {
            HoaDonDAO.Capnhatphuongthuc(ID, PhuongThuc);
        }
        // xoa hoa don
        public static void XoaHoaDon(int ID)
        {
            HoaDonDAO.XoaHoaDon(ID);
        }
        // tim kiem hoa don theo ngay
        public static DataTable TimKiemHoaDonTheoNgay(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            return HoaDonDAO.TimKiemHoaDonTheoNgay(NgayBatDau, NgayKetThuc);
        }
    }
}
