using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement.GUI
{
    public partial class QuanLyNguyenLieu : UserControl
    {
        private NguyenLieuBUS nl_bus = new NguyenLieuBUS();
        private DataTable dtNguyenLieu;
        private string originalImagePath = string.Empty;
        private DanhMucBUS danhMucBUS = new DanhMucBUS();
        private bool DangThaoTac = false;
        public QuanLyNguyenLieu()
        {
            InitializeComponent();
            this.Load += QuanLyNguyenLieu_Load;
            txtTenNguyenLieu.TextChanged += Control_TextChanged;
            txtGiaNhap.TextChanged += Control_TextChanged;
            txtMoTa.TextChanged += Control_TextChanged;
            txtSLTon.TextChanged += Control_TextChanged;
            txtDonVi.TextChanged += Control_TextChanged;
            cmbDanhMucID.SelectedIndexChanged += Control_SelectionChanged;
            cmbTrangThai.SelectedIndexChanged += Control_SelectionChanged;
        }

        private void QuanLyNguyenLieu_Load(object sender, EventArgs e)
        {
            //
            List<string> trangThais = new List<string> { "Hoạt động", "Hết"};
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
            txtTenNguyenLieu.ReadOnly = true;
            txtGiaNhap.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            txtSLTon.ReadOnly = true;
            txtDonVi.ReadOnly = true;

            dtNguyenLieu = new DataTable();
            dtNguyenLieu.Columns.Add("STT", typeof(int));
            dtNguyenLieu.Columns.Add("NguyenLieuID", typeof(int));
            dtNguyenLieu.Columns.Add("TenNguyenLieu", typeof(string));
            dtNguyenLieu.Columns.Add("GiaNhap", typeof(decimal));
            dtNguyenLieu.Columns.Add("MoTa", typeof(string));
            dtNguyenLieu.Columns.Add("TrangThai", typeof(string));
            dtNguyenLieu.Columns.Add("DanhMucID", typeof(int));
            dtNguyenLieu.Columns.Add("DonVi", typeof(string));
            dtNguyenLieu.Columns.Add("SoLuongTon", typeof(decimal));
            dtNguyenLieu.Columns.Add("Hinh", typeof(string));

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dtNguyenLieu;
            LocNguyenLieu();

            dataGridView1.Columns["STT"].HeaderText = "STT";
            dataGridView1.Columns["NguyenLieuID"].HeaderText = "Mã Nguyên Liệu";
            dataGridView1.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
            dataGridView1.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dataGridView1.Columns["MoTa"].HeaderText = "Mô Tả";
            dataGridView1.Columns["DanhMucID"].HeaderText = "Mã Danh Mục";
            dataGridView1.Columns["DonVi"].HeaderText = "Đơn Vị";
            dataGridView1.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            dataGridView1.Columns["Hinh"].HeaderText = "Hình";

            dataGridView1.EnableHeadersVisualStyles = false; // ⚠️ Bắt buộc để màu custom có hiệu lực

            // ẨN CỘT SanPhamID(nếu không muốn hiện ID thật)
            dataGridView1.Columns["NguyenLieuID"].Visible = false;

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
            LocNguyenLieu();
        }

        private void LoadDanhMucVaoComboBox()
        {
            var danhMucList = danhMucBUS.LayTatCaDanhMuc();
            var itemMacDinh = new DanhMucDTO
            {
                DanhMucID = 0,
                TenDanhMuc = "-- Chọn danh mục --",  // Đặt tên rõ ràng
                TrangThai = "Hoạt động"
            };
            danhMucList.Insert(0, itemMacDinh);

            cmbDanhMucID.DataSource = danhMucList;
            cmbDanhMucID.DisplayMember = "TenDanhMuc";
            cmbDanhMucID.ValueMember = "DanhMucID";
            cmbDanhMucID.SelectedIndex = 0; // Sẽ là "-- Chọn danh mục --"
        }
        private void ClearForm()
        {
            txtID.Clear();
            txtTenNguyenLieu.Clear();
            txtGiaNhap.Clear();
            txtMoTa.Clear();
            txtSLTon.Clear();
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
            btnThemAnh.Enabled = false;// ẨN NÚT HỦY
            btnThem.Enabled = true;
            txtID.Enabled = true;
            btnSua.Enabled = false;  // TẮT SỬA
            btnXoa.Enabled = false;// TẮT XÓA

            //
            txtID.ReadOnly = true;
            txtTenNguyenLieu.ReadOnly = true;
            txtGiaNhap.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            cmbTrangThai.Enabled = false;
            cmbDanhMucID.Enabled = false;
            txtSLTon.ReadOnly = true;
            ClearForm();
            ClearErrorProvider();
        }

        private void ClearErrorProvider()
        {
            errorProvider1.Clear();
        }


        private void LocNguyenLieu()
        {
            dtNguyenLieu.Clear();
            var nguyenLieus = nl_bus.LayTatCaNguyenLieu();

            nguyenLieus = nguyenLieus.Where(sp => sp.TrangThai == "Hoạt động" || sp.TrangThai == "Hết").ToList();

            int stt = 1;
            foreach (var nl in nguyenLieus)
            {
                DataRow row = dtNguyenLieu.NewRow();
                row["STT"] = stt++;
                row["NguyenLieuID"] = nl.NguyenLieuID;
                row["TenNguyenLieu"] = nl.TenNguyenLieu;
                row["GiaNhap"] = nl.GiaNhap;
                row["MoTa"] = nl.MoTa;
                row["TrangThai"] = nl.TrangThai;
                row["DanhMucID"] = nl.DanhMucID;
                row["DonVi"] = nl.DonVi;
                row["SoLuongTon"] = nl.SoLuongTon;
                row["Hinh"] = nl.Hinh;
                dtNguyenLieu.Rows.Add(row);
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;
            ClearErrorProvider();
            if (string.IsNullOrWhiteSpace(txtTenNguyenLieu.Text))
            {
                errorProvider1.SetError(txtTenNguyenLieu, "Tên nguyên liệu không được để trống!");
                isValid = false;
            }
            else if(!Regex.IsMatch(txtTenNguyenLieu.Text, @"^[\p{L}\s]+$"))
            {
                errorProvider1.SetError(txtTenNguyenLieu,
                    "Chỉ cho phép chữ cái (kể cả tiếng Việt) và khoảng trắng!");
                isValid = false;
            }

            var culture = new CultureInfo("vi-VN");
            if (!decimal.TryParse(txtGiaNhap.Text, NumberStyles.Number, culture, out decimal giaNhap) || giaNhap < 0)
            {
                errorProvider1.SetError(txtGiaNhap, "Giá nhập phải là số dương!");
                isValid = false;
            }

            if (cmbDanhMucID.SelectedIndex < 1)
            {
                errorProvider1.SetError(cmbDanhMucID, "Vui lòng chọn danh mục!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtDonVi.Text))
            {
                errorProvider1.SetError(txtDonVi, "Đơn vị không được để trống!");
                isValid = false;
            }
            else if (!Regex.IsMatch(txtDonVi.Text, @"^[\p{L}\s]+$"))
            {
                errorProvider1.SetError(txtDonVi,
                    "Chỉ cho phép chữ cái (kể cả tiếng Việt) và khoảng trắng!");
                isValid = false;
            }

            if (!decimal.TryParse(txtSLTon.Text, out decimal slTon) || slTon < 0)
            {
                errorProvider1.SetError(txtSLTon, "Số lượng tồn phải là số không âm!");
                isValid = false;
            }
            if (!isValid)
            {
                foreach (Control ctrl in new Control[] { txtTenNguyenLieu, cmbTrangThai,txtMoTa, txtGiaNhap, cmbDanhMucID,txtSLTon,txtDonVi  })
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

        private void ExportExcel()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Lưu file Excel";
                sfd.FileName = "DanhSachNguyenLieu_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var package = new ExcelPackage())
                        {
                            var ws = package.Workbook.Worksheets.Add("NguyenLieu");

                            // Tiêu đề
                            ws.Cells[1, 1].Value = "STT";
                            ws.Cells[1, 2].Value = "Tên Nguyên Liệu";
                            ws.Cells[1, 3].Value = "Giá Nhập";
                            ws.Cells[1, 4].Value = "Mô Tả";
                            ws.Cells[1, 5].Value = "Trạng Thái";
                            ws.Cells[1, 6].Value = "Danh Mục ID";
                            ws.Cells[1, 7].Value = "Đơn Vị";
                            ws.Cells[1, 8].Value = "Số Lượng Tồn";
                            //ws.Cells[1, 9].Value = "Hình";

                            // Format tiêu đề
                            using (var range = ws.Cells[1, 1, 1, 9])
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
                                ws.Cells[i + 2, 2].Value = dataGridView1.Rows[i].Cells["TenNguyenLieu"].Value;
                                ws.Cells[i + 2, 3].Value = dataGridView1.Rows[i].Cells["GiaNhap"].Value;
                                ws.Cells[i + 2, 4].Value = dataGridView1.Rows[i].Cells["MoTa"].Value;
                                ws.Cells[i + 2, 5].Value = dataGridView1.Rows[i].Cells["TrangThai"].Value;
                                ws.Cells[i + 2, 6].Value = dataGridView1.Rows[i].Cells["DanhMucID"].Value;
                                ws.Cells[i + 2, 7].Value = dataGridView1.Rows[i].Cells["DonVi"].Value;
                                ws.Cells[i + 2, 8].Value = dataGridView1.Rows[i].Cells["SoLuongTon"].Value;
                                //ws.Cells[i + 2, 9].Value = dataGridView1.Rows[i].Cells["Hinh"].Value;
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
                ofd.Title = "Chọn file Excel để nhập nguyên liệu";

                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
                    {
                        var ws = package.Workbook.Worksheets[1]; // Sheet đầu tiên
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
                                string tenNL = ws.Cells[row, 2].Value?.ToString().Trim();
                                if (string.IsNullOrWhiteSpace(tenNL))
                                {
                                    thatBai++;
                                    continue;
                                }

                                // Giá nhập
                                decimal giaNhap = 0;
                                decimal.TryParse(ws.Cells[row, 3].Value?.ToString(), out giaNhap);

                                // Mô tả
                                string moTa = ws.Cells[row, 4].Value?.ToString().Trim() ?? "";

                                // Trạng thái
                                string trangThai = ws.Cells[row, 5].Value?.ToString().Trim();
                                if (string.IsNullOrWhiteSpace(trangThai)) trangThai = "Hoạt động";

                                // Danh mục
                                int danhMucID = 1;
                                int.TryParse(ws.Cells[row, 6].Value?.ToString(), out danhMucID);

                                // Đơn vị
                                string donVi = ws.Cells[row, 7].Value?.ToString().Trim();
                                if (string.IsNullOrWhiteSpace(donVi)) donVi = "Cái";

                                // Số lượng tồn
                                decimal soLuongTon = 0;
                                decimal.TryParse(ws.Cells[row, 8].Value?.ToString(), out soLuongTon);

                                var nl = new NguyenLieuDTO
                                {
                                    NguyenLieuID = nl_bus.LayNguyenLieuIDLonNhat() + 1,
                                    TenNguyenLieu = tenNL,
                                    GiaNhap = giaNhap,
                                    MoTa = moTa,
                                    TrangThai = trangThai,
                                    DanhMucID = danhMucID > 0 ? danhMucID : 1,
                                    DonVi = donVi,
                                    SoLuongTon = soLuongTon,
                                    Hinh = null
                                };

                                if (nl_bus.busThemNguyenLieu(nl, out string msg, out string err))
                                    thanhCong++;
                                else
                                    thatBai++;
                            }
                            catch
                            {
                                thatBai++;
                            }
                        }

                        LocNguyenLieu();
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



        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cmbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                btnThem.Text = "Lưu";
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                int idMoi = nl_bus.LayNguyenLieuIDLonNhat() + 1;

                txtTenNguyenLieu.ReadOnly = false;
                txtGiaNhap.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                txtDonVi.ReadOnly = false;
                txtSLTon.ReadOnly = false;
                cmbTrangThai.Enabled = true;
                cmbDanhMucID.Enabled = true;

                ClearForm();
                ClearErrorProvider();

                // Tự động sinh ID mới              
                txtID.Text = idMoi.ToString();
                txtTenNguyenLieu.Focus();

                DangThaoTac = true;
            }
            else
            {
                if (ValidateForm())
                {
                    var nl = new NguyenLieuDTO
                    {
                        NguyenLieuID = int.Parse(txtID.Text),
                        TenNguyenLieu = txtTenNguyenLieu.Text.Trim(),
                        GiaNhap = decimal.Parse(txtGiaNhap.Text),
                        MoTa = txtMoTa.Text.Trim(),
                        TrangThai = cmbTrangThai.SelectedItem.ToString(),
                        DanhMucID = (int)cmbDanhMucID.SelectedValue,
                        DonVi = txtDonVi.Text.Trim(),
                        SoLuongTon = decimal.Parse(txtSLTon.Text),
                        Hinh = pictureBox2.Tag?.ToString()
                    };

                    if (nl_bus.busThemNguyenLieu(nl, out string msg, out string err))
                    {
                        MessageBox.Show("Thêm nguyên liệu thành công!");
                        LocNguyenLieu();
                        ResetForm();
                        DangThaoTac = false;
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi: {msg}");
                    }
                }
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (btnSua.Text == "Sửa")
            {
                btnSua.Text = "Lưu";
                btnThoat.Visible = true;
                btnThemAnh.Enabled = true;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;

                txtTenNguyenLieu.ReadOnly = false;
                txtGiaNhap.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                txtDonVi.ReadOnly = false;
                txtSLTon.ReadOnly = false;
                cmbTrangThai.Enabled = true;
                cmbDanhMucID.Enabled = true;
                txtID.ReadOnly = true; // Không sửa ID

                DangThaoTac = true;
            }
            else
            {
                if (ValidateForm())
                {
                    var nl = new NguyenLieuDTO
                    {
                        NguyenLieuID = int.Parse(txtID.Text),
                        TenNguyenLieu = txtTenNguyenLieu.Text.Trim(),
                        GiaNhap = decimal.Parse(txtGiaNhap.Text.Trim()), 
                        MoTa = txtMoTa.Text.Trim(),
                        TrangThai = cmbTrangThai.SelectedItem.ToString(),
                        DanhMucID = (int)cmbDanhMucID.SelectedValue,
                        DonVi = txtDonVi.Text.Trim(),
                        SoLuongTon = decimal.Parse(txtSLTon.Text),
                        Hinh = pictureBox2.Tag?.ToString()
                    };

                    if (nl_bus.busSuaNguyenLieu(nl, out string msg, out string err))
                    {
                        MessageBox.Show("Sửa nguyên liệu thành công!");
                        LocNguyenLieu();
                        ResetForm();

                        DangThaoTac = false;
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi: {msg}");
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nguyên liệu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = int.Parse(txtID.Text);
                if (nl_bus.busXoaNguyenLieu(id, out string msg))
                {
                    MessageBox.Show("Xóa nguyên liệu thành công!");
                    LocNguyenLieu();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show($"Lỗi: {msg}");
                }
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
            ResetForm();
            DangThaoTac = false;
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
                txtID.Text = row.Cells["NguyenLieuID"].Value.ToString();
                txtTenNguyenLieu.Text = row.Cells["TenNguyenLieu"].Value.ToString();
                txtGiaNhap.Text = row.Cells["GiaNhap"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                txtDonVi.Text = row.Cells["DonVi"].Value.ToString();
                txtSLTon.Text = row.Cells["SoLuongTon"].Value.ToString();

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

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportExcel();
        }

        private void btnThemAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh nguyên liệu";

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

        private void QuanLyNguyenLieu_SizeChanged(object sender, EventArgs e)
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
                this.Width - leftPanelWidth - rightPanelWidth - padding * 2,
                this.Height - topHeaderHeight - padding * 2
            );

            // === 5. DATAGRIDVIEW DƯỚI CÙNG - CHIẾM HẾT PHẦN DƯỚI ===
            dataGridView1.Location = new Point(0, this.Height - (int)(this.Height * 0.30));
            dataGridView1.Size = new Size(this.Width, (int)(this.Height * 0.30));
            this.Refresh();
        }
    }
}
