using CoffeeManagement.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeManagement.DAO
{
    public class HoaDonDAO:DBConnect
    {
        // lấy hóa đơn theo ID
        public static DataTable HoaDonTheoID(int hoaDonID)
        {
            string query = "SELECT * FROM hoadon WHERE HoaDonID = @id";
            MySqlParameter[] param = {
                new MySqlParameter("@id", hoaDonID)
            };
            return ExecuteQuery(query, param);
        }

        // lấy tất cả chi tiết hóa đơn
        public static DataTable ChiTietHoaDon()
        {
            string query = "SELECT * FROM chitiethoadon;";
            return ExecuteQuery(query, null);
        }

        // lấy chi tiết hóa đơn theo ID
        public static DataTable ChiTietHoaDonTheoID(int hoaDonID)
        {
            string query = "SELECT * FROM chitiethoadon WHERE HoaDonID = @id";
            MySqlParameter[] param = {
                new MySqlParameter("@id", hoaDonID)
            };
            return ExecuteQuery(query, param);
        }

        // tạo hóa đơn mới
        public static void TaoHoaDon(int nhanVienID, int banID, DateTime ngayLap,
            decimal tongTien, string trangthai, int idKM)
        {
            string query =
            @"INSERT INTO hoadon 
              (NhanVienID, BanID, NgayKhoiTao, TongTien, TrangThai, KhuyenMaiID)
              VALUES (@nv, @ban, @ngay, @tong, @tt, @km);";

            MySqlParameter[] param = {
                new MySqlParameter("@nv", nhanVienID),
                new MySqlParameter("@ban", banID == 0 ? (object)DBNull.Value : banID),
                new MySqlParameter("@ngay", ngayLap),
                new MySqlParameter("@tong", tongTien),
                new MySqlParameter("@tt", trangthai),
                new MySqlParameter("@km", idKM == 0 ? (object)DBNull.Value : idKM)
            };

            ExecuteNonQuery(query, param);
        }

        // tạo chi tiết hóa đơn mới
        public static void TaoChiTietHoaDon(int soLuong, decimal donGia,
            int hoaDonID, int sanPhamID, decimal thanhTien)
        {
            string query =
            @"INSERT INTO chitiethoadon 
              (SoLuong, DonGia, HoaDonID, SanPhamID, ThanhTien)
              VALUES (@sl, @dg, @hd, @sp, @tt);";

            MySqlParameter[] param = {
                new MySqlParameter("@sl", soLuong),
                new MySqlParameter("@dg", donGia),
                new MySqlParameter("@hd", hoaDonID),
                new MySqlParameter("@sp", sanPhamID),
                new MySqlParameter("@tt", thanhTien)
            };

            ExecuteNonQuery(query, param);
        }

        // lấy tất cả hóa đơn
        public static DataTable LayTatCaHoaDon()
        {
            string query = "SELECT * FROM hoadon;";
            return ExecuteQuery(query, null);
        }

        // LaySanPhamCuaHoaDon
        public static DataTable LaySanPhamCuaHoaDon(int hoaDonID)
        {
            string query =
            @"SELECT CTHD.SanPhamID, SP.TenSanPham, CTHD.SoLuong, SP.GiaBan
              FROM chitiethoadon CTHD
              JOIN sanpham SP ON CTHD.SanPhamID = SP.SanPhamID
              WHERE CTHD.HoaDonID = @id";

            MySqlParameter[] param = {
                new MySqlParameter("@id", hoaDonID)
            };

            return ExecuteQuery(query, param);
        }

        // sửa trạng thái
        public static void SuaTrangThai(int hoaDonID, string trangThai)
        {
            string query = "UPDATE hoadon SET TrangThai = @tt WHERE HoaDonID = @id";

            MySqlParameter[] param = {
                new MySqlParameter("@tt", trangThai),
                new MySqlParameter("@id", hoaDonID)
            };

            ExecuteNonQuery(query, param);
        }

        // cập nhật phương thức thanh toán
        public static void Capnhatphuongthuc(int hoaDonID, string phuongThuc)
        {
            string query = "UPDATE hoadon SET PhuongThucThanhToan = @pt WHERE HoaDonID = @id";

            MySqlParameter[] param = {
                new MySqlParameter("@pt", phuongThuc),
                new MySqlParameter("@id", hoaDonID)
            };

            ExecuteNonQuery(query, param);
        }

        // xóa hóa đơn
        public static void XoaHoaDon(int id)
        {
            string query1 = "DELETE FROM chitiethoadon WHERE HoaDonID = @id";
            string query2 = "DELETE FROM thanhtoan WHERE HoaDonID = @id";
            string query3 = "DELETE FROM hoadon WHERE HoaDonID = @id";

            MySqlParameter[] param = { new MySqlParameter("@id", id) };

            ExecuteNonQuery(query1, param);
            ExecuteNonQuery(query2, param);
            ExecuteNonQuery(query3, param);
        }

        // tìm kiếm hóa đơn theo ngày
        public static DataTable TimKiemHoaDonTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string query =
            $"SELECT * FROM hoadon WHERE NgayKhoiTao >= '{tuNgay:yyyy-MM-dd}' AND NgayKhoiTao < '{denNgay:yyyy-MM-dd}';";


            return ExecuteQuery(query);
        }
    }
}
