using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BanBUS
    {
        // Event để UI đăng ký khi dữ liệu bàn thay đổi
        public static event Action TablesChanged;

        public static void RaiseTablesChanged()
        {
            TablesChanged?.Invoke();
        }

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

        // ResetTatCaBan chỉ cập nhật DB, KHÔNG tự raise event để tránh recursion.
        public static void ResetTatCaBan()
        {
            DataTable dt = LayTatCaBan();
            List<BanDTO> list = ChuyenDataTableSangDTO(dt);
            DateTime now = DateTime.Now;
            DateTime twoHoursLater = now.AddHours(2);

            foreach (var item in list)
            {
                List<DatBanDTO> dsDatBan = DatBanBUS.ChuyenDataTableSangDTO(DatBanBUS.LayDatBanTheoBan(item.BanID));

                bool hasBookingInNext2Hours = false;
                bool hasActiveBooking = false;

                if (dsDatBan != null && dsDatBan.Count > 0)
                {
                    foreach (var datBan in dsDatBan)
                    {
                        DateTime start = datBan.Ngay.Date + datBan.GioBatDau;
                        DateTime end = datBan.Ngay.Date + datBan.GioKetThuc;

                        if (start <= now && now < end) hasActiveBooking = true;
                        if (end >= now && start <= twoHoursLater) hasBookingInNext2Hours = true;
                    }
                }

                if (!hasActiveBooking && !hasBookingInNext2Hours)
                {
                    CapNhatTrangThaiBan(item.BanID, "Trống");
                }
                else if (hasActiveBooking)
                {
                    CapNhatTrangThaiBan(item.BanID, "Có người");
                }
            }
        }



        public static List<BanDTO> LayTatCaBan2()
        {
            DataTable dt = BanDAO.LayTatCaBan(); // Lấy dữ liệu bàn từ DAO
            List<BanDTO> list = new List<BanDTO>(); // Khởi tạo danh sách các BanDTO

            foreach (DataRow row in dt.Rows)
            {
                // Tạo đối tượng BanDTO từ mỗi dòng trong DataTable
                list.Add(new BanDTO(
                    Convert.ToInt32(row["BanID"]),
                    row["TenBan"].ToString(),
                    Convert.ToInt32(row["SucChua"]),
                    row["TrangThai"].ToString()
                ));
            }

            return list;
        }


        public static void ThemBan(BanDTO newBan)
        {
            BanDAO.ThemBan(newBan.TenBan, newBan.SucChua, newBan.TrangThai);
            RaiseTablesChanged();
        }

        public static void SuaBan(BanDTO updatedBan)
        {
            BanDAO.SuaBan(updatedBan.BanID, updatedBan.TenBan, updatedBan.SucChua, updatedBan.TrangThai);
            RaiseTablesChanged();
        }

        public static void XoaBan(int banID)
        {
            BanDAO.XoaBan(banID);
            RaiseTablesChanged();
        }

        public static DataTable TimKiemBan(string keyword)
        {
            return BanDAO.TimKiemBan(keyword);
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
