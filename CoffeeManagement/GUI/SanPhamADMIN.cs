using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool DangThaoTac = false;
        public event Action<int> RequestOpenCTSP;
        public SanPhamADMIN()
        {
            InitializeComponent();
            this.Load += SanPhamADMIN_Load;
            this.VisibleChanged += SanPhamADMIN_VisibleChanged;
            txtTenSP.TextChanged += Control_TextChanged;
            txtGia.TextChanged += Control_TextChanged;
            txtMoTa.TextChanged += Control_TextChanged;
            cmbDanhMucID.SelectedIndexChanged += Control_SelectionChanged;
            cmbTrangThai.SelectedIndexChanged += Control_SelectionChanged;

        }

        private void SanPhamADMIN_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadDanhMucVaoComboBox(); 
                LocSanPham();             
            }
        }
        private void SanPhamADMIN_Load(object sender, EventArgs e)
        {
            List<string> trangThais = new List<string> {"Trống","Hoạt động", "Ngừng bán"};
            cmbTrangThai.DataSource = trangThais;
            cmbTrangThai.SelectedIndex = 0; 
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Visible = false;
            btnThemAnh.Enabled = false;
            cmbDanhMucID.Enabled = false;
            cmbTrangThai.Enabled = false;
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
            if (!dataGridView1.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "Xem Nguyên Liệu";
                btnView.Name = "btnView";
                btnView.Text = "VIEW";
                btnView.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnView);
            }
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
            dataGridView1.Columns["SanPhamID"].Visible = false;
            dataGridView1.Columns["STT"].Width = 50;
            dataGridView1.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["STT"].DisplayIndex = 0;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue; // Màu nền
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;     // Màu chữ
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold); // Font chữ
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   // Căn giữa header
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false; // Chỉ chọn 1 dòng tại 1 thời điểm
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Load ảnh mặc định
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
            cmbTrangThai.SelectedIndex = 0; 
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
            btnThemAnh.Enabled = false;
            btnThem.Enabled = true;
            txtID.Enabled = true;
            btnSua.Enabled = false;  
            btnXoa.Enabled = false;
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

        private bool ValidateForm()
        {
            bool isValid = true;
            ClearErrorProvider();
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                errorProvider1.SetError(txtTenSP, "Tên sản phẩm không được để trống!");
                isValid = false;
            }
            else if (!Regex.IsMatch(txtTenSP.Text, @"^[\p{L}\s]+$"))
            {
                errorProvider1.SetError(txtTenSP, "Tên sản phẩm chỉ được chứa chữ cái (kể cả tiếng Việt) và khoảng trắng!");
                isValid = false;
            }
            var culture = new CultureInfo("vi-VN");
            if (!decimal.TryParse(txtGia.Text, NumberStyles.Number, culture, out decimal giaBan) || giaBan < 0)
            {
                errorProvider1.SetError(txtGia, "Giá bán phải từ 0 trở lên!");
                isValid = false;
            }
            if (cmbDanhMucID.SelectedValue == null ||
                !int.TryParse(cmbDanhMucID.SelectedValue.ToString(), out int danhMucID) ||
                danhMucID <= 0)
            {
                errorProvider1.SetError(cmbDanhMucID, "Vui lòng chọn một danh mục!");
                isValid = false;
            }
            if (cmbTrangThai.SelectedIndex <= 0) //
            {
                errorProvider1.SetError(cmbTrangThai, "Vui lòng chọn trạng thái hợp lệ!");
                isValid = false;
            }
            if (!isValid)
            {
                foreach (Control ctrl in new Control[] { txtTenSP, txtGia, cmbDanhMucID, cmbTrangThai })
                {
                    if (!string.IsNullOrEmpty(errorProvider1.GetError(ctrl)))
                    {
                        ctrl.Focus();
                        break;
                    }
                }
            }
            return isValid;
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.TextBox tb)
            {
                errorProvider1.SetError(tb, "");
            }
        }

        private void Control_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox cmb)
            {
                errorProvider1.SetError(cmb, "");
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
                cmbTrangThai.SelectedItem = row.Cells["TrangThai"].Value.ToString(); 
                cmbDanhMucID.SelectedValue = row.Cells["DanhMucID"].Value; 

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

        // Nút Thêm ==============================================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                btnThem.Text = "Lưu";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;
                txtID.ReadOnly = true;
                txtTenSP.ReadOnly = false;
                txtGia.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                cmbTrangThai.Enabled = true;
                cmbDanhMucID.Enabled = true;

                int idMoi = sp_bus.LaySanPhamIDLonNhat() + 1;
                txtID.Text = idMoi.ToString();
                ClearForm();
                txtID.Text = idMoi.ToString();
                txtTenSP.Focus();
                txtGia.Text = "0";
                DangThaoTac = true;
                return;
            }
            // === LƯU THÊM ===
            if (!ValidateForm()) return;
            int danhMucID = (int)cmbDanhMucID.SelectedValue;
            var sp = new SanPhamDTO
            {
                SanPhamID = int.Parse(txtID.Text),
                TenSanPham = txtTenSP.Text.Trim(),
                GiaBan = decimal.Parse(txtGia.Text.Trim()),
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cmbTrangThai.Text,
                DanhMucID = danhMucID,
                Hinh = pictureBox2.Tag?.ToString()
            };

            if (sp_bus.busThemSanPham(sp, out string msg, out string err))
            {
                MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LocSanPham();
                ResetForm();
                DangThaoTac = false;
            }
            else
            {
                MessageBox.Show("Thêm thất bại: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Sửa==========================================================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (btnSua.Text == "Sửa")
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSua.Text = "Lưu";
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;
                txtID.ReadOnly = true;
                txtTenSP.ReadOnly = false;
                txtGia.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                cmbTrangThai.Enabled = true;
                cmbDanhMucID.Enabled = true;
                DangThaoTac = true;
                return;
            }

            // === LƯU SỬA ===
            if (!ValidateForm()) return;
            int danhMucID = (int)cmbDanhMucID.SelectedValue;
            var sp = new SanPhamDTO
            {
                SanPhamID = int.Parse(txtID.Text),
                TenSanPham = txtTenSP.Text.Trim(),
                GiaBan = decimal.Parse(txtGia.Text.Trim()),
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cmbTrangThai.Text,
                DanhMucID = danhMucID,
                Hinh = pictureBox2.Tag?.ToString() ?? dataGridView1.CurrentRow.Cells["Hinh"].Value?.ToString()
            };

            if (sp_bus.busSuaSanPham(sp, out string message, out string errorField))
            {
                MessageBox.Show("Sửa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LocSanPham();
                ResetForm();
                DangThaoTac = false;
            }
            else
            {
                MessageBox.Show("Sửa thất bại: " + message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int sanPhamID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SanPhamID"].Value);

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
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                LocSanPham();
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
            DialogResult result = MessageBox.Show(
                "Bạn có muốn hủy thao tác hiện tại?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;
            this.ActiveControl = null;
            ResetForm();
            DangThaoTac = false;
        }

        private void LoadDanhMucVaoComboBox()
        {
            var danhMucList = danhMucBUS.LayTatCaDanhMuc(); 
            var listChoLoc = new List<DanhMucDTO>(danhMucList); 
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
            // === 2. CHO cmbDanhMucID ===
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

            listSP = listSP.Where(sp => sp.TrangThai == "Hoạt động" || sp.TrangThai == "Ngừng bán").ToList();
            int stt = 1;
            foreach (var sp in listSP)
            {
                string trangThaiHienThi = sp.TrangThai;
                if (sp.TrangThai == "Hoạt động") 
                {
                    bool coNguyenLieuThieu = sp_bus.KiemTraNguyenLieuThieu(sp.SanPhamID);
                    if (coNguyenLieuThieu)
                    {
                        sp_bus.CapNhatTrangThaiSanPham(sp.SanPhamID, "Ngừng bán");
                        sp.TrangThai = "Ngừng bán";
                    }
                }
                dtSanPham.Rows.Add(
                    stt++,
                    sp.SanPhamID,
                    sp.TenSanPham,
                    sp.GiaBan,
                    sp.MoTa ?? "",
                    sp.TrangThai, 
                    sp.DanhMucID,
                    sp.Hinh ?? ""
                );
            }
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
                if (ofd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
                    {
                        var ws = package.Workbook.Worksheets[1];
                        if (ws?.Dimension == null || ws.Dimension.Rows < 2)
                        {
                            MessageBox.Show("File Excel không có dữ liệu hoặc không đọc được!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int rowCount = ws.Dimension.Rows;
                        int thanhCong = 0;
                        int thatBai = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                string tenSP = ws.Cells[row, 2].Value?.ToString().Trim();
                                if (string.IsNullOrWhiteSpace(tenSP))
                                {
                                    thatBai++;
                                    continue;
                                }

                                // Giá bán
                                decimal giaBan = 0;
                                decimal.TryParse(ws.Cells[row, 3].Value?.ToString(), out giaBan);

                                // Mô tả
                                string moTa = ws.Cells[row, 4].Value?.ToString().Trim() ?? "";

                                // Trạng thái
                                string trangThai = ws.Cells[row, 5].Value?.ToString().Trim();
                                if (string.IsNullOrWhiteSpace(trangThai)) trangThai = "Hoạt động";

                                // Danh mục
                                int danhMucID = 1;
                                int.TryParse(ws.Cells[row, 6].Value?.ToString(), out danhMucID);

                                var sp = new SanPhamDTO
                                {
                                    SanPhamID = sp_bus.LaySanPhamIDLonNhat()+1,
                                    TenSanPham = tenSP,
                                    GiaBan = giaBan,
                                    MoTa = moTa,
                                    TrangThai = trangThai,
                                    DanhMucID = danhMucID > 0 ? danhMucID : 1,
                                    Hinh = null
                                };

                                if (sp_bus.busThemSanPham(sp, out string msg, out string err))
                                    thanhCong++;
                                else
                                    thatBai++;
                            }
                            catch
                            {
                                thatBai++; // dòng lỗi → tính thất bại, bỏ qua
                            }
                        }

                        LocSanPham();
                        MessageBox.Show($"Nhập xong!\nThành công: {thanhCong}\nThất bại: {thatBai}",
                                        "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mở file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            int padding = 15;                    // Khoảng cách lề đẹp
            int topHeaderHeight = 94;            // Chiều cao phần tiêu đề "QUẢN LÝ SẢN PHẨM" (theo Designer)
            int leftPanelWidth = 263;            // Ảnh bên trái (giữ cố định hoặc scale nhẹ)
            int rightPanelWidth = 250;           // Panel nút Thêm/Sửa/Xóa/Hủy
            // === 1. PANEL TIÊU ĐỀ (label2) - giữ nguyên trên cùng ===
            label2.Size = new Size(this.Width, topHeaderHeight);
            // === 2. PANEL ẢNH BÊN TRÁI (panel3) ===
            panel3.Location = new Point(0, topHeaderHeight);
            panel3.Size = new Size(leftPanelWidth, this.Height - topHeaderHeight - padding);
            // === 3. PANEL NÚT BÊN PHẢI (panel6) ===
            panel6.Location = new Point(this.Width - rightPanelWidth - padding, topHeaderHeight);
            panel6.Size = new Size(rightPanelWidth, this.Height - topHeaderHeight - padding);
            // === 4. PANEL NHẬP LIỆU GIỮA (panel7) - TỰ ĐỘNG FILL ===
            panel7.Location = new Point(leftPanelWidth + padding, topHeaderHeight + padding);
            panel7.Size = new Size(
                this.Width - leftPanelWidth - rightPanelWidth - padding* 5 ,   
                this.Height - topHeaderHeight - padding * 2
            );
            // === 5. DATAGRIDVIEW DƯỚI CÙNG - CHIẾM HẾT PHẦN DƯỚI ===
            dataGridView1.Location = new Point(0, this.Height - (int)(this.Height * 0.30));
            dataGridView1.Size = new Size(this.Width, (int)(this.Height * 0.30));
            this.Refresh();
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
                    pictureBox2.Tag = tenFileMoi; 

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DangThaoTac)
            {
                return;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnView" && e.RowIndex >= 0)
            {
                int sanphamID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SanPhamID"].Value);
                RequestOpenCTSP?.Invoke(sanphamID);
            }
        }
    }
}
