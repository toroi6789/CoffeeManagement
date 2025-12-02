using CoffeeManagement.DAO;
<<<<<<< HEAD
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
=======
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 43c2d34e269f3c22361bb4a5a4321679e5468b6a

namespace CoffeeManagement.BUS
{
    public class NhanVienBUS
    {
<<<<<<< HEAD
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
            nv.NgayCapNhat = DateTime.Now;
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
=======
        public static DataTable LayNV_userID(int UserID)
        {
            return NhanVienDAO.LayNV_userID(UserID);
>>>>>>> 43c2d34e269f3c22361bb4a5a4321679e5468b6a
        }
    }
}
