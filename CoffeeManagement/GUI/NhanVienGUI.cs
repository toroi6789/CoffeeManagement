using CoffeeManagement.BUS;
using CoffeeManagement.DAO;
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
    public partial class NhanVienGUI : UserControl
    {
        private NhanVienBUS nvBUS;

        public NhanVienGUI()
        {
            InitializeComponent();

            // Khởi tạo BUS với DAO
            nvBUS = new NhanVienBUS(new NhanVienDAO());

            LoadNhanVienData();
        }

        private void LoadNhanVienData()
        {
            flowLayoutPanel1.Controls.Clear(); // Xóa các control cũ

            var listNV = nvBUS.GetAllNhanVien();

            foreach (var nv in listNV)
            {
                // Tạo panel cho mỗi nhân viên
                Panel pnl = new Panel();
                pnl.Width = 300;
                pnl.Height = 100;
                pnl.Margin = new Padding(10);
                pnl.BackColor = Color.LightCyan;
                pnl.BorderStyle = BorderStyle.FixedSingle;

                // Label hiển thị tên đầy đủ
                Label lblName = new Label();
                lblName.Text = nv.FullName;
                lblName.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                lblName.Location = new Point(10, 10);
                lblName.AutoSize = true;

                // Label hiển thị Phone
                Label lblPhone = new Label();
                lblPhone.Text = $"Phone: {nv.Phone}";
                lblPhone.Font = new Font("Microsoft Sans Serif", 10);
                lblPhone.Location = new Point(10, 40);
                lblPhone.AutoSize = true;

                // Label hiển thị Trạng thái
                Label lblStatus = new Label();
                lblStatus.Text = $"Trạng thái: {nv.TrangThai}";
                lblStatus.Font = new Font("Microsoft Sans Serif", 10);
                lblStatus.Location = new Point(10, 65);
                lblStatus.AutoSize = true;

                // Thêm các label vào panel
                pnl.Controls.Add(lblName);
                pnl.Controls.Add(lblPhone);
                pnl.Controls.Add(lblStatus);

                // Thêm panel vào flowLayoutPanel
                flowLayoutPanel1.Controls.Add(pnl);
            }
        }
    }
}
