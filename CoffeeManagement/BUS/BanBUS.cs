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
            ResetTatCaBan();

            return BanDAO.LayTatCaBanHoatDong();
        }
        public static void ResetTatCaBan()
        {
            List<BanDTO> list = ChuyenDataTableSangDTO(LayTatCaBan());
            DateTime now = DateTime.Now;
            DateTime twoHoursLater = now.AddHours(2);

            foreach (var item in list)
            {
                List<DatBanDTO> dsDatBan = DatBanBUS.ChuyenDataTableSangDTO(
                                            DatBanBUS.LayDatBanTheoBan(item.BanID));

                bool hasBookingInNext2Hours = false;
                bool hasActiveBooking = false;

                if (dsDatBan != null && dsDatBan.Count > 0)
                {
                    foreach (var datBan in dsDatBan)
                    {
                        DateTime start = datBan.Ngay.Date + datBan.GioBatDau;
                        DateTime end = datBan.Ngay.Date + datBan.GioKetThuc;

                        // 1. Hiện tại có người đang dùng bàn
                        if (start <= now && now < end)
                        {
                            hasActiveBooking = true;
                        }

                        // 2. Có đặt bàn trong vòng 2h tới
                        if (end >= now && start <= twoHoursLater)
                        {
                            hasBookingInNext2Hours = true;
                        }
                    }
                }

                // ❗ Điều kiện để set Trống:
                // Không có người đang ngồi AND không có booking trong 2 giờ tới
                if (!hasActiveBooking && !hasBookingInNext2Hours)
                {
                    CapNhatTrangThaiBan(item.BanID, "Trống");
                }
            }
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

        public static List<BanDTO> ChuyenDataTableSangDTO(DataTable dt)
        {
            List<BanDTO> list = new List<BanDTO>();

            if (dt == null || dt.Rows.Count == 0)
                return list;

            foreach (DataRow row in dt.Rows)
            {
                BanDTO ban = new BanDTO(
                    Convert.ToInt32(row["BanID"]),
                    row["TenBan"].ToString(),
                    Convert.ToInt32(row["SucChua"]),
                    row["TrangThai"].ToString()
                );

                list.Add(ban);
            }

            return list;
        }
    }
}
