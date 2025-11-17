using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeManagement.DAO
{
    public class HoaDonDAO
    {
        // lấy hóa đơn theo ID
        public static DataTable HoaDonTheoID(int HoaDonID)
        {
            string query = $"SELECT * FROM coffeemanagement.hoadon where HoaDonID={HoaDonID};";
            return DBConnect.ExecuteQuery(query);
        }

        // lấy tất cả chi tiết hóa đơn
        public static DataTable ChiTietHoaDon()
        {
            string query = "SELECT * FROM chitiethoadon;";
            return DBConnect.ExecuteQuery(query);
        }
        // lấy chi tiết hóa đơn theo ID
        public static DataTable ChiTietHoaDonTheoID(int HoaDonID)
        {
            string query = $"SELECT * FROM chitiethoadon WHERE HoaDonID = {HoaDonID};";
            return DBConnect.ExecuteQuery(query);
        }
        // tạo hóa đơn mới
        public static void TaoHoaDon(int NhanVienID, int BanID, DateTime NgayLap, decimal TongTien, string trangthai)
        {
            string query = $"INSERT INTO hoadon (NhanVienID, BanID, NgayKhoiTao, TongTien, TrangThai) VALUES ({NhanVienID}, {BanID}, '{NgayLap.ToString("yyyy-MM-dd HH:mm:ss")}', {TongTien}, '{trangthai}');";
            DBConnect.ExecuteNonQuery(query);
        }
        // tạo chi tiết hóa đơn mới
        public static void TaoChiTietHoaDon(int SoLuong, decimal DonGia, int HoaDonID, int SanPhamID)
        {
            string query = $"INSERT INTO chitiethoadon (SoLuong, DonGia, HoaDonID, SanPhamID) VALUES ({SoLuong}, {DonGia}, {HoaDonID}, {SanPhamID});";
            DBConnect.ExecuteNonQuery(query);
        }
        //tạo Thanh toán
        public void ThanhToan(int ThanhToanID, decimal SoTienThanhToan, string PhuongThuc, DateTime NgayThanhToan, string TrangThai, int HoaDonID, int NhanVienID)
        {
            string query = $"ISERT INTO thanhtoan (ThanhToanID, SoTien, PhuongThuc, NgayThanhToan, TrangThai, HoaDonID, NhanVienID) VALUES ({ThanhToanID}, {SoTienThanhToan}, {PhuongThuc}, {NgayThanhToan}, {TrangThai}, {HoaDonID}, {NhanVienID});";
        }
        // lấy tất cả hóa đơn
        public static DataTable LayTatCaHoaDon()
        {
            string query = "SELECT * FROM hoadon;";
            return DBConnect.ExecuteQuery(query);
        }
        //
        public static DataTable LaySanPhamCuaHoaDon(int IDHoaDon)
        {
            string query = $"SELECT CTHD.SanPhamID,SP.TenSanPham,CTHD.SoLuong,SP.GiaBan FROM coffeemanagement.chitiethoadon as CTHD Join coffeemanagement.sanpham as SP ON CTHD.SanPhamID = SP.SanPhamID where CTHD.HoaDonID = {IDHoaDon};";
            return DBConnect.ExecuteQuery(query);
        }
    }
}