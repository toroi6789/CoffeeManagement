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
using DTO;
using BUS;

namespace GUI.ThongKe
{
    public partial class SanPhamDashboardGUI: UserControl
    {
        private List<SanPhamDTO> listSP = new List<SanPhamDTO>();
        private List<HoaDonDTO> listHD = new List<HoaDonDTO>();

        private SanPhamBUS spBUS = new SanPhamBUS();
        private HoaDonBUS hdBUS = new HoaDonBUS();
        public SanPhamDashboardGUI()
        {
            InitializeComponent();
            chartSanPham.Series[0].ToolTip = "#VAL";
        }

        private void RefreshDataFromDB()
        {
            listSP = spBUS.LayTatCaSanPham();
            listHD = HoaDonBUS.GetAllListHD();
        }

        private void SanPhamDashboardGUI_Load(object sender, EventArgs e)
        {
            RefreshDataFromDB();
            HienThiBieuDoSanPham();
        }

        private Dictionary<int, int> TinhSoLuongBanTheoSanPham()
        {
            // Từ điển: ProductID -> Số lượng bán
            Dictionary<int, int> result = new Dictionary<int, int>();
            List<ChiTietHoaDonDTO> listCTHD = new List<ChiTietHoaDonDTO>();

            foreach (var hd in listHD)
            {
                listCTHD = HoaDonBUS.MapToListCTHD(HoaDonBUS.GetChiTietHoaDonByID(hd.HoaDonID));
                foreach (var ct in listCTHD) // chỉnh lại theo cấu trúc thật của bạn
                {
                    if (!result.ContainsKey(ct.SanPhamID))
                        result[ct.SanPhamID] = 0;

                    result[ct.SanPhamID] += ct.SoLuong;
                }
            }

            return result;
        }

        private void HienThiBieuDoSanPham()
        {
            chartSanPham.Series.Clear();
            chartSanPham.ChartAreas[0].AxisX.Interval = 1;
            chartSanPham.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Xoay cho gọn nếu tên dài

            // Tự tạo Series
            Series series = new Series("Sản phẩm");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true; // Hiển thị số lượng trên cột

            // Sử dụng Palette (mỗi cột mỗi màu)
            chartSanPham.Palette = ChartColorPalette.BrightPastel;

            var soLuongBan = TinhSoLuongBanTheoSanPham();
            int tongSLBan = 0;

            foreach (var item in soLuongBan)
            {
                tongSLBan += item.Value;
            }

            foreach (var sp in listSP)
            {
                int soLuong = soLuongBan.ContainsKey(sp.SanPhamID)
                                ? soLuongBan[sp.SanPhamID]
                                : 0;

                series.Points.AddXY(sp.TenSanPham, soLuong);
            }

            lblTongDoanhThu.Text = "Tổng bán ra: " + tongSLBan;
            chartSanPham.Series.Add(series);

            // Thêm style
            chartSanPham.ChartAreas[0].AxisY.Title = "Số lượng bán";
            chartSanPham.ChartAreas[0].AxisX.Title = "Tên sản phẩm";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataFromDB();
            HienThiBieuDoSanPham();
        }

        private void SanPhamDashboardGUI_SizeChanged(object sender, EventArgs e)
        {
            // Chiều cao vùng header trong panel (Doanh thu + nút refresh)
            int headerHeight = 45;

            // Resize label doanh thu
            lblTongDoanhThu.Location = new Point(20, 10);

            // Resize nút refresh
            btnRefresh.Location = new Point(
                panel1.Width - btnRefresh.Width - 20,
                7
            );

            // Resize biểu đồ
            chartSanPham.Location = new Point(20, headerHeight);

            chartSanPham.Size = new Size(
                panel1.Width - 40,               // chừa 20px mỗi bên
                panel1.Height - headerHeight - 20 // chừa 20px bottom
            );
        }

        private void SanPhamDashboardGUI_Load_1(object sender, EventArgs e)
        {

        }
    }
}
