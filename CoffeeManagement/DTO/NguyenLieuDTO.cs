using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DTO
{
    public class NguyenLieuDTO
    {
        private int nguyenLieuID;
        private string tenNguyenLieu;
        private decimal giaNhap;
        private decimal soLuongTon;
        private string moTa;
        private string donVi;
        private int danhMucID;
        private string trangThai;

        public int NguyenLieuID
        {
            get { return nguyenLieuID; }
            set
            {
                if (value > 0)
                    nguyenLieuID = value;
                else
                    throw new ArgumentException("Mã Nguyen Lieu phải lớn hơn 0 và Không được để trống", "NguyenLieuID");
            }
        }
        public string TenNguyenLieu
        {
            get { return tenNguyenLieu; }
            set
            {
                tenNguyenLieu = value;
            }
        }
        public decimal GiaNhap
        {
            get { return giaNhap; }
            set
            {
                if (value >=  0)
                    giaNhap = value;
                else
                    throw new ArgumentException("giá nhập phải từ 0 trở lên và không được để trống", "GiaNhap");
            }
        }

        public decimal SoLuongTon
        {
            get { return soLuongTon; }
            set
            {
                if (soLuongTon >= 0)
                    giaNhap = value;
                else
                    throw new ArgumentException("Số lượng tồn phải từ 1000 trở lên và không được để trống", "GiaBan");
            }
        }

        public NguyenLieuDTO()
        {
            soLuongTon = 0;  
        }

        public string MoTa
        {
            get { return moTa; }
            set { moTa = value; }
        }
        public string DonVi
        {
            get { return donVi; }
            set { donVi = value; }
        }
        public int DanhMucID
        {
            get { return danhMucID; }
            set
            {
                if (value > 0)
                    danhMucID = value;
                else
                    throw new ArgumentException("Mã Danh mục phải lớn hơn 0 và Không được để trống", "DanhMucID");
            }
        }
        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }
    }
}
