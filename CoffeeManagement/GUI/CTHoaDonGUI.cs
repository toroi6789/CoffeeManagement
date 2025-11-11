
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCP
{
    public partial class CTHoaDonGUI : UserControl
    {
        public CTHoaDonGUI( )
        {
            InitializeComponent();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            //HoaDon.DataSource = HoaDonBLL.ChiTietHoaDonID(hoaDonID);
            int total = 0;
            foreach (DataGridViewRow row in HoaDon.Rows)
            {
                total +=( Convert.ToInt32(row.Cells["ThanhTien"].Value) * Convert.ToInt32(row.Cells["Soluong"].Value));
            }
            txtTotal.Text = total.ToString();
        }

        private void Order_SizeChanged(object sender, EventArgs e)
        {
            label1.Location = new Point((this.Width - label1.Width) / 2, label1.Location.Y);

            panel1.Size = new Size(this.Width/2 - 50, this.Height - 60);
            panel1.Location = new Point((this.Width  / 2 ) - panel1.Width - 25, panel1.Location.Y);

            panel2.Size = new Size(this.Width / 2 - 50, this.Height  - 60);
            panel2.Location = new Point(this.Width - panel2.Width - 25 , panel2.Location.Y);

            HoaDon.Width = panel1.Width - 6;

            pictureBox1.Width = panel2.Width - 6;
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
                //DataTable dt = SanPhamBLL.SanPhamTheoID(IDsp);
                /*if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtQuantity.Text = soluong.ToString();
                    txtID.Text = dr["SanPhamID"].ToString();
                    txtName.Text = dr["TenSanPham"].ToString();
                    txtPrice.Text = dr["GiaBan"].ToString();
                    txtStatus.Text = dr["TrangThai"].ToString();
                    txtDescribe.Text = dr["MoTa"].ToString();
                    txtIDType.Text = dr["DanhMucID"].ToString();
                }*/
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
    }
}
