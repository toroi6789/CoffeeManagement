using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace GUI
{
    public partial class ListSanPham : UserControl
    {
        private SanPhamBUS sanPhamBUS = new SanPhamBUS();
        private DanhMucBUS danhMucBUS = new DanhMucBUS();
        private string originalImagePath = string.Empty;

        public ListSanPham()
        {
            InitializeComponent();
            this.Load += ListSanPham_Load;
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanel2.WrapContents = true;
            this.flowLayoutPanel2.Padding = new Padding(10);
            this.flowLayoutPanel2.BackColor = Color.FromArgb(245, 245, 245);
            txtTim.KeyDown += TxtTim_KeyDown;
            cmbDanhMuc.SelectedIndexChanged += cmbDanhMuc_SelectedIndexChanged;
        }

        private void ListSanPham_Load(object sender, EventArgs e)
        {
            LoadDanhMucVaoComboBox();
            HienThiTatCaSanPham();         
        }

        // === HIỂN THỊ TẤT CẢ SẢN PHẨM ===
        private void HienThiTatCaSanPham()
        {
            var listSP = sanPhamBUS.LayTatCaSanPham()
            .Where(sp => string.IsNullOrEmpty(sp.TrangThai) || sp.TrangThai == "Hoạt động") // CHỈ HIỆN "HOẠT ĐỘNG"
            .ToList();
            HienThiSanPham(listSP);
        }

        

        // Load danh mục vào cmbDanhMuc
        private void LoadDanhMucVaoComboBox()
        {
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel2.Controls.Clear();

            var danhMucList = danhMucBUS.LayTatCaDanhMuc();

            // Thêm mục "Tất cả"
            var allItem = new DanhMucDTO
            {
                DanhMucID = 0,
                TenDanhMuc = "Tất cả danh mục",
                TrangThai = "Hoạt động"
            };
            danhMucList.Insert(0, allItem);

            cmbDanhMuc.DataSource = danhMucList;
            cmbDanhMuc.DisplayMember = "TenDanhMuc";  // Hiển thị tên
            cmbDanhMuc.ValueMember = "DanhMucID";     // Lấy ID
            cmbDanhMuc.SelectedIndex = 0;

            flowLayoutPanel2.ResumeLayout();
        }

        // Lọc sản phẩm
        private void LocSanPham()
        {
            // LẤY DỮ LIỆU TỪ BUS
            var tatCaSP = sanPhamBUS.LayTatCaSanPham();

            // === BƯỚC QUAN TRỌNG: CHỈ LẤY SẢN PHẨM "HOẠT ĐỘNG" ===
            tatCaSP = tatCaSP
                .Where(sp => string.IsNullOrEmpty(sp.TrangThai) || sp.TrangThai == "Hoạt động")
                .ToList();

            // LẤY DANH MỤC HIỆN TẠI
            var dm = cmbDanhMuc.SelectedItem as DanhMucDTO;
            int danhMucID = dm?.DanhMucID ?? 0;

            // LẤY TỪ KHÓA TÌM KIẾM
            string tuKhoa = txtTim.Text.Trim();

            // DÙNG LINQ ĐỂ LỌC
            var ketQua = tatCaSP.AsQueryable();

            // 1. LỌC THEO DANH MỤC
            if (danhMucID > 0)
            {
                ketQua = ketQua.Where(sp => sp.DanhMucID == danhMucID);
            }

            // 2. LỌC THEO TÊN (nếu có từ khóa)
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                errorProvider1.SetError(txtTim, "");
                ketQua = ketQua.Where(sp =>
                    sp.TenSanPham != null &&
                    sp.TenSanPham.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

                // HIỂN THỊ KẾT QUẢ
            var danhSach = ketQua.ToList();
            HienThiSanPham(danhSach);           
        }



        // === HIỂN THỊ DANH SÁCH SẢN PHẨM VÀO FLOWLAYOUTPANEL ===
        private void HienThiSanPham(List<SanPhamDTO> danhSach)
        {
            flowLayoutPanel2.Controls.Clear();
            if (danhSach == null || !danhSach.Any())
            {
                var lbl = new Label
                {
                    Text = "Không có sản phẩm nào.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(20)
                };
                flowLayoutPanel2.Controls.Add(lbl);
                return;
            }

            // Tạo tối đa 50 Product control
            var listItems = new Product[Math.Min(danhSach.Count, 50)];

            for (int i = 0; i < listItems.Length && i < danhSach.Count; i++)
            {
                var sp = danhSach[i];
                listItems[i] = new Product
                {
                    Tensanpham = sp.TenSanPham,
                    Giaban = (double)sp.GiaBan,
                    URL = LayDuongDanAnh(sp.Hinh)
                };
                flowLayoutPanel2.Controls.Add(listItems[i]);
            }
        }

        // === LẤY ĐƯỜNG DẪN ẢNH (AN TOÀN) ===
        private string LayDuongDanAnh(string tenFile)
        {
            if (string.IsNullOrEmpty(tenFile)) 
                return Path.Combine(Application.StartupPath, @"Images\null.png");
            string fullPath = Path.Combine(Application.StartupPath, "Images", tenFile);
            return File.Exists(fullPath) ? fullPath : Path.Combine(Application.StartupPath, @"Images\null.png");
        }

        // === BẮT SỰ KIỆN ENTER TRONG TXT TIM ===
        private void TxtTim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TimKiemSanPham();
                e.Handled = true;
                e.SuppressKeyPress = true; // Ngăn tiếng "ding"
                LocSanPham();
            }
        }

        // === NÚT TÌM (nếu bạn có button1) ===
        private void button1_Click(object sender, EventArgs e)
        {
            //TimKiemSanPham();
            var txt = txtTim.Text.Trim();
            if (string.IsNullOrWhiteSpace(txt))
            {

                MessageBox.Show(
                "Vui lòng nhập tên sản phẩm cần tìm!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

                errorProvider1.SetError(txtTim, "Vui lòng nhập tên sản phẩm cần tìm!");

            }
            LocSanPham();
        }

        // === XÓA LỖI KHI GÕ ===
        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTim.Text))
            {
                errorProvider1.SetError(txtTim, "");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTim.Clear();                   
            errorProvider1.SetError(txtTim, "");
            //HienThiTatCaSanPham();
            cmbDanhMuc.SelectedIndex = 0;
            LocSanPham();
        }

        private void cmbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocSanPham();
        }

        private void cmbDanhMuc_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

