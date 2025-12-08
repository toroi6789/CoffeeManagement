using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoffeeManagement.GUI
{
    public partial class NCCGUI : UserControl
    {
        private int selectedID = -1;
        bool isAdding = false;
        bool isEditing = false;
        bool isDeleting = false;

        public NCCGUI()
        {
            InitializeComponent();

            // Fixed: tự động resize khi control thay đổi kích thước
            this.SizeChanged += NCCGUI_SizeChanged;

            dgvNCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNCC.MultiSelect = false;

            cboTrangThai.Items.Add("Hoạt động");
            cboTrangThai.Items.Add("Ngừng");
            cboTrangThai.SelectedIndex = 0;

            LoadNCC();
            ClearFields(false);
        }

        private void LoadNCC()
        {
            dgvNCC.DataSource = NCCBUS.GetAll();
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvNCC.Rows[e.RowIndex];

            selectedID = Convert.ToInt32(row.Cells["NhaCungCapID"].Value);

            txtID.Text = selectedID.ToString();
            txtTen.Text = row.Cells["TenNhaCungCap"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtWebsite.Text = row.Cells["Website"].Value.ToString();
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

            if (!ValidateNCC())
                return;

            // Đang ở chế độ Lưu thêm
            var n = ReadNCCFromForm();
            NCCBUS.Insert(n);
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
                    MessageBox.Show("Hãy chọn NCC để sửa!");
                    return;
                }
                ForceMode("edit");
                return;
            }

            if (!ValidateNCC())
                return;

            var n = ReadNCCFromForm();
            n.NhaCungCapID = selectedID;
            NCCBUS.Update(n);

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

            NCCBUS.Delete(selectedID);
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
                dgvNCC.DataSource = NCCBUS.Search(key);
        }

        // ==============================
        //         HÀM HỖ TRỢ
        // ==============================
        private NCCDTO ReadNCCFromForm()
        {
            return new NCCDTO(
                0,
                txtTen.Text,
                txtDiaChi.Text,
                txtSDT.Text,
                txtEmail.Text,
                txtWebsite.Text,
                cboTrangThai.Text
            );
        }

        private void ClearFields(bool enable)
        {
            txtID.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";

            txtTen.ReadOnly = !enable;
            txtDiaChi.ReadOnly = !enable;
            txtSDT.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            txtWebsite.ReadOnly = !enable;
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

                    if (cols.Length < 7) continue;

                    NCCDTO n = new NCCDTO(
                        0,
                        cols[1],
                        cols[2],
                        cols[3],
                        cols[4],
                        cols[5],
                        cols[6]
                    );

                    NCCBUS.Insert(n);
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
                    sw.WriteLine("ID,TenNCC,DiaChi,SoDienThoai,Email,Website,TrangThai");

                    // Rows
                    foreach (DataGridViewRow row in dgvNCC.Rows)
                    {
                        if (row.IsNewRow) continue;

                        sw.WriteLine(
                            EscapeCsv(row.Cells["NhaCungCapID"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["TenNhaCungCap"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["DiaChi"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["SoDienThoai"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["Email"].Value?.ToString()) + "," +
                            EscapeCsv(row.Cells["Website"].Value?.ToString()) + "," +
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

        private bool ValidateNCC()
        {
            bool valid = true;
            error.Clear();

            // Tên NCC
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                error.SetError(txtTen, "Tên nhà cung cấp không được để trống!");
                valid = false;
            }

            // Địa chỉ
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                error.SetError(txtDiaChi, "Địa chỉ không được để trống!");
                valid = false;
            }

            // Số điện thoại
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                error.SetError(txtSDT, "Số điện thoại không được để trống!");
                valid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text, @"^[0-9]{9,11}$"))
            {
                error.SetError(txtSDT, "Số điện thoại phải từ 9–11 số!");
                valid = false;
            }

            // Email (không bắt buộc)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    error.SetError(txtEmail, "Email không hợp lệ!");
                    valid = false;
                }
            }

            return valid;
        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
