using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;

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
        }
    }
}
