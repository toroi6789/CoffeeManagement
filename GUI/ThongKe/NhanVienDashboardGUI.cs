using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI.ThongKe
{
    public partial class NhanVienDashboardGUI: UserControl
    {
        private NhanVienBUS nvBUS = new NhanVienBUS();
        private UserBUS userBUS = new UserBUS();
        private List<NhanVienDTO> listNV = new List<NhanVienDTO>();
        private List<UserDTO> listUser = new List<UserDTO>();
        public NhanVienDashboardGUI()
        {
            InitializeComponent();
        }

        private void NhanVienDashboardGUI_Load(object sender, EventArgs e)
        {
            listNV = nvBUS.GetAllNhanVien();
            listUser = userBUS.GetUsers();

            ShowThongTinChung();
            LoadPieChartNhanVien();
        }
        private void LoadPieChartNhanVien()
        {
            chartNhanVien.Series.Clear();
            chartNhanVien.Legends.Clear();

            // Legend
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend("ChucVu");
            chartNhanVien.Legends.Add(legend);

            // Series
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Nhân viên");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.Legend = "ChucVu"; // GÁN LEGEND ĐÚNG TÊN
            series.LegendText = "#VALX";

            series.Points.AddXY("Quản trị viên", listUser.Count(u => u.RoleID == 1));
            series.Points.AddXY("Thu ngân", listUser.Count(u => u.RoleID == 2));
            series.Points.AddXY("Pha chế", listUser.Count(u => u.RoleID == 3));
            series.Points.AddXY("Phục vụ", listUser.Count(u => u.RoleID == 4));
            series.Points.AddXY("Quản lý kho", listUser.Count(u => u.RoleID == 5));

            // Label hiển thị trên biểu đồ
            series.Label = "#PERCENT{P1}";

            chartNhanVien.Series.Add(series);
        }

        private void ShowThongTinChung()
        {
            lblTong.Text = "Tổng: " + listNV.Count;

            lblTongDangLam.Text = "Đang làm: " +
                listNV.Count(n => n.TrangThai == "Đang làm việc");

            lblTongTrongLich.Text = "Trống lịch: " +
                listNV.Count(n => n.TrangThai == "Trống lịch");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listNV = nvBUS.GetAllNhanVien();
            listUser = userBUS.GetUsers();

            ShowThongTinChung();
            LoadPieChartNhanVien();
        }

        private void NhanVienDashboardGUI_Load_1(object sender, EventArgs e)
        {

        }

        private void NhanVienDashboardGUI_SizeChanged(object sender, EventArgs e)
        {
            int padding = 10;  // Khoảng cách giữa các điều khiển

            // Cập nhật kích thước và vị trí cho tiêu đề (lblTitle)
            int titleHeight = 60;
            lblTitle.Size = new Size(this.Width, titleHeight);

            // Cập nhật vị trí và kích thước cho Panel chính (panel1)
            panel1.Location = new Point(0, lblTitle.Bottom + padding);
            panel1.Size = new Size(this.Width, this.Height - titleHeight - padding);

            // Cập nhật vị trí và kích thước cho Chart (chartNhanVien)
            chartNhanVien.Location = new Point(padding, 80);  // Khoảng cách từ trái và từ trên
            chartNhanVien.Size = new Size(panel1.Width - padding * 2, panel1.Height - 160);

            // Cập nhật vị trí và kích thước cho các label
            lblTong.Location = new Point(64, 108);
            lblTong.Size = new Size(83, 38);

            lblTongDangLam.Location = new Point(64, 160);
            lblTongDangLam.Size = new Size(133, 38);

            lblTongTrongLich.Location = new Point(64, 210);
            lblTongTrongLich.Size = new Size(133, 38);

            // Cập nhật vị trí của nút refresh
            btnRefresh.Location = new Point(this.Width - 60, 36);
        }

    }
}
