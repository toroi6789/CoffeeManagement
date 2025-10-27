using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class UserDAO
    {
        private static List<UserDTO> users = new List<UserDTO>();

        public List<UserDTO> GetAll()
        {
            return users;
        }

        public void Add(UserDTO user)
        {
            users.Add(user);
        }
    }
}
