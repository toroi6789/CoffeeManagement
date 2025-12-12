using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
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
        public void CapNhatSoLuongSuDung(int sanphamID, int nguyenLieuID, decimal soLuongMoi)
        {
            dao.CapNhatSoLuongSuDung(sanphamID, nguyenLieuID, soLuongMoi);
        }
    }

}
