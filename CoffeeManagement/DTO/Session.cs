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
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}

