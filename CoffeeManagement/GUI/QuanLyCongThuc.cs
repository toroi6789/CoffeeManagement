using CoffeeManagement.BUS;
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

namespace CoffeeManagement.GUI
{
    public partial class QuanLyCongThuc : UserControl
    {
        private SanPhamBUS sp_bus = new SanPhamBUS();
        private DataTable dtSanPham;
        private string originalImagePath = string.Empty;
        private DanhMucBUS danhMucBUS = new DanhMucBUS();
        private SanPhamNguyenLieuBUS congthuc = new SanPhamNguyenLieuBUS();
        private bool DangThaoTac = false;
        public QuanLyCongThuc()
        {
            InitializeComponent();
        }

        private void SanPhamADMIN_Load(object sender, EventArgs e)
        {
            //
            List<string> trangThais = new List<string> { "Hoạt động", "Ngừng bán", "Deleted" };
            cmbTrangThai.DataSource = trangThais;
            cmbTrangThai.SelectedIndex = 0; // Mặc định "Hoạt động"

            btnSua.Enabled = false;
            btnXoa.Visible = false;
            btnThoat.Visible = false;
            btnThemAnh.Enabled = false;
            cmbDanhMucID.Enabled = false;
            cmbTrangThai.Enabled = false;

            //
            txtID.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtGia.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            txtSoLuongSuDung.ReadOnly = true;
            txtidNguyenLieu.ReadOnly = true;


            dtSanPham = new DataTable();
            dtSanPham.Columns.Add("STT", typeof(int));
            dtSanPham.Columns.Add("SanPhamID", typeof(int));
            dtSanPham.Columns.Add("TenSanPham", typeof(string));
            dtSanPham.Columns.Add("GiaBan", typeof(decimal));
            dtSanPham.Columns.Add("MoTa", typeof(string));
            dtSanPham.Columns.Add("TrangThai", typeof(string));
            dtSanPham.Columns.Add("DanhMucID", typeof(int));
            dtSanPham.Columns.Add("Hinh", typeof(string));



            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dtSanPham;
            //LocSanPham();

            dataGridView1.Columns["STT"].HeaderText = "STT";
            dataGridView1.Columns["SanPhamID"].HeaderText = "Mã Sản Phẩm";
            dataGridView1.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dataGridView1.Columns["GiaBan"].HeaderText = "Giá Bán";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dataGridView1.Columns["MoTa"].HeaderText = "Mô Tả";
            dataGridView1.Columns["DanhMucID"].HeaderText = "Mã Danh Mục";
            dataGridView1.Columns["Hinh"].HeaderText = "Hinh";

            dataGridView1.EnableHeadersVisualStyles = false; // ⚠️ Bắt buộc để màu custom có hiệu lực

            // ẨN CỘT SanPhamID(nếu không muốn hiện ID thật)
            dataGridView1.Columns["SanPhamID"].Visible = false;

            // Đặt lại tiêu đề và vị trí cột STT

            dataGridView1.Columns["STT"].Width = 50;
            dataGridView1.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Đưa cột STT ra đầu tiên
            dataGridView1.Columns["STT"].DisplayIndex = 0;

            // Chỉ đọc (nếu bạn chỉ muốn hiển thị)
            dataGridView1.ReadOnly = true;

            // Tự động điều chỉnh chiều cao dòng
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue; // Màu nền
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;     // Màu chữ
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold); // Font chữ
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   // Căn giữa header

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false; // Chỉ chọn 1 dòng tại 1 thời điểm

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;


            string relativePath = @"Images\null.png";
            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            try
            {
                if (File.Exists(fullPath))
                {
                    pictureBox1.LoadAsync(fullPath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy hình ảnh tại đường dẫn: {fullPath}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hình ảnh: " + ex.Message);
            }


            //LoadDanhMucVaoComboBox();
            //LocSanPham();
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtTenSP.Clear();
            txtGia.Clear();
            txtMoTa.Clear();
            txtidNguyenLieu.Clear();
            txtSoLuongSuDung.Clear();
            cmbTrangThai.SelectedIndex = 0; // Mặc định "Hoạt động"
            cmbDanhMucID.SelectedIndex = -1;

            string relativePath = @"Images\null.png";
            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            pictureBox1.LoadAsync(fullPath);
        }

        private void ResetForm()
        {
            btnXemvaThem.Text = "Xem Nguyên Liệu";
            btnSua.Text = "Sửa";
            btnThoat.Visible = false;
            btnThemAnh.Enabled = false;// ẨN NÚT HỦY
            btnXemvaThem.Enabled = true;
            txtID.Enabled = true;
            btnSua.Enabled = false;  // TẮT SỬA
            btnXoa.Enabled = false;// TẮT XÓA

            //
            txtID.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtGia.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            cmbTrangThai.Enabled = false;
            cmbDanhMucID.Enabled = false;

            ClearForm();
            //ClearErrorProvider();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DangThaoTac)
            {
                MessageBox.Show("Đang ở chế độ thêm/sửa. Vui lòng Lưu hoặc Hủy trước khi chọn nguyên liệu khác!",
                               "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["SanPhamID"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSanPham"].Value.ToString();
                txtGia.Text = row.Cells["GiaBan"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();

                // Set cho ComboBox
                cmbTrangThai.SelectedItem = row.Cells["TrangThai"].Value.ToString(); // Chọn theo string
                cmbDanhMucID.SelectedValue = row.Cells["DanhMucID"].Value; // Chọn theo Value (ID)

                // === HIỂN THỊ ẢNH ===
                string tenFileAnh = row.Cells["Hinh"].Value?.ToString(); // Lấy tên file từ DB
                string relativePath = @"Images\null.png";
                string fullPathDefault = Path.Combine(Application.StartupPath, relativePath);
                if (!string.IsNullOrEmpty(tenFileAnh))
                {
                    string fullPath = Path.Combine(Application.StartupPath, "Images", tenFileAnh);
                    if (File.Exists(fullPath))
                    {
                        pictureBox1.LoadAsync(fullPath);
                        originalImagePath = fullPath; // Lưu tạm để sửa sau
                    }
                    else
                    {
                        pictureBox1.LoadAsync(fullPathDefault); // Ảnh mặc định
                        originalImagePath = "";
                    }
                }
                else
                {
                    pictureBox1.LoadAsync(fullPathDefault);
                    originalImagePath = "";
                }

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnXemvaThem.Enabled = true;
            }
        }

        private void btnXemvaThem_Click(object sender, EventArgs e)
        {

        }
    }
}
