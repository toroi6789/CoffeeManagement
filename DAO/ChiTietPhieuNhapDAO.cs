using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class ChiTietPhieuNhapDAO:DBConnect
    {
        // Lấy chi tiết phiếu nhập theo PhieuNhapID, join với NguyenLieu để lấy tên
        public List<ChiTietPhieuNhapDTO> GetChiTietByPhieuNhapID(int phieuNhapID)
        {
            List<ChiTietPhieuNhapDTO> list = new List<ChiTietPhieuNhapDTO>();
            string query = @"
                SELECT ct.ChiTietPhieuNhapID, ct.SoLuong, ct.DonGia, ct.ThanhTien, ct.PhieuNhapID, ct.NguyenLieuID,
                       nl.TenNguyenLieu, nl.DonVi, nl.Hinh
                FROM chitietphieunhap ct
                INNER JOIN nguyenlieu nl ON ct.NguyenLieuID = nl.NguyenLieuID
                WHERE ct.PhieuNhapID = @PhieuNhapID";

            MySqlParameter[] param = { new MySqlParameter("@PhieuNhapID", phieuNhapID) };
            DataTable dt = DBConnect.ExecuteQuery(query, param);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietPhieuNhapDTO
                {
                    ChiTietPhieuNhapID = Convert.ToInt32(row["ChiTietPhieuNhapID"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"]),
                    PhieuNhapID = Convert.ToInt32(row["PhieuNhapID"]),
                    NguyenLieuID = Convert.ToInt32(row["NguyenLieuID"]),
                    TenNguyenLieu = row["TenNguyenLieu"].ToString(),
                    DonVi = row["DonVi"].ToString(),
                    Hinh = row["Hinh"].ToString()
                });
            }
            return list;
        }

        // Thêm chi tiết phiếu nhập (nếu cần khi thêm phiếu nhập)
        public bool InsertChiTiet(ChiTietPhieuNhapDTO ct)
        {
            string query = @"
                INSERT INTO chitietphieunhap (SoLuong, DonGia, ThanhTien, PhieuNhapID, NguyenLieuID)
                VALUES (@SoLuong, @DonGia, @ThanhTien, @PhieuNhapID, @NguyenLieuID)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@SoLuong", ct.SoLuong),
                new MySqlParameter("@DonGia", ct.DonGia),
                new MySqlParameter("@ThanhTien", ct.ThanhTien),
                new MySqlParameter("@PhieuNhapID", ct.PhieuNhapID),
                new MySqlParameter("@NguyenLieuID", ct.NguyenLieuID)
            };

            return DBConnect.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
