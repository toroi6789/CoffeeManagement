using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Data;

namespace CoffeeManagement.BUS
{
    public class DatBanBUS
    {
        public static void DatBan(DatBanDTO dat)
        {
            DatBanDAO.DatBan(dat.BanID, dat.Ngay, dat.GioBatDau, dat.GioKetThuc);
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

    }
}
