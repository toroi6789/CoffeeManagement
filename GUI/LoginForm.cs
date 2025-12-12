using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class LoginForm : Form
    {
        private UserBUS userBUS = new UserBUS();

        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            SetupCustomControls();

            // TESTING ONLY
            txtEmail.Text = "admin@cafe.vn";
            txtPassword.Text = "123456";
        }

        private void SetupCustomControls()
        {
            // Thêm hiệu ứng hover cho buttons
            btnLogin.MouseEnter += (s, e) => {
                btnLogin.BackColor = Color.FromArgb(0, 150, 150);
                btnLogin.Cursor = Cursors.Hand;
            };
            btnLogin.MouseLeave += (s, e) => {
                btnLogin.BackColor = Color.FromArgb(0, 139, 139);
            };

            btnExit.MouseEnter += (s, e) => {
                btnExit.BackColor = Color.FromArgb(100, 100, 100);
                btnExit.Cursor = Cursors.Hand;
            };
            btnExit.MouseLeave += (s, e) => {
                btnExit.BackColor = Color.FromArgb(128, 128, 128);
            };

            // Thêm placeholder cho textbox
            txtEmail.Enter += (s, e) => {
                if (txtEmail.Text == "Nhập email của bạn")
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = Color.Black;
                }
            };
            txtEmail.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = "Nhập email của bạn";
                    txtEmail.ForeColor = Color.Gray;
                }
            };

            txtPassword.Enter += (s, e) => {
                if (txtPassword.Text == "Nhập mật khẩu")
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.Black;
                    txtPassword.PasswordChar = '●';
                }
            };
            txtPassword.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.Text = "Nhập mật khẩu";
                    txtPassword.ForeColor = Color.Gray;
                    txtPassword.PasswordChar = '\0';
                }
            };

            // Set initial placeholder
            txtEmail.Text = "Nhập email của bạn";
            txtEmail.ForeColor = Color.Gray;
            txtPassword.Text = "Nhập mật khẩu";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.PasswordChar = '\0';

            txtPassword.TextChanged += (s, e) =>
            {
                if (txtPassword.Text != "Nhập mật khẩu" && txtPassword.Text.Length > 0)
                    txtPassword.PasswordChar = '●';
                else
                    txtPassword.PasswordChar = '\0';
            };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Kiểm tra placeholder
            if (string.IsNullOrEmpty(email) || email == "Nhập email của bạn")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password) || password == "Nhập mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                UserDTO user = userBUS.Login(email, password);

                if (user != null)
                {
                    

                    // Lưu thông tin user vào session
                    Session.Login(user);

                    // Đóng form login và mở MainForm
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email hoặc mật khẩu không đúng!\nTài khoản có thể đã dừng hoạt động", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ border bo góc cho panel
            Panel panel = sender as Panel;
            if (panel != null)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 15;
                    Rectangle rect = panel.ClientRectangle;
                    
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();
                    
                    panel.Region = new Region(path);
                    
                    // Vẽ border đẹp
                    using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 1))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                }
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ gradient cho header
            Panel panel = sender as Panel;
            if (panel != null)
            {
                Rectangle rect = panel.ClientRectangle;
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect, 
                    Color.FromArgb(0, 139, 139), 
                    Color.FromArgb(0, 120, 120), 
                    90f))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

