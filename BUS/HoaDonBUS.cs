using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class HoaDonBUS
    {
        public static DataTable GetChiTietHoaDonByID(int ID)
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

        // Hàm chuyển DataTable sang List<HoaDonDTO>
        public static List<HoaDonDTO> MapToListHD(DataTable dt)
        {
            List<HoaDonDTO> list = new List<HoaDonDTO>();

            foreach (DataRow row in dt.Rows)
            {
                HoaDonDTO hd = new HoaDonDTO()
                {
                    HoaDonID = Convert.ToInt32(row["HoaDonID"]),

                    NhanVienID = row["NhanVienID"] == DBNull.Value
                        ? null
                        : (int?)Convert.ToInt32(row["NhanVienID"]),

                    BanID = row["BanID"] == DBNull.Value
                        ? null
                        : (int?)Convert.ToInt32(row["BanID"]),

                    NgayKhoiTao = Convert.ToDateTime(row["NgayKhoiTao"]),

                    TongTien = Convert.ToDecimal(row["TongTien"]),

                    TrangThai = row["TrangThai"].ToString(),

                    KhuyenMaiID = row["KhuyenMaiID"] == DBNull.Value
                        ? null
                        : (int?)Convert.ToInt32(row["KhuyenMaiID"])
                };

                list.Add(hd);
            }

            return list;
        }

        // Hàm chuyển DataTable sang List<ChiTietHoaDonDTO>
        public static List<ChiTietHoaDonDTO> MapToListCTHD(DataTable dt)
        {
            List<ChiTietHoaDonDTO> list = new List<ChiTietHoaDonDTO>();

            foreach (DataRow row in dt.Rows)
            {
                ChiTietHoaDonDTO item = new ChiTietHoaDonDTO()
                {
                    HoaDonID = Convert.ToInt32(row["HoaDonID"]),
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                };

                list.Add(item);
            }

            return list;
        }

        // Hàm lấy toàn bộ hóa đơn dưới dạng List<HoaDonDTO>
        public static List<HoaDonDTO> GetAllListHD()
        {
            return MapToListHD(TatCaHoaDon());
        }

        // Hàm lọc theo ngày
        public static List<HoaDonDTO> GetByDate(DateTime date)
        {
            List<HoaDonDTO> all = GetAllListHD();

            return all
                .Where(hd => hd.NgayKhoiTao.Date == date.Date)
                .ToList();
        }

        // Hàm tính tổng doanh thu theo ngày
        public static decimal GetTotalByDate(DateTime date)
        {
            return GetByDate(date).Sum(hd => hd.TongTien);
        }

    }
}
