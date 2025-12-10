using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class HoaDonDTO
    {
        public int HoaDonID { get; set; }
        public int? NhanVienID { get; set; }
        public int? BanID { get; set; }
        public DateTime NgayKhoiTao { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public int? KhuyenMaiID { get; set; }

        // Nếu muốn lấy danh sách chi tiết hóa đơn
        //public List<ChiTietHoaDonDTO> ChiTiet { get; set; }
    }
}
