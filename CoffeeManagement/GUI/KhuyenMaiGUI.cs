using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoffeeManagement.GUI
{
    public partial class KhuyenMaiGUI : UserControl
    {
        private int selectedID = -1;
        bool isAdding = false;
        bool isEditing = false;
        bool isDeleting = false;

        public KhuyenMaiGUI()
        {
            InitializeComponent();

            // Fixed: tự động resize khi control thay đổi kích thước
            this.SizeChanged += NCCGUI_SizeChanged;

            dgvNCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNCC.MultiSelect = false;

            cboTrangThai.Items.Add("Hoạt động");
            cboTrangThai.Items.Add("Ngừng");
            cboTrangThai.SelectedIndex = 0;

            cbb_Loai.Items.Add("Phần trăm");
            cbb_Loai.Items.Add("Tiền mặt");
            cbb_Loai.SelectedIndex = 0;

            LoadNCC();
            ClearFields(false);
        }

        private void LoadNCC()
        {
            dgvNCC.DataSource = KhuyenMaiBUS.GetAllKM();
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvNCC.Rows[e.RowIndex];

            selectedID = Convert.ToInt32(row.Cells["KhuyenMaiID"].Value);

            txtID.Text = selectedID.ToString();
            txtTen.Text = row.Cells["TenKhuyenMai"].Value.ToString();
            cbb_Loai.Text = row.Cells["LoaiKhuyenMai"].Value.ToString();
            txtMoTA.Text = row.Cells["MoTa"].Value.ToString();
            txtGiaTri.Text = row.Cells["GiaTri"].Value.ToString();
            dateTimePickerStart.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
            dateTimePickerEnd.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);
            cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
        }

        // ==============================
        //          THÊM
        // ==============================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                ForceMode("add");
                return;
            }

            if (!Validate())
                return;

            KhuyenMaiBUS.InsertKM(
                txtTen.Text.Trim(),
                cbb_Loai.Text,
                txtMoTA.Text.Trim(),
                Convert.ToDecimal( txtGiaTri.Text.Trim()),
                dateTimePickerStart.Value,
                dateTimePickerEnd.Value,
                cboTrangThai.Text
            );

            // Đang ở chế độ Lưu thêm
            MessageBox.Show("Đã thêm!");

            FinishMode();
        }



        // ==============================
        //           SỬA
        // ==============================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                if (selectedID == -1)
                {
                    MessageBox.Show("Hãy chọn KM để sửa!");
                    return;
                }
                ForceMode("edit");
                return;
            }

            if (!Validate())
                return;


            KhuyenMaiBUS.UpdateKM(
                selectedID,
                txtTen.Text.Trim(),
                cbb_Loai.Text,
                txtMoTA.Text.Trim(),
                Convert.ToDecimal( txtGiaTri.Text.Trim()),
                dateTimePickerStart.Value,
                dateTimePickerEnd.Value,
                cboTrangThai.Text
            );
            MessageBox.Show("Đã sửa!");
            FinishMode();
        }



        // ==============================
        //           XÓA
        // ==============================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!isDeleting)
            {
                if (selectedID == -1)
                {
                    MessageBox.Show("Hãy chọn NCC để xóa!");
                    return;
                }
                ForceMode("delete");
                return;
            }

            KhuyenMaiBUS.DeleteKM(selectedID);
            MessageBox.Show("Đã xóa!");

            FinishMode();
        }



        // ==============================
        //         TÌM KIẾM
        // ==============================
        private void btnTim_Click(object sender, EventArgs e)
        {
            string key = txtSearch.Text.Trim();

            if (key == "")
                LoadNCC();
            else
                dgvNCC.DataSource = KhuyenMaiBUS.GetKM_Name(key);
        }

        // ==============================
        //         HÀM HỖ TRỢ
        // ==============================


        private void ClearFields(bool enable)
        {
            txtID.Text = "";
            txtTen.Text = "";
            txtMoTA.Text = "";
            txtGiaTri.Text = "";

            txtTen.ReadOnly = !enable;
            txtMoTA.ReadOnly = !enable;
            txtGiaTri.ReadOnly = !enable;
            cboTrangThai.Enabled = enable;
        }

        private void ForceMode(string mode)
        {
            // Reset hết trước
            isAdding = isEditing = isDeleting = false;

            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";

            // Bật mode mới
            if (mode == "add")
            {
                isAdding = true;
                btnThem.Text = "Lưu";
            }
            else if (mode == "edit")
            {
                isEditing = true;
                btnSua.Text = "Lưu";
            }
            else if (mode == "delete")
            {
                isDeleting = true;
                btnXoa.Text = "Xác nhận";
            }

            // Cho phép nhập
            ClearFields(true);
        }


        private void FinishMode()
        {
            isAdding = isEditing = isDeleting = false;

            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";

            ClearFields(false);
            LoadNCC();
        }


        // ==============================
        //        TỰ ĐỘNG CANH LAYOUT
        // ==============================
        private void NCCGUI_SizeChanged(object sender, EventArgs e)
        {
            int padding = 10;

            // ---- KÍCH THƯỚC CỐ ĐỊNH CHO TIÊU ĐỀ & CHỨC NĂNG ----
            int titleHeight = 60;
            int functionHeight = 50;
            int rightPanelWidth = 350;

            // ---- PANEL TITLE ----
            pnTitle.Location = new Point(0, 0);
            pnTitle.Size = new Size(this.Width, titleHeight);

            // ---- PANEL CHỨC NĂNG ----
            pnChucnang.Location = new Point(0, pnTitle.Bottom + padding);
            pnChucnang.Size = new Size(this.Width, functionHeight);

            // ---- PANEL THÔNG TIN PHẢI ----
            panelInfo.Size = new Size(rightPanelWidth, this.Height - pnChucnang.Bottom - padding * 2);
            panelInfo.Location = new Point(this.Width - rightPanelWidth - padding, pnChucnang.Bottom + padding);

            // ---- DGV ----
            dgvNCC.Location = new Point(padding, pnChucnang.Bottom + padding);
            dgvNCC.Size = new Size(
                panelInfo.Left - padding * 2,
                this.Height - dgvNCC.Top - padding
            );

            // ---- CONTAINER (TUỲ CHỌN) ----
            pnContainer.Location = new Point(0, 0);
            pnContainer.Size = new Size(this.Width, this.Height);
        }

        private void dgvNCC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvNCC.Rows[e.RowIndex];

            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";

            if (trangThai == "Hoạt động")
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (trangThai == "Ngừng")
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;   // hoặc LightGray
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                row.DefaultCellStyle.BackColor = dgvNCC.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgvNCC.DefaultCellStyle.ForeColor;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV files (*.csv)|*.csv";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string[] lines = System.IO.File.ReadAllLines(open.FileName);

                if (lines.Length <= 1)
                {
                    MessageBox.Show("File rỗng hoặc không đúng định dạng!");
                    return;
                }

                // Bỏ dòng header
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] cols = SimpleCsvSplit(line);

                    if (cols.Length < 8 ) continue;

                    KhuyenMaiBUS.InsertKM(cols[1], cols[2], cols[3],Convert.ToDecimal( cols[4]),Convert.ToDateTime( cols[5]),Convert.ToDateTime( cols[6]), cols[7]);
                }

                LoadNCC();
                MessageBox.Show("Import thành công!");
            }
        }



        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV files (*.csv)|*.csv";
            save.FileName = "NCC_export.csv";

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new System.IO.StreamWriter(save.FileName, false))
                {

                    // Header
                    sw.WriteLine("KhuyenMaiID,TenKhuyenMai,LoaiKhuyenMai,MoTa,GiaTri,NgayBatDau,NgayKetThuc,TrangThai");

                    // Rows
                    foreach (DataGridViewRow row in dgvNCC.Rows)
                    {
                        if (row.IsNewRow) continue;

                        sw.WriteLine(
                            EscapeCsv(row.Cells["KhuyenMaiID"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["TenKhuyenMai"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["LoaiKhuyenMai"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["MoTa"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["GiaTri"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["NgayBatDau"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["NgayKetThuc"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["TrangThai"].Value?.ToString())
                        );
                    }
                }

                MessageBox.Show("Xuất file thành công!");
            }
        }

        private string EscapeCsv(string value)
        {
            if (value == null) return "";

            // Double quotes phải escape bằng cách lặp lại ""
            value = value.Replace("\"", "\"\"");

            return $"\"{value}\"";  // Bọc bằng dấu ngoặc kép
        }

        private string[] SimpleCsvSplit(string line)
        {
            List<string> parts = new List<string>();
            bool insideQuotes = false;
            string current = "";

            foreach (char c in line)
            {
                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                }
                else if (c == ',' && !insideQuotes)
                {
                    parts.Add(current);
                    current = "";
                }
                else
                {
                    current += c;
                }
            }

            parts.Add(current);
            return parts.ToArray();
        }

        private bool Validate()
        {
            bool valid = true;
            error.Clear();

            // Tên NCC
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                error.SetError(txtTen, "Tên không được để trống!");
                valid = false;
            }

            // date start < date end
            if (dateTimePickerStart.Value.Date >= dateTimePickerEnd.Value.Date)
            {
                error.SetError(dateTimePickerEnd, "Ngày kết thúc phải sau ngày bắt đầu!");
                valid = false;
            }


            // gia
            if (!string.IsNullOrWhiteSpace(txtGiaTri.Text))
            {
                if (Convert.ToDecimal(txtGiaTri.Text) <= 0 )
                {
                    error.SetError(txtGiaTri, "Giá trị không hợp lệ!");
                    valid = false;
                }
            }

            return valid;
        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
