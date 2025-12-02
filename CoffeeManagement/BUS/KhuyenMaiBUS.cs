using CoffeeManagement.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class KhuyenMaiBUS
    {
        // lay tat ca KM
        public static DataTable GetAllKM()
        {
            return KhuyenMaiDAO.GetAllKM();
        }
        // lay KM theo ID
        public static DataTable GetKM_ID(int ID)
        {
            return KhuyenMaiDAO.GetKM_ID(ID);
        }
    }
}
