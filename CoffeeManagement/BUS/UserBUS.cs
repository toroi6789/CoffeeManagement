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

        public bool Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            userDAO.Add(new UserDTO { Username = username, Password = password });
            return true;
        }
    }
}
