namespace CoffeeManagement.DTO
{
    public class BanDTO
    {
        public int BanID { get; set; }
        public string TenBan { get; set; }
        public int SucChua { get; set; }
        public string TrangThai { get; set; }

        public BanDTO(int banID, string tenBan, int sucChua, string trangThai)
        {
            BanID = banID;
            TenBan = tenBan;
            SucChua = sucChua;
            TrangThai = trangThai;
        }
    }
}
