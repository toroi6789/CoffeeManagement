using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class SanPhamNguyenLieuBUS
    {
        private SanPhamNguyenLieuDAO dao = new SanPhamNguyenLieuDAO();
        // Lấy công thức theo sản phẩm (trả về List để dễ dùng)
        public List<SanPhamNguyenLieuDTO> LayCongThucTheoSanPhamBUS(int sanPhamID)
        {
            return dao.LayCongThucTheoSanPham(sanPhamID);
        }

    }
}
