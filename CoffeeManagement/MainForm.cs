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

        GUI.BanHangGUI banHang = new GUI.BanHangGUI();
        GUI.DSHoaDonGUI dSHoaDon = new GUI.DSHoaDonGUI();
        GUI.SanPhamADMIN quanlySanPham = new GUI.SanPhamADMIN();

        public MainForm()
        {
            InitializeComponent();

            EnableDraggingContent();

            banHang.Dock = DockStyle.Fill;
            dSHoaDon.Dock = DockStyle.Fill;
            //tạo control user BanHangGUI để lắng nghe sự kiện thay đổi panel body
            banHang.PnlBodyChangedToThanhToan += OnPnlBodyChangedToThanhToan;
            //lắng nghe sự kiện mở chi tiết hóa đơn từ control user DSHoaDonGUI
            dSHoaDon.RequestOpenCTHoaDon += OnRequestOpenCTHoaDon;
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
            this.pnlBody.Controls.Clear();
            banHang.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(banHang);
            //MessageBox.Show("Mở giao diện bán hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            //MessageBox.Show("Quản lý hóa đơn!");
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
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            // Tạo và thêm UserControl DSHoaDon vào pnlBody
            this.pnlBody.Controls.Add(quanlySanPham);
            quanlySanPham.Dock = DockStyle.Fill;
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

        private void DSHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            // Tạo và thêm UserControl DSHoaDon vào pnlBody
            this.pnlBody.Controls.Add(dSHoaDon);
            dSHoaDon.Dock = DockStyle.Fill;
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.pnlBody.Size = new Size(this.Width, this.Height - 64);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.pnlBody.Controls.Clear();
            banHang.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(banHang);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //
        //lắng nghe sự kiện thay đổi panel body trong control user 
        //
        //thanh toan
        public void OnPnlBodyChangedToThanhToan( int HoaDonID)
        {
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            // Tạo và thêm UserControl Thanh toan vào pnlBody
            GUI.ThanhToanGUI thanhToan = new GUI.ThanhToanGUI(HoaDonID);
            thanhToan.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(thanhToan);
            //lắng nghe sự kiện thay đổi panel body từ control user ThanhToanGUI
            thanhToan.RequestPnlBodyToBanHang += OnPnlBodyChangedToBanHang;
        }
        //ban hang
        public void OnPnlBodyChangedToBanHang()
        {
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            banHang.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(banHang);
        }

        // lắng nghe sự kiện mở chi tiết hóa đơn từ control user DSHoaDonGUI
        public void OnRequestOpenCTHoaDon(int ID)
        {
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            GUI.CTHoaDonGUI cTHoaDon = new GUI.CTHoaDonGUI(ID);
            cTHoaDon.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(cTHoaDon);
        }
        

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
