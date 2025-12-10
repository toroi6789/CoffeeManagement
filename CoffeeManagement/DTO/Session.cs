using CoffeeManagement.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    // Class để lưu thông tin session của user đang đăng nhập
    public static class Session
    {
        public static UserDTO CurrentUser { get; set; }
        public static bool IsLoggedIn => CurrentUser != null;


        public static void Login(UserDTO user)
        {
            NhanVienBUS nhanVienBUS = new NhanVienBUS();
            // Set trạng thái nhân viên
            NhanVienDTO nv = nhanVienBUS.ConvertRowToDTO(
                NhanVienBUS.LayNV_userID(user.UserID).Rows[0]);
            nv.TrangThai = "Đang làm việc";
            nhanVienBUS.UpdateNhanVien(nv);

            CurrentUser = user;
        }

        public static void Logout()
        {
            NhanVienBUS nhanVienBUS = new NhanVienBUS();
            // Set trạng thái nhân viên
            NhanVienDTO nv = nhanVienBUS.ConvertRowToDTO(
                NhanVienBUS.LayNV_userID(CurrentUser.UserID).Rows[0]);
            nv.TrangThai = "Trống lịch";
            nhanVienBUS.UpdateNhanVien(nv);

            CurrentUser = null;
        }
    }
}

