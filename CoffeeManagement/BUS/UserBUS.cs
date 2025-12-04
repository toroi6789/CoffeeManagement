using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool Register(string email, string password, int roleID = 2)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                userDAO.Add(new UserDTO 
                { 
                    Email = email, 
                    MatKhau = password,
                    RoleID = roleID,
                    TrangThai = 1
                });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
