using CoffeeManagement.DTO;
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
            //PhieuNhapDAO.DeletePN(phieuNhapID);
        }

        public static List<PhieuNhapDTO> ConvertToDTO(DataTable dt)
        {
            List<PhieuNhapDTO> list = new List<PhieuNhapDTO>();

            foreach (DataRow row in dt.Rows)
            {
                PhieuNhapDTO pn = new PhieuNhapDTO
                {
                    PhieuNhapID = row["PhieuNhapID"] != DBNull.Value ? Convert.ToInt32(row["PhieuNhapID"]) : 0,
                    NgayNhap = row["NgayNhap"] != DBNull.Value ? Convert.ToDateTime(row["NgayNhap"]) : DateTime.MinValue,
                    TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0m,
                    GhiChu = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString() : null,
                    TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : null,
                    NhanVienID = row["NhanVienID"] != DBNull.Value ? Convert.ToInt32(row["NhanVienID"]) : 0,
                    NhaCungCapID = row["NhaCungCapID"] != DBNull.Value ? Convert.ToInt32(row["NhaCungCapID"]) : 0
                };

                list.Add(pn);
            }

            return list;
        }

    }
}
