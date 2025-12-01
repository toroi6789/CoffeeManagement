using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class NhanVienDAO
    {
        public static DataTable LayNV_userID(int IDuser)
        {
            string query = $"SELECT * FROM coffeemanagement.nhanvien where UserID = '{IDuser}';";
            return DBConnect.ExecuteQuery(query) ;
        }
    }
}
