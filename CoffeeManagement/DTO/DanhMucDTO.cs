using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class DanhMucDTO
    {
        public int DanhMucID { get; set; }
        public string TenDanhMuc { get; set; }
        public string TrangThai { get; set; } = "Hoạt động";
        public string MoTa { get; set; }
    }
}
