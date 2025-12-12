using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BUS
{
    public class UserBUS
    {
        private readonly UserDAO userDAO = new UserDAO();

        public List<UserDTO> GetUsers()
        {
            return userDAO.GetAll();
        }

        // Đăng nhập
        public UserDTO Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            UserDTO user = userDAO.GetUserByEmailAndPassword(email, password);

            if (user != null)
            {
                userDAO.UpdateLastLoginDate(user.UserID);
            }

            return user;
        }

        // Đăng ký
        public Result Register(string email, string password, int roleID, NhanVienDTO nhanVien)
        {
            if (!IsValidEmail(email))
                return new Result { Success = false, Message = "Email không hợp lệ!" };

            if (!IsValidPassword(password))
                return new Result { Success = false, Message = "Password phải từ 6 ký tự trở lên!" };

            if (roleID <= 0)
                return new Result { Success = false, Message = "Role không hợp lệ!" };

            if (userDAO.ExistsByEmail(email))
                return new Result { Success = false, Message = "Email đã tồn tại!" };

            NhanVienBUS nhanVienBUS = new NhanVienBUS();

            if (nhanVienBUS.PhoneExists(nhanVien.Phone))
                return new Result { Success = false, Message = "Số điện thoại đã tồn tại!" };

            try
            {
                UserDTO user = new UserDTO
                {
                    Email = email,
                    MatKhau = password,
                    RoleID = roleID,
                    TrangThai = 1
                };

                int last_insert_id = userDAO.Add(user);

                nhanVien.UserID = last_insert_id;

                if (!nhanVienBUS.AddNhanVien(nhanVien).Success)
                {
                    userDAO.DeleteUser(last_insert_id);
                    return new Result { Success = false, Message = "Lỗi khi thêm nhân viên!" };
                }

                return new Result { Success = true, Message = "Đăng ký thành công!" };
            }
            catch
            {
                return new Result { Success = false, Message = "Đăng ký thất bại!" };
            }
        }

        public UserDTO GetUserByID(int id)
        {
            return userDAO.GetUserByID(id);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }

        // Xóa cứng
        public Result DeleteUser(int userID)
        {
            UserDTO user = GetUserByID(userID);

            if (user != null)
            {
                int adminCount = GetUsers().Count(u => u.RoleID == 1);

                if (user.RoleID == 1 && adminCount == 1)
                {
                    return new Result { Success = false, Message = "Không thể xóa Admin cuối cùng!" };
                }
            }

            bool ok = userDAO.DeleteUser(userID);

            return new Result
            {
                Success = ok,
                Message = ok ? "Xóa thành công!" : "Xóa thất bại!"
            };
        }

        // Xóa mềm
        public Result SoftDeleteUser(int userID)
        {
            UserDTO user = GetUserByID(userID);
            NhanVienBUS nvBUS = new NhanVienBUS();
            NhanVienDTO nv = nvBUS.ConvertRowToDTO(NhanVienBUS.LayNV_userID(userID).Rows[0]);

            if (user != null)
            {
                int adminCount = GetUsers().Count(u => u.RoleID == 1);

                if (user.RoleID == 1 && adminCount == 1)
                {
                    return new Result { Success = false, Message = "Không thể xóa Admin cuối cùng!" };
                }
            }

            bool ok = userDAO.SoftDeleteUser(userID);
            if (ok)
            {
                nv.TrangThai = "Trống lịch";
                nvBUS.UpdateNhanVien(nv);
            }
            return new Result
            {
                Success = ok,
                Message = ok ? "Xóa thành công!" : "Xóa thất bại!"
            };
        }

        // Update user
        public Result UpdateUser(UserDTO user)
        {
            if (user == null)
                return new Result { Success = false, Message = "User không hợp lệ!" };

            var oldUser = GetUserByID(user.UserID);
            int adminCount = GetUsers().Count(u => u.RoleID == 1);

            if (oldUser.RoleID == 1 && user.RoleID != 1 && adminCount == 1)
            {
                return new Result { Success = false, Message = "Không thể đổi role của Admin cuối cùng!" };
            }

            bool ok = userDAO.UpdateUserByID(user);

            return new Result
            {
                Success = ok,
                Message = ok ? "Cập nhật thành công!" : "Cập nhật thất bại!"
            };
        }
    }
}
