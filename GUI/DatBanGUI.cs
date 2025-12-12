using BUS;
using System.Drawing;
using DTO;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class DatBanGUI : UserControl
    {
        private int selectedBanID = -1; // Lưu bàn được chọn

        public DatBanGUI()
        {
            InitializeComponent();
            // Đăng ký để nhận thông báo khi có thay đổi dữ liệu bàn từ BUS
            BanBUS.TablesChanged += OnTablesChanged;
            dgvDatban.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoadBan();
            InitPickers();
        }

        private void OnTablesChanged()
        {
            // Refresh lại danh sách bàn khi có thông báo
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(LoadBan));
            }
            else
            {
                LoadBan();
            }
        }

        // Load toàn bộ bàn
        private void LoadBan()
        {
            // Không gọi ResetTatCaBan() ở đây — Reset có thể được gọi chủ động sau khi thao tác đặt/hủy,
            // và việc gọi Reset trong Load khi kết hợp với event TablesChanged có thể gây vòng lặp.
            dgvBan.DataSource = BanBUS.LayTatCaBan();

            // Cập nhật trạng thái thực tế (tạm thời hiển thị theo lịch đặt)
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

            // Nếu thời gian đặt trùng với thời điểm hiện tại -> cập nhật trạng thái bàn thành "Có người"
            DateTime now2 = DateTime.Now;
            bool isActiveNow =
                dat.Ngay.Date == now2.Date &&
                dat.GioBatDau <= now2.TimeOfDay &&
                now2.TimeOfDay < dat.GioKetThuc;

            if (isActiveNow)
            {
                // Cập nhật DB cho bàn này nếu chưa là "Có người"
                BanBUS.CapNhatTrangThaiBan(selectedBanID, "Có người");
            }

            // Thông báo cho UI refresh (ResetTatCaBan chỉ cập nhật DB; Raise để UI load lại)
            BanBUS.ResetTatCaBan();
            BanBUS.RaiseTablesChanged();

            MessageBox.Show("Đặt bàn thành công!");

            // Load lại lịch đặt của bàn
            LoadBan();
            dgvDatban.DataSource = DatBanBUS.LayDatBanTheoBan(selectedBanID);
        }

        // Ví dụ gọi phương thức xóa đặt bàn
        private void btnXoaDatBan_Click(object sender, EventArgs e)
        {
            btnHuy.Text = "Thực hiện";

            // Kiểm tra nếu không có hàng nào được chọn
            if (dgvDatban.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đặt bàn để xóa.");
                return; // Dừng lại nếu không có hàng nào được chọn
            }

            // Lấy thông tin đặt bàn được chọn
            int datBanID = Convert.ToInt32(dgvDatban.SelectedRows[0].Cells["DatBanID"].Value);

            // Xác nhận người dùng muốn hủy đặt bàn
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn hủy đặt bàn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // Thực hiện xóa đặt bàn
                bool isDeleted = DatBanBUS.XoaDatBan(datBanID);

                if (isDeleted)
                {
                    MessageBox.Show("Đặt bàn đã được hủy thành công!");

                    // Cập nhật lại danh sách đặt bàn
                    dgvDatban.DataSource = DatBanBUS.LayDatBanTheoBan(selectedBanID);

                    // Cập nhật lại trạng thái bàn: tính toán lại DB rồi notify UI
                    BanBUS.ResetTatCaBan();
                    BanBUS.RaiseTablesChanged();

                    // Refresh local view
                    LoadBan();
                    btnHuy.Text = "Hủy đặt";
                }
                else
                {
                    MessageBox.Show("Không thể hủy đặt bàn. Vui lòng thử lại.");
                }
            }

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

            // Nếu KHÔNG trùng giờ → trả về màu mặc định (bỏ màu lightgreen)
            if (!trungGio)
            {
                row.DefaultCellStyle.BackColor = dgvDatban.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = dgvDatban.DefaultCellStyle.ForeColor;
                return;
            }

            // Nếu trùng giờ → đánh dấu luôn là Có người và tô LightSalmon cho hàng đặt
            row.DefaultCellStyle.BackColor = Color.LightSalmon;
            row.DefaultCellStyle.ForeColor = Color.Black;

            // Cập nhật trạng thái bàn tương ứng (nếu chưa là "Có người")
            // Lấy banID từ row đặt bàn
            int banIdOfBooking;
            if (int.TryParse(row.Cells["BanID"].Value?.ToString(), out banIdOfBooking))
            {
                // Cập nhật ô TrangThai trong dgvBan nếu có
                foreach (DataGridViewRow banRow in dgvBan.Rows)
                {
                    if (banRow.IsNewRow) continue;
                    object cell = banRow.Cells["BanID"].Value;
                    if (cell == null) continue;

                    int banIdInBanGrid;
                    if (!int.TryParse(cell.ToString(), out banIdInBanGrid)) continue;

                    if (banIdInBanGrid == banIdOfBooking)
                    {
                        string current = banRow.Cells["TrangThai"].Value?.ToString();
                        if (current != "Có người")
                        {
                            // Update UI cell
                            banRow.Cells["TrangThai"].Value = "Có người";
                            // Update DB once
                            try
                            {
                                BanBUS.CapNhatTrangThaiBan(banIdOfBooking, "Có người");
                            }
                            catch
                            {
                                // ignore DB update errors here to avoid crashing during formatting
                            }
                        }

                        // ensure banRow is repainted with correct color
                        banRow.DefaultCellStyle.BackColor = Color.LightSalmon;
                        banRow.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    }
                }
            }
        }

        private void DatBanGUI_Load(object sender, EventArgs e)
        {
            LoadBan();
        }

        // Thay vì override Dispose (designer partial thường đã có Dispose),
        // override OnHandleDestroyed để hủy đăng ký event một cách an toàn
        protected override void OnHandleDestroyed(EventArgs e)
        {
            BanBUS.TablesChanged -= OnTablesChanged;
            base.OnHandleDestroyed(e);
        }

        private void dgvDatban_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;  // Kiểm tra xem có chọn đúng dòng hợp lệ không

            // Lấy dòng được chọn
            DataGridViewRow selectedRow = dgvDatban.Rows[e.RowIndex];
        }

        private void pnTitle_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
