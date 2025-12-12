using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SanPhamNguyenLieuDAO:DBConnect
    {
        private static List<NguyenLieuDTO> dsNguyenLieu = new List<NguyenLieuDTO>();
        private static List<SanPhamDTO> dsSP = new List<SanPhamDTO>();
        private SanPhamDAO spDao = new SanPhamDAO();
        private NguyenLieuDAO nlDao = new NguyenLieuDAO();

        // Lấy công thức của 1 sản phẩm 
        public List<SanPhamNguyenLieuDTO> LayCongThucTheoSanPham(int sanPhamID)
        {
            List<SanPhamNguyenLieuDTO> ds = new List<SanPhamNguyenLieuDTO>();
            string sql = @"
            SELECT spnl.NguyenLieuID, spnl.SoLuongSuDung
            FROM SanPhamNguyenLieu spnl
            WHERE spnl.SanPhamID = @SanPhamID";
            var param = new MySqlParameter("@SanPhamID", Convert.ToInt32(sanPhamID));
            //var param = new MySqlParameter("@SanPhamID", MySqlDbType.Int32) { Value = sanPhamID };
            DataTable dt = ExecuteQuery(sql, new[] { param });
            var sanPham = spDao.GetAll().FirstOrDefault(x => x.SanPhamID == sanPhamID);
            if (sanPham == null) return ds;
            foreach (DataRow row in dt.Rows)
            {
                int nlId = Convert.ToInt32(row["NguyenLieuID"]);
                decimal sl = Convert.ToDecimal(row["SoLuongSuDung"]);
                var nguyenLieu = nlDao.GetAll().FirstOrDefault(x => x.NguyenLieuID == nlId);
                if (nguyenLieu != null)
                {
                    ds.Add(new SanPhamNguyenLieuDTO
                    {
                        SanPham = sanPham,
                        NguyenLieu = nguyenLieu,
                        SoLuongSuDung = sl
                    });
                }
            }
            return ds;
        }

        // Lấy tất cả công thức trong hệ thống
        public List<SanPhamNguyenLieuDTO> LayTatCaCongThuc()
        {
            List<SanPhamNguyenLieuDTO> ds = new List<SanPhamNguyenLieuDTO>();
            string sql = "SELECT SanPhamID, NguyenLieuID, SoLuongSuDung FROM SanPhamNguyenLieu";
            DataTable dt = ExecuteQuery(sql);
            var allSP = spDao.GetAll();
            var allNL = nlDao.GetAll();
            foreach (DataRow row in dt.Rows)
            {
                int spId = Convert.ToInt32(row["SanPhamID"]);
                int nlId = Convert.ToInt32(row["NguyenLieuID"]);
                decimal sl = Convert.ToDecimal(row["SoLuongSuDung"]);
                var sp = allSP.FirstOrDefault(x => x.SanPhamID == spId);
                var nl = allNL.FirstOrDefault(x => x.NguyenLieuID == nlId);
                if (sp != null && nl != null)
                {
                    ds.Add(new SanPhamNguyenLieuDTO
                    {
                        SanPham = sp,
                        NguyenLieu = nl,
                        SoLuongSuDung = sl
                    });
                }
            }
            return ds;
        }

        public void ThemNguyenLieuVaoSanPham(int SanPhamID, int NguyenLieuID, decimal SoLuongSuDung)
        {
            string query =
                $"INSERT INTO sanphamnguyenlieu (SanPhamID, NguyenLieuID, SoLuongSuDung) " +
                $"VALUES ({SanPhamID}, {NguyenLieuID}, {SoLuongSuDung})";
            DBConnect.ExecuteNonQuery(query);
        }
        public void XoaNguyenLieuCuaSanPham(int ID)
        {
            string query = $"delete FROM coffeemanagement.sanphamnguyenlieu where NguyenLieuID = '{ID}';";
            DBConnect.ExecuteNonQuery(query);
        }


        // 
        public static bool KiemTraNguyenLieuTonKhoChoSanPham(int IDsp)
        {
            List<SanPhamNguyenLieuDTO> ls = new List<SanPhamNguyenLieuDTO>();
            SanPhamNguyenLieuDAO nl = new SanPhamNguyenLieuDAO();
            ls = nl.LayCongThucTheoSanPham(IDsp);
            foreach (var i in ls)
            {
                NguyenLieuDAO nlDao = new NguyenLieuDAO();
                NguyenLieuDTO NguyenLieu = new NguyenLieuDTO();
                NguyenLieu = nlDao.LayNguyenLieuTheoID(i.NguyenLieu.NguyenLieuID);
                if (NguyenLieu.SoLuongTon < i.SoLuongSuDung)
                {
                    return true;
                }
            }
            return false;
        }
        public void CapNhatSoLuongSuDung(int sanphamID, int nguyenLieuID, decimal soLuongMoi)
        {
            string sql = $"UPDATE sanphamnguyenlieu SET SoLuongSuDung = '{soLuongMoi}' WHERE SanPhamID = '{sanphamID}' AND NguyenLieuID = '{nguyenLieuID}';";
            DBConnect.ExecuteNonQuery(sql);
        }
    }
}
