using CoffeeManagement.BUS;
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CoffeeManagement.GUI
{
    public partial class NhanVienGUI : UserControl
    {
        private NhanVienBUS nvBUS;
        private DataTable dtNhanVien;

        private NhanVienDTO selectedNV;

        public NhanVienGUI()
        {
            InitializeComponent();

            // Khởi tạo BUS với DAO
            nvBUS = new NhanVienBUS(new NhanVienDAO());

            dataGridView1.DataBindingComplete += DataGridViewNhanVien_DataBindingComplete;
            cbTrangThai.SelectedIndex = 1;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dataGridView1.AllowUserToAddRows = false;

            LoadNhanVienData();
        }
        private void DataGridViewNhanVien_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridView1;

            if (dgv.Columns["STT"] != null)
            {
                dgv.Columns["STT"].HeaderText = "STT";
                dgv.Columns["STT"].Width = 50;
                dgv.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["STT"].DisplayIndex = 0;
            }

            if (dgv.Columns["NhanVienID"] != null)
                dgv.Columns["NhanVienID"].Visible = false;

            if (dgv.Columns["FullName"] != null)
                dgv.Columns["FullName"].HeaderText = "Tên Nhân Viên";

            if (dgv.Columns["Phone"] != null)
                dgv.Columns["Phone"].HeaderText = "Số Điện Thoại";

            if (dgv.Columns["TrangThai"] != null)
                dgv.Columns["TrangThai"].HeaderText = "Trạng Thái";

            // Chỉ đọc
            dgv.ReadOnly = true;

            // Header style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }

        private void LoadNhanVienData()
        {
            dtNhanVien = new DataTable();
            dtNhanVien.Columns.Add("STT", typeof(int));
            dtNhanVien.Columns.Add("NhanVienID", typeof(int));
            dtNhanVien.Columns.Add("FullName", typeof(string));
            dtNhanVien.Columns.Add("Phone", typeof(string));
            dtNhanVien.Columns.Add("TrangThai", typeof(string));

            // Lấy danh sách nhân viên từ BUS
            var listNV = nvBUS.GetAllNhanVien();

            int stt = 1;
            foreach (var nv in listNV)
            {
                dtNhanVien.Rows.Add(
                    stt++,
                    nv.NhanVienID,
                    nv.FullName,
                    nv.Phone,
                    nv.TrangThai
                );
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = nvBUS.GetAllNhanVien();

            //// Đặt tên cột
            //dataGridView1.Columns["STT"].HeaderText = "STT";
            //dataGridView1.Columns["NhanVienID"].HeaderText = "Mã NV";
            //dataGridView1.Columns["FullName"].HeaderText = "Tên Nhân Viên";
            //dataGridView1.Columns["Phone"].HeaderText = "Số Điện Thoại";
            //dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

            //// Ẩn cột ID nếu không cần
            //dataGridView1.Columns["NhanVienID"].Visible = false;

            //// Căn giữa STT
            //dataGridView1.Columns["STT"].Width = 50;
            //dataGridView1.Columns["STT"].DefaultCellStyle.Alignment =
            //    DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["STT"].DisplayIndex = 0;

            // Chỉ đọc
            dataGridView1.ReadOnly = true;

            // Định dạng header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Tự điều chỉnh các cột
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Chọn theo dòng
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private bool ValidateInput()
        {


            // Check Họ
            if (string.IsNullOrWhiteSpace(txtHo.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHo.Focus();
                return false;
            }

            // Check Tên
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTen.Focus();
                return false;
            }

            // Check SĐT (chỉ nhận số và độ dài hợp lệ)
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSdt.Focus();
                return false;
            }
            else if (!txtSdt.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSdt.Focus();
                return false;
            }
            else if (txtSdt.Text.Length < 9 || txtSdt.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải từ 9–11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSdt.Focus();
                return false;
            }

            // Check DateJoin
            if (dateTimePicker1.Value >  DateTime.Now)
            {
                MessageBox.Show("Thời gian nhập không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Focus();
                return false;
            }

            return true; // Passed all checks
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text) || string.IsNullOrWhiteSpace(txtNhanVienID.Text))
            {
                MessageBox.Show("Vui lòng nhập chọn Nhân Viên cần sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHo.Focus();
                return;
            }

            if (!ValidateInput()) return;

            selectedNV.Ho = txtHo.Text;
            selectedNV.Ten = txtTen.Text;
            selectedNV.Phone = txtSdt.Text;
            selectedNV.DateJoin = dateTimePicker1.Value;
            selectedNV.TrangThai = cbTrangThai.SelectedItem.ToString();

            if (nvBUS.UpdateNhanVien(selectedNV))
            {
                MessageBox.Show("Update thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadNhanVienData();
        }

        private void cellClicked(object sender, DataGridViewCellEventArgs e)
        {
            LoadDataToForm();   
        }

        private void LoadDataToForm()
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["NhanVienID"].Value);
            selectedNV = nvBUS.GetNhanVienByID(id);

            if (selectedNV == null) return;

            // Gán thông tin từ DTO nv
            txtNhanVienID.Text = selectedNV.NhanVienID.ToString();
            txtHo.Text = selectedNV.Ho;
            txtTen.Text = selectedNV.Ten;
            txtSdt.Text = selectedNV.Phone;
            cbTrangThai.Text = selectedNV.TrangThai;

            // DateJoin
            if (selectedNV.DateJoin.HasValue)
            {
                dateTimePicker1.Value = selectedNV.DateJoin.Value;
                txtNgayThamGia.Text = selectedNV.DateJoin.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                txtNgayThamGia.Text = "";
            }

            // NgayCapNhat
            if (selectedNV.NgayCapNhat.HasValue)
            {
                txtNgayCapNhat.Text = selectedNV.NgayCapNhat.Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                txtNgayCapNhat.Text = "";
            }

            txtUserID.Text = selectedNV.UserID.ToString();
        }

        private void NhanVienGUI_SizeChanged(object sender, EventArgs e)
        {
            // Lấy kích thước hiện tại của form
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Phần chiều cao phía trên = panel1 + flowLayoutPanel1
            int topSectionHeight = 224; // đúng theo Designer

            // Tính lại chiều rộng 70/30 cho panel1 và flowLayoutPanel1
            int leftWidth = (int)(formWidth * 0.70);
            int rightWidth = formWidth - leftWidth;

            // Cập nhật panel trái
            panel1.Width = leftWidth;

            // Cập nhật panel phải
            flowLayoutPanel1.Width = rightWidth;

            // Cập nhật dataGridView chiếm toàn bộ phần còn lại bên dưới
            dataGridView1.Height = formHeight - topSectionHeight - lblTitle.Height;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách nhân viên
                NhanVienBUS nhanVienBUS = new NhanVienBUS(new NhanVienDAO());
                List<NhanVienDTO> listNV = nhanVienBUS.GetAllNhanVien();

                if (listNV == null || listNV.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chọn nơi lưu file
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Excel File|*.xlsx";
                saveFile.FileName = "DanhSachNhanVien.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        // Tạo sheet
                        ExcelWorksheet ws = package.Workbook.Worksheets.Add("NhanVien");

                        // Tạo tiêu đề cột
                        ws.Cells[1, 1].Value = "ID";
                        ws.Cells[1, 2].Value = "Họ";
                        ws.Cells[1, 3].Value = "Tên";
                        ws.Cells[1, 4].Value = "Họ tên";
                        ws.Cells[1, 5].Value = "SĐT";
                        ws.Cells[1, 6].Value = "Trạng thái";
                        ws.Cells[1, 7].Value = "Ngày vào làm";
                        ws.Cells[1, 8].Value = "Ngày cập nhật";
                        ws.Cells[1, 9].Value = "Ngày khởi tạo";
                        ws.Cells[1, 10].Value = "UserID";

                        // Format header
                        using (var range = ws.Cells[1, 1, 1, 10])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }

                        // Đổ dữ liệu
                        int row = 2;
                        foreach (var nv in listNV)
                        {
                            ws.Cells[row, 1].Value = nv.NhanVienID;
                            ws.Cells[row, 1].Style.Numberformat.Format = "@";
                            ws.Cells[row, 2].Value = nv.Ho;
                            ws.Cells[row, 3].Value = nv.Ten;
                            ws.Cells[row, 4].Value = nv.FullName;
                            ws.Cells[row, 5].Value = nv.Phone;
                            ws.Cells[row, 5].Style.Numberformat.Format = "@";

                            ws.Cells[row, 6].Value = nv.TrangThai;

                            ws.Cells[row, 7].Value = nv.DateJoin;
                            ws.Cells[row, 7].Style.Numberformat.Format = "dd/MM/yyyy";


                            ws.Cells[row, 8].Value = nv.NgayCapNhat?.ToString("dd/MM/yyyy");
                            ws.Cells[row, 8].Style.Numberformat.Format = "dd/MM/yyyy";

                            ws.Cells[row, 9].Value = nv.NgayKhoiTao.ToString("dd/MM/yyyy");
                            ws.Cells[row, 9].Style.Numberformat.Format = "dd/MM/yyyy";
                            ws.Cells[row, 10].Value = nv.UserID;
                            ws.Cells[row, 10].Style.Numberformat.Format = "@";


                            row++;
                        }

                        // Auto fit
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // Lưu file
                        package.SaveAs(new FileInfo(saveFile.FileName));
                    }

                    MessageBox.Show("Xuất file Excel thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel Files|*.xlsx";

            NhanVienBUS nhanVienBUS = new NhanVienBUS(new NhanVienDAO());

            if (open.ShowDialog() == DialogResult.OK)
            {
                List<NhanVienDTO> listImport = ImportNhanVienFromExcel(open.FileName);

                foreach (var item in listImport)
                {
                    // hiện tại có NV cùng id thì update, không thì thêm mới
                    if (nhanVienBUS.GetNhanVienByID(item.NhanVienID) != null)
                    {
                        nhanVienBUS.UpdateNhanVien(item);
                    }
                    else
                    {
                        UserBUS userBUS = new UserBUS();
                        if (userBUS.GetUserByID(item.UserID) == null) // thêm khi nv sở hữu userID duy nhất
                            nhanVienBUS.AddNhanVien(item);
                        else nvBUS.UpdateNhanVien(item); // update khi co san 1 user trống cho nv assgin vào
                    }
                }

                if (listImport.Count > 0)
                {
                    // Ví dụ: đưa lên DataGridView
                    dataGridView1.DataSource = nhanVienBUS.GetAllNhanVien();

                    MessageBox.Show("Import thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để import!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private List<NhanVienDTO> ImportNhanVienFromExcel(string filePath)
        {
            List<NhanVienDTO> ds = new List<NhanVienDTO>();

            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    // Kiểm tra có sheet không
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        MessageBox.Show("File Excel không chứa sheet nào!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return ds;
                    }

                    // EPPlus dùng index 1-based !!!
                    ExcelWorksheet ws = package.Workbook.Worksheets[1];

                    int rowCount = ws.Dimension.Rows;

                    UserBUS userBUS = new UserBUS();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        NhanVienDTO nv = new NhanVienDTO();

                        nv.NhanVienID = int.TryParse(ws.Cells[row, 1].Value?.ToString(), out int id) ? id : 0;
                        nv.Ho = ws.Cells[row, 2].Value?.ToString();
                        nv.Ten = ws.Cells[row, 3].Value?.ToString();
                        nv.Phone = ws.Cells[row, 5].Value?.ToString();
                        nv.TrangThai = ws.Cells[row, 6].Value?.ToString();
                        nv.DateJoin = ws.Cells[row, 7].GetValue<DateTime?>();
                        nv.NgayCapNhat = DateTime.TryParse(ws.Cells[row, 8].Value?.ToString(), out DateTime nc) ? nc : (DateTime?)null;
                        nv.NgayKhoiTao = DateTime.TryParse(ws.Cells[row, 9].Value?.ToString(), out DateTime nk) ? nk : DateTime.Now;

                        nv.UserID = int.TryParse(ws.Cells[row, 10].Value?.ToString(), out int uid) ? uid : 0;

                        // Check user tồn tại
                        if (userBUS.GetUserByID(nv.UserID) == null)
                        {
                            MessageBox.Show($"Lỗi import: UserID {nv.UserID} không tồn tại!",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        ds.Add(nv);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi import file Excel: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ds;
        }

    }
}
