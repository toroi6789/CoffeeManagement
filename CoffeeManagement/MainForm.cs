using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        public MainForm()
        {
            InitializeComponent();
            
            EnableDraggingContent();
        }

        private void EnableDraggingContent()
        {
            pnlTitle.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };
        }

        private void banHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mở giao diện bán hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Thay bằng code mở UserControl hoặc Form bán hàng
            // Ví dụ:
            // var formBanHang = new BanHangForm();
            // formBanHang.TopLevel = false;
            // pnlBody.Controls.Clear();
            // pnlBody.Controls.Add(formBanHang);
            // formBanHang.Show();
        }

        private void quanLyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mở giao diện quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Mở giao diện quản lý
            // var formQuanLy = new QuanLyForm();
            // formQuanLy.TopLevel = false;
            // pnlBody.Controls.Clear();
            // pnlBody.Controls.Add(formQuanLy);
            // formQuanLy.Show();
        }

        private void hoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản lý hóa đơn!");
        }

        private void nhanVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản lý nhân viên!");
        }

        private void nhapKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản lý nhập kho!");
        }

        private void sanPhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản lý sản phẩm!");
        }

        private void datBanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đặt bàn!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Nếu đang phóng to thì trả về kích thước bình thường
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                // Ngược lại thì phóng to
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
