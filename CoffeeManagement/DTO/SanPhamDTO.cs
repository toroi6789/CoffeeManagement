using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class SanPhamDTO
    {
        private int sanPhamID;
        private string tenSanPham;
        private decimal giaBan;
        private string moTa;
        private int danhMucID;
        private string trangThai;
        public string hinh;

        public int SanPhamID
        {
            get { return sanPhamID; }
            set
            {
                if (value > 0)
                    sanPhamID = value;
                else
                    throw new ArgumentException("Mã Sản Phẩm phải lớn hơn 0 và Không được để trống","SanPhamID");
            }
        }
        public string TenSanPham
        {
            get { return tenSanPham; }
            set { 
                tenSanPham = value; 
            }
        }
        public decimal GiaBan
        {
            get { return giaBan; }
            set
            {
                if (value > 1000)
                    giaBan = value;
                else
                    throw new ArgumentException("giá bán phải từ 1000 trở lên và không được để trống","GiaBan");
            }
        }
        public string MoTa
        {
            get { return moTa; }
            set { moTa = value; }
        }
        public string Hinh
        {
            get { return hinh; }
            set { hinh = value; }
        }
        public int DanhMucID
        {
            get { return danhMucID; }
            set
            {
                if (value > 0)
                    danhMucID = value;
                else
                    throw new ArgumentException("Mã Danh mục phải lớn hơn 0 và Không được để trống","DanhMucID");
            }
        }
        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }

    }
}
