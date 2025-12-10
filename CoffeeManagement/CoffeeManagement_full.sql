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
    SoLuongTon    DECIMAL(18,2)          NOT NULL DEFAULT 0,
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
    ThanhTien       DECIMAL(18,2) NULL,
    HoaDonID        INT           NOT NULL,
    SanPhamID       INT           NOT NULL,
    CONSTRAINT fk_cthd_hoadon
        FOREIGN KEY (HoaDonID)  REFERENCES HoaDon(HoaDonID),
    CONSTRAINT fk_cthd_sanpham
        FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
) ENGINE=InnoDB;

DELIMITER //
CREATE TRIGGER trg_CTHD_ThanhTien
BEFORE INSERT ON ChiTietHoaDon
FOR EACH ROW
BEGIN
    SET NEW.ThanhTien = NEW.SoLuong * NEW.DonGia;
END //
DELIMITER ;

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
('Nguyễn', 'An', '0909000001', 'Trống lịch', '2024-01-10', 1),
('Trần', 'Bình', '0909000002', 'Trống lịch', '2024-02-05', 2),
('Lê', 'Cường', '0909000003', 'Trống lịch', '2024-02-20', 3),
('Phạm', 'Duyên', '0909000004', 'Trống lịch', '2024-03-01', 4),
('Hoàng', 'Em', '0909000005', 'Trống lịch', '2024-03-15', 5);

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
('Trà đào cam sả', 'Hoạt động', 'Trà trái cây', 35000, 5, 'tradaocamsa.png'),
('Cà phê đen đá', 'Hoạt động', 'Cà phê pha phin truyền thống', 20000, 1, 'caphedenda.png'),
('Trà vải hoa hồng', 'Hoạt động', 'Trà trái cây với vải', 32000, 2, 'travaihong.jpg'),
('Sinh tố dứa', 'Hoạt động', 'Sinh tố dứa tươi', 28000, 3, 'sinhtodua.png'),
('Bánh cookie socola', 'Hoạt động', 'Bánh ngọt giòn', 18000, 4, 'cookiesocola.jpg'),
('Hồng trà latte', 'Hoạt động', 'Trà sữa vị hồng trà', 30000, 2, 'hongtralatte.jpg');

-- 7. Nhà cung cấp
INSERT INTO NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, Website, TrangThai) VALUES
('CTY Nguyên liệu A', '123 Đường 1, HCM', '0908111222', 'contact@ctya.vn', 'https://ctya.vn', 'Hoạt động'),
('CTY Nguyên liệu B', '456 Đường 2, HCM', '0911222333', 'info@ctyb.vn', NULL, 'Hoạt động');

-- 8. Nguyên liệu
INSERT INTO NguyenLieu (TenNguyenLieu, TrangThai, MoTa, DonVi, GiaNhap, SoLuongTon, DanhMucID, Hinh) VALUES
('Cà phê hạt', 'Hoạt động', 'Nguyên liệu pha phin', 'kg', 180000, 20, 5.00,'hatcafe.jpg'),
('Đào miếng', 'Hoạt động', 'Nguyên liệu trà đào', 'kg', 150000, 10, 5.00,'daomieng.jpg'),
('Xoài tươi', 'Hoạt động', 'Sinh tố xoài', 'kg', 40000, 15, 5,'xoaituoi.jpg'),
('Đường cát', 'Hoạt động', 'Sử dụng chung', 'kg', 20000, 30, 5,'duongcat.jpg'),
('Bột làm bánh', 'Hoạt động', 'Nguyên liệu bánh', 'kg', 50000, 12, 5,'botlambanh.jpg'),
('Pepsi lon', 'Hoạt động', 'Lon Pepsi', 'lon', 10000, 20, 5, 'pepsilon.png'),
('Siro vải', 'Hoạt động', 'Sử dụng cho trà trái cây', 'ml', 90000, 5000, 5, 'sirovai.jpg'),
('Dứa tươi', 'Hoạt động', 'Nguyên liệu sinh tố dứa', 'kg', 35000, 20, 5, 'duatuoi.jpg'),
('Bột cacao', 'Hoạt động', 'Dùng làm bánh và topping', 'kg', 120000, 10, 5, 'botcacao.jpg'),
('Trà đen', 'Hoạt động', 'Nguyên liệu hồng trà latte', 'kg', 80000, 15, 5, 'traden.jpg'),
('Sữa đặc', 'Hoạt động', 'Dùng pha cà phê', 'lon', 20000, 50, 5, 'suadac.jpg');

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
INSERT INTO HoaDon (TrangThai, TongTien, PhuongThucThanhToan, BanID, NhanVienID, KhuyenMaiID, NgayKhoiTao) VALUES
('Đã thanh toán', 100000, 'Tiền mặt', 1, 1, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 105000, 'Chuyển khoản', 2, 2, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 78000, 'Tiền mặt', 3, 3, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 115000, 'Ví điện tử', 4, 4, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 97000, 'Tiền mặt', 1, 2, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 95000, 'Chuyển khoản', 2, 3, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 101000, 'Tiền mặt', 3, 4, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 127000, 'Ví điện tử', 4, 1, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 60000, 'Tiền mặt', 1, 2, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 112000, 'Tiền mặt', 2, 3, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 103000, 'Chuyển khoản', 3, 4, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 97000, 'Tiền mặt', 4, 1, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 86000, 'Ví điện tử', 1, 2, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 90000, 'Tiền mặt', 2, 3, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 80000, 'Chuyển khoản', 3, 4, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 115000, 'Tiền mặt', 4, 1, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 67000, 'Ví điện tử', 1, 2, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 83000, 'Tiền mặt', 2, 3, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 101000, 'Chuyển khoản', 3, 4, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY),
('Đã thanh toán', 91000, 'Tiền mặt', 4, 1, NULL, CURRENT_TIMESTAMP - INTERVAL 1 DAY);

-- Thêm 20 hóa đơn (ID 21 - 40)
INSERT INTO HoaDon (TrangThai, TongTien, PhuongThucThanhToan, BanID, NhanVienID, KhuyenMaiID, NgayKhoiTao) VALUES
('Đã thanh toán', 75000,  'Tiền mặt',       1, 2, NULL, '2021-03-12 09:15:00'),
('Đã thanh toán', 83000,  'Chuyển khoản',   2, 3, NULL, '2021-08-21 14:40:00'),
('Đã thanh toán', 48000,  'Tiền mặt',       3, 4, NULL, '2021-10-02 11:22:00'),
('Đã thanh toán', 65000,  'Ví điện tử',     4, 1, NULL, '2021-12-11 16:55:00'),

('Đã thanh toán', 67000,  'Tiền mặt',       1, 3, NULL, '2022-03-05 10:12:00'),
('Đã thanh toán', 58000,  'Tiền mặt',       2, 4, NULL, '2022-07-19 17:20:00'),
('Đã thanh toán', 96000,  'Chuyển khoản',   3, 1, NULL, '2022-10-08 09:45:00'),
('Đã thanh toán', 45000,  'Ví điện tử',     4, 2, NULL, '2022-12-28 20:30:00'),

('Đã thanh toán', 54000,  'Tiền mặt',       1, 3, NULL, '2023-01-15 13:00:00'),
('Đã thanh toán', 87000,  'Tiền mặt',       2, 4, NULL, '2023-04-20 18:32:00'),
('Đã thanh toán', 72000,  'Ví điện tử',     3, 1, NULL, '2023-09-11 08:22:00'),
('Đã thanh toán', 60000,  'Chuyển khoản',   4, 2, NULL, '2023-11-29 19:50:00'),

('Đã thanh toán', 68000,  'Tiền mặt',       1, 3, NULL, '2024-02-18 09:10:00'),
('Đã thanh toán', 76000,  'Ví điện tử',     2, 4, NULL, '2024-06-12 15:45:00'),
('Đã thanh toán', 90000,  'Tiền mặt',       3, 1, NULL, '2024-09-03 11:33:00'),
('Đã thanh toán', 52000,  'Chuyển khoản',   4, 2, NULL, '2024-12-22 20:11:00'),

('Đã thanh toán', 80000,  'Ví điện tử',     1, 3, NULL, '2025-01-25 10:25:00'),
('Đã thanh toán', 66000,  'Tiền mặt',       2, 4, NULL, '2025-04-14 16:40:00'),
('Đã thanh toán', 97000,  'Chuyển khoản',   3, 1, NULL, '2025-07-19 09:18:00'),
('Đã thanh toán', 84000,  'Tiền mặt',       4, 2, NULL, '2025-11-03 21:55:00');

-- 13. Chi tiết hóa đơn
INSERT INTO ChiTietHoaDon (SoLuong, DonGia, ThanhTien, HoaDonID, SanPhamID) VALUES
-- HD1
(2, 25000, 50000, 1, 1),
(1, 30000, 30000, 1, 2),
(1, 20000, 20000, 1, 3),

-- HD2
(1, 15000, 15000, 2, 4),
(2, 35000, 70000, 2, 5),
(1, 20000, 20000, 2, 6),

-- HD3
(1, 32000, 32000, 3, 7),
(1, 28000, 28000, 3, 8),
(1, 18000, 18000, 3, 9),

-- HD4
(2, 30000, 60000, 4, 10),
(1, 25000, 25000, 4, 1),
(1, 30000, 30000, 4, 2),

-- HD5
(1, 35000, 35000, 5, 5),
(1, 32000, 32000, 5, 7),
(2, 15000, 30000, 5, 4),

-- HD6
(2, 20000, 40000, 6, 3),
(1, 30000, 30000, 6, 10),
(1, 25000, 25000, 6, 1),

-- HD7
(1, 30000, 30000, 7, 2),
(2, 18000, 36000, 7, 9),
(1, 35000, 35000, 7, 5),

-- HD8
(1, 28000, 28000, 8, 8),
(2, 32000, 64000, 8, 7),
(1, 35000, 35000, 8, 5),

-- HD9
(1, 20000, 20000, 9, 6),
(1, 15000, 15000, 9, 4),
(1, 25000, 25000, 9, 1),

-- HD10
(2, 30000, 60000, 10, 10),
(1, 32000, 32000, 10, 7),
(1, 20000, 20000, 10, 6),

-- HD11
(1, 35000, 35000, 11, 5),
(2, 25000, 50000, 11, 1),
(1, 18000, 18000, 11, 9),

-- HD12
(1, 30000, 30000, 12, 2),
(1, 35000, 35000, 12, 5),
(1, 32000, 32000, 12, 7),

-- HD13
(2, 15000, 30000, 13, 4),
(2, 18000, 36000, 13, 9),
(1, 20000, 20000, 13, 3),

-- HD14
(1, 25000, 25000, 14, 1),
(1, 35000, 35000, 14, 5),
(1, 30000, 30000, 14, 2),

-- HD15
(1, 28000, 28000, 15, 8),
(1, 20000, 20000, 15, 6),
(1, 32000, 32000, 15, 7),

-- HD16
(2, 35000, 70000, 16, 5),
(1, 30000, 30000, 16, 2),
(1, 15000, 15000, 16, 4),

-- HD17
(1, 20000, 20000, 17, 6),
(1, 32000, 32000, 17, 7),
(1, 15000, 15000, 17, 4),

-- HD18
(1, 25000, 25000, 18, 1),
(1, 28000, 28000, 18, 8),
(1, 30000, 30000, 18, 2),

-- HD19
(2, 18000, 36000, 19, 9),
(1, 35000, 35000, 19, 5),
(1, 30000, 30000, 19, 10),

-- HD20
(1, 35000, 35000, 20, 5),
(1, 20000, 20000, 20, 3),
(2, 18000, 36000, 20, 9);

-- Chi tiết hóa đơn cho hóa đơn 21-40
INSERT INTO ChiTietHoaDon (HoaDonID, SanPhamID, SoLuong, DonGia) VALUES
(21, 1, 2, 25000), (21, 4, 1, 15000),

(22, 2, 1, 30000), (22, 5, 1, 35000), (22, 3, 1, 20000),

(23, 4, 2, 15000), (23, 9, 1, 18000),

(24, 6, 2, 20000), (24, 3, 1, 20000), (24, 4, 1, 15000),

(25, 1, 1, 25000), (25, 7, 1, 32000), (25, 4, 1, 15000),

(26, 3, 1, 20000), (26, 9, 2, 18000),

(27, 5, 1, 35000), (27, 2, 1, 30000), (27, 8, 1, 28000),

(28, 4, 3, 15000),

(29, 6, 1, 20000), (29, 1, 1, 25000), (29, 9, 1, 18000),

(30, 2, 1, 30000), (30, 10, 1, 30000), (30, 4, 1, 15000),

(31, 5, 2, 35000),

(32, 3, 2, 20000), (32, 6, 1, 20000),

(33, 7, 1, 32000), (33, 1, 2, 25000),

(34, 8, 1, 28000), (34, 10, 1, 30000),

(35, 2, 1, 30000), (35, 5, 1, 35000), (35, 6, 1, 20000),

(36, 4, 2, 15000), (36, 1, 1, 25000),

(37, 9, 2, 18000), (37, 7, 1, 32000), (37, 3, 1, 20000),

(38, 10, 1, 30000), (38, 6, 1, 20000), (38, 8, 1, 28000),

(39, 5, 1, 35000), (39, 2, 1, 30000), (39, 9, 1, 18000),

(40, 1, 2, 25000), (40, 3, 2, 20000), (40, 4, 1, 15000);


-- 14. Sản phẩm – Nguyên liệu
INSERT INTO SanPhamNguyenLieu (SanPhamID, NguyenLieuID, SoLuongSuDung) VALUES
(1, 1, 0.02),
(1, 4, 0.01),
(2, 3, 0.03),
(2, 4, 0.01),
(3, 6, 1),
(4, 5, 0.10),
(5, 2, 0.20),
(5, 4, 0.03),
-- Cà phê đen đá (ID 6)
(6, 1, 0.02),     -- Cà phê hạt
(6, 4, 0.01),     -- Đường cát
(6, 11, 0.05),    -- Sữa đặc
-- Trà vải hoa hồng (ID 7)
(7, 7, 20),       -- Siro vải
(7, 4, 0.01),     -- Đường
-- Sinh tố dứa (ID 8)
(8, 8, 0.25),     -- Dứa tươi
(8, 4, 0.01),     -- Đường
(9, 9, 0.10),     -- Bột cacao
(9, 5, 0.20),     -- Bột làm bánh
-- Hồng trà latte (ID 10)
(10, 10, 0.05),   -- Trà đen
(10, 11, 0.03);   -- Sữa đặc

-- 15. Thanh toán (khớp ngày với Hóa đơn)
INSERT INTO ThanhToan (SoTien, PhuongThuc, TrangThai, HoaDonID, NhanVienID, NgayThanhToan) VALUES
(100000, 'Tiền mặt',       'Hoàn tất', 1, 1, '2021-02-15 10:12:00'),
(105000, 'Chuyển khoản',   'Hoàn tất', 2, 2, '2021-07-03 14:45:00'),
(78000,  'Tiền mặt',       'Hoàn tất', 3, 3, '2021-11-22 09:20:00'),
(115000, 'Ví điện tử',     'Hoàn tất', 4, 4, '2021-12-30 18:10:00'),

(97000,  'Tiền mặt',       'Hoàn tất', 5, 2, '2022-01-12 13:30:00'),
(95000,  'Chuyển khoản',   'Hoàn tất', 6, 3, '2022-04-08 16:05:00'),
(101000, 'Tiền mặt',       'Hoàn tất', 7, 4, '2022-09-25 11:50:00'),
(127000, 'Ví điện tử',     'Hoàn tất', 8, 1, '2022-12-18 20:22:00'),

(60000,  'Tiền mặt',       'Hoàn tất', 9, 2, '2023-03-02 08:40:00'),
(112000, 'Tiền mặt',       'Hoàn tất', 10, 3, '2023-06-14 15:18:00'),
(103000, 'Chuyển khoản',   'Hoàn tất', 11, 4, '2023-09-07 17:52:00'),
(97000,  'Tiền mặt',       'Hoàn tất', 12, 1, '2023-12-29 10:05:00'),

(86000,  'Ví điện tử',     'Hoàn tất', 13, 2, '2024-02-09 12:00:00'),
(90000,  'Tiền mặt',       'Hoàn tất', 14, 3, '2024-05-23 19:40:00'),
(80000,  'Chuyển khoản',   'Hoàn tất', 15, 4, '2024-08-11 09:28:00'),
(115000, 'Tiền mặt',       'Hoàn tất', 16, 1, '2024-11-05 21:12:00'),

(67000,  'Ví điện tử',     'Hoàn tất', 17, 2, '2025-01-16 11:45:00'),
(83000,  'Tiền mặt',       'Hoàn tất', 18, 3, '2025-03-20 15:10:00'),
(101000, 'Chuyển khoản',   'Hoàn tất', 19, 4, '2025-06-30 17:35:00'),
(91000,  'Tiền mặt',       'Hoàn tất', 20, 1, '2025-10-08 20:55:00');

-- Thanh toán 20 hóa đơn mới (21-40)
INSERT INTO ThanhToan (SoTien, PhuongThuc, TrangThai, HoaDonID, NhanVienID, NgayThanhToan) VALUES
(75000,  'Tiền mặt',      'Hoàn tất', 21, 2, '2021-03-12 09:15:00'),
(83000,  'Chuyển khoản',  'Hoàn tất', 22, 3, '2021-08-21 14:40:00'),
(48000,  'Tiền mặt',      'Hoàn tất', 23, 4, '2021-10-02 11:22:00'),
(65000,  'Ví điện tử',    'Hoàn tất', 24, 1, '2021-12-11 16:55:00'),

(67000,  'Tiền mặt',      'Hoàn tất', 25, 3, '2022-03-05 10:12:00'),
(58000,  'Tiền mặt',      'Hoàn tất', 26, 4, '2022-07-19 17:20:00'),
(96000,  'Chuyển khoản',  'Hoàn tất', 27, 1, '2022-10-08 09:45:00'),
(45000,  'Ví điện tử',    'Hoàn tất', 28, 2, '2022-12-28 20:30:00'),

(54000,  'Tiền mặt',      'Hoàn tất', 29, 3, '2023-01-15 13:00:00'),
(87000,  'Tiền mặt',      'Hoàn tất', 30, 4, '2023-04-20 18:32:00'),
(72000,  'Ví điện tử',    'Hoàn tất', 31, 1, '2023-09-11 08:22:00'),
(60000,  'Chuyển khoản',  'Hoàn tất', 32, 2, '2023-11-29 19:50:00'),

(68000,  'Tiền mặt',      'Hoàn tất', 33, 3, '2024-02-18 09:10:00'),
(76000,  'Ví điện tử',    'Hoàn tất', 34, 4, '2024-06-12 15:45:00'),
(90000,  'Tiền mặt',      'Hoàn tất', 35, 1, '2024-09-03 11:33:00'),
(52000,  'Chuyển khoản',  'Hoàn tất', 36, 2, '2024-12-22 20:11:00'),

(80000,  'Ví điện tử',    'Hoàn tất', 37, 3, '2025-01-25 10:25:00'),
(66000,  'Tiền mặt',      'Hoàn tất', 38, 4, '2025-04-14 16:40:00'),
(97000,  'Chuyển khoản',  'Hoàn tất', 39, 1, '2025-07-19 09:18:00'),
(84000,  'Tiền mặt',      'Hoàn tất', 40, 2, '2025-11-03 21:55:00');

-- 16. đặt bàn
INSERT INTO DatBan (BanID, Ngay, GioBatDau, GioKetThuc) VALUES
(1, '2026-05-20', '08:00:00', '09:00:00'),
(2, '2026-05-20', '10:00:00', '11:00:00'),
(3, '2026-05-21', '14:00:00', '15:00:00'),
(4, '2026-05-21', '19:00:00', '20:00:00'),
(5, '2026-05-22', '09:00:00', '10:00:00');
