using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class KhuyenMaiDAO
    {
        // lay tat ca KM
        public static DataTable GetAllKM()
        {
            String query = $"SELECT * FROM coffeemanagement.khuyenmai;";
            return DBConnect.ExecuteQuery(query);
        }
        // lay KM theo ID
        public static DataTable GetKM_ID(int ID)
        {
            String query = $"SELECT * FROM coffeemanagement.khuyenmai where KhuyenMaiID = '{ID}';";
            return DBConnect.ExecuteQuery(query);
        }
    }
}
