using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class BanDAO
    {
        public static void CapNhatTrangThaiBan(int BanID, string TrangThai)
        {
            string query = $"UPDATE ban SET TrangThai = '{TrangThai}' WHERE BanID = {BanID};";
            DBConnect.ExecuteNonQuery(query);
        }
        public static DataTable LayTatCaBanHoatDong()
        {
            string query = "SELECT BanID,TenBan FROM ban WHERE TrangThai = 'Trống';";
            return DBConnect.ExecuteQuery(query);
        }

    }
}
