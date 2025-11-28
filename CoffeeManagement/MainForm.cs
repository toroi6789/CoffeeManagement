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
using CoffeeManagement.DTO;
using CoffeeManagement.GUI;

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
        GUI.ListSanPham listSanPham = new GUI.ListSanPham();
        GUI.QuanLyNguyenLieu quanLyNguyenLieu = new GUI.QuanLyNguyenLieu();

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

            // Đảm bảo layout được cập nhật khi form được hiển thị
            this.Shown += (s, e) => {
                UpdateTitleBarLayout();
            };
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Xóa session
                Session.Logout();

                // Ẩn MainForm
                this.Hide();

                // Hiển thị lại form đăng nhập
                LoginForm loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Cập nhật thông tin user mới
                    UpdateUserInfo();
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
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
            this.pnlBody.Size = new Size(this.Width, this.Height - 69);
            // Cập nhật lại vị trí các controls trong title bar khi resize
            UpdateTitleBarLayout();
        }

        private void UpdateTitleBarLayout()
        {
            try
            {
                if (this.Width <= 0 || btnLogout == null || lblUserInfo == null)
                    return;

                // Đảm bảo btnLogout luôn ở vị trí đúng (trước window controls)
                int windowControlsWidth = 159; // 53*3 = 159 (minimize + maximize + close)
                int newX = this.Width - windowControlsWidth - btnLogout.Width - 10;
                
                // Đảm bảo nút không bị cắt
                if (newX < 300) // Nếu form quá nhỏ, đặt ở vị trí tối thiểu
                {
                    newX = 300;
                }
                
                if (newX > 0 && newX < this.Width - windowControlsWidth)
                {
                    btnLogout.Location = new Point(newX, 5);
                    btnLogout.Visible = true;
                }
                
                // Đảm bảo lblUserInfo không overlap với btnLogout
                if (btnLogout.Left > lblUserInfo.Left)
                {
                    int maxUserInfoWidth = btnLogout.Left - lblUserInfo.Left - 15;
                    if (maxUserInfoWidth > 50)
                    {
                        if (maxUserInfoWidth < 500)
                        {
                            lblUserInfo.Width = maxUserInfoWidth;
                        }
                        else
                        {
                            lblUserInfo.Width = 490; // Giữ kích thước tối đa
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error nếu cần
                System.Diagnostics.Debug.WriteLine("Error in UpdateTitleBarLayout: " + ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin user đang đăng nhập
            if (Session.IsLoggedIn && Session.CurrentUser != null)
            {
                UpdateUserInfo();
            }
            else
            {
                // Nếu chưa đăng nhập, quay lại form login
                this.Hide();
                LoginForm loginForm = new LoginForm();
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
                UpdateUserInfo();
                this.Show();
            }

            // Force update layout sau khi form đã load xong
            this.BeginInvoke(new Action(() => {
                UpdateTitleBarLayout();
            }));

            this.pnlBody.Controls.Clear();
            banHang.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(banHang);
        }

        private void UpdateUserInfo()
        {
            if (Session.IsLoggedIn && Session.CurrentUser != null)
            {
                // Hiển thị email và role - rút ngắn nếu cần
                string email = Session.CurrentUser.Email;
                string role = Session.CurrentUser.TenRole;
                
                // Rút ngắn email: chỉ hiển thị phần trước @ và rút ngắn domain
                if (email.Contains("@"))
                {
                    string[] parts = email.Split('@');
                    string emailPart = parts[0];
                    string domainPart = parts[1];
                    
                    // Rút ngắn phần trước @
                    if (emailPart.Length > 10)
                    {
                        emailPart = emailPart.Substring(0, 8) + "..";
                    }
                    
                    // Rút ngắn domain
                    if (domainPart.Length > 10)
                    {
                        domainPart = domainPart.Substring(0, 8) + "..";
                    }
                    
                    email = emailPart + "@" + domainPart;
                }
                
                // Rút ngắn role
                if (role.Length > 10)
                {
                    role = role.Substring(0, 8) + "..";
                }
                
                lblUserInfo.Text = $"{email} | {role}";
                
                // Cập nhật layout sau khi set text
                UpdateTitleBarLayout();
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlTitle_Resize(object sender, EventArgs e)
        {
            UpdateTitleBarLayout();
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

        private void bánHàngDanhSáchSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Danh sách sản phẩm!");
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            listSanPham.Dock = DockStyle.Fill;
            this.pnlBody.Controls.Add(listSanPham);
        }

        private void pnlTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nguyênLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản lý nguyên liệu!");
            // Xóa nội dung hiện tại trong pnlBody
            this.pnlBody.Controls.Clear();
            this.pnlBody.Controls.Add(quanLyNguyenLieu);
            quanLyNguyenLieu.Dock = DockStyle.Fill;
        }
    }
}
