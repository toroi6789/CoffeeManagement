using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class ChiTietHoaDonDTO
    {
        public int ChiTietID { get; set; }
        public int HoaDonID { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
