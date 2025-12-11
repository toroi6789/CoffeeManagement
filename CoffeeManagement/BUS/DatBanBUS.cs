using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace CoffeeManagement.BUS
{
    public class DatBanBUS
    {
        public static void DatBan(DatBanDTO dat)
        {
            DatBanDAO.DatBan(dat.BanID, dat.Ngay, dat.GioBatDau, dat.GioKetThuc);

            // Sau khi insert booking vào DB, cập nhật trạng thái bàn trong DB rồi notify UI
            BanBUS.ResetTatCaBan();       // cập nhật trạng thái trong bảng Ban nếu cần
            BanBUS.RaiseTablesChanged();  // thông báo cho mọi UI đã đăng ký (BanGUI/DatBanGUI)
        }

        // Gọi hàm xóa đặt bàn từ DAO
        public static bool XoaDatBan(int datBanID)
        {
            // Gọi phương thức DAO để xóa đặt bàn
            return DatBanDAO.XoaDatBan(datBanID);
        }

        public static bool KiemTraTrung(int banID, DateTime ngay, TimeSpan gioBD, TimeSpan gioKT)
        {
            return DatBanDAO.KiemTraTrung(banID, ngay, gioBD, gioKT);
        }
        public static DataTable LayDatBanTheoBan(int banID)
        {
            return DatBanDAO.LayDatBanTheoBan(banID);
        }

        public static bool BanDangCoNguoi(int banID)
        {
            DateTime now = DateTime.Now;
            DateTime ngay = now.Date;
            TimeSpan gio = now.TimeOfDay;

            DataTable dt = DatBanDAO.LayDatBanTheoBan(banID);

            foreach (DataRow r in dt.Rows)
            {
                DateTime ngayDat = Convert.ToDateTime(r["Ngay"]);
                TimeSpan gioBD = (TimeSpan)r["GioBatDau"];
                TimeSpan gioKT = (TimeSpan)r["GioKetThuc"];

                if (ngayDat == ngay && gioBD <= gio && gio < gioKT)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<DatBanDTO> ChuyenDataTableSangDTO(DataTable dt)
        {
            List<DatBanDTO> list = new List<DatBanDTO>();

            foreach (DataRow row in dt.Rows)
            {
                DatBanDTO dto = new DatBanDTO
                {
                    DatBanID = Convert.ToInt32(row["DatBanID"]),
                    BanID = Convert.ToInt32(row["BanID"]),
                    Ngay = Convert.ToDateTime(row["Ngay"]),
                    GioBatDau = TimeSpan.Parse(row["GioBatDau"].ToString()),
                    GioKetThuc = TimeSpan.Parse(row["GioKetThuc"].ToString())
                };

                list.Add(dto);
            }

            return list;
        }

        public static DatBanDTO GetByID(int ID)
        {
            DatBanDTO db = DatBanDAO.GetDatBanByID(ID);

            if (db != null)
            {
                return null;
            }

            return db;
        }

        public static bool UpdateByID(DatBanDTO datBanDTO)
        {
            return DatBanDAO.UpdateDatBanByID(datBanDTO);
        }
    }
}
