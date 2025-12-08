using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; } = "";
        public string MatKhau { get; set; } = "";
        public int TrangThai { get; set; } = 1;
        public int RoleID { get; set; }
        public string TenRole { get; set; } = "";
        public DateTime? NgayDangNhapCuoi { get; set; }
    }
}
