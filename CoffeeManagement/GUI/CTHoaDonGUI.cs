using CoffeeManagement.BUS;
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
    public partial class CTHoaDonGUI : UserControl
    {
        public int hoaDonID;
        public CTHoaDonGUI()
        {
            InitializeComponent();
        }
        public CTHoaDonGUI(int id)
        {
            InitializeComponent();
            hoaDonID = id;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            HoaDon.DataSource = HoaDonBUS.ChiTietHoaDonID(hoaDonID);
            txtID_HD.Text = hoaDonID.ToString();
            int total = 0;
            foreach (DataGridViewRow row in HoaDon.Rows)
            {
                total +=( Convert.ToInt32(row.Cells["GiaBan"].Value) * Convert.ToInt32(row.Cells["Soluong"].Value));
            }
            txtTotal.Text = total.ToString();
            HoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Order_SizeChanged(object sender, EventArgs e)
        {
            label1.Location = new Point((this.Width - label1.Width) / 2, label1.Location.Y);

            panel1.Size = new Size(this.Width/2 - 50, this.Height - 110);
            panel1.Location = new Point((this.Width  / 2 ) - panel1.Width - 25, panel1.Location.Y);

            panel2.Size = new Size(this.Width / 2 - 50, this.Height  - 110);
            panel2.Location = new Point(this.Width - panel2.Width - 25 , panel2.Location.Y);

            HoaDon.Width = panel1.Width - 6;

            pictureBox1.Width = panel2.Width - 6;
            pictureBox1.Height = (int)(panel2.Height * 0.4);

            txtID_HD.Location = new Point((panel1.Width - txtID_HD.Width) / 2, txtID_HD.Location.Y);
            txtTotal.Location = new Point((panel1.Width - txtTotal.Width) / 2, txtTotal.Location.Y);

            label2.Location = new Point(label2.Location.X, pictureBox1.Location.Y + pictureBox1.Height + 20);
            label3.Location = new Point(label3.Location.X, label2.Location.Y + label2.Height + 20);
            label4.Location = new Point(label4.Location.X, label3.Location.Y + label3.Height + 20);
            label5.Location = new Point(label5.Location.X, label4.Location.Y + label4.Height + 20);
            label7.Location = new Point(label7.Location.X, label5.Location.Y + label5.Height + 20);
            label6.Location = new Point(label6.Location.X, label7.Location.Y + label7.Height + 20);
            label9.Location = new Point(label9.Location.X, label6.Location.Y + label6.Height + 20);

            txtID.Location = new Point(txtID.Location.X, label2.Location.Y);
            txtName.Location = new Point(txtName.Location.X, label3.Location.Y);
            txtStatus.Location = new Point(txtStatus.Location.X, label4.Location.Y);
            txtDescribe.Location = new Point(txtDescribe.Location.X, label5.Location.Y);
            txtPrice.Location = new Point(txtPrice.Location.X, label7.Location.Y);
            txtIDType.Location = new Point(txtIDType.Location.X, label6.Location.Y);
            txtQuantity.Location = new Point(txtQuantity.Location.X, label9.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                txtIDType.Text = "";
                txtID.Text = "";
                txtName.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
                txtStatus.Text = "";
                txtDescribe.Text = "";
                return;
            }

            if (e.RowIndex >= 0 )
            {
                DataGridViewRow row = this.HoaDon.Rows[e.RowIndex];
                if (row.Cells["SanPhamID"].Value == DBNull.Value) return;
                int soluong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                int IDsp = Convert.ToInt32(row.Cells["SanPhamID"].Value);
                DataTable dt = SanPhamBUS.SanPhamTheoID(IDsp);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtQuantity.Text = soluong.ToString();
                    txtID.Text = dr["SanPhamID"].ToString();
                    txtName.Text = dr["TenSanPham"].ToString();
                    txtPrice.Text = dr["GiaBan"].ToString();
                    txtStatus.Text = dr["TrangThai"].ToString();
                    txtDescribe.Text = dr["MoTa"].ToString();
                    txtIDType.Text = dr["DanhMucID"].ToString();

                    string img = dr["Hinh"].ToString();
                    string path = Path.Combine(Application.StartupPath, @"Images", img);
                    Image img2 = Image.FromFile(path);
                    pictureBox1.Image = Compoment.ResizeImage(img2, pictureBox1.Size.Width, pictureBox1.Size.Height);
                    pictureBox1.Tag = path;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // mở controll user Thanh Toan
        }

        private void HoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
