using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
using CoffeeManagement.DTO;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI.ThongKe
{
    public partial class PhieuNhapDashboardGUI: UserControl
    {
        private List<ChiTietPhieuNhapDTO> listCTPN;
        private List<PhieuNhapDTO> listPN;
        private PhieuNhapBUS phieuNhapBUS = new PhieuNhapBUS();
        private ChiTietPhieuNhapBUS chiTietPhieuNhapBUS = new ChiTietPhieuNhapBUS();
        public PhieuNhapDashboardGUI()
        {
            InitializeComponent();
            cbDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDate.SelectedIndex = 0;
            chartPhieuNhapDuong.Series[0].Name = "VNĐ";
            chartPhieuNhapDuong.Series[0].ToolTip = "Ngày/Tháng/Năm: #VALX\nDoanh thu: #VAL{N0} VNĐ";
            chartPhieuNhapCot.Series[0].Name = "Phiếu nhập";
            chartPhieuNhapCot.Series[0].ToolTip = "#VAL";
        }

        private void RefreshDataFromDB()
        {
            listPN = PhieuNhapBUS.ConvertToDTO(PhieuNhapBUS.PhieuNhap());
            foreach (var item in listPN)
            {
                MessageBox.Show(item.PhieuNhapID + "");
                listCTPN = chiTietPhieuNhapBUS.LayChiTietTheoPhieuNhapID(item.PhieuNhapID);
            }
            foreach (var item in listCTPN)
            {
                MessageBox.Show(item.ChiTietPhieuNhapID + "");
            }
        }

        private void PhieuNhapDashboardGUI_Load(object sender, EventArgs e)
        {
            RefreshDataFromDB();
            ShowChartBaseOnDate();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataFromDB();
        }

        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChartBaseOnDate();
        }

        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {
            ThongKeTheoNgay();
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
        private void ThongKeTheoNgay()
        {
            if (listPN == null) return;

            DateTime date = dtpNgay.Value.Date;

            // Số phiếu nhập theo ngày
            int soPN = listPN
                .Count(p => p.NgayNhap.Date == date);

            lblTongPhieu.Text = soPN + " phiếu nhập";

            var chart = chartPhieuNhapCot.Series[0];
            chart.Points.Clear();

            chart.Points.AddXY(date.ToString("dd/MM"), soPN);
        }
        private void Duong_ThongKeTheoNgay()
        {
            if (listPN == null) return;

            DateTime date = dtpNgay.Value.Date;

            decimal tongTien = listPN
                .Where(p => p.NgayNhap.Date == date)
                .Sum(p => p.TongTien);

            lblTongGiaNhap.Text = "Tổng tiền: " + tongTien;

            var series = chartPhieuNhapDuong.Series[0];
            series.Points.Clear();

            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;

            series.Points.AddXY(date.ToString("dd/MM"), tongTien);
        }
        private void ThongKeTheoThang()
        {
            if (listPN == null) return;

            int year = dtpNgay.Value.Year;
            int[] soPNThang = new int[12];

            var pnNam = listPN.Where(p => p.NgayNhap.Year == year);

            foreach (var pn in pnNam)
            {
                soPNThang[pn.NgayNhap.Month - 1]++;
            }

            lblTongPhieu.Text = soPNThang.Sum() + " phiếu nhập";

            var chart = chartPhieuNhapCot.Series[0];
            chart.Points.Clear();
            chart.ChartType = SeriesChartType.Column;

            for (int i = 1; i <= 12; i++)
            {
                chart.Points.AddXY("T" + i, soPNThang[i - 1]);
            }
        }
        private void Duong_ThongKeTheoThang()
        {
            if (listPN == null) return;

            int year = dtpNgay.Value.Year;
            decimal[] tongTienThang = new decimal[12];

            var pnNam = listPN.Where(p => p.NgayNhap.Year == year);

            foreach (var pn in pnNam)
            {
                tongTienThang[pn.NgayNhap.Month - 1] += pn.TongTien;
            }

            lblTongGiaNhap.Text = "Tổng tiền: " + tongTienThang.Sum();

            var series = chartPhieuNhapDuong.Series[0];
            series.Points.Clear();

            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 7;

            chartPhieuNhapDuong.ChartAreas[0].AxisX.Interval = 1;

            for (int i = 1; i <= 12; i++)
            {
                series.Points.AddXY("T" + i, tongTienThang[i - 1]);
            }
        }
        private void ThongKeTheoNam()
        {
            if (listPN == null) return;

            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - 9;

            int[] soPNNam = new int[10];

            var pnFilter = listPN
                .Where(p => p.NgayNhap.Year >= startYear &&
                            p.NgayNhap.Year <= currentYear);

            foreach (var pn in pnFilter)
            {
                int index = pn.NgayNhap.Year - startYear;
                soPNNam[index]++;
            }

            lblTongPhieu.Text = soPNNam.Sum() + " phiếu nhập";

            var chart = chartPhieuNhapCot.Series[0];
            chart.Points.Clear();

            for (int i = 0; i < 10; i++)
            {
                int nam = startYear + i;
                chart.Points.AddXY(nam.ToString(), soPNNam[i]);
            }
        }
        private void Duong_ThongKeTheoNam()
        {
            if (listPN == null) return;

            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - 9;

            decimal[] tongTienNam = new decimal[10];

            var pnFilter = listPN
                .Where(p => p.NgayNhap.Year >= startYear &&
                            p.NgayNhap.Year <= currentYear);

            foreach (var pn in pnFilter)
            {
                int index = pn.NgayNhap.Year - startYear;
                tongTienNam[index] += pn.TongTien;
            }

            lblTongGiaNhap.Text = "Tổng tiền: " + tongTienNam.Sum();

            var series = chartPhieuNhapDuong.Series[0];
            series.Points.Clear();

            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 7;

            chartPhieuNhapDuong.ChartAreas[0].AxisX.Interval = 1;

            for (int i = 0; i < 10; i++)
            {
                int nam = startYear + i;
                series.Points.AddXY(nam.ToString(), tongTienNam[i]);
            }
        }

        private void PhieuNhapDashboardGUI_SizeChanged(object sender, EventArgs e)
        {
            int topControlsHeight = 60; // chiều cao của dtp, combobox, label phía trên

            int availableHeight = panel1.Height - topControlsHeight - 20;
            int chartHeight = availableHeight / 2;

            // Chart Cột (trên)
            chartPhieuNhapCot.Left = 10;
            chartPhieuNhapCot.Top = topControlsHeight;
            chartPhieuNhapCot.Width = panel1.Width - 20;
            chartPhieuNhapCot.Height = chartHeight - 10;

            // Chart Đường (dưới)
            chartPhieuNhapDuong.Left = 10;
            chartPhieuNhapDuong.Top = chartPhieuNhapCot.Bottom + 10;
            chartPhieuNhapDuong.Width = panel1.Width - 20;
            chartPhieuNhapDuong.Height = chartHeight - 10;
        }

    }
}
