using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CoffeeManagement.DAO
{
    public class SanPhamDAO
    {
        // Lấy tất cả sản phẩm
        public static DataTable SanPham()
        {
            string query = "SELECT * FROM sanpham;";
            return DBConnect.ExecuteQuery(query);
        }
        // Lấy sản phẩm theo ID
        public static DataTable SanPhamTheoID(int sanPhamID)
        {
            string query = $"SELECT * FROM sanpham WHERE SanPhamID = {sanPhamID};";
            return DBConnect.ExecuteQuery(query);
        }
    }
}
