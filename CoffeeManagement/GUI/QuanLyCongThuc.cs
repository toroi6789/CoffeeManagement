using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
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
        public int sanphamID;
        private NguyenLieuBUS nl_bus = new NguyenLieuBUS();
        private DataTable dtNguyenLieu;
        private DataTable dtNguyenLieuSP;
        private SanPhamNguyenLieuBUS spnl_bus = new SanPhamNguyenLieuBUS();
        bool DangThaoTac = false;
        public QuanLyCongThuc()
        {
            InitializeComponent();
            this.Load += QuanLyCongThuc_Load;
        }
        public QuanLyCongThuc(int id)
        {
            InitializeComponent();
            sanphamID = id;
            this.Load += QuanLyCongThuc_Load;
        }

        private void QuanLyCongThuc_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            txtID.Text = sanphamID.ToString();
            txtID.ReadOnly = true;
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
            AllNguyenLieu.AutoGenerateColumns = true;
            AllNguyenLieu.DataSource = dtNguyenLieu;
            LocTatCaNguyenLieu();

            // Format AllNguyenLieu
            FormatDataGridView(AllNguyenLieu);

            // DataTable cho NguyenLieuSP (tương tự)
            dtNguyenLieuSP = new DataTable();
            dtNguyenLieuSP.Columns.Add("STT", typeof(int));
            dtNguyenLieuSP.Columns.Add("NguyenLieuID", typeof(int));
            dtNguyenLieuSP.Columns.Add("TenNguyenLieu", typeof(string));
            dtNguyenLieuSP.Columns.Add("GiaNhap", typeof(decimal));
            dtNguyenLieuSP.Columns.Add("MoTa", typeof(string));
            dtNguyenLieuSP.Columns.Add("TrangThai", typeof(string));
            dtNguyenLieuSP.Columns.Add("DanhMucID", typeof(int));
            dtNguyenLieuSP.Columns.Add("DonVi", typeof(string));
            dtNguyenLieuSP.Columns.Add("SoLuongTon", typeof(decimal));
            dtNguyenLieuSP.Columns.Add("SoLuongSuDung", typeof(decimal));
            NguyenLieuSP.AutoGenerateColumns = true;
            NguyenLieuSP.DataSource = dtNguyenLieuSP;
            LocNguyenLieuCuaSanPham();
            // Format NguyenLieuSP tương tự
            FormatDataGridView(NguyenLieuSP);
            FormatDataGridView(AllNguyenLieu);
        }

        private void FormatDataGridView(DataGridView dgv)
        {
            dgv.Columns["STT"].HeaderText = "STT";
            dgv.Columns["NguyenLieuID"].HeaderText = "Mã Nguyên Liệu";
            dgv.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
            dgv.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            dgv.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgv.Columns["MoTa"].HeaderText = "Mô Tả";
            dgv.Columns["DanhMucID"].HeaderText = "Mã Danh Mục";
            dgv.Columns["DonVi"].HeaderText = "Đơn Vị";
            dgv.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            dgv.Columns["STT"].Width = 50;
            dgv.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["STT"].DisplayIndex = 0;
            dgv.ReadOnly = true;
            // Tự động điều chỉnh chiều cao dòng
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue; // Màu nền
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;     // Màu chữ
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Font chữ
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;   // Căn giữa header
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false; 
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void QuanLyCongThuc_SizeChanged(object sender, EventArgs e)
        {
            panel1.Size = new Size(this.Width / 2 - 50, this.Height - 110);
            panel1.Location = new Point((this.Width / 2) - panel1.Width - 25, panel1.Location.Y);
            panel2.Size = new Size(this.Width / 2 - 50, this.Height - 110);
            panel2.Location = new Point(this.Width - panel2.Width - 25, panel2.Location.Y);
            AllNguyenLieu.Width = panel1.Width - 6;
            AllNguyenLieu.Height = panel1.Height - 6;
            NguyenLieuSP.Width = panel2.Width - 6;
            NguyenLieuSP.Height = panel2.Height - 6;
            txtID.Location = new Point((panel1.Width - txtID.Width) / 2, txtID.Location.Y);
            txtID_NL.Location = new Point((panel1.Width - txtID_NL.Width) / 2, txtID_NL.Location.Y);
        }

        private void AllNguyenLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DangThaoTac)
            {
                MessageBox.Show("Đang ở chế độ thêm/sửa. Vui lòng Lưu hoặc Hủy trước khi chọn nguyên liệu khác!",
                               "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (e.RowIndex >= 0)
            {
                btnThem.Enabled = true;
                btnXoa.Enabled = NguyenLieuSP.Rows.Count > 0; 
                DataGridViewRow row = AllNguyenLieu.Rows[e.RowIndex];
                txtID_NL.Text = row.Cells["NguyenLieuID"].Value.ToString();
            }
        }

        // Load tất cả nguyên liệu vào AllNguyenLieu
        private void LocTatCaNguyenLieu()
        {
            dtNguyenLieu.Clear();
            var nguyenLieus = nl_bus.LayTatCaNguyenLieu(); 
            int stt = 1;
            foreach (var nl in nguyenLieus)
            {
                if (nl.TrangThai != "Hoạt động")
                    continue;
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
                dtNguyenLieu.Rows.Add(row);
            }
        }

        private void LocNguyenLieuCuaSanPham()
        {
            dtNguyenLieuSP.Clear();
            var congThucList = spnl_bus.LayCongThucTheoSanPhamBUS(sanphamID);
            int stt = 1;
            foreach (var ct in congThucList)
            {
                var nl = ct.NguyenLieu; 
                if (nl != null)
                {
                    DataRow row = dtNguyenLieuSP.NewRow();
                    row["STT"] = stt++;
                    row["NguyenLieuID"] = nl.NguyenLieuID;
                    row["TenNguyenLieu"] = nl.TenNguyenLieu;
                    row["GiaNhap"] = nl.GiaNhap;
                    row["MoTa"] = nl.MoTa;
                    row["TrangThai"] = nl.TrangThai;
                    row["DanhMucID"] = nl.DanhMucID;
                    row["DonVi"] = nl.DonVi;
                    row["SoLuongTon"] = nl.SoLuongTon;
                    row["SoLuongSuDung"] = ct.SoLuongSuDung; 
                    dtNguyenLieuSP.Rows.Add(row);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (AllNguyenLieu.CurrentRow == null || AllNguyenLieu.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một nguyên liệu từ danh sách bên trái để thêm vào công thức!",
                                "Chưa chọn nguyên liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nguyenLieuID = Convert.ToInt32(AllNguyenLieu.CurrentRow.Cells["NguyenLieuID"].Value);
            bool daTonTai = dtNguyenLieuSP.AsEnumerable()
                .Any(row => row.Field<int>("NguyenLieuID") == nguyenLieuID);

            if (daTonTai)
            {
                MessageBox.Show("Nguyên liệu này đã có trong công thức rồi!\nBạn có thể sửa số lượng bằng cách xóa và thêm lại.",
                                "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy thông tin nguyên liệu 
            DataGridViewRow selectedRow = AllNguyenLieu.CurrentRow;
            DataRow newRow = dtNguyenLieuSP.NewRow();
            newRow["STT"] = dtNguyenLieuSP.Rows.Count + 1;
            newRow["NguyenLieuID"] = selectedRow.Cells["NguyenLieuID"].Value;
            newRow["TenNguyenLieu"] = selectedRow.Cells["TenNguyenLieu"].Value;
            newRow["GiaNhap"] = selectedRow.Cells["GiaNhap"].Value;
            newRow["TrangThai"] = selectedRow.Cells["TrangThai"].Value;
            newRow["MoTa"] = selectedRow.Cells["MoTa"].Value;
            newRow["DanhMucID"] = selectedRow.Cells["DanhMucID"].Value;
            newRow["SoLuongTon"] = selectedRow.Cells["SoLuongTon"].Value;
            newRow["DonVi"] = selectedRow.Cells["DonVi"].Value;
            newRow["SoLuongSuDung"] = 0;
            spnl_bus.ThemNguyenLieuVaoSanPham(
                sanphamID,
                (int)selectedRow.Cells["NguyenLieuID"].Value,
                0
            );
            dtNguyenLieuSP.Rows.Add(newRow);
            LocTatCaNguyenLieu();
            AllNguyenLieu.CurrentRow.Selected = false;
            btnThem.Enabled = false;
            MessageBox.Show("Đã thêm nguyên liệu vào công thức!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (NguyenLieuSP.CurrentRow == null || NguyenLieuSP.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu trong công thức để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa nguyên liệu này khỏi công thức?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int nguyenLieuID = Convert.ToInt32(
                    NguyenLieuSP.CurrentRow.Cells["NguyenLieuID"].Value
                );
                NguyenLieuSP.Rows.Remove(NguyenLieuSP.CurrentRow);
                spnl_bus.XoaNguyenLieuCuaSanPham(nguyenLieuID);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch_ID.Text))
            {
                errorProvider1.SetError(txtSearch_ID, "Vui lòng nhập ID nguyên liệu cần tìm!");
                LocTatCaNguyenLieu();
                return;
            }
            errorProvider1.Clear();
            int idCanTim;
            if (!int.TryParse(txtSearch_ID.Text.Trim(), out idCanTim))
            {
                errorProvider1.SetError(txtSearch_ID, "ID phải là số nguyên!");
                LocTatCaNguyenLieu();
                return;
            }
            DataView dv = dtNguyenLieu.DefaultView;
            dv.RowFilter = $"NguyenLieuID = {idCanTim}";
            dv.Sort = "STT ASC";
            AllNguyenLieu.DataSource = dv;
            // Kiểm tra kết quả tìm kiếm
            if (AllNguyenLieu.Rows.Count == 0 || (AllNguyenLieu.Rows.Count == 1 && AllNguyenLieu.Rows[0].IsNewRow))
            {
                MessageBox.Show($"Không tìm thấy nguyên liệu có ID = {idCanTim}",
                                "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LocTatCaNguyenLieu();
                txtSearch_ID.Focus();
                txtSearch_ID.SelectAll();
            }
            else
            {
                AllNguyenLieu.Rows[0].Selected = true;
                AllNguyenLieu.CurrentCell = AllNguyenLieu.Rows[0].Cells[1];
                AllNguyenLieu.FirstDisplayedScrollingRowIndex = 0;
                MessageBox.Show($"Đã tìm thấy nguyên liệu ID = {idCanTim}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
