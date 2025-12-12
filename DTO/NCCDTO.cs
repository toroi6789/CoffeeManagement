namespace DTO
{
    public class NCCDTO
    {
        public int NhaCungCapID { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TrangThai { get; set; }

        public NCCDTO() { }

        public NCCDTO(int id, string ten, string diachi, string sdt, string email, string web, string trangthai)
        {
            NhaCungCapID = id;
            TenNhaCungCap = ten;
            DiaChi = diachi;
            SoDienThoai = sdt;
            Email = email;
            Website = web;
            TrangThai = trangthai;
        }
    }
}
