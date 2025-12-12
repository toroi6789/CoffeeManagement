using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SanPhamNguyenLieuDTO
    {
        public SanPhamDTO SanPham { get; set; } 
        public NguyenLieuDTO NguyenLieu { get; set; } 
        public decimal SoLuongSuDung { get; set; }   
    }
}
