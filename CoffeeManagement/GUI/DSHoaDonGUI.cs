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
    public partial class DSHoaDonGUI : UserControl
    {
        public DSHoaDonGUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnView")
            {
                DataGridViewButtonCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                cell.Style.BackColor = Color.MediumSeaGreen;
                cell.Style.ForeColor = Color.White;
                cell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DataGridViewButtonCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                cell.Style.BackColor = Color.DeepPink;
                cell.Style.ForeColor = Color.White;
                cell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void DSHoaDon_SizeChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Size = new Size((int)(this.Width * 0.7), (int)(this.Height * 0.8));
            this.dataGridView1.Location = new Point(30,90);
            this.panel1.Location = new Point(dataGridView1.Size.Width + dataGridView1.Location.X + 30, 90);
            this.label2.Location = new Point(30,60);
            this.txtSearch_ID.Size = new Size((int)(this.Width * 0.5), txtSearch_ID.Size.Height);
            this.txtSearch_ID.Location = new Point(label2.Location.X + label2.Size.Width + 20, label2.Location.Y);
            this.btnSearch.Location = new Point(label2.Location.X + label2.Size.Width + txtSearch_ID.Size.Width + 30, label2.Location.Y + 4);
        }

        private void DSHoaDon_Load(object sender, EventArgs e)
        {
            // load data to datagridview
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
    }
}
