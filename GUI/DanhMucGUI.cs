using BUS;
using System.Drawing;
using DTO;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class DanhMucGUI : UserControl
    {
        public DanhMucGUI()
        {
            InitializeComponent();
            dgvDanhMuc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhMuc.MultiSelect = false;
            LoadDanhMuc();
            cboStatus.Items.Add("Hoạt động");
            cboStatus.Items.Add("Không hoạt động");
        }
        private bool isAdding = false;
        private bool isEditing = false;
        private bool isDeleting = false;
        private int selectedID = -1;
        DanhMucBUS bll = new DanhMucBUS();

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                ForceSwitchMode("add");
                btnThem.Text = "Lưu";

                txtTenDanhMuc.Focus();
                txtTenDanhMuc.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                cboStatus.Enabled = true;

                txtID.Text = "";
                txtTenDanhMuc.Text = "";
                txtMoTa.Text = "";
                cboStatus.SelectedIndex = 0;
                txtPrice.Text = "";

                return;
            }

            if (!ValidateDanhMuc())
                return;

            // Lưu dữ liệu thêm
            try
            {
                DanhMucDTO dm = new DanhMucDTO(0,
                    txtTenDanhMuc.Text,
                    cboStatus.SelectedItem.ToString(),
                    txtMoTa.Text,
                    0);

                bll.Insert(dm);

                MessageBox.Show("Đã thêm danh mục!");
                LoadDanhMuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

            ResetAllModes();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!isDeleting)
            {
                ForceSwitchMode("delete");
                btnXoa.Text = "Xác nhận";
                MessageBox.Show("Hãy chọn danh mục để xóa!");
                return;
            }

            if (selectedID == -1)
            {
                MessageBox.Show("Chưa chọn danh mục!");
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bll.Delete(selectedID);
                    LoadDanhMuc();
                    MessageBox.Show("Đã xóa!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

            ResetAllModes();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                ForceSwitchMode("edit");
                btnSua.Text = "Lưu";
                MessageBox.Show("Hãy chọn danh mục để sửa!");
                return;
            }

            if (selectedID == -1)
            {
                MessageBox.Show("Chưa chọn danh mục!");
                return;
            }

                txtTenDanhMuc.Focus();
            if (!ValidateDanhMuc())
                return;

            try
            {
                DanhMucDTO dm = new DanhMucDTO(
                    selectedID,
                    txtTenDanhMuc.Text,
                    cboStatus.SelectedItem.ToString(),
                    txtMoTa.Text,
                    0);

                bll.Update(dm);

                MessageBox.Show("Đã sửa danh mục!");
                LoadDanhMuc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

            ResetAllModes();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (keyword == "")
            {
                LoadDanhMuc();     
                return;
            }

            dgvDanhMuc.DataSource = bll.Search(keyword);
        }

        private bool ValidateDanhMuc()
        {
            bool valid = true;
            error.Clear();

            if (string.IsNullOrWhiteSpace(txtTenDanhMuc.Text))
            {
                error.SetError(txtTenDanhMuc, "Tên danh mục không được để trống!");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtMoTa.Text))
            {
                error.SetError(txtMoTa, "Mô tả không được để trống!");
                valid = false;
            }

            return valid;
        }

        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];

            selectedID = Convert.ToInt32(row.Cells["DanhMucID"].Value);

            txtID.Text = row.Cells["DanhMucID"].Value.ToString();
            txtTenDanhMuc.Text = row.Cells["TenDanhMuc"].Value.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            cboStatus.SelectedItem = row.Cells["TrangThai"].Value.ToString();
            txtPrice.Text = row.Cells["GiaBan"].Value.ToString();

            if (isEditing)
            {
                txtTenDanhMuc.ReadOnly = false;
                txtMoTa.ReadOnly = false;
                cboStatus.Enabled = true;
            }
        }


        private void ForceSwitchMode(string newMode)
        {
            // Nếu đang trong chế độ khác → reset hết
            if (isAdding || isEditing || isDeleting)
                ResetAllModes();

            // Bật chế độ mới
            if (newMode == "add") isAdding = true;
            if (newMode == "edit") isEditing = true;
            if (newMode == "delete") isDeleting = true;
        }


        private void ResetAllModes()
        {
            isAdding = false;
            isEditing = false;
            isDeleting = false;

            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";

            txtTenDanhMuc.ReadOnly = true;
            txtMoTa.ReadOnly = true;
            cboStatus.Enabled = false;

            txtTenDanhMuc.Text = "";
            txtMoTa.Text = "";
            cboStatus.Text = "";

            selectedID = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadDanhMuc()
        {
            dgvDanhMuc.DataSource = bll.GetAllDanhMuc();
        }

        // ==============================
        //        TỰ ĐỘNG CANH LAYOUT
        // ==============================
        private void DanhMucGUI_SizeChanged(object sender, EventArgs e)
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
            panelInfo.Location = new Point(this.Width - rightPanelWidth - padding, pnChucnang.Bottom + padding);
            panelInfo.Size = new Size(rightPanelWidth, this.Height - pnChucnang.Bottom - padding * 2);

            // ---- DGV ----
            dgvDanhMuc.Location = new Point(padding, pnChucnang.Bottom + padding);
            dgvDanhMuc.Size = new Size(
                panelInfo.Left - padding * 2,
                this.Height - dgvDanhMuc.Top - padding
            );

            // ---- CONTAINER (TUỲ CHỌN) ----
            pnContainer.Location = new Point(0, 0);
            pnContainer.Size = new Size(this.Width, this.Height);
        }

        private void panelInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDanhMuc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvDanhMuc.Rows[e.RowIndex];
            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";

            if (trangThai == "Hoạt động")
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (trangThai == "Không hoạt động")
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                // reset màu về mặc định
                row.DefaultCellStyle.BackColor = dgvDanhMuc.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgvDanhMuc.DefaultCellStyle.ForeColor;
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
                    MessageBox.Show("File rỗng hoặc không đúng cấu trúc!");
                    return;
                }

                // Bỏ qua dòng header
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] cols = line.Split(',');

                    if (cols.Length < 5)
                        continue;

                    // Tạo DTO từ file
                    DanhMucDTO dm = new DanhMucDTO(
                        0,                       // ID tự tăng → để 0
                        cols[1],                 // TenDanhMuc
                        cols[2],                 // TrangThai
                        cols[3],                 // MoTa
                        Convert.ToDecimal(cols[4]) // GiaBan
                    );

                    bll.Insert(dm);
                }

                MessageBox.Show("Import thành công!");
                LoadDanhMuc();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV files (*.csv)|*.csv";
            save.FileName = "DanhMuc_export.csv";

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new System.IO.StreamWriter(save.FileName, false))
                {
                    // Header
                    sw.WriteLine("ID,TenDanhMuc,TrangThai,MoTa,GiaBan");

                    // Rows
                    foreach (DataGridViewRow row in dgvDanhMuc.Rows)
                    {
                        if (row.IsNewRow) continue;

                        sw.WriteLine(
                            $"{row.Cells["DanhMucID"].Value}," +
                            $"{row.Cells["TenDanhMuc"].Value}," +
                            $"{row.Cells["TrangThai"].Value}," +
                            $"{row.Cells["MoTa"].Value}," +
                            $"{row.Cells["GiaBan"].Value}"
                        );
                    }
                }

                MessageBox.Show("Xuất file thành công!");
            }
        }

        private void pnTitle_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
