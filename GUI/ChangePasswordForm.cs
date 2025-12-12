using BUS;
using DTO;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ChangePasswordForm: Form
    {
        private int userID;
        private UserBUS userBUS = new UserBUS();
        public ChangePasswordForm(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            SetupCustomControls();

            StartPosition = FormStartPosition.CenterScreen;
        }


        private void SetupCustomControls()
        {
            // Thêm hiệu ứng hover cho buttons
            btnLuu.MouseEnter += (s, e) => {
                btnLuu.BackColor = Color.FromArgb(0, 150, 150);
                btnLuu.Cursor = Cursors.Hand;
            };
            btnLuu.MouseLeave += (s, e) => {
                btnLuu.BackColor = Color.FromArgb(0, 139, 139);
            };

            btnExit.MouseEnter += (s, e) => {
                btnExit.BackColor = Color.FromArgb(100, 100, 100);
                btnExit.Cursor = Cursors.Hand;
            };
            btnExit.MouseLeave += (s, e) => {
                btnExit.BackColor = Color.FromArgb(128, 128, 128);
            };

            // Thêm placeholder cho textbox
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string password2 = txtPassword2.Text;
            UserDTO userDTO = userBUS.GetUserByID(this.userID);

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

            if (password.Equals(userDTO.MatKhau))
            {
                MessageBox.Show("Mật khẩu phải khác mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != password2)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword2.Focus();
                return;
            }

            try
            {
                userDTO.MatKhau = password;
                bool update = userBUS.UpdateUser(userDTO).Success;
                if (update)
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
                  

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
