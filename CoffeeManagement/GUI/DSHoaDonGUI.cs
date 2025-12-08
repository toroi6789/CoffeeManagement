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
using ClosedXML.Excel;
using System.Data;

namespace CoffeeManagement.GUI
{
    public partial class DSHoaDonGUI : UserControl
    {
        public event Action<int> RequestOpenCTHoaDon;

        public DSHoaDonGUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= Convert.ToInt32(txtSoHD.Text)) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnView")
            {
                int hoaDonID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["HoaDonID"].Value);
                RequestOpenCTHoaDon?.Invoke(hoaDonID);
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                int hoaDonID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["HoaDonID"].Value);
                var result = MessageBox.Show("Are you sure you want to delete this invoice?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Delete the invoice from the database
                    BUS.HoaDonBUS.XoaHoaDon(hoaDonID);
                    MessageBox.Show("Invoice deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Delete the row from DataGridView
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    txtSoHD.Text = (dataGridView1.Rows.Count - 1).ToString();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            if (txtSearch_ID.Text == "") { 
                errorProvider1 = new ErrorProvider();
                errorProvider1.SetError(txtSearch_ID, "nhap ID");
                DSHoaDon_Load(sender,e);
                return; 
            }
            errorProvider1.Clear();
            dataGridView1.DataSource = BUS.HoaDonBUS.HoaDonID(Convert.ToInt32(txtSearch_ID.Text));
            txtSoHD.Text = (dataGridView1.Rows.Count - 1).ToString();
            // thêm cột button sau khi gán
            if (!dataGridView1.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "View";
                btnView.Name = "btnView";
                btnView.Text = "VIEW";
                btnView.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnView);
            }
            if (!dataGridView1.Columns.Contains("btnDelete"))
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.HeaderText = "Delete";
                btnDelete.Name = "btnDelete";
                btnDelete.Text = "DELETE";
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnDelete);
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnView")
            {
                DataGridViewButtonCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                cell.Style.BackColor = Color.MediumSeaGreen;
                cell.Style.ForeColor = Color.MediumSeaGreen;
                cell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DataGridViewButtonCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                cell.Style.BackColor = Color.DeepPink;
                cell.Style.ForeColor = Color.DeepPink;
                cell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void DSHoaDon_SizeChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Size = new Size((int)(this.Width * 0.7), (int)(this.Height * 0.6));
            this.dataGridView1.Location = new Point(30,160);
            this.panel1.Location = new Point(dataGridView1.Size.Width + dataGridView1.Location.X + 30, 90);
            this.label2.Location = new Point(30,60);
            this.txtSearch_ID.Size = new Size((int)(this.Width * 0.5), txtSearch_ID.Size.Height);
            this.txtSearch_ID.Location = new Point(label2.Location.X + label2.Size.Width + 20, label2.Location.Y);
            this.btnSearch.Location = new Point(label2.Location.X + label2.Size.Width + txtSearch_ID.Size.Width + 30, label2.Location.Y + 4);
        }

        private void DSHoaDon_Load(object sender, EventArgs e)
        {
            // load data to datagridview
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = BUS.HoaDonBUS.TatCaHoaDon();
            // 
            txtSoHD.Text = (dataGridView1.Rows.Count - 1).ToString();
            // thêm cột button sau khi gán
            if (!dataGridView1.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "View";
                btnView.Name = "btnView";
                btnView.Text = "VIEW";
                btnView.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnView);
            }

            if (!dataGridView1.Columns.Contains("btnDelete"))
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.HeaderText = "Delete";
                btnDelete.Name = "btnDelete";
                btnDelete.Text = "DELETE";
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnDelete);
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void DSHoaDonGUI_ParentChanged(object sender, EventArgs e)
        {
            // load data to datagridview
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = BUS.HoaDonBUS.TatCaHoaDon();
            // 
            txtSoHD.Text = (dataGridView1.Rows.Count - 1).ToString();
            // thêm cột button sau khi gán
            if (!dataGridView1.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "View";
                btnView.Name = "btnView";
                btnView.Text = "VIEW";
                btnView.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnView);
            }

            if (!dataGridView1.Columns.Contains("btnDelete"))
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.HeaderText = "Delete";
                btnDelete.Name = "btnDelete";
                btnDelete.Text = "DELETE";
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnDelete);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = BUS.HoaDonBUS.TimKiemHoaDonTheoNgay(dateTimePicker1.Value,dateTimePicker2.Value);
            txtSoHD.Text = (dataGridView1.Rows.Count - 1).ToString();
            // thêm cột button sau khi gán
            if (!dataGridView1.Columns.Contains("btnView"))
            {
                DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                btnView.HeaderText = "View";
                btnView.Name = "btnView";
                btnView.Text = "VIEW";
                btnView.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnView);
            }
            if (!dataGridView1.Columns.Contains("btnDelete"))
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.HeaderText = "Delete";
                btnDelete.Name = "btnDelete";
                btnDelete.Text = "DELETE";
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnDelete);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sfd.Title = "Chọn nơi lưu file Excel";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;

                if (dataGridView1.DataSource is DataTable dt)
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
        private void btnImportExcel_Click(object sender, EventArgs e)
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

                    dataGridView1.DataSource = dt;
                }

                MessageBox.Show("Nhập Excel thành công!");
            }
        }
        private void button2_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
