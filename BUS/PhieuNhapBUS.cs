using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class PhieuNhapBUS
    {
        //lay tat ca phieu nhap
        public static DataTable PhieuNhap()
        {
            return PhieuNhapDAO.PhieuNhap();
        }
        //lay phieu nhap theo id
        public static DataTable PhieuNhapID(int phieuNhapID)
        {
            return PhieuNhapDAO.PhieuNhapID(phieuNhapID);
        }

        // them KM
        public static void InsertPN(DateTime NgayNhap, Decimal TongTien, string GhiChu, string TrangThai, int NhanVienID, int NhaCungCapID)
        {
            PhieuNhapDAO.InsertPN(NgayNhap, TongTien, GhiChu, TrangThai,NhanVienID,NhaCungCapID);
        }
        // sua KM
        public static void UpdatePN(int phieuNhapID, DateTime NgayNhap, Decimal TongTien, string GhiChu, string TrangThai, int NhanVienID, int NhaCungCapID)
        {
            PhieuNhapDAO.UpdatePN(phieuNhapID, NgayNhap, TongTien, GhiChu, TrangThai, NhanVienID, NhaCungCapID);
        }
        // xoa KM
        public static void DeletePN(int phieuNhapID)
        {
            PhieuNhapDAO.DeletePN(phieuNhapID);
        }
    }
}
