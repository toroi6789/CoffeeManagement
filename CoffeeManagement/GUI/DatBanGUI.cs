using CoffeeManagement.BUS;
using System.Drawing;
using CoffeeManagement.DTO;
using System;
using System.Windows.Forms;

namespace CoffeeManagement.GUI
{
    public partial class DatBanGUI : UserControl
    {
        private int selectedBanID = -1; // Lưu bàn được chọn

        public DatBanGUI()
        {
            InitializeComponent();
            LoadBan();
            InitPickers();
        }

        // Load toàn bộ bàn
        private void LoadBan()
        {
            dgvBan.DataSource = BanBUS.LayTatCaBan();

            // Cập nhật trạng thái thực tế
            foreach (DataGridViewRow row in dgvBan.Rows)
            {
                int banID = Convert.ToInt32(row.Cells["BanID"].Value);

                bool coNguoi = DatBanBUS.BanDangCoNguoi(banID);

                row.Cells["TrangThai"].Value = coNguoi ? "Có người" : "Trống";
            }
        }


        // Setup cho DateTimePicker
        private void InitPickers()
        {
            dtpNgay.MinDate = DateTime.Today;
            dtpNgay.Format = DateTimePickerFormat.Short;

            dtpGioBD.Format = DateTimePickerFormat.Time;
            dtpGioBD.CustomFormat = "HH:mm";
            dtpGioBD.ShowUpDown = true;
        }

        // Khi click nút Đặt Bàn
        private void btnDatBan_Click(object sender, EventArgs e)
        {
            if (selectedBanID == -1)
            {
                MessageBox.Show("Bạn phải chọn 1 bàn để đặt!");
                return;
            }

            DateTime ngay = dtpNgay.Value.Date;
            TimeSpan gioBD = dtpGioBD.Value.TimeOfDay;

            // --- KHÔNG CHO ĐẶT GIỜ QUÁ KHỨ ---
            DateTime now = DateTime.Now;

            if (ngay == now.Date && gioBD < now.TimeOfDay)
            {
                MessageBox.Show("chỉ có thể đặt bàn kể từ bây giờ trở đi!");
                return;
            }

            // Chỉ đặt trong khung 7h - 21h
            if (gioBD.Hours < 7 || gioBD.Hours >= 22)
            {
                MessageBox.Show("Giờ đặt phải nằm trong khoảng 7h00 - 21h00!");
                return;
            }

            TimeSpan gioKT = gioBD.Add(TimeSpan.FromHours(1)); // Mặc định 1 giờ

            // Kiểm tra trùng lịch
            bool trung = DatBanBUS.KiemTraTrung(selectedBanID, ngay, gioBD, gioKT);
            if (trung)
            {
                MessageBox.Show("Khung giờ này đã có người đặt!");
                return;
            }

            // Lưu đặt bàn
            DatBanDTO dat = new DatBanDTO(0, selectedBanID, ngay, gioBD, gioKT);
            DatBanBUS.DatBan(dat);

            MessageBox.Show("Đặt bàn thành công!");

            // Load lại lịch đặt của bàn
            LoadBan();
            dgvDatban.DataSource = DatBanBUS.LayDatBanTheoBan(selectedBanID);
        }

        // Khi click vào bất kỳ ô nào trong bảng bàn
        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            DataGridViewRow row = dgvBan.Rows[e.RowIndex];

            // Lấy ID bàn
            selectedBanID = Convert.ToInt32(row.Cells["BanID"].Value);

            // Load lịch đặt bàn tương ứng
            dgvDatban.DataSource = DatBanBUS.LayDatBanTheoBan(selectedBanID);
        }


        private void DatBanGUI_SizeChanged(object sender, EventArgs e)
        {
            int padding = 10;

            int titleHeight = 60;
            int infoPanelWidth = 350;
            int infoPanelHeight = 140;

            // PANEL TITLE
            pnTitle.Location = new Point(0, 0);
            pnTitle.Size = new Size(this.Width, titleHeight);

            // PANEL NGÀY GIỜ ĐẶT BÀN (panelInfo)
            panelInfo.Location = new Point(
                this.Width - infoPanelWidth - padding,
                pnTitle.Bottom + padding
            );
            panelInfo.Size = new Size(
                infoPanelWidth,
                infoPanelHeight
            );

            // DGV BÀN  (bảng nằm giữa màn hình)
            dgvBan.Location = new Point(
                padding,
                pnTitle.Bottom + padding
            );
            dgvBan.Size = new Size(
                panelInfo.Left - padding * 2,
                this.Height / 3
            );

            // DGV LỊCH ĐẶT BÀN (nằm dưới cùng)
            dgvDatban.Location = new Point(
                padding,
                dgvBan.Bottom + padding
            );
            dgvDatban.Size = new Size(
                this.Width - padding * 2,
                this.Height - dgvDatban.Top - padding
            );

            // CONTAINER
            pnContainer.Location = new Point(0, 0);
            pnContainer.Size = new Size(this.Width, this.Height);
        }

        private void dgvBan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvBan.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                string trangThai = e.Value?.ToString() ?? "";

                DataGridViewRow row = dgvBan.Rows[e.RowIndex];

                if (trangThai == "Trống")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;   // màu xanh
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (trangThai == "Có người")
                {
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;  // màu khác
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    // reset về mặc định nếu giá trị không khớp
                    row.DefaultCellStyle.BackColor = dgvBan.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvBan.DefaultCellStyle.ForeColor;
                }
            }
        }

        private void dgvDatban_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy row đặt bàn hiện tại
            var row = dgvDatban.Rows[e.RowIndex];

            DateTime ngay = Convert.ToDateTime(row.Cells["Ngay"].Value);
            TimeSpan gioBD = (TimeSpan)row.Cells["GioBatDau"].Value;
            TimeSpan gioKT = (TimeSpan)row.Cells["GioKetThuc"].Value;

            DateTime now = DateTime.Now;

            bool trungGio =
                ngay.Date == now.Date &&
                gioBD <= now.TimeOfDay &&
                now.TimeOfDay < gioKT;

            // Nếu KHÔNG trùng giờ → trả về màu mặc định
            if (!trungGio)
            {
                row.DefaultCellStyle.BackColor = dgvDatban.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgvDatban.DefaultCellStyle.ForeColor;
                return;
            }

            // Nếu trùng giờ → kiểm tra trạng thái bàn bên dgvBan
            if (selectedBanID != -1)
            {
                // Tìm hàng bàn tương ứng
                foreach (DataGridViewRow r in dgvBan.Rows)
                {
                    if (Convert.ToInt32(r.Cells["BanID"].Value) == selectedBanID)
                    {
                        string trangThai = r.Cells["TrangThai"].Value.ToString();

                        if (trangThai == "Có người")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }

                        return;
                    }
                }
            }
        }

        private void DatBanGUI_Load(object sender, EventArgs e)
        {
            LoadBan();
        }
    }
}
