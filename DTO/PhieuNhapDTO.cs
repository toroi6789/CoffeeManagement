using System;

namespace CoffeeManagement.DTO
{
    public class PhieuNhapDTO
    {
        public int PhieuNhapID { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }
        public int NhanVienID { get; set; }
        public int NhaCungCapID { get; set; }
    }
}
