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
                //size
                Button btn = new Button();
                btn.BackColor = Color.BurlyWood;
                btn.Width = 100;
                btn.Height = 100;
                btn.Margin = new Padding(10);
                //text
                btn.Text = row["TenSanPham"].ToString() + "\n" + row["GiaBan"].ToString() + " VND";
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;
                btn.Name = row["SanPhamID"].ToString();
                //img
                //string img = row["Hinh"].ToString();
                //string path = Path.Combine(Application.StartupPath, @"Images", img);
                //Image img2 = Image.FromFile(path);
                //btn.Image = Compoment.ResizeImage(img2, 77, 77);
                //btn.Tag = path;

                string imgName = row["Hinh"].ToString();
                if (string.IsNullOrWhiteSpace(imgName))
                {
                    imgName = "null.png";
                }
                string path = Path.Combine(Application.StartupPath, @"Images", imgName);
                if (!File.Exists(path))
                {
                    path = Path.Combine(Application.StartupPath, @"Images", "null.png");
                }
                Image img2 = null;
                if (File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        img2 = Image.FromStream(stream);
                    }
                }

                btn.Image = Compoment.ResizeImage(img2, 77, 77);
                btn.Tag = path;
                //function
                btn.Click += Btn_Click;
                btn.SizeChanged += Btn_SizeChanged;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        // xử lý sự kiện khi thay đổi kích thước của BanHangGUI
        private void BanHangGUI_SizeChanged(object sender, EventArgs e)
        {
            orderGUI1.Size = new Size((int)(this.Width * 0.3), this.Height );
            flowLayoutPanel1.Size = new Size(this.Width - orderGUI1.Width - 20, this.Height - 20);
            orderGUI1.Location = new Point(flowLayoutPanel1.Width , 3);
            foreach(Button btn in flowLayoutPanel1.Controls)
            {
                btn.Width = (int)(flowLayoutPanel1.Width / 4);
                btn.Height = btn.Width;
            }
        }
        // xử lý sự kiện khi nhấn nút sản phẩm
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == Color.BurlyWood)
            {
                int sanPhamID = int.Parse(btn.Name);
                DataTable sanPhamTable = SanPhamBUS.SanPhamTheoID(sanPhamID);
                DataRow row = sanPhamTable.Rows[0];

                if (!row["TrangThai"].ToString().Equals("Hoạt động"))
                {
                    MessageBox.Show("Sản phẩm tạm dừng bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btn.BackColor = Color.LightGreen;
                if (sanPhamTable.Rows.Count > 0)
                {
                    int maSanPham = (int)row["SanPhamID"];
                    string tenSanPham = row["TenSanPham"].ToString();
                    decimal giaBan = (decimal)row["GiaBan"];
                    // Thêm sản phẩm vào đơn hàng trong OrderGUI


                    this.orderGUI1.dataGridView1.Rows.Add(maSanPham, tenSanPham, giaBan, 1);
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

        //
        public void Btn_SizeChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Width < 10) return;
            Image img = Image.FromFile((btn.Tag).ToString());
            btn.Image = Compoment.ResizeImage(img, (int)(btn.Width * 0.7), (int)(btn.Width * 0.7));
            if (btn.Width > 200)
            {
                btn.Font = new Font("Times New Roman", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            }
            else
            {
                btn.Font = new Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            }
        }

        //xử lý sự kiện khi nhấn nút thanh toán trong OrderGUI
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
