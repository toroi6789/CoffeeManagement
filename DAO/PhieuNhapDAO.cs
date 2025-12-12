using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class PhieuNhapDAO
    {
        //lay tat ca phieu nhap
        public static DataTable PhieuNhap()
        {
            //code lay tat ca phieu nhap
            string query = "SELECT * FROM phieunhap";
            return DBConnect.ExecuteQuery(query);
        }
        //lay phieu nhap theo id
        public static DataTable PhieuNhapID(int phieuNhapID)
        {
            //code lay phieu nhap theo id
            string query = "SELECT * FROM phieunhap WHERE PhieuNhapID = " + phieuNhapID;
            return DBConnect.ExecuteQuery(query);
        }

        // them phieu nhap
        public static void InsertPN(DateTime NgayNhap, Decimal TongTien, string GhiChu,string TrangThai, int NhanVienID, int NhaCungCapID)
        {
            String query = $"INSERT INTO coffeemanagement.PhieuNhap (NgayNhap, TongTien, GhiChu, TrangThai, NhanVienID, NhaCungCapID) " +
                $"VALUES ('{NgayNhap.ToString("yyyy-MM-dd")}', '{TongTien}', '{GhiChu}', '{TrangThai}', '{TrangThai}', '{NhanVienID}', '{NhaCungCapID}');";
            DBConnect.ExecuteNonQuery(query);
        }

        // sua PN
        public static void UpdatePN(int phieuNhapID, DateTime NgayNhap, Decimal TongTien, string GhiChu, string TrangThai, int NhanVienID, int NhaCungCapID)
        {
            String query = $"UPDATE coffeemanagement.phieunhap " +
                $"SET NgayNhap ='{NgayNhap.ToString("yyyy-MM-dd")}', TongTien = '{TongTien}', GhiChu = '{GhiChu}', TrangThai = '{TrangThai}',NhanVienID = '{NhanVienID}', NhaCungCapID = '{NhaCungCapID}' " +
                $"WHERE PhieuNhapID = '{phieuNhapID}';";
            DBConnect.ExecuteNonQuery(query);
        }
        // xoa PN
        public static void DeletePN(int phieuNhapID)
        {
            String query = $"DELETE FROM coffeemanagement.phieunhap WHERE PhieuNhapID = '{phieuNhapID}';";
            DBConnect.ExecuteNonQuery(query);
        }
    }
}
