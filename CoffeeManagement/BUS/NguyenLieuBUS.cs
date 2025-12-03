using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class NguyenLieuBUS
    {
        private NguyenLieuDAO dao = new NguyenLieuDAO();

        public List<NguyenLieuDTO> LayTatCaNguyenLieu()
        {
            return dao.GetAll();
        }
        // LAY ID TIEP THEO
        public int GetNextId()
        {
            return dao.GetNextNguyenLieuId();
        }
        // KIEM TRA ID TRUNG
        public bool KiemTraIdTrung(int id)
        {
            return dao.IsNguyenLieuIdExists(id);
        }

        // BUS them san pham
        public bool busThemNguyenLieu(NguyenLieuDTO nl, out string message, out string errorField)
        {
            message = "";
            errorField = "";

            // === 1. KIỂM TRA ID ===
            if (nl.NguyenLieuID < 0)
            {
                message = "Mã Nguyên liệu phải lớn hơn 0.";
                errorField = "NguyenLieuID";
                return false;
            }

            // Kiểm tra trùng ID trong DB
            if (dao.IsNguyenLieuIdExists(nl.NguyenLieuID))
            {
                message = $"Mã sản phẩm {nl.TenNguyenLieu} đã tồn tại.";
                errorField = "NguyenLieuID";
                return false;
            }

            // === 2. KIỂM TRA TÊN SẢN PHẨM ===
            if (string.IsNullOrWhiteSpace(nl.TenNguyenLieu))
            {
                message = "Tên nguyên liệu không được để trống.";
                errorField = "TenNguyenLieu";
                return false;
            }

            // === 3. KIỂM TRA GIÁ BÁN ===
            if (nl.GiaNhap < 0)
            {
                message = "Giá nhập phải lớn hơn hoặc bằng 0.";
                errorField = "GiaNhap";
                return false;
            }

            if (nl.SoLuongTon < 0)
            {
                message = "Số lượng tồn phải lớn hơn hoặc bằng 0.";
                errorField = "SoLuongTon";
                return false;
            }

            // === 4. KIỂM TRA DANH MỤC (nếu có) ===
            if (nl.DanhMucID <= 0)
            {
                message = "Vui lòng chọn danh mục.";
                errorField = "DanhMucID";
                return false;
            }

            if (string.IsNullOrWhiteSpace(nl.DonVi))
            {
                message = "Vui lòng nhập đơn vị.";
                errorField = "DonVi";
                return false;
            }

            // === 5. GỌI DAL THÊM SẢN PHẨM ===
            bool kq = dao.daoThemNguyenLieu(nl);

            if (kq)
            {
                message = "Thêm nguyên liệu thành công!";
                return true;
            }
            else
            {
                message = "Thêm nguyên liệu thất bại!";
                errorField = "Database";
                return false;
            }
        }

        // Sua san pham
        public bool busSuaNguyenLieu(NguyenLieuDTO nl, out string message, out string errorField)
        {
            message = ""; errorField = "";

            if (string.IsNullOrWhiteSpace(nl.TenNguyenLieu))
            {
                message = "Tên nguyên liệu không được để trống.";
                errorField = "TenNguyenLieu";
                return false;
            }

            if (nl.GiaNhap < 0)
            {
                message = "Giá nhập phải lớn hơn hoặc bằng 0.";
                errorField = "GiaNhap";
                return false;
            }

            if (nl.DanhMucID <= 0)
            {
                message = "Mã danh mục không hợp lệ.";
                errorField = "DanhMucID";
                return false;
            }

            bool kq = dao.daoSuaNguyenLieu(nl);
            message = kq ? "Sửa nguyên liệu thành công!" : "Sửa thất bại!";
            return kq;
        }

        // 
        public bool busXoaNguyenLieu(int nguyenLieuID, out string message)
        {
            message = "";

            if (nguyenLieuID <= 0)
            {
                message = "ID nguyên liệu không hợp lệ.";
                return false;
            }
            // GỌI DAL XÓA
            bool kq = dao.daoCapNhatTrangThaiNguyenLieu(nguyenLieuID, "Deleted");
            message = kq ? "Xóa sản phẩm thành công!" : "Xóa thất bại! Có thể sản phẩm đang được sử dụng.";
            return kq;
        }

        // LẤY ID LỚN NHẤT TRONG DATABASE + 1
        public int LayNguyenLieuIDLonNhat()
        {
            try
            {
                return dao.LayNguyenLieuIDLonNhat();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BUS lấy ID lớn nhất: " + ex.Message);
            }
        }
    }
}
