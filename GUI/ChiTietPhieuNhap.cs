using BUS;
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
namespace CoffeeManagement.GUI
{
    public partial class ChiTietPhieuNhap : UserControl
    {
        public int phieunhapID;
        private DataTable dtChitietPN;
        private BUS.ChiTietPhieuNhapBUS chitietphieunhap = new BUS.ChiTietPhieuNhapBUS();
        private NguyenLieuBUS nl_bus = new NguyenLieuBUS();
        private PhieuNhapBUS phieunhap = new PhieuNhapBUS();
        public ChiTietPhieuNhap()
        {
            InitializeComponent();
            this.Load += ChiTietPhieuNhap_Load;
        }
        public ChiTietPhieuNhap(int id)
        {
            InitializeComponent();
            phieunhapID = id;
            this.Load += ChiTietPhieuNhap_Load;
        }
        private void ChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            dtChitietPN = new DataTable();
            dtChitietPN.Columns.Add("STT", typeof(int));
            dtChitietPN.Columns.Add("NguyenLieuID", typeof(int));
            dtChitietPN.Columns.Add("TenNguyenLieu", typeof(string));
            dtChitietPN.Columns.Add("GiaNhap", typeof(decimal));
            dtChitietPN.Columns.Add("MoTa", typeof(string));
            CTPN.AutoGenerateColumns = false;
            CTPN.DataSource = dtChitietPN;
        }

        
        private void ChiTietPhieuNhap_SizeChanged_1(object sender, EventArgs e)
        {
            label1.Location = new Point((this.Width - label1.Width) / 2, label1.Location.Y);

            panel1.Size = new Size(this.Width / 2 - 50, this.Height - 110);
            panel1.Location = new Point((this.Width / 2) - panel1.Width - 25, panel1.Location.Y);

            panel2.Size = new Size(this.Width / 2 - 50, this.Height - 110);
            panel2.Location = new Point(this.Width - panel2.Width - 25, panel2.Location.Y);

            CTPN.Width = panel1.Width - 6;

            pictureBox1.Width = panel2.Width - 6;
            pictureBox1.Height = (int)(panel2.Height * 0.4);

            txtID_HD.Location = new Point((panel1.Width - txtID_HD.Width) / 2, txtID_HD.Location.Y);
            txtTotal.Location = new Point((panel1.Width - txtTotal.Width) / 2, txtTotal.Location.Y);

            label2.Location = new Point(label2.Location.X, pictureBox1.Location.Y + pictureBox1.Height + 20);
            label3.Location = new Point(label3.Location.X, label2.Location.Y + label2.Height + 20);
            label4.Location = new Point(label4.Location.X, label3.Location.Y + label3.Height + 20);
            label5.Location = new Point(label5.Location.X, label4.Location.Y + label4.Height + 20);
            label6.Location = new Point(label6.Location.X, label5.Location.Y + label5.Height + 20);

            txtID.Location = new Point(txtID.Location.X, label2.Location.Y);
            txtIDNL.Location = new Point(txtIDNL.Location.X, label3.Location.Y);
            txtTenNL.Location = new Point(txtTenNL.Location.X, label4.Location.Y);
            txtSoLuong.Location = new Point(txtSoLuong.Location.X, label5.Location.Y);
            txtPrice.Location = new Point(txtPrice.Location.X, label6.Location.Y);

        }

        private void CTPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = CTPN.Rows[e.RowIndex];
            int selectedID = Convert.ToInt32(row.Cells["PhieuNhapID"].Value);
            txtID.Text = selectedID.ToString();
            txtIDNL.Text = row.Cells["NgayNhap"].Value.ToString();
            txtTenNL.Text = row.Cells["TongTien"].Value.ToString();
            txtSoLuong.Text = row.Cells["GhiChu"].Value.ToString();
            txtPrice.Text = row.Cells["NhanVienID"].Value.ToString();
            txtTotal.Text = row.Cells["NhanVienID"].Value.ToString();
        }
    }
}
