-- ======================================
--  DATABASE: CoffeeManagement
-- ======================================
-- ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY '';
-- FLUSH PRIVILEGES;



DROP DATABASE IF EXISTS CoffeeManagement;
CREATE DATABASE CoffeeManagement
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;
USE CoffeeManagement;

-- ======================================
-- 1. ROLE & USER
-- ======================================
CREATE TABLE `Role` (
    RoleID      INT AUTO_INCREMENT PRIMARY KEY,
    TenRole     VARCHAR(50)  NOT NULL,
    MoTa        VARCHAR(255) NULL
) ENGINE=InnoDB;

CREATE TABLE `User` (
    UserID             INT AUTO_INCREMENT PRIMARY KEY,
    TrangThai          INT NOT NULL DEFAULT 1,
    Email              VARCHAR(100) NOT NULL UNIQUE,
    MatKhau            VARCHAR(100) NOT NULL,
    NgayDangNhapCuoi   DATETIME     NULL,
    NgayCapNhat        DATETIME     NULL,
    NgayKhoiTao        DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    RoleID             INT          NOT NULL,
    CONSTRAINT fk_user_role
        FOREIGN KEY (RoleID) REFERENCES `Role`(RoleID)
) ENGINE=InnoDB;

-- ======================================
-- 2. NHÂN VIÊN
-- ======================================
CREATE TABLE NhanVien (
    NhanVienID   INT AUTO_INCREMENT PRIMARY KEY,
    Ho           VARCHAR(50)  NULL,
    Ten          VARCHAR(50)  NULL,
    Phone        VARCHAR(20)  NULL,
    TrangThai    VARCHAR(20)  NULL,
    DateJoin     DATE         NULL,
    NgayCapNhat  DATETIME     NULL,
    NgayKhoiTao  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UserID       INT          NOT NULL,
    CONSTRAINT fk_nhanvien_user
        FOREIGN KEY (UserID) REFERENCES `User`(UserID)
) ENGINE=InnoDB;

-- ======================================
-- 3. BÀN
-- ======================================
CREATE TABLE Ban (
    BanID      INT AUTO_INCREMENT PRIMARY KEY,
    TenBan     VARCHAR(50) NOT NULL,
    SucChua    INT         NULL,
    TrangThai  VARCHAR(20) NULL
) ENGINE=InnoDB;

CREATE TABLE DatBan (
    DatBanID INT AUTO_INCREMENT PRIMARY KEY,
    BanID INT NOT NULL,
    Ngay DATE NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    FOREIGN KEY (BanID) REFERENCES Ban(BanID)
);

-- ======================================
-- 4. DANH MỤC SẢN PHẨM / NGUYÊN LIỆU
-- ======================================
CREATE TABLE DanhMuc (
    DanhMucID   INT AUTO_INCREMENT PRIMARY KEY,
    TenDanhMuc  VARCHAR(100) NOT NULL,
    TrangThai   VARCHAR(20)  NULL,
    MoTa        VARCHAR(255) NULL,
    GiaBan      DECIMAL(18,2) DEFAULT 0
) ENGINE=InnoDB;

-- ======================================
-- 5. SẢN PHẨM
-- ======================================
CREATE TABLE SanPham (
    SanPhamID   INT AUTO_INCREMENT PRIMARY KEY,
    TenSanPham  VARCHAR(100) NOT NULL,
    TrangThai   VARCHAR(20)  NULL,
    MoTa        VARCHAR(255) NULL,
    GiaBan      DECIMAL(18,2) NOT NULL,
    DanhMucID   INT          NOT NULL,
    Hinh 		VARCHAR(20) NULL,
    CONSTRAINT fk_sanpham_danhmuc
        FOREIGN KEY (DanhMucID) REFERENCES DanhMuc(DanhMucID)
) ENGINE=InnoDB;

-- ======================================
-- 6. NHÀ CUNG CẤP
-- ======================================
CREATE TABLE NhaCungCap (
    NhaCungCapID  INT AUTO_INCREMENT PRIMARY KEY,
    TenNhaCungCap VARCHAR(150) NOT NULL,
    DiaChi        VARCHAR(255) NULL,
    SoDienThoai   VARCHAR(20)  NULL,
    Email         VARCHAR(100) NULL,
    Website       VARCHAR(150) NULL,
    TrangThai     VARCHAR(20)  NULL
) ENGINE=InnoDB;

-- ======================================
-- 7. NGUYÊN LIỆU
-- ======================================
CREATE TABLE NguyenLieu (
    NguyenLieuID  INT AUTO_INCREMENT PRIMARY KEY,
    TenNguyenLieu VARCHAR(150) NOT NULL,
    TrangThai     VARCHAR(20)  NULL,
    MoTa          VARCHAR(255) NULL,
    DonVi         VARCHAR(50)  NULL,
    GiaNhap       DECIMAL(18,2) NOT NULL,
    SoLuongTon    INT          NOT NULL DEFAULT 0,
    DanhMucID     INT          NOT NULL,
    Hinh 		VARCHAR(20) NULL,
    CONSTRAINT fk_nguyenlieu_danhmuc
        FOREIGN KEY (DanhMucID) REFERENCES DanhMuc(DanhMucID)
) ENGINE=InnoDB;

-- ======================================
-- 8. PHIẾU NHẬP
-- ======================================
CREATE TABLE PhieuNhap (
    PhieuNhapID   INT AUTO_INCREMENT PRIMARY KEY,
    NgayNhap      DATETIME     NOT NULL,
    TongTien      DECIMAL(18,2) DEFAULT 0,
    GhiChu        VARCHAR(255) NULL,
    TrangThai     VARCHAR(20)  NULL,
    NhanVienID    INT          NOT NULL,
    NhaCungCapID  INT          NOT NULL,
    CONSTRAINT fk_phieunhap_nhanvien
        FOREIGN KEY (NhanVienID)   REFERENCES NhanVien(NhanVienID),
    CONSTRAINT fk_phieunhap_nhacungcap
        FOREIGN KEY (NhaCungCapID) REFERENCES NhaCungCap(NhaCungCapID)
) ENGINE=InnoDB;

-- ======================================
-- 9. CHI TIẾT PHIẾU NHẬP
-- ======================================
CREATE TABLE ChiTietPhieuNhap (
    ChiTietPhieuNhapID INT AUTO_INCREMENT PRIMARY KEY,
    SoLuong            INT           NOT NULL,
    DonGia             DECIMAL(18,2) NOT NULL,
    ThanhTien          DECIMAL(18,2) NOT NULL,
    PhieuNhapID        INT           NOT NULL,
    NguyenLieuID       INT           NOT NULL,
    CONSTRAINT fk_ctpn_phieunhap
        FOREIGN KEY (PhieuNhapID)  REFERENCES PhieuNhap(PhieuNhapID),
    CONSTRAINT fk_ctpn_nguyenlieu
        FOREIGN KEY (NguyenLieuID) REFERENCES NguyenLieu(NguyenLieuID)
) ENGINE=InnoDB;

-- ======================================
-- 10. HÓA ĐƠN
-- ======================================
CREATE TABLE HoaDon (
    HoaDonID              INT AUTO_INCREMENT PRIMARY KEY,
    NgayKhoiTao           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TrangThai             VARCHAR(20)  NULL,
    TongTien              DECIMAL(18,2) DEFAULT 0,
    PhuongThucThanhToan   VARCHAR(50)  NULL,
    BanID                 INT          NULL,
    NhanVienID            INT          NOT NULL,
    KhuyenMaiID           INT          NULL,
    CONSTRAINT fk_hoadon_ban
        FOREIGN KEY (BanID)      REFERENCES Ban(BanID),
    CONSTRAINT fk_hoadon_nhanvien
        FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
) ENGINE=InnoDB;

-- ======================================
-- 11. KHUYẾN MÃI
-- ======================================
CREATE TABLE KhuyenMai (
    KhuyenMaiID   INT AUTO_INCREMENT PRIMARY KEY,
    TenKhuyenMai  VARCHAR(100) NOT NULL,
    LoaiKhuyenMai VARCHAR(50)  NULL,
    MoTa          VARCHAR(255) NULL,
    GiaTri        DECIMAL(18,2) NOT NULL,
    NgayBatDau    DATE         NOT NULL,
    NgayKetThuc   DATE         NOT NULL,
    TrangThai     VARCHAR(20)  NULL
) ENGINE=InnoDB;

ALTER TABLE HoaDon
    ADD CONSTRAINT fk_hoadon_khuyenmai
        FOREIGN KEY (KhuyenMaiID) REFERENCES KhuyenMai(KhuyenMaiID);

-- ======================================
-- 12. CHI TIẾT HÓA ĐƠN
-- ======================================
CREATE TABLE ChiTietHoaDon (
    ChiTietHoaDonID INT AUTO_INCREMENT PRIMARY KEY,
    SoLuong         INT           NOT NULL,
    DonGia          DECIMAL(18,2) NOT NULL,
    ThanhTien       DECIMAL(18,2) NOT NULL,
    HoaDonID        INT           NOT NULL,
    SanPhamID       INT           NOT NULL,
    CONSTRAINT fk_cthd_hoadon
        FOREIGN KEY (HoaDonID)  REFERENCES HoaDon(HoaDonID),
    CONSTRAINT fk_cthd_sanpham
        FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
) ENGINE=InnoDB;

-- ======================================
-- 13. SẢN PHẨM – NGUYÊN LIỆU
-- ======================================
CREATE TABLE SanPhamNguyenLieu (
    SanPhamID     INT NOT NULL,
    NguyenLieuID  INT NOT NULL,
    SoLuongSuDung DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (SanPhamID, NguyenLieuID),
    CONSTRAINT fk_spnl_sanpham
        FOREIGN KEY (SanPhamID)    REFERENCES SanPham(SanPhamID),
    CONSTRAINT fk_spnl_nguyenlieu
        FOREIGN KEY (NguyenLieuID) REFERENCES NguyenLieu(NguyenLieuID)
) ENGINE=InnoDB;

-- ======================================
-- 14. THANH TOÁN
-- ======================================
CREATE TABLE ThanhToan (
    ThanhToanID   INT AUTO_INCREMENT PRIMARY KEY,
    SoTien        DECIMAL(18,2) NOT NULL,
    PhuongThuc    VARCHAR(50)   NULL,
    NgayThanhToan DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TrangThai     VARCHAR(20)   NULL,
    HoaDonID      INT           NOT NULL,
    NhanVienID    INT           NOT NULL,
    CONSTRAINT fk_thanhtoan_hoadon
        FOREIGN KEY (HoaDonID)  REFERENCES HoaDon(HoaDonID),
    CONSTRAINT fk_thanhtoan_nhanvien
        FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
) ENGINE=InnoDB;

-- ======================================
-- DỮ LIỆU MẪU
-- ======================================

-- 1. Role
INSERT INTO `Role` (TenRole, MoTa) VALUES
('Quản trị viên', 'Toàn quyền hệ thống'),
('Thu ngân', 'Xử lý hóa đơn & thu tiền'),
('Pha chế', 'Pha chế đồ uống'),
('Phục vụ', 'Tiếp nhận & phục vụ khách'),
('Quản lý kho', 'Quản lý nguyên liệu & nhập hàng');

-- 2. User
INSERT INTO `User` (TrangThai, Email, MatKhau, RoleID) VALUES
(1, 'admin@cafe.vn', '123456', 1),
(1, 'thungan1@cafe.vn', '123456', 2),
(1, 'phache1@cafe.vn', '123456', 3),
(1, 'phucvu1@cafe.vn', '123456', 4),
(1, 'kho1@cafe.vn', '123456', 5);

-- 3. Nhân viên
INSERT INTO NhanVien (Ho, Ten, Phone, TrangThai, DateJoin, UserID) VALUES
('Nguyễn', 'An', '0909000001', 'Đang làm', '2024-01-10', 1),
('Trần', 'Bình', '0909000002', 'Đang làm', '2024-02-05', 2),
('Lê', 'Cường', '0909000003', 'Đang làm', '2024-02-20', 3),
('Phạm', 'Duyên', '0909000004', 'Đang làm', '2024-03-01', 4),
('Hoàng', 'Em', '0909000005', 'Đang làm', '2024-03-15', 5);

-- 4. Bàn
INSERT INTO Ban (TenBan, SucChua, TrangThai) VALUES
('Bàn 1', 4, 'Trống'),
('Bàn 2', 4, 'Trống'),
('Bàn 3', 2, 'Trống'),
('Bàn 4', 6, 'Trống'),
('Bàn 5', 4, 'Trống');

-- 5. Danh mục
INSERT INTO DanhMuc (TenDanhMuc, TrangThai, MoTa, GiaBan) VALUES
('Cà phê', 'Hoạt động', 'Nhóm sản phẩm cà phê', 0),
('Trà', 'Hoạt động', 'Các loại trà trái cây', 0),
('Sinh tố', 'Hoạt động', 'Các loại sinh tố trái cây', 0),
('Đồ ăn nhẹ', 'Hoạt động', 'Bánh ngọt và snack', 0),
('Nguyên liệu', 'Hoạt động', 'Kho nguyên liệu', 0);

-- 6. Sản phẩm
INSERT INTO SanPham (TenSanPham, TrangThai, MoTa, GiaBan, DanhMucID, Hinh) VALUES
('Cà phê sữa đá', 'Hoạt động', 'Cà phê pha phin', 25000, 1,'caphesua.png'),
('Sinh tố xoài', 'Hoạt động', 'Sinh tố trái cây', 30000, 2,'sinhtoxoai.png'),
('Pepsi lon', 'Hoạt động', 'Nước giải khát', 20000, 3, 'pepsilon.png'),
('Bánh su kem', 'Hoạt động', 'Bánh ngọt mini', 15000, 4, 'banhsukem.png'),
('Trà đào cam sả', 'Hoạt động', 'Trà trái cây', 35000, 5, 'tradaocamsa.png');

-- 7. Nhà cung cấp
INSERT INTO NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, Website, TrangThai) VALUES
('CTY Nguyên liệu A', '123 Đường 1, HCM', '0908111222', 'contact@ctya.vn', 'https://ctya.vn', 'Hoạt động'),
('CTY Nguyên liệu B', '456 Đường 2, HCM', '0911222333', 'info@ctyb.vn', NULL, 'Hoạt động');

-- 8. Nguyên liệu
INSERT INTO NguyenLieu (TenNguyenLieu, TrangThai, MoTa, DonVi, GiaNhap, SoLuongTon, DanhMucID, Hinh) VALUES
('Cà phê hạt', 'Hoạt động', 'Nguyên liệu pha phin', 'kg', 180000, 20, 5,'tradaocamsa.png'),
('Đào miếng', 'Hoạt động', 'Nguyên liệu trà đào', 'kg', 150000, 10, 5,'tradaocamsa.png'),
('Xoài tươi', 'Hoạt động', 'Sinh tố xoài', 'kg', 40000, 15, 5,'tradaocamsa.png'),
('Đường cát', 'Hoạt động', 'Sử dụng chung', 'kg', 20000, 30, 5,'tradaocamsa.png'),
('Bột làm bánh', 'Hoạt động', 'Nguyên liệu bánh', 'kg', 50000, 12, 5,'tradaocamsa.png');

-- 9. Phiếu nhập
INSERT INTO PhieuNhap (NgayNhap, TongTien, GhiChu, TrangThai, NhanVienID, NhaCungCapID) VALUES
('2024-05-01 10:00:00', 3600000, 'Nhập nguyên liệu đầu tháng', 'Hoàn tất', 5, 1),
('2024-05-15 09:30:00', 1500000, 'Bổ sung nguyên liệu', 'Hoàn tất', 5, 2);

-- 10. Chi tiết phiếu nhập
INSERT INTO ChiTietPhieuNhap (SoLuong, DonGia, ThanhTien, PhieuNhapID, NguyenLieuID) VALUES
(10, 180000, 1800000, 1, 1),
(10, 150000, 1500000, 1, 2),
(10, 30000, 300000, 1, 4),
(10, 40000, 400000, 2, 3),
(10, 50000, 500000, 2, 5),
(10, 20000, 200000, 2, 4);

-- 11. Khuyến mãi
INSERT INTO KhuyenMai (TenKhuyenMai, LoaiKhuyenMai, MoTa, GiaTri, NgayBatDau, NgayKetThuc, TrangThai) VALUES
('Giảm 10% toàn menu', 'Phần trăm', 'Áp dụng giờ vàng 14h-17h', 10, '2024-05-01', '2024-05-31', 'Hoạt động'),
('Giảm 5.000đ đồ uống', 'Tiền mặt', 'Không áp dụng combo', 5000, '2024-05-10', '2024-05-20', 'Hoạt động');

-- 12. Hóa đơn
INSERT INTO HoaDon (TrangThai, TongTien, PhuongThucThanhToan, BanID, NhanVienID, KhuyenMaiID) VALUES
('Đã thanh toán', 60000, 'Tiền mặt', 1, 2, NULL),
('Đang phục vụ', 0, NULL, 2, 4, NULL),
('Đã thanh toán', 55000, 'Chuyển khoản', 3, 2, 1);

-- 13. Chi tiết hóa đơn
INSERT INTO ChiTietHoaDon (SoLuong, DonGia, ThanhTien, HoaDonID, SanPhamID) VALUES
(2, 25000, 50000, 1, 1),
(1, 10000, 10000, 1, 4),
(1, 20000, 20000, 3, 5),
(1, 35000, 35000, 3, 2);

-- 14. Sản phẩm – Nguyên liệu
INSERT INTO SanPhamNguyenLieu (SanPhamID, NguyenLieuID, SoLuongSuDung) VALUES
(1, 1, 0.02),
(1, 4, 0.01),
(2, 2, 0.03),
(2, 4, 0.01),
(3, 3, 0.25),
(4, 5, 0.10);

-- 15. Thanh toán
INSERT INTO ThanhToan (SoTien, PhuongThuc, TrangThai, HoaDonID, NhanVienID) VALUES
(60000, 'Tiền mặt', 'Hoàn tất', 1, 2),
(55000, 'Chuyển khoản', 'Hoàn tất', 3, 2);

-- 16. đặt bàn
INSERT INTO DatBan (BanID, Ngay, GioBatDau, GioKetThuc) VALUES
(1, '2026-05-20', '08:00:00', '09:00:00'),
(2, '2026-05-20', '10:00:00', '11:00:00'),
(3, '2026-05-21', '14:00:00', '15:00:00'),
(4, '2026-05-21', '19:00:00', '20:00:00'),
(5, '2026-05-22', '09:00:00', '10:00:00');
