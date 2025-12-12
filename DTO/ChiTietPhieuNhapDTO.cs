using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietPhieuNhapDTO
    {
            public int ChiTietPhieuNhapID { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
            public decimal ThanhTien { get; set; }
            public int PhieuNhapID { get; set; }
            public int NguyenLieuID { get; set; }
            public string TenNguyenLieu { get; set; }
            public string DonVi { get; set; }
            public string Hinh { get; set; } // tên file ảnh

            // Format hiển thị
            public string DonGiaFormat => DonGia.ToString("N0") + " ₫";
            public string ThanhTienFormat => ThanhTien.ToString("N0") + " ₫";
            public string SoLuongDonVi => $"{SoLuong} {DonVi}";
    }
}
