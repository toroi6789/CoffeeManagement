using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

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

        public static DataTable LayTatCaBan()
        {
            return BanDAO.LayTatCaBan();
        }

        public static void ThemBan(BanDTO newBan)
        {
            BanDAO.ThemBan(newBan.TenBan, newBan.SucChua, newBan.TrangThai);
        }

        public static void SuaBan(BanDTO updatedBan)
        {
            BanDAO.SuaBan(updatedBan.BanID, updatedBan.TenBan, updatedBan.SucChua, updatedBan.TrangThai);
        }

        public static void XoaBan(int banID)
        {
            BanDAO.XoaBan(banID);
        }

        public static DataTable TimKiemBan(string keyword)
        {
            return BanDAO.TimKiemBan(keyword);
        }

        // Kiểm tra nếu bàn có người ngồi hay không
        public static bool CheckBanAvailability(int banID)
        {
            return BanDAO.IsBanAvailable(banID);
        }

        public static BanDTO LayBanTheoID(int banID)
        {
            return BanDAO.LayBanTheoID(banID);
        }
    }
}
