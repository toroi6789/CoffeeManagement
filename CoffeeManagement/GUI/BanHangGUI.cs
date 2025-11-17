using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagement.BUS;

namespace CoffeeManagement.GUI
{
    public partial class BanHangGUI : UserControl
    {
        public event Action<int> PnlBodyChangedToThanhToan;
        public BanHangGUI()
        {
            InitializeComponent();


        }


        // xử lý sự kiện khi load BanHangGUI
        private void BanHang_Load(object sender, EventArgs e)
        {
            DataTable sanPhamTable = SanPhamBUS.SanPham();
            foreach (DataRow row in sanPhamTable.Rows)
            {
                Button btn = new Button();
                btn.BackColor = Color.BurlyWood;
                btn.Width = 100;
                btn.Height = 100;
                btn.Margin = new Padding(10);
                btn.Text = row["TenSanPham"].ToString() + "\n" + row["GiaBan"].ToString() + " VND";
                btn.Name = row["SanPhamID"].ToString();
                btn.Click += Btn_Click;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        // xử lý sự kiện khi thay đổi kích thước của BanHangGUI
        private void BanHangGUI_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Size = new Size(this.Width - orderGUI1.Width - 20, this.Height - 20);
            orderGUI1.Location = new Point(flowLayoutPanel1.Width + 10, 3);
        }
        // xử lý sự kiện khi nhấn nút sản phẩm
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == Color.BurlyWood)
            {
                btn.BackColor = Color.LightGreen;
                int sanPhamID = int.Parse(btn.Name);
                DataTable sanPhamTable = SanPhamBUS.SanPhamTheoID(sanPhamID);
                if (sanPhamTable.Rows.Count > 0)
                {
                    DataRow row = sanPhamTable.Rows[0];
                    int maSanPham = (int)row["SanPhamID"];
                    string tenSanPham = row["TenSanPham"].ToString();
                    decimal giaBan = (decimal)row["GiaBan"];
                    // Thêm sản phẩm vào đơn hàng trong OrderGUI


                    this.orderGUI1.dataGridView1.Rows.Add(maSanPham, tenSanPham, giaBan);
                }
            }
            else
            {
                btn.BackColor = Color.BurlyWood;
                int sanPhamID = int.Parse(btn.Name);
                // Xóa sản phẩm khỏi đơn hàng trong OrderGUI
                foreach (DataGridViewRow dgvRow in this.orderGUI1.dataGridView1.Rows)
                {
                    if (dgvRow.Cells["SanPhamID"].Value != null && (int)dgvRow.Cells["SanPhamID"].Value == sanPhamID)
                    {
                        this.orderGUI1.dataGridView1.Rows.Remove(dgvRow);
                        break;
                    }
                }
            }

        }

        // xử lý sự kiện khi nhấn nút thanh toán trong OrderGUI
        public void OnOrderRequestPnlBodyChangedToThanhToan(int HoaDonID)
        {
            //restart button colors
            foreach (Button btn in flowLayoutPanel1.Controls)
            {
                btn.BackColor = Color.BurlyWood;
            }
            PnlBodyChangedToThanhToan?.Invoke(HoaDonID);
        }

        private void orderGUI1_Load(object sender, EventArgs e)
        {

        }
    }
}
