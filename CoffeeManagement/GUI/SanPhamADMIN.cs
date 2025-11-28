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
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace CoffeeManagement.GUI
{
    public partial class SanPhamADMIN : UserControl
    {
        private  SanPhamBUS sp_bus = new SanPhamBUS();
        private DataTable dtSanPham;
        private string originalImagePath = string.Empty;
        private DanhMucBUS danhMucBUS = new DanhMucBUS();
        private bool DangThaoTac = false;
        public SanPhamADMIN()
        {
            InitializeComponent();
            // BẮT BUỘC: KHỞI TẠO ErrorProvider
            this.Load += SanPhamADMIN_Load;
        }

        private void SanPhamADMIN_Load(object sender, EventArgs e)
        {
            //
            List<string> trangThais = new List<string> { "Hoạt động", "Ngừng bán", "Deleted" };
            cmbTrangThai.DataSource = trangThais;
            cmbTrangThai.SelectedIndex = 0; // Mặc định "Hoạt động"

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Visible = false;
            btnThemAnh.Enabled = false;
            cmbDanhMucID.Enabled = false;
            cmbTrangThai.Enabled = false;

            //
            txtID.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtGia.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            


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
            txtGia.Clear();
            txtMoTa.Clear();
            cmbTrangThai.SelectedIndex = 0; // Mặc định "Hoạt động"
            cmbDanhMucID.SelectedIndex = -1;

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
            txtMoTa.ReadOnly = true;
            cmbTrangThai.Enabled = false;
            cmbDanhMucID.Enabled = false;

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
                case "MoTa":
                    return txtMoTa;
                case "GiaBan":
                    return txtGia;
                case "TrangThai":
                    return cmbTrangThai;
                case "DanhMucID":
                    return cmbDanhMucID;
                default:
                    return null;
            }
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
                new { Name = "txtMoTa", Control = (Control)txtMoTa },
                new { Name = "txtGia", Control = (Control)txtGia }
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
                // === CHUYỂN SANG CHẾ ĐỘ THÊM ===
                btnThem.Text = "Lưu";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;

                // Khóa txtID
                txtID.ReadOnly = true;
                txtID.BackColor = Color.FromArgb(240, 240, 240);
                txtID.ForeColor = Color.Blue;
                txtID.Font = new Font(txtID.Font, FontStyle.Bold);

                // Tự động sinh ID mới
                int idMoi = sp_bus.LaySanPhamIDLonNhat() + 1;
                txtID.Text = idMoi.ToString();

                // Mở các field để nhập
                txtTenSP.ReadOnly = false;
                txtGia.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                cmbTrangThai.Enabled = true;
                cmbDanhMucID.Enabled = true;

                // Reset form + giữ lại ID mới
                ClearForm();
                txtID.Text = idMoi.ToString();
                txtTenSP.Focus();

                DangThaoTac = true;
                return;
            }

            // =================== LƯU THÊM MỚI ===================
            try
            {
                // === LẤY ĐÚNG ID DANH MỤC TỪ SelectedValue (QUAN TRỌNG NHẤT) ===
                int danhMucID = 0;
                if (cmbDanhMucID.SelectedValue != null)
                {
                    // Ưu tiên lấy từ ValueMember → luôn là ID chính xác
                    if (int.TryParse(cmbDanhMucID.SelectedValue.ToString(), out int id))
                        danhMucID = id;
                }

                // Nếu người dùng chưa chọn gì (vẫn là "-- Chọn danh mục --") → báo lỗi
                if (danhMucID <= 0)
                    throw new ArgumentException("Vui lòng chọn danh mục hợp lệ!");

                var sp = new SanPhamDTO
                {
                    SanPhamID = int.Parse(txtID.Text),
                    TenSanPham = txtTenSP.Text.Trim(),
                    GiaBan = decimal.TryParse(txtGia.Text.Trim(), out decimal gia) ? gia : 0,
                    MoTa = txtMoTa.Text.Trim(),
                    TrangThai = cmbTrangThai.Text.Trim(),
                    DanhMucID = danhMucID, 
                    Hinh = pictureBox2.Tag?.ToString()
                };

                // === VALIDATE ===
                if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                    throw new ArgumentException("Tên sản phẩm không được để trống!");

                if (sp.GiaBan <= 0)
                    throw new ArgumentException("Giá bán phải lớn hơn 0!");



                // === GỌI BUS THÊM ===
                if (sp_bus.busThemSanPham(sp, out string msg, out string err))
                {
                    MessageBox.Show($"Thêm sản phẩm thành công!\nID: {sp.SanPhamID}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LocSanPham();  // Làm mới bảng
                    ResetForm();   // Trở về trạng thái ban đầu
                    DangThaoTac = false;
                }
                else
                {
                    MessageBox.Show("Thêm thất bại: " + msg, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá bán không đúng định dạng!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // === CHUYỂN SANG CHẾ ĐỘ SỬA ===
                btnSua.Text = "Lưu";
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;

                txtID.ReadOnly = true; // ID không được sửa
                txtTenSP.ReadOnly = false;
                txtGia.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                cmbTrangThai.Enabled = true;
                cmbDanhMucID.Enabled = true;

                DangThaoTac = true;
                return;
            }

            // =================== LƯU SỬA ===================
            try
            {
                // === LẤY ĐÚNG ID DANH MỤC TỪ SelectedValue (BẮT BUỘC) ===
                int danhMucID = 0;
                if (cmbDanhMucID.SelectedValue != null)
                {
                    if (int.TryParse(cmbDanhMucID.SelectedValue.ToString(), out int id))
                        danhMucID = id;
                }

                // Kiểm tra bắt buộc chọn danh mục hợp lệ
                if (danhMucID <= 0)
                    throw new ArgumentException("Vui lòng chọn danh mục hợp lệ!", "DanhMucID");

                var sp = new SanPhamDTO
                {
                    SanPhamID = int.Parse(txtID.Text),
                    TenSanPham = txtTenSP.Text.Trim(),
                    GiaBan = decimal.TryParse(txtGia.Text.Trim(), out decimal gia) ? gia : 0,
                    MoTa = txtMoTa.Text.Trim(),
                    TrangThai = cmbTrangThai.Text.Trim(),
                    DanhMucID = danhMucID, // ĐÚNG RỒI!
                    Hinh = pictureBox2.Tag?.ToString()
                           ?? dataGridView1.CurrentRow.Cells["Hinh"].Value?.ToString()
                };

                // === VALIDATE ===
                if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                    throw new ArgumentException("Tên sản phẩm không được để trống!", "TenSanPham");

                if (sp.GiaBan <= 0)
                    throw new ArgumentException("Giá bán phải lớn hơn 0!", "GiaBan");

                // DanhMucID đã kiểm tra ở trên

                // === GỌI BUS SỬA ===
                if (sp_bus.busSuaSanPham(sp, out string message, out string errorField))
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LocSanPham();
                    ResetForm();
                    DangThaoTac = false;
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
            catch (FormatException)
            {
                MessageBox.Show("Giá bán không đúng định dạng số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DangThaoTac = false;
        }



        private void LoadDanhMucVaoComboBox()
        {
            var danhMucList = danhMucBUS.LayTatCaDanhMuc(); // Lấy từ DB

            // === 1. CHO cmbDanhMuc (dùng để lọc) ===
            var listChoLoc = new List<DanhMucDTO>(danhMucList); // Copy để không ảnh hưởng
            var allItem = new DanhMucDTO
            {
                DanhMucID = 0,
                TenDanhMuc = "Tất cả danh mục",
                TrangThai = "Hoạt động"
            };
            listChoLoc.Insert(0, allItem);

            cmbDanhMuc.DataSource = listChoLoc;
            cmbDanhMuc.DisplayMember = "TenDanhMuc";
            cmbDanhMuc.ValueMember = "DanhMucID";
            cmbDanhMuc.SelectedIndex = 0;

            // === 2. CHO cmbDanhMucID (dùng khi thêm/sửa sản phẩm) ===
            var listChoNhap = new List<DanhMucDTO>(danhMucList); 
            var itemMacDinh = new DanhMucDTO
            {
                DanhMucID = 0,
                TenDanhMuc = "-- Chọn danh mục --",  
                TrangThai = "Hoạt động"
            };
            listChoNhap.Insert(0, itemMacDinh);

            cmbDanhMucID.DataSource = listChoNhap;
            cmbDanhMucID.DisplayMember = "TenDanhMuc";
            cmbDanhMucID.ValueMember = "DanhMucID";
            cmbDanhMucID.SelectedIndex = 0; // Sẽ là "-- Chọn danh mục --"
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
            listSP = listSP.Where(sp => sp.TrangThai == "Hoạt động" || sp.TrangThai == "Ngừng bán" ).ToList();

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


        // ==================== XUẤT RA EXCEL ====================
        private void ExportExcel()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.Title = "Xuất danh sách sản phẩm";
                sfd.FileName = "DanhSachSanPham_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("Sản Phẩm");

                            // Tiêu đề cột (bỏ STT và SanPhamID nếu không muốn)
                            ws.Cells[1, 1].Value = "STT";
                            ws.Cells[1, 2].Value = "Tên Sản Phẩm";
                            ws.Cells[1, 3].Value = "Giá Bán";
                            ws.Cells[1, 4].Value = "Mô Tả";
                            ws.Cells[1, 5].Value = "Trạng Thái";
                            ws.Cells[1, 6].Value = "Mã Danh Mục";

                            // Style tiêu đề
                            using (var range = ws.Cells[1, 1, 1, 6])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(Color.SteelBlue);
                                range.Style.Font.Color.SetColor(Color.White);
                                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }

                            // Đổ dữ liệu từ DataGridView
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                ws.Cells[i + 2, 1].Value = dataGridView1.Rows[i].Cells["STT"].Value;
                                ws.Cells[i + 2, 2].Value = dataGridView1.Rows[i].Cells["TenSanPham"].Value;
                                ws.Cells[i + 2, 3].Value = dataGridView1.Rows[i].Cells["GiaBan"].Value;
                                ws.Cells[i + 2, 4].Value = dataGridView1.Rows[i].Cells["MoTa"].Value;
                                ws.Cells[i + 2, 5].Value = dataGridView1.Rows[i].Cells["TrangThai"].Value;
                                ws.Cells[i + 2, 6].Value = dataGridView1.Rows[i].Cells["DanhMucID"].Value;
                            }

                            // AutoFit cột
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            // Lưu file
                            FileInfo fi = new FileInfo(sfd.FileName);
                            package.SaveAs(fi);

                            MessageBox.Show("Xuất file Excel thành công!\nĐường dẫn: " + sfd.FileName,
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ==================== NHẬP TỪ EXCEL ====================
        private void ImportExcel()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                ofd.Title = "Chọn file Excel để nhập sản phẩm";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
                        {
                            var ws = package.Workbook.Worksheets[0]; // Sheet đầu tiên
                            int rowCount = ws.Dimension.Rows;

                            if (rowCount < 2)
                            {
                                MessageBox.Show("File Excel không có dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            int thanhCong = 0;
                            int thatBai = 0;

                            // Bắt đầu từ dòng 2 (dòng 1 là tiêu đề)
                            for (int row = 2; row <= rowCount; row++)
                            {
                                try
                                {
                                    // ĐỌC ĐÚNG THỨ TỰ CỘT CỦA BẠN
                                    string tenSP = ws.Cells[row, 2].GetValue<string>()?.Trim();
                                    string trangThai = ws.Cells[row, 3].GetValue<string>()?.Trim();
                                    string moTa = ws.Cells[row, 4].GetValue<string>()?.Trim();
                                    decimal giaBan = ws.Cells[row, 5].GetValue<decimal>();
                                    int danhMucID = ws.Cells[row, 6].GetValue<int>();
                                    string hinh = ws.Cells[row, 7].GetValue<string>()?.Trim();

                                    // VALIDATE DỮ LIỆU
                                    if (string.IsNullOrWhiteSpace(tenSP))
                                    {
                                        thatBai++;
                                        continue;
                                    }
                                    if (giaBan <= 0)
                                    {
                                        giaBan = 10000; // mặc định nếu sai
                                    }
                                    if (danhMucID <= 0)
                                    {
                                        danhMucID = 1; // mặc định danh mục "Đồ uống" hoặc bạn chọn
                                    }
                                    if (string.IsNullOrWhiteSpace(trangThai))
                                        trangThai = "Hoạt động";

                                    var sp = new SanPhamDTO
                                    {
                                        TenSanPham = tenSP,
                                        GiaBan = giaBan,
                                        MoTa = moTa,
                                        TrangThai = trangThai,
                                        DanhMucID = danhMucID,
                                        Hinh = string.IsNullOrWhiteSpace(hinh) ? null : hinh
                                    };

                                    // THÊM VÀO CSDL
                                    if (sp_bus.busThemSanPham(sp, out string msg, out string err))
                                    {
                                        thanhCong++;
                                    }
                                    else
                                    {
                                        thatBai++;
                                        // Có thể log lỗi nếu cần: Console.WriteLine($"Lỗi dòng {row}: {msg}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    thatBai++;
                                    // Bỏ qua dòng lỗi, tiếp tục dòng khác
                                }
                            }

                            // LÀM MỚI BẢNG
                            LocSanPham();

                            MessageBox.Show($"Nhập Excel thành công!\n" +
                                            $"Đã thêm: {thanhCong} sản phẩm\n" +
                                            $"Bị lỗi: {thatBai} dòng",
                                            "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi đọc file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportExcel();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void SanPhamADMIN_SizeChanged(object sender, EventArgs e)
        {
            //panel1.Size = new Size(this.Width, this.Height);
            //dataGridView1.Size = new Size((int)(this.Width * 0.5), (int)(this.Height * 0.5));
        }

        private void cmbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocSanPham();
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void btnImport_Click_1(object sender, EventArgs e)
        {
            ImportExcel();
        }

        private void btnThemAnh_Click_1(object sender, EventArgs e)
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
    }
}
