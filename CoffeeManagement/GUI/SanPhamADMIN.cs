using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CoffeeManagement.GUI
{
    public partial class SanPhamADMIN : UserControl
    {
        private  SanPhamBUS sp_bus = new SanPhamBUS();
        private DataTable dtSanPham;
        private string originalImagePath = string.Empty;
        private DanhMucBUS danhMucBUS = new DanhMucBUS();
        public SanPhamADMIN()
        {
            InitializeComponent();
            // BẮT BUỘC: KHỞI TẠO ErrorProvider
            this.Load += SanPhamADMIN_Load;
        }

        private void SanPhamADMIN_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Visible = false;
            btnThemAnh.Enabled = false;

            //
            txtID.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtGia.ReadOnly = true;
            txtDanhMucID.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            txtTrangThai.ReadOnly = true;
            


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
            LocSanPham();

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
                    pictureBox2.LoadAsync(fullPath);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
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


            LoadDanhMucVaoComboBox();
            LocSanPham();
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtTenSP.Clear();
            txtTrangThai.Clear();
            txtGia.Clear();
            txtMoTa.Clear();
            txtDanhMucID.Clear();

            string relativePath = @"Images\null.png";
            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            pictureBox2.LoadAsync(fullPath);
        }

        private void ResetForm()
        {
            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnThoat.Visible = false;
            btnThemAnh.Enabled = false;// ẨN NÚT HỦY
            btnThem.Enabled = true;
            txtID.Enabled = true;
            btnSua.Enabled = false;  // TẮT SỬA
            btnXoa.Enabled = false;// TẮT XÓA

            //
            txtID.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtGia.ReadOnly = true;
            txtDanhMucID.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            txtTrangThai.ReadOnly = true;

            ClearForm();
            ClearErrorProvider();
        }

        private void ClearErrorProvider()
        {
            errorProvider1.Clear();
        }

        private Control GetControlByErrorField(string errorField)
        {
            switch(errorField)
            {
                case "SanPhamID":
                    return txtID;
                case "TenSanPham":
                    return txtTenSP;
                case "TrangThai":
                    return txtTrangThai;
                case "MoTa":
                    return txtMoTa;
                case "GiaBan":
                    return txtGia;
                case "DanhMucID":
                    return txtDanhMucID;
                default:
                    return null;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["SanPhamID"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSanPham"].Value.ToString();
                txtTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
                txtGia.Text = row.Cells["GiaBan"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                txtDanhMucID.Text = row.Cells["DanhMucID"].Value.ToString();

                // === HIỂN THỊ ẢNH ===
                string tenFileAnh = row.Cells["Hinh"].Value?.ToString(); // Lấy tên file từ DB
                string relativePath = @"Images\null.png";
                string fullPathDefault = Path.Combine(Application.StartupPath, relativePath);
                if (!string.IsNullOrEmpty(tenFileAnh))
                {
                    string fullPath = Path.Combine(Application.StartupPath,"Images", tenFileAnh);
                    if (File.Exists(fullPath))
                    {
                        pictureBox2.LoadAsync(fullPath);
                        originalImagePath = fullPath; // Lưu tạm để sửa sau
                    }
                    else
                    {
                        pictureBox2.LoadAsync(fullPathDefault); // Ảnh mặc định
                        originalImagePath = "";
                    }
                }
                else
                {
                    pictureBox2.LoadAsync(fullPathDefault);
                    originalImagePath = "";
                }

                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnThem.Enabled = true;
            }
        }

        

        private void textBox_TextChanged(object sender, EventArgs e)
        {
           
            if (!(sender is System.Windows.Forms.TextBox tb)) return;

            // Danh sách TextBox cần xóa lỗi
            var controls = new[]
            {
                new { Name = "txtID", Control = (Control)txtID },
                new { Name = "txtTenSP", Control = (Control)txtTenSP },
                new { Name = "txtTrangThai", Control = (Control)txtTrangThai },
                new { Name = "txtMoTa", Control = (Control)txtMoTa },
                new { Name = "txtGia", Control = (Control)txtGia },
                new { Name = "txtDanhMucID", Control = (Control)txtDanhMucID }
            };

            // Tìm TextBox đang gõ → Xóa lỗi
            var matched = controls.FirstOrDefault(c => c.Name == tb.Name);
            if (matched != null)
            {
                errorProvider1.SetError(matched.Control, "");
            }
        }

        // Nút Thêm ==============================================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                ClearForm();
                txtID.Focus();
                btnThem.Text = "Lưu";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;

                //
                txtID.ReadOnly = false;
                txtTenSP.ReadOnly = false;
                txtGia.ReadOnly = false;
                txtDanhMucID.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                txtTrangThai.ReadOnly = false;
                return;
            }

            var sp = new SanPhamDTO();

            try
            {
                sp.SanPhamID = int.TryParse(txtID.Text.Trim(), out int id) ? id : 0;
                if (sp.SanPhamID <= 0)
                    throw new ArgumentException("Mã sản phẩm phải lớn hơn 0.", "SanPhamID");

                sp.TenSanPham = txtTenSP.Text.Trim();
                if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                    throw new ArgumentException("Tên sản phẩm không được để trống.", "TenSanPham");

                sp.TrangThai = txtTrangThai.Text.Trim();
                if (string.IsNullOrWhiteSpace(sp.TrangThai))
                    throw new ArgumentException("Trạng thái không được để trống.", "TrangThai");

                sp.MoTa = txtMoTa.Text.Trim();
                if (string.IsNullOrWhiteSpace(sp.MoTa))
                    throw new ArgumentException("MoTa không được để trống.", "MoTa");

                sp.GiaBan = decimal.TryParse(txtGia.Text.Trim(), out decimal gia) ? gia : 0;
                // → DTO sẽ tự ném lỗi nếu <= 0

                sp.DanhMucID = int.TryParse(txtDanhMucID.Text.Trim(), out int dm) ? dm : 0;
                if (sp.DanhMucID <= 0)
                    throw new ArgumentException("Mã danh mục phải lớn hơn 0.", "DanhMucID");

                sp.Hinh = pictureBox2.Tag?.ToString();

                // GỌI BUS
                if (sp_bus.busThemSanPham(sp, out string message, out string errorField))
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LocSanPham();
                    ResetForm();
                    LocSanPham();
                }
                else
                {
                    throw new InvalidOperationException(message);
                }
            }
            catch (ArgumentException ex)
            {
                ClearErrorProvider();
                MessageBox.Show(ex.Message, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // LẤY TÊN FIELD TỪ ParamName
                string fieldName = ex.ParamName ?? "GiaBan";
                Control ctrl = GetControlByErrorField(fieldName);

                if (ctrl != null)
                {
                    ctrl.Focus();
                    errorProvider1.SetError(ctrl, ex.Message); // HIỆN ICON ĐÚNG Ô
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Sửa==========================================================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (btnSua.Text == "Sửa")
            {
                if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm trong bảng để sửa!", "Chưa chọn sản phẩm",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSua.Text = "Lưu";
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                txtID.Enabled = false;
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;

                //
                txtID.ReadOnly = false;
                txtTenSP.ReadOnly = false;
                txtGia.ReadOnly = false;
                txtDanhMucID.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                txtTrangThai.ReadOnly = false;

                return;
            }

            // === LƯU SỬA ===
            var sp = new SanPhamDTO
            {
                SanPhamID = int.Parse(txtID.Text),
                TenSanPham = txtTenSP.Text.Trim(),
                GiaBan = decimal.TryParse(txtGia.Text.Trim(), out decimal gia) ? gia : 0,
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = txtTrangThai.Text.Trim(),
                DanhMucID = int.TryParse(txtDanhMucID.Text.Trim(), out int dm) ? dm : 0,
                Hinh = pictureBox2.Tag?.ToString()
                                    ?? dataGridView1.CurrentRow.Cells["Hinh"].Value?.ToString()
            };

            try
            {
                if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                    throw new ArgumentException("Tên sản phẩm không được để trống.", "TenSanPham");
                if (sp.GiaBan <= 0)
                    throw new ArgumentException("Giá bán phải lớn hơn 0.", "GiaBan");
                if (sp.DanhMucID <= 0)
                    throw new ArgumentException("Mã danh mục phải lớn hơn 0.", "DanhMucID");

               
                // === GỌI BUS ===
                if (sp_bus.busSuaSanPham(sp, out string message, out string errorField))
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LocSanPham();
                    ResetForm();
                    LocSanPham();
                    this.ActiveControl = null;
                }
                else
                {
                    throw new InvalidOperationException(message);
                }
            }
            catch (ArgumentException ex)
            {
                ClearErrorProvider();
                MessageBox.Show(ex.Message, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string field = ex.ParamName ?? "GiaBan";
                Control ctrl = GetControlByErrorField(field);
                if (ctrl != null)
                {
                    ctrl.Focus();
                    errorProvider1.SetError(ctrl, ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra có dòng được chọn không
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy ID từ dòng hiện tại
            int sanPhamID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SanPhamID"].Value);

            // XÁC NHẬN XÓA
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm ID: {sanPhamID}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            // GỌI BUS XÓA
            if (sp_bus.busXoaSanPham(sanPhamID, out string message))
            {
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // XÓA KHỎI DATAGRIDVIEW NGAY LẬP TỨC
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                LocSanPham();
                // Reset form
                ClearForm();
                ClearErrorProvider();
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // HỎI XÁC NHẬN (TÙY CHỌN)
            DialogResult result = MessageBox.Show(
                "Bạn có muốn hủy thao tác hiện tại?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;
            
            this.ActiveControl = null;
            // THOÁT CHẾ ĐỘ THÊM / SỬA
            ResetForm();
        }


        private void btnThemAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh sản phẩm";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. ĐƯỜNG DẪN THƯ MỤC Images TRONG bin\Debug
                    string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    // 2. TÊN FILE MỚI: DỰA VÀO ID HOẶC GUID
                    string tenFileMoi = Path.GetFileName(ofd.FileName); // Giữ tên gốc
                    string destPath = Path.Combine(imagesFolder, tenFileMoi);

                    // 3. COPY ẢNH (GHI ĐÈ NẾU CÓ)
                    File.Copy(ofd.FileName, destPath, true);

                    // 4. LƯU ĐƯỜNG DẪN ĐÍCH + TÊN FILE
                    originalImagePath = destPath; // Đường dẫn đầy đủ trong Images/
                    pictureBox2.Tag = tenFileMoi; // Chỉ tên file → lưu vào DB

                    // 5. HIỂN THỊ
                    pictureBox2.LoadAsync(destPath);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi copy ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    originalImagePath = "";
                    pictureBox2.Tag = null;
                }
            }
        }

        private void LoadDanhMucVaoComboBox()
        {
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
        }

        private void LocSanPham()
        {
            dtSanPham.Clear();
            var dm = cmbDanhMuc.SelectedItem as DanhMucDTO;
            int danhMucID = dm?.DanhMucID ?? 0;
            var listSP = sp_bus.LayTatCaSanPham();
            // Lọc theo danh mục
            if (danhMucID > 0)
                listSP = listSP.Where(sp => sp.DanhMucID == danhMucID).ToList();

            // === BẮT BUỘC: CHỈ HIỂN THỊ SẢN PHẨM "Hoạt động" ===
            listSP = listSP.Where(sp => sp.TrangThai == "Hoạt động").ToList();

            // ĐỔ DỮ LIỆU + TỰ ĐỘNG THÊM STT
            int stt = 1;
            foreach (var sp in listSP)
            {
                dtSanPham.Rows.Add(
                    stt++,                           
                    sp.SanPhamID,
                    sp.TenSanPham,
                    sp.GiaBan,
                    sp.MoTa ?? "",
                    sp.TrangThai ?? "Hoạt động",
                    sp.DanhMucID,
                    sp.Hinh ?? ""
                );
            }
            //this.ActiveControl = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocSanPham();
        }
    }
}
