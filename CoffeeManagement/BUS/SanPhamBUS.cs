using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagement.DAO;

namespace CoffeeManagement.BUS
{
    public class SanPhamBUS
    {
        public static DataTable SanPham()
        {
            return SanPhamDAO.SanPham();
        }
        public static DataTable SanPhamTheoID(int sanPhamID)
        {
            return SanPhamDAO.SanPhamTheoID(sanPhamID);
        }
    }
}
