using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeeManagement.BUS
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
                // Cập nhật ngày đăng nhập cuối
                userDAO.UpdateLastLoginDate(user.UserID);
            }

            return user;
        }

        // Đăng ký (nếu cần)
        public bool Register(string email, string password, int roleID, NhanVienDTO nhanVien)
        {
            // Validate email format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // Validate password strength
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password không hợp lệ! (6 ký tự trở lên)", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Validate roleID
            if (roleID <= 0)
            {
                MessageBox.Show("role không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Check if email already exists (optional but recommended)
            if (userDAO.ExistsByEmail(email))
            {
                MessageBox.Show("Email đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            NhanVienBUS nhanVienBUS = new NhanVienBUS(new NhanVienDAO());
            if (nhanVienBUS.PhoneExists(nhanVien.Phone))
            {
                MessageBox.Show("SDT đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {

                UserDTO user = (new UserDTO
                {
                    Email = email,
                    MatKhau = password,  // Should hash in real case
                    RoleID = roleID,
                    TrangThai = 1
                });

                // Create a new user
                int last_insert_id = userDAO.Add(user);

                nhanVien.UserID = last_insert_id;
                if (!nhanVienBUS.AddNhanVien(nhanVien))
                {
                    // rollback nếu add nhân viên lỗi
                    userDAO.DeleteUser(GetUserByID(last_insert_id).UserID);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserDTO GetUserByID(int id)
        {
            UserDTO user = userDAO.GetUserByID(id);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        // Validate email pattern using regex
        private bool IsValidEmail(string email)
        {
            // Validate empty fields
            if (string.IsNullOrWhiteSpace(email))
                return false;
            // Standard email regex pattern
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        // Validate password strength
        private bool IsValidPassword(string password)
        {
            // Validate empty fields
            if (string.IsNullOrWhiteSpace(password))
                return false;
            // Minimum 6 characters (you can expand rules: uppercase/special chars)
            return password.Length >= 6;
        }

        public bool DeleteUser(int userID)
        {
            return userDAO.DeleteUser(userID);
        }

        public bool UpdateUser(UserDTO user)
        {
            return userDAO.UpdateUserByID(user);
        }
    }


}
