using ClosedXML.Excel;
//using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using CoffeeManagement.DTO;
namespace CoffeeManagement.GUI
{
    public partial class PhieuNhapGUI : UserControl
    {
        public event Action<int> RequestOpenCTPN;
        private bool DangThaoTac = false;
        private PhieuNhapBUS bus = new PhieuNhapBUS();
        /*private ChiTietPhieuNhapBUS ctBus = new ChiTietPhieuNhapBUS();*/
        private int selectedID = -1;
        private bool isAdding = false;
        private bool isEditing = false;
        private bool isDeleting = false;
        public PhieuNhapGUI()
        {
            InitializeComponent();
            this.Load += UserControl1_Load;
            this.SizeChanged += PhieuNhapGUI_SizeChanged;
            dgvPN.CellContentClick += dgvPN_CellContentClick;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.SelectedIndex = 0;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            DataTable dt = PhieuNhapBUS.PhieuNhap();
            dgvPN.DataSource = dt;
            if (!dgvPN.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "Chi tiết phiếu nhập";
                btnView.Name = "btnView";
                btnView.Text = "XEM";
                btnView.UseColumnTextForButtonValue = true;
                dgvPN.Columns.Add(btnView);
            }

            ClearFields(false);
        }

        private void LoadData()
        {
            dgvPN.DataSource = PhieuNhapBUS.PhieuNhap();
            if (!dgvPN.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "Chi tiết";
                btnView.Name = "btnView";
                btnView.Text = "XEM";
                btnView.UseColumnTextForButtonValue = true;
                dgvPN.Columns.Add(btnView);
            }
        }

        private void PhieuNhapGUI_SizeChanged(object sender, EventArgs e)
        {
            // Đảm bảo panelInfo giữ nguyên chiều rộng bên phải
            int infoWidth = panelInfo.Width;

            // Luôn cập nhật chiều rộng dgvPN để chiếm toàn bộ phần còn lại
            dgvPN.Width = this.ClientSize.Width - infoWidth;

            // Cập nhật chiều cao panelInfo và dgvPN để khớp phần còn lại (trừ top panel)
            int heightRest = this.ClientSize.Height - pnChucnang.Height;

            dgvPN.Height = heightRest;
            panelInfo.Height = heightRest;
        }

        private void ClearFields(bool enable)
        {
            txtID.Text = "";
            txtTotal.Text = "";
            txtGhiChu.Text = "";
            txtNVID.Text = "";
            txtNCCID.Text = "";

            txtTotal.ReadOnly = !enable;
            txtGhiChu.ReadOnly = !enable;
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
            LoadData();
        }

        private void dgvPN_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvPN.Rows[e.RowIndex];

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
                row.DefaultCellStyle.BackColor = dgvPN.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgvPN.DefaultCellStyle.ForeColor;
            }
        }

        private bool Validate()
        {
            bool valid = true;
            error.Clear();
            return valid;
        }

        private void dgvPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DangThaoTac)
            {
                return;
            }
            if (dgvPN.Columns[e.ColumnIndex].Name == "btnView" && e.RowIndex >= 0)
            {
                int sanphamID = Convert.ToInt32(dgvPN.Rows[e.RowIndex].Cells["PhieuNhapID"].Value);
                RequestOpenCTPN?.Invoke(sanphamID);
            }
        }

        private void dgvPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPN.Rows[e.RowIndex];

            selectedID = Convert.ToInt32(row.Cells["PhieuNhapID"].Value);
            txtID.Text = selectedID.ToString();
            dateTimePickerNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
            txtTotal.Text = row.Cells["TongTien"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            txtNVID.Text = row.Cells["NhanVienID"].Value.ToString();
            txtNCCID.Text = row.Cells["NhaCungCapID"].Value.ToString();
            if ( row.Cells["TrangThai"].Value.ToString() == "Hoàn tất")
            {
                cboTrangThai.SelectedIndex = 0;
            }
            else
            {
                cboTrangThai.SelectedIndex = 1;
            }

        }

        private void pnChucnang_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            ofd.Title = "Chọn file Excel để nhập";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;

                using (XLWorkbook wb = new XLWorkbook(path))
                {
                    var ws = wb.Worksheet(1); // sheet đầu tiên
                    DataTable dt = new DataTable();
                    bool firstRow = true;

                    foreach (var row in ws.RowsUsed())
                    {
                        if (firstRow)
                        {
                            // tạo cột từ dòng đầu
                            foreach (var cell in row.Cells())
                                dt.Columns.Add(cell.GetValue<string>());

                            firstRow = false;
                        }
                        else
                        {
                            // thêm dữ liệu từng dòng
                            dt.Rows.Add(row.Cells().Select(c => c.Value.ToString()).ToArray());
                        }
                    }

                    dgvPN.DataSource = dt;
                }

                MessageBox.Show("Nhập Excel thành công!");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sfd.Title = "Chọn nơi lưu file Excel";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;

                if (dgvPN.DataSource is DataTable dt)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Sheet1");
                        wb.SaveAs(path);
                    }

                    MessageBox.Show("Xuất Excel thành công!");
                }
                else
                {
                    MessageBox.Show("DataGridView không chứa DataTable!");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string text = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                LoadData();
                return;
            }
            if (!int.TryParse(text, out int key))
            {
                MessageBox.Show("Mã phiếu nhập phải là số!");
                return;
            }
            dgvPN.DataSource = PhieuNhapBUS.PhieuNhapID(key);
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Nếu chưa vào chế độ xóa
            if (!isDeleting)
            {
                // Chưa chọn dòng
                if (selectedID < 1)
                {
                    MessageBox.Show("Hãy chọn Phiếu nhập để xóa!");
                    return;
                }

                // Đang ở chế độ khác → chặn
                if (isAdding || isEditing)
                    return;

                // Vào chế độ xóa
                ForceMode("delete");
                return;
            }


            PhieuNhapDTO phieuNhapDTO = PhieuNhapBUS.ConvertToDTO(PhieuNhapBUS.PhieuNhapID(selectedID))[0];
            PhieuNhapBUS.UpdatePN(phieuNhapDTO.PhieuNhapID, phieuNhapDTO.NgayNhap, phieuNhapDTO.TongTien,
                phieuNhapDTO.GhiChu, "Chưa hoàn tất", phieuNhapDTO.NhanVienID, phieuNhapDTO.NhaCungCapID);
            MessageBox.Show("Đã set về chưa tất!");

            //selectedID = -1; // Nếu xóa thật

            FinishMode();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                // Nếu chưa vào chế độ thêm
                if (!isAdding)
                {
                    // Nếu đang ở chế độ khác → chặn
                    if (isEditing || isDeleting)
                        return;

                    ForceMode("add");
                    return;
                }
            }

            if (!ValidateInput())
                return;
            NhanVienBUS nvBUS = new NhanVienBUS();
            NhanVienDTO nv = nvBUS.ConvertRowToDTO(NhanVienBUS.LayNV_userID(Session.CurrentUser.UserID).Rows[0]);
            try
            {
                PhieuNhapBUS.InsertPN(
                    dateTimePickerNhap.Value,
                    Convert.ToDecimal(txtTotal.Text.Trim()),
                    txtGhiChu.Text.Trim(),
                    cboTrangThai.SelectedItem.ToString(),
                    nv.NhanVienID,
                    1
                );
            }
            catch (Exception ex)
            {
                return;
            }


            // Đang ở chế độ Lưu thêm
            MessageBox.Show("Đã thêm!");

            FinishMode();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                // Chưa vào chế độ sửa → kiểm tra trước

                if (selectedID < 1)
                {
                    MessageBox.Show("Hãy chọn PN để sửa!");
                    return;
                }

                // Nếu đang ở chế độ khác → chặn
                if (isAdding || isDeleting)
                    return;

                ForceMode("edit");
                return;
            }

            if (!ValidateInput())
                return;

            NhanVienBUS nvBUS = new NhanVienBUS();
            NhanVienDTO nv = nvBUS.ConvertRowToDTO(NhanVienBUS.LayNV_userID(Session.CurrentUser.UserID).Rows[0]);

            try
            {
                PhieuNhapBUS.UpdatePN(
                selectedID,
                dateTimePickerNhap.Value,
                Convert.ToDecimal(txtTotal.Text.Trim()),
                txtGhiChu.Text.Trim(),
                cboTrangThai.SelectedItem.ToString(),
                nv.NhanVienID,
                1
                );
            }
            catch (Exception ex)
            {
                return;
            }


            MessageBox.Show("Đã sửa!");
            FinishMode();
        }
        private bool IsBusy()
        {
            return isAdding || isEditing || isDeleting;
        }


        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtTotal.Text) || !txtTotal.Text.Trim().Replace(".", "").Replace(".", "").All(char.IsDigit))
            {
                MessageBox.Show("Tổng tiền chỉ được chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotal.Focus();
                return false;
            }
            
            if (cboTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Phải chọn trạng thái.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }    
            return true; // Passed all checks
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FinishMode();
        }
    }

}
