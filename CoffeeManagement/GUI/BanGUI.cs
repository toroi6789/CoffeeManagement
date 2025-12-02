using CoffeeManagement.BUS;
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace CoffeeManagement.GUI
{
    public partial class BanGUI : UserControl
    {
        private int selectedBanID = -1;

        public BanGUI()
        {
            InitializeComponent();
            dgvBan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBan.MultiSelect = false;
            LoadBan();
            cboStatus.Items.Add("Trống");
            cboStatus.Items.Add("Có người");
            cboStatus.SelectedIndex = 0;
        }

        // Tải danh sách bàn
        private void LoadBan()
        {
            dgvBan.DataSource = BanBUS.LayTatCaBan();
            BanDAO.ResetAutoIncrement();
        }

        bool isAdding = false;
        bool isEditing = false;
        bool isDeleting = false;

        // Validate thông tin bàn khi thêm hoặc sửa
        private bool ValidateBanData()
        {
            bool valid = true;
            error.Clear(); // Xóa lỗi cũ

            // Kiểm tra tên bàn không trống
            if (string.IsNullOrWhiteSpace(txtTenBan.Text))
            {
                error.SetError(txtTenBan, "Tên bàn không được để trống!");
                valid = false;
            }

            // Kiểm tra sức chứa > 0
            if (updownSucchua.Value <= 0)
            {
                error.SetError(updownSucchua, "Sức chứa phải lớn hơn 0!");
                valid = false;
            }

            return valid;
        }

        // Thêm bàn
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm") // Nếu là "Thêm", chuyển sang chế độ Thực hiện
            {
                ForceSwitchMode("add");
                btnThem.Text = "Thực hiện";
                txtTenBan.Focus();

                // Mở các trường nhập liệu
                txtTenBan.ReadOnly = false;
                updownSucchua.Enabled = true;
                cboStatus.Enabled = true;

                txtTenBan.Text = "";
                txtID.Text = "";

                updownSucchua.Value = 1;
                cboStatus.SelectedIndex = 0;

                return;
            }
            else // Nếu là "Thực hiện", thực hiện thao tác Thêm
            {
                // Validate dữ liệu
                if (!ValidateBanData())
                    return;

                // Lưu dữ liệu thêm
                var newBan = new BanDTO(0, txtTenBan.Text, (int)updownSucchua.Value, cboStatus.SelectedItem.ToString());
                BanBUS.ThemBan(newBan);

                MessageBox.Show("Đã thêm bàn mới!");
                LoadBan();

                // Reset lại
                ResetAllModes();
            }
        }

        // Sửa bàn
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Nếu đang trong chế độ "Sửa" và nhấn nút "Sửa"
            if (btnSua.Text == "Sửa")
            {
                ForceSwitchMode("edit");
                btnSua.Text = "Thực hiện"; // Đổi text thành "Thực hiện"
                MessageBox.Show("chọn bàn để sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở các trường nhập liệu để người dùng sửa
                txtTenBan.ReadOnly = false;
                updownSucchua.Enabled = true;
                cboStatus.Enabled = true;
            }
            else // Nếu là "Thực hiện", thực hiện thao tác Sửa
            {
                // Validate dữ liệu
                if (!ValidateBanData())
                    return;

                // Lưu dữ liệu sửa bàn
                var updatedBan = new BanDTO(selectedBanID, txtTenBan.Text, (int)updownSucchua.Value, cboStatus.SelectedItem.ToString());
                BanBUS.SuaBan(updatedBan);

                MessageBox.Show("Đã sửa bàn!");
                LoadBan(); // Tải lại danh sách bàn

                // Reset lại
                ResetAllModes();
            }
        }

        // Xóa bàn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Nếu đang trong chế độ "Xóa" và nhấn nút "Xóa"
            if (btnXoa.Text == "Xóa")
            {
                ForceSwitchMode("delete");
                btnXoa.Text = "Xác nhận"; // Đổi text thành "Xác nhận"

                MessageBox.Show("Hãy chọn bàn để xóa!");
                return;
            }
            else // Nếu là "Xác nhận", thực hiện thao tác Xóa
            {
                var confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        BanBUS.XoaBan(selectedBanID); // Xóa bàn
                        LoadBan(); // Tải lại danh sách bàn
                        MessageBox.Show("Đã xóa!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }

                // Reset lại
                ResetAllModes();
            }
        }

        // Khi chọn 1 dòng trong DataGridView
        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvBan.Rows[e.RowIndex];

            selectedBanID = Convert.ToInt32(row.Cells["BanID"].Value);
            txtID.Text = row.Cells["BanID"].Value.ToString();
            txtTenBan.Text = row.Cells["TenBan"].Value.ToString();
            updownSucchua.Value = Convert.ToInt32(row.Cells["SucChua"].Value);
            cboStatus.SelectedItem = row.Cells["TrangThai"].Value.ToString();
        }

        // Tìm kiếm bàn
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadBan();
            }
            else
            {
                dgvBan.DataSource = BanBUS.TimKiemBan(keyword);
            }
        }

        // Hủy chế độ thêm/sửa/xóa nếu chọn chức năng khác
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

        // Reset các trạng thái sau khi hoàn thành thao tác
        private void ResetAllModes()
        {
            isAdding = false;
            isEditing = false;
            isDeleting = false;

            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";

            txtTenBan.ReadOnly = true;
            updownSucchua.Enabled = false;
            cboStatus.Enabled = false;

            txtTenBan.Text = "";
            updownSucchua.Value = 1;
            cboStatus.SelectedIndex = 0;

            selectedBanID = -1;
        }

        private void BanGUI_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // ==============================
        //        TỰ ĐỘNG CANH LAYOUT
        // ==============================
        private void BanGUI_SizeChanged(object sender, EventArgs e)
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
            dgvBan.Location = new Point(padding, pnChucnang.Bottom + padding);
            dgvBan.Size = new Size(
                panelInfo.Left - padding * 2,
                this.Height - dgvBan.Top - padding
            );

            // ---- CONTAINER (TUỲ CHỌN) ----
            pnContainer.Location = new Point(0, 0);
            pnContainer.Size = new Size(this.Width, this.Height);
        }

        private void panelInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvBan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvBan.Rows[e.RowIndex];

            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "";

            if (trangThai == "Trống")
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (trangThai == "Có người")
            {
                row.DefaultCellStyle.BackColor = Color.LightSalmon;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                row.DefaultCellStyle.BackColor = dgvBan.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgvBan.DefaultCellStyle.ForeColor;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV files (*.csv)|*.csv";

            if (open.ShowDialog() == DialogResult.OK)
            {
                var lines = System.IO.File.ReadAllLines(open.FileName);

                if (lines.Length <= 1)
                {
                    MessageBox.Show("File rỗng hoặc sai định dạng!");
                    return;
                }

                // Bỏ dòng header
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] cols = lines[i].Split(',');

                    if (cols.Length < 3) continue; // tùy bảng bạn bao nhiêu cột

                    // Ví dụ import cho bảng Bàn
                    var ban = new BanDTO(
                        0,
                        cols[1],
                        Convert.ToInt32(cols[2]),
                        cols[3]
                    );

                    BanBUS.ThemBan(ban);
                }

                MessageBox.Show("Import thành công!");
                LoadBan();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV files (*.csv)|*.csv";
            save.FileName = "export.csv";

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(save.FileName, false))
                {
                    // Ghi header
                    for (int i = 0; i < dgvBan.Columns.Count; i++)
                    {
                        sw.Write(dgvBan.Columns[i].HeaderText);
                        if (i < dgvBan.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();

                    // Ghi dữ liệu từng dòng
                    foreach (DataGridViewRow row in dgvBan.Rows)
                    {
                        if (row.IsNewRow) continue;

                        for (int i = 0; i < dgvBan.Columns.Count; i++)
                        {
                            sw.Write(row.Cells[i].Value?.ToString());
                            if (i < dgvBan.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();
                    }
                }

                MessageBox.Show("Xuất file thành công!");
            }
        }

    }
}
