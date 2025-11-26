using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class NhanVienDTO
    {
        public int NhanVienID { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Phone { get; set; }
        public string TrangThai { get; set; }
        public DateTime? DateJoin { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public DateTime NgayKhoiTao { get; set; }
        public int UserID { get; set; }

        // FullName (optional utility)
        public string FullName => $"{Ho} {Ten}".Trim();
    }
}
