using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagement.DTO;
using CoffeeManagement.BUS;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoffeeManagement.GUI.ThongKe
{
    public partial class NguyenLieuDashboardGUI: UserControl
    {
        private List<NguyenLieuDTO> listNL;
        private NguyenLieuBUS nlBUS = new NguyenLieuBUS();
        public NguyenLieuDashboardGUI()
        {
            InitializeComponent();
            chartNguyenLieu.Series[0].ToolTip = "#VAL";
        }

        private void NguyenLieuDashboardGUI_Load(object sender, EventArgs e)
        {
            listNL = nlBUS.LayTatCaNguyenLieu();
            LoadChartNguyenLieu();
        }
        private void LoadChartNguyenLieu()
        {
            if (listNL == null || listNL.Count == 0)
                return;

            lblTongNL.Text = "Tổng: " + listNL.Count;
            var chart = chartNguyenLieu;

            // Clear old data
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();

            // Create chart area
            var chartArea = new ChartArea("ChartArea1");
            chart.ChartAreas.Add(chartArea);

            // Create legend
            var legend = new Legend("Legend1");
            chart.Legends.Add(legend);

            // Create series
            var series = new Series("NguyenLieu");
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            chart.Series.Add(series);

            // Add data points with unit conversion + color
            foreach (var nl in listNL)
            {
                decimal quantity = nl.SoLuongTon;
                string displayUnit = nl.DonVi;

                // Convert ml -> l
                if (nl.DonVi.Equals("ml", StringComparison.OrdinalIgnoreCase))
                {
                    quantity = quantity / 1000m;
                    displayUnit = "l";
                }

                // Add point
                series.Points.AddXY(nl.TenNguyenLieu, quantity);
                int lastIndex = series.Points.Count - 1;
                var point = series.Points[lastIndex];

                // Label
                point.Label = quantity + " " + displayUnit;

                // --- Set color by unit ---
                switch (displayUnit.ToLower())
                {
                    case "kg":
                        point.Color = Color.Chocolate;
                        break;

                    case "l":
                        point.Color = Color.RoyalBlue;
                        break;

                    case "lon":
                        point.Color = Color.ForestGreen;
                        break;

                    default:
                        point.Color = Color.Gray;  // fallback
                        break;
                }
            }

            // Formatting
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            series["PointWidth"] = "0.5";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listNL = nlBUS.LayTatCaNguyenLieu();
            LoadChartNguyenLieu();
        }

        private void NguyenLieuDashboardGUI_SizeChanged(object sender, EventArgs e)
        {
            ResizeChart();
        }
        private void ResizeChart()
        {
            if (chartNguyenLieu == null || panel1 == null)
                return;

            int margin = 30;         // khoảng cách hai bên
            int topOffset = 50;      // chừa chỗ cho label + nút refresh

            chartNguyenLieu.Left = margin;
            chartNguyenLieu.Top = topOffset;

            chartNguyenLieu.Width = panel1.Width - margin * 2;
            chartNguyenLieu.Height = panel1.Height - topOffset - margin;
        }

    }
}
