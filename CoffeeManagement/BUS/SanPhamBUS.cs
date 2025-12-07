using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeManagement.BUS
{
    public class SanPhamBUS
    {
        public static DataTable SanPham()
        {
            return SanPhamDAO.SanPham();
        }
        public static DataTable SanPhamTheoID(int sanPhamID)
        {
            return SanPhamDAO.SanPhamTheoID(sanPhamID);
        }

        //====================================================
        private SanPhamDAO dao = new SanPhamDAO();
        public List<SanPhamDTO> LayTatCaSanPham()
        {
            return dao.GetAll();
        }
        // LAY ID TIEP THEO
        public int GetNextId()
        {
            return dao.GetNextSanPhamId();
        }
        // KIEM TRA ID TRUNG
        public bool KiemTraIdTrung(int id)
        {
            return dao.IsSanPhamIdExists(id);
        }

        // BUS them san pham
        public bool busThemSanPham(SanPhamDTO sp, out string message, out string errorField)
        {
            message = "";
            errorField = "";

            // === 1. KIỂM TRA ID ===
            if (sp.SanPhamID < 0)
            {
                message = "Mã sản phẩm phải lớn hơn 0.";
                errorField = "SanPhamID";
                return false;
            }

            // Kiểm tra trùng ID trong DB
            if (dao.IsSanPhamIdExists(sp.SanPhamID))
            {
                message = $"Mã sản phẩm {sp.SanPhamID} đã tồn tại.";
                errorField = "SanPhamID";
                return false;
            }

            // === 2. KIỂM TRA TÊN SẢN PHẨM ===
            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
            {
                message = "Tên sản phẩm không được để trống.";
                errorField = "TenSanPham";
                return false;
            }

            // === 3. KIỂM TRA GIÁ BÁN ===
            if (sp.GiaBan < 0)
            {
                message = "Giá bán phải lớn hơn hoặc 0.";
                errorField = "GiaBan";
                return false;
            }

            // === 4. KIỂM TRA DANH MỤC (nếu có) ===
            if (sp.DanhMucID <= 0)
            {
                message = "Vui lòng chọn danh mục.";
                errorField = "DanhMucID";
                return false;
            }

            // === 5. GỌI DAL THÊM SẢN PHẨM ===
            bool kq = dao.daoThemSanPham(sp);

            if (kq)
            {
                message = "Thêm sản phẩm thành công!";
                return true;
            }
            else
            {
                message = "Thêm sản phẩm thất bại!";
                errorField = "Database";
                return false;
            }
        }

        // Sua san pham
        public bool busSuaSanPham(SanPhamDTO sp, out string message, out string errorField)
        {
            message = ""; errorField = "";

            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
            {
                message = "Tên sản phẩm không được để trống.";
                errorField = "TenSanPham";
                return false;
            }

            if (sp.GiaBan < 0)
            {
                message = "Giá bán phải lớn hơn hoặc bằng 0.";
                errorField = "GiaBan";
                return false;
            }

            if (sp.DanhMucID <= 0)
            {
                message = "Mã danh mục không hợp lệ.";
                errorField = "DanhMucID";
                return false;
            }

            bool kq = dao.daoSuaSanPham(sp);
            message = kq ? "Sửa sản phẩm thành công!" : "Sửa thất bại!";
            return kq;
        }

        // XOA SAN PHAM
        public bool busXoaSanPham(int sanPhamID, out string message)
        {
            message = "";

            if (sanPhamID <= 0)
            {
                message = "ID sản phẩm không hợp lệ.";
                return false;
            }
            // GỌI DAL XÓA
            bool kq = dao.daoCapNhatTrangThaiSanPham(sanPhamID, "Deleted");
            message = kq ? "Xóa sản phẩm thành công!" : "Xóa thất bại! Có thể sản phẩm đang được sử dụng.";
            return kq;
        }

        // LẤY ID LỚN NHẤT TRONG DATABASE + 1
        public int LaySanPhamIDLonNhat()
        {
            try
            {
                return dao.LaySanPhamIDLonNhat(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BUS lấy ID lớn nhất: " + ex.Message);
            }
        }

        // KIEM TRA SAN PHAM HET HANG
        public static void KiemTraSanPhamHetHang()
        {
            SanPhamDAO.KiemTraSanPhamHetHang();
        }
    }
}
