using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class KhuyenMaiBUS
    {
        // lay tat ca KM
        public static DataTable GetAllKM()
        {
            return KhuyenMaiDAO.GetAllKM();
        }
        // lay KM theo ID
        public static DataTable GetKM_ID(int ID)
        {
            return KhuyenMaiDAO.GetKM_ID(ID);
        }
        // them KM
        public static void InsertKM(string TenKM, string LoaiKhuyenMai, string MoTa, Decimal giatri, DateTime NgayBatDau, DateTime NgayKetThuc, string TrangThai)
        {
            KhuyenMaiDAO.InsertKM(TenKM, LoaiKhuyenMai, MoTa, giatri, NgayBatDau, NgayKetThuc, TrangThai);
        }
        // sua KM
        public static void UpdateKM(int KhuyenMaiID, string TenKM, string LoaiKhuyenMai, string MoTa, Decimal giatri, DateTime NgayBatDau, DateTime NgayKetThuc, string TrangThai)
        {
            KhuyenMaiDAO.UpdateKM(KhuyenMaiID, TenKM, LoaiKhuyenMai, MoTa, giatri, NgayBatDau, NgayKetThuc, TrangThai);
        }
        // xoa KM
        public static void DeleteKM(int KhuyenMaiID)
        {
            KhuyenMaiDAO.DeleteKM(KhuyenMaiID);
        }
        //lay KM theo ten
        public static DataTable GetKM_Name(string TenKM)
        {
            DataTable allKM = KhuyenMaiDAO.GetAllKM();
            DataTable result = allKM.Clone(); // Tạo một DataTable mới với cùng cấu trúc
            foreach (DataRow row in allKM.Rows)
            {
                if (row["TenKhuyenMai"].ToString().IndexOf(TenKM, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.ImportRow(row); // Thêm dòng phù hợp vào DataTable kết quả
                }
            }
            return result;
        }
        // lay km hoat dong 
        public static DataTable GetActiveKM()
        {
            DataTable allKM = KhuyenMaiDAO.GetAllKM();
            DataTable result = allKM.Clone(); // Tạo một DataTable mới với cùng cấu trúc
            DateTime today = DateTime.Today;
            foreach (DataRow row in allKM.Rows)
            {
                if (row["TrangThai"].ToString().Equals("Hoạt Động", StringComparison.OrdinalIgnoreCase) && Convert.ToDateTime(row["NgayBatDau"]) <= today && today < Convert.ToDateTime(row["NgayKetThuc"]))
                {
                    result.ImportRow(row); // Thêm dòng phù hợp vào DataTable kết quả
                }
            }
            return result;
        }
    }
}
