using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagement.DAO;

namespace CoffeeManagement.BUS
{
    public class BanBUS
    {
        public static void CapNhatTrangThaiBan(int BanID, string TrangThai)
        {
            BanDAO.CapNhatTrangThaiBan(BanID, TrangThai);
        }
        public static DataTable LayTatCaBanHoatDong()
        {
            return BanDAO.LayTatCaBanHoatDong();
        }
    }
}
