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
        public List<SanPhamNguyenLieuDTO> LayCongThucTheoSanPhamBUS(int sanPhamID)
        {
            return dao.LayCongThucTheoSanPham(sanPhamID);
        }
        public void ThemNguyenLieuVaoSanPham(int SanPhamID, int NguyenLieuID , decimal SoLuongSuDung)
        {
            dao.ThemNguyenLieuVaoSanPham(SanPhamID,NguyenLieuID,SoLuongSuDung);
        }
        public void XoaNguyenLieuCuaSanPham(int NguyenLieuID)
        {
            dao.XoaNguyenLieuCuaSanPham( NguyenLieuID);
        }
    }

}
