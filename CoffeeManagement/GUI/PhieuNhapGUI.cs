using ClosedXML.Excel;
using CoffeeManagement.BUS;
using MySqlX.XDevAPI;
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
    public partial class PhieuNhapGUI : UserControl
    {
        public PhieuNhapGUI()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            DataTable dt = PhieuNhapBUS.PhieuNhap();
            dgvPN.DataSource = dt;
        }

        private void PhieuNhapGUI_SizeChanged(object sender, EventArgs e)
        {
            int padding = 10;

            int titleHeight = 60;
            int functionHeight = 50;
            int rightPanelWidth = 350;

            // CONTAINER
            pnContainer.Location = new Point(0, 0);
            pnContainer.Size = new Size(this.Width, this.Height);



            // PANEL CHỨC NĂNG
            pnChucnang.Location = new Point(0, padding);
            pnChucnang.Size = new Size((int)(pnContainer.Width * 0.8), functionHeight);

            int halfWidth = this.Width / 2;

            // PANEL INFO = nửa phải
            panelInfo.Size = new Size(halfWidth - padding * 2, this.Height - pnChucnang.Bottom - padding * 2);
            panelInfo.Location = new Point(halfWidth + padding, pnChucnang.Bottom + padding);

            // DGV = nửa trái
            dgvPN.Location = new Point(padding, pnChucnang.Bottom + padding);
            dgvPN.Size = new Size(
                pnContainer.Width / 2,
                this.Height - pnChucnang.Bottom - padding * 2
            );

            // CONTAINER
            pnContainer.Location = new Point(0, 0);
            pnContainer.Size = new Size(this.Width, this.Height);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dgvPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPN.Rows[e.RowIndex];

            int selectedID = Convert.ToInt32(row.Cells["PhieuNhapID"].Value);

            txtID.Text = selectedID.ToString();
            txtTen.Text = row.Cells["NgayNhap"].Value.ToString();
            txtDiaChi.Text = row.Cells["TongTien"].Value.ToString();
            txtSDT.Text = row.Cells["GhiChu"].Value.ToString();
            txtEmail.Text = row.Cells["NhanVienID"].Value.ToString();
            txtWebsite.Text = row.Cells["NhaCungCapID"].Value.ToString();
            cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

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
            
            dgvPN.Columns.Clear();
            dgvPN.DataSource = BUS.PhieuNhapBUS.PhieuNhapID(Convert.ToInt32(txtSearch.Text));
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
