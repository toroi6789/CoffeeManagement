using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BUS;
using DTO;

namespace GUI.ThongKe
{
    public partial class DoanhThuGUI: UserControl
    {
        private List<HoaDonDTO> listHD;
        private readonly HoaDonBUS hdBUS = new HoaDonBUS();
        public DoanhThuGUI()
        {                                       
            InitializeComponent();
            cbDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDate.SelectedIndex = 0;                               
            chartDoanhThu.Series[0].Name = "VNĐ";
            chartDoanhThu.Series[0].ToolTip = "Ngày/Tháng/Năm: #VALX\nDoanh thu: #VAL{N0} VNĐ";                              
            chartBieuDoDuong.Series[0].Name = "VNĐ";
            chartBieuDoDuong.Series[0].ToolTip = "Ngày/Tháng/Năm: #VALX\nDoanh thu: #VAL{N0} VNĐ";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ThongKeTheoNgay();
        }

        private void ThongKeTheoNgay()
        {
            DateTime selectedDate = dtpNgay.Value.Date;

            var listHD = HoaDonBUS.GetByDate(selectedDate);
            decimal total = HoaDonBUS.GetTotalByDate(selectedDate);

            lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

            // Vẽ biểu đồ nếu có
            if (chartDoanhThu.Series.Count > 0)
            {
                chartDoanhThu.Series[0].Points.Clear();
                chartDoanhThu.Series[0].Points.AddXY(
                    selectedDate.ToString("dd/MM/yyyy"),
                    total
                );
            }
        }
        private void ThongKeTheoThang()
        {
            int year = dtpNgay.Value.Year;

            var hdNam = listHD
                .Where(h => h.NgayKhoiTao.Year == year &&
                            h.TrangThai == "Đã thanh toán")
                .ToList();

            decimal total = hdNam.Sum(h => h.TongTien);
            lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

            chartDoanhThu.Series[0].Points.Clear();
            chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;
            chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

            // Tạo mảng 12 tháng có giá trị mặc định = 0
            decimal[] doanhThuThang = new decimal[12];

            foreach (var hd in hdNam)
            {
                int thang = hd.NgayKhoiTao.Month;
                doanhThuThang[thang - 1] += hd.TongTien;
            }

            // Hiển thị 12 cột
            for (int i = 1; i <= 12; i++)
            {
                chartDoanhThu.Series[0].Points.AddXY(
                    "T" + i,
                    doanhThuThang[i - 1]
                );
            }
        }
        private void ThongKeTheoNam()
        {
            int currentYear = DateTime.Now.Year;

            // Lấy 10 năm gần nhất: currentYear - 9 → currentYear
            int startYear = currentYear - 9;

            chartDoanhThu.Series[0].Points.Clear();
            chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;
            chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

            // Mảng 10 phần tử (tương ứng 10 năm)
            decimal[] doanhThuNam = new decimal[10];

            // Cộng doanh thu theo từng năm
            foreach (var hd in listHD)
            {
                if (hd.TrangThai == "Đã thanh toán" &&
                    hd.NgayKhoiTao.Year >= startYear &&
                    hd.NgayKhoiTao.Year <= currentYear)
                {
                    int index = hd.NgayKhoiTao.Year - startYear; // từ 0 → 9
                    doanhThuNam[index] += hd.TongTien;
                }
            }

            // Tổng doanh thu 10 năm
            decimal total = doanhThuNam.Sum();
            lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

            // Hiển thị 10 cột: từ startYear → currentYear
            for (int i = 0; i < 10; i++)
            {
                int nam = startYear + i;
                chartDoanhThu.Series[0].Points.AddXY(nam.ToString(), doanhThuNam[i]);
            }
        }

        private void Duong_ThongKeTheoThang()
        {
            int year = dtpNgay.Value.Year;

            // Lọc hóa đơn theo năm và đã thanh toán
            var hdNam = listHD
                .Where(h => h.NgayKhoiTao.Year == year &&
                            h.TrangThai == "Đã thanh toán")
                .ToList();

            // Mảng 12 tháng
            decimal[] doanhThuThang = new decimal[12];

            foreach (var hd in hdNam)
            {
                int thang = hd.NgayKhoiTao.Month;
                doanhThuThang[thang - 1] += hd.TongTien;
            }

            // Hiển thị tổng năm
            decimal total = doanhThuThang.Sum();
            lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

            // Cấu hình chart đường
            var series = chartBieuDoDuong.Series[0];
            series.Points.Clear();
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series.BorderWidth = 3;
            series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series.MarkerSize = 7;

            chartBieuDoDuong.ChartAreas[0].AxisX.Interval = 1;

            // Vẽ 12 điểm
            for (int i = 1; i <= 12; i++)
            {
                series.Points.AddXY("T" + i, doanhThuThang[i - 1]);
            }
        }
        private void Duong_ThongKeTheoNam()
        {
            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - 9;

            decimal[] doanhThuNam = new decimal[10];

            // Tính doanh thu từng năm
            foreach (var hd in listHD)
            {
                if (hd.TrangThai == "Đã thanh toán" &&
                    hd.NgayKhoiTao.Year >= startYear &&
                    hd.NgayKhoiTao.Year <= currentYear)
                {
                    int index = hd.NgayKhoiTao.Year - startYear;
                    doanhThuNam[index] += hd.TongTien;
                }
            }

            // Tổng doanh thu 10 năm
            decimal total = doanhThuNam.Sum();
            lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

            // Cấu hình chart
            var series = chartBieuDoDuong.Series[0];
            series.Points.Clear();
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series.BorderWidth = 3;
            series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series.MarkerSize = 7;

            chartBieuDoDuong.ChartAreas[0].AxisX.Interval = 1;

            // Vẽ 10 năm
            for (int i = 0; i < 10; i++)
            {
                int nam = startYear + i;
                series.Points.AddXY(nam.ToString(), doanhThuNam[i]);
            }
        }
        private void Duong_ThongKeTheoNgay()
        {
            DateTime date = dtpNgay.Value.Date;

            decimal total = HoaDonBUS.GetTotalByDate(date);
            lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

            var series = chartBieuDoDuong.Series[0];
            series.Points.Clear();
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series.BorderWidth = 3;

            series.Points.AddXY(date.ToString("dd/MM"), total);
        }


        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChartBaseOnDate();
        }

        private void DoanhThuGUI_Load(object sender, EventArgs e)
        {
            listHD = HoaDonBUS.MapToListHD(HoaDonBUS.TatCaHoaDon());
            ShowChartBaseOnDate();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listHD = HoaDonBUS.MapToListHD(HoaDonBUS.TatCaHoaDon());
            ShowChartBaseOnDate();
        }

        private void ShowChartBaseOnDate()
        {
            switch (cbDate.SelectedIndex)
            {
                case 0:
                    ThongKeTheoNgay();
                    Duong_ThongKeTheoNgay();
                    break;
                case 1:
                    ThongKeTheoThang();
                    Duong_ThongKeTheoThang();
                    break;
                case 2:
                    ThongKeTheoNam();
                    Duong_ThongKeTheoNam();
                    break;
            }
        }

        private void DoanhThuGUI_SizeChanged(object sender, EventArgs e)
        {
            if (panel1 == null) return;

            int topMargin = 50;  // khoảng cách từ top xuống chart đầu tiên
            int spacing = 10;    // khoảng cách giữa 2 chart

            int chartWidth = panel1.Width - 20;
            int chartHeight = (panel1.Height - topMargin - spacing - 20) / 2;

            // Chart cột trên
            chartDoanhThu.Location = new Point(10, topMargin);
            chartDoanhThu.Size = new Size(chartWidth, chartHeight);

            // Chart đường dưới
            chartBieuDoDuong.Location = new Point(10, topMargin + chartHeight + spacing);
            chartBieuDoDuong.Size = new Size(chartWidth, chartHeight);
        }

        private void DoanhThuGUI_Load_1(object sender, EventArgs e)
        {

        }
    }
}
