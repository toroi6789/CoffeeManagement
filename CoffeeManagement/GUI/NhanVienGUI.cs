using CoffeeManagement.BUS;
using CoffeeManagement.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement.GUI
{
    public partial class NhanVienGUI : UserControl
    {
        private NhanVienBUS nvBUS;
        private DataTable dtNhanVien;

        public NhanVienGUI()
        {
            InitializeComponent();

            // Khởi tạo BUS với DAO
            nvBUS = new NhanVienBUS(new NhanVienDAO());
            dataGridViewNhanVien.DataBindingComplete += DataGridViewNhanVien_DataBindingComplete;

            LoadNhanVienData();
        }
        private void DataGridViewNhanVien_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dataGridViewNhanVien;

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

            dataGridViewNhanVien.AutoGenerateColumns = true;
            dataGridViewNhanVien.DataSource = dtNhanVien;

            // Đặt tên cột
            dataGridViewNhanVien.Columns["STT"].HeaderText = "STT";
            dataGridViewNhanVien.Columns["NhanVienID"].HeaderText = "Mã NV";
            dataGridViewNhanVien.Columns["FullName"].HeaderText = "Tên Nhân Viên";
            dataGridViewNhanVien.Columns["Phone"].HeaderText = "Số Điện Thoại";
            dataGridViewNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái";

            // Ẩn cột ID nếu không cần
            dataGridViewNhanVien.Columns["NhanVienID"].Visible = false;

            // Căn giữa STT
            dataGridViewNhanVien.Columns["STT"].Width = 50;
            dataGridViewNhanVien.Columns["STT"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dataGridViewNhanVien.Columns["STT"].DisplayIndex = 0;

            // Chỉ đọc
            dataGridViewNhanVien.ReadOnly = true;

            // Định dạng header
            dataGridViewNhanVien.EnableHeadersVisualStyles = false;
            dataGridViewNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridViewNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewNhanVien.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridViewNhanVien.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Tự điều chỉnh các cột
            dataGridViewNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Chọn theo dòng
            dataGridViewNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewNhanVien.MultiSelect = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
