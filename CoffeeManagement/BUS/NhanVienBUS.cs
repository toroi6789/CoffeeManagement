using CoffeeManagement.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class NhanVienBUS
    {
        public static DataTable LayNV_userID(int UserID)
        {
            return NhanVienDAO.LayNV_userID(UserID);
        }
    }
}
