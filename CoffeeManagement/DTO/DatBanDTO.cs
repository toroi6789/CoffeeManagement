using System;

namespace CoffeeManagement.DTO
{
    public class DatBanDTO
    {
        public int DatBanID { get; set; }
        public int BanID { get; set; }
        public DateTime Ngay { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        public DatBanDTO() { }

        public DatBanDTO(int id, int banId, DateTime ngay, TimeSpan gioBD, TimeSpan gioKT)
        {
            DatBanID = id;
            BanID = banId;
            Ngay = ngay;
            GioBatDau = gioBD;
            GioKetThuc = gioKT;
        }
    }
}
