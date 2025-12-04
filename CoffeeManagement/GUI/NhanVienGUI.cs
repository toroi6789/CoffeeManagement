using CoffeeManagement.BUS;
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Data;
using System.Drawing;
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
            dataGridView1.DataSource = dtNhanVien;

            // Đặt tên cột
            dataGridView1.Columns["STT"].HeaderText = "STT";
            dataGridView1.Columns["NhanVienID"].HeaderText = "Mã NV";
            dataGridView1.Columns["FullName"].HeaderText = "Tên Nhân Viên";
            dataGridView1.Columns["Phone"].HeaderText = "Số Điện Thoại";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

            // Ẩn cột ID nếu không cần
            dataGridView1.Columns["NhanVienID"].Visible = false;

            // Căn giữa STT
            dataGridView1.Columns["STT"].Width = 50;
            dataGridView1.Columns["STT"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["STT"].DisplayIndex = 0;

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

    }
}
