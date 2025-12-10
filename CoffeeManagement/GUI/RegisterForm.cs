using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
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
    public partial class RegisterForm: Form
    {
        private UserBUS userBUS = new UserBUS();
        private NhanVienBUS nhanVienBUS = new NhanVienBUS();
        public RegisterForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            SetupCustomControls();

            // TEST ONLY
            txtEmail.Text = "test1@gmail.com";
            txtPassword.Text = "test123";
            txtPassword2.Text = "test123";
            txtHo.Text = "test";
            txtTen.Text = "ter";
            txtSDT.Text = "0900996789";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string password2 = txtPassword2.Text;
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            string SDT = txtSDT.Text.Trim();
            int roleIndex = comboBox1.SelectedIndex;

            if (string.IsNullOrEmpty(email) || email == "Nhập email của bạn")
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Check email format
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password) || password == "Nhập mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Độ dài tối thiểu
            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password2) || password2 == "Nhập lại mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword2.Focus();
                return;
            }

            if (password != password2)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ho) || ho == "Nhập họ")
            {
                MessageBox.Show("Vui lòng nhập họ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ten) || ten == "Nhập tên")
            {
                MessageBox.Show("Vui lòng nhập tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrEmpty(SDT) || SDT == "Nhập số điện thoại")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            // Check SDT đúng 10 số:
            if (!System.Text.RegularExpressions.Regex.IsMatch(SDT, @"^(0[1-9][0-9]{8})$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ (phải gồm 10 số)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            if (roleIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn quyền (Role)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return;
            }

            int roleID = roleIndex + 1;

            try
            {
                NhanVienDTO nv = new NhanVienDTO();
                nv.Ten = ten;
                nv.Ho = ho;
                nv.Phone = SDT;
                nv.DateJoin = dateTimePicker1.Value;
                nv.TrangThai = "Trống lịch";

                bool register = userBUS.Register(email, password, roleID, nv);
                if (register)
                {
                    // Đóng form login và mở MainForm
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }                                           
                                
        private void SetupCustomControls()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            // Thêm hiệu ứng hover cho buttons
            btnRegister.MouseEnter += (s, e) => {
                btnRegister.BackColor = Color.FromArgb(0, 150, 150);
                btnRegister.Cursor = Cursors.Hand;
            };  
            btnRegister.MouseLeave += (s, e) => {
                btnRegister.BackColor = Color.FromArgb(0, 139, 139);
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

            txtPassword2.Enter += (s, e) => {
                if (txtPassword2.Text == "Nhập mật khẩu")
                {
                    txtPassword2.Text = "";
                    txtPassword2.ForeColor = Color.Black;
                    txtPassword2.PasswordChar = '●';
                }
            };
            txtPassword2.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtPassword2.Text))
                {
                    txtPassword2.Text = "Nhập mật khẩu";
                    txtPassword2.ForeColor = Color.Gray;
                    txtPassword2.PasswordChar = '\0';
                }
            };

            // Set initial placeholder
            txtEmail.Text = "Nhập email của bạn";
            txtEmail.ForeColor = Color.Gray;
            txtPassword.Text = "Nhập mật khẩu";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.PasswordChar = '\0';
            txtPassword2.Text = "Nhập mật khẩu";
            txtPassword2.ForeColor = Color.Gray;
            txtPassword2.PasswordChar = '\0';

            txtPassword.TextChanged += (s, e) =>
            {
                if (txtPassword.Text != "Nhập mật khẩu" && txtPassword.Text.Length > 0)
                    txtPassword.PasswordChar = '●';
                else
                    txtPassword.PasswordChar = '\0';
            };

            txtPassword2.TextChanged += (s, e) =>
            {
                if (txtPassword2.Text != "Nhập mật khẩu" && txtPassword2.Text.Length > 0)
                    txtPassword2.PasswordChar = '●';
                else
                    txtPassword2.PasswordChar = '\0';
            };
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPassword2.Focus();
            }
        }

        private void txtPassword2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                dateTimePicker1.Focus();
            }
        }

        private void txtSDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtHo.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtSDT.Focus();
            }
        }

        private void txtHo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtTen.Focus();
            }
        }

        private void btnRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnExit.Focus();
            }
        }

        private void txtTen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnRegister.Focus();
            }
        }

        private void btnExit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtEmail.Focus();
            }
        }
    }
}
