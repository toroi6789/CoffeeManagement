using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagement.BUS; // Đảm bảo bạn đã thêm namespace này
using CoffeeManagement.DTO;

namespace CoffeeManagement.DTO
{
    public class DanhMucDTO
    {
        public int DanhMucID { get; set; }
        public string TenDanhMuc { get; set; }
        public string TrangThai { get; set; }
        public string MoTa { get; set; }
        public decimal GiaBan { get; set; }

        public DanhMucDTO()
        {
        }

        public DanhMucDTO(int id, string tenDanhMuc, string trangThai, string moTa, decimal giaBan)
        {
            DanhMucID  = id;
            TenDanhMuc = tenDanhMuc;
            TrangThai = trangThai;
            MoTa = moTa;
            GiaBan = giaBan;
        }
    }
}
