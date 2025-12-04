using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAO dao;

        public NhanVienBUS(NhanVienDAO dao)
        {
            this.dao = dao;
        }

        // Get all employees
        public List<NhanVienDTO> GetAllNhanVien()
        {
            return dao.GetAll();
        }

        // Add new employee (with validation if needed)
        public bool AddNhanVien(NhanVienDTO nv)
        {
            nv.NgayKhoiTao = DateTime.Now;
            return dao.Insert(nv);
        }

        // Update employee
        public bool UpdateNhanVien(NhanVienDTO nv)
        {
            nv.NgayCapNhat = DateTime.Now;
            return dao.Update(nv);
        }

        // Delete employee
        public bool DeleteNhanVien(int id)
        {
            return dao.Delete(id);
        }
        public static DataTable LayNV_userID(int UserID)
        {
            return NhanVienDAO.LayNV_userID(UserID);
        }

        public NhanVienDTO GetNhanVienByID(int nhanVienID)
        {
            DataTable dt = NhanVienDAO.LayNV_userID(nhanVienID);

            if (dt.Rows.Count == 0)
                return null;

            return ConvertRowToDTO(dt.Rows[0]);
        }

        private NhanVienDTO ConvertRowToDTO(DataRow row)
        {
            NhanVienDTO nv = new NhanVienDTO();

            nv.NhanVienID = Convert.ToInt32(row["NhanVienID"]);
            nv.Ho = row["Ho"].ToString();
            nv.Ten = row["Ten"].ToString();
            nv.Phone = row["Phone"].ToString();
            nv.TrangThai = row["TrangThai"].ToString();

            nv.DateJoin = row["DateJoin"] == DBNull.Value
                ? (DateTime?)null
                : (DateTime?)Convert.ToDateTime(row["DateJoin"]);

            nv.NgayCapNhat = row["NgayCapNhat"] == DBNull.Value
                ? (DateTime?)null
                : (DateTime?)Convert.ToDateTime(row["NgayCapNhat"]);


            nv.NgayKhoiTao = Convert.ToDateTime(row["NgayKhoiTao"]);
            nv.UserID = Convert.ToInt32(row["UserID"]);

            return nv;
        }

    }
}
