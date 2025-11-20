using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class NguyenLieuDTO
    {
        public int NguyenLieuID { get; set; }
        public string TenNguyenLieu { get; set; }
        public string DonVi { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal SoLuongTon { get; set; }
        public bool TrangThai { get; set; }
    }
}
