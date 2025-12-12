using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;



namespace GUI
{
    public partial class OrderGUI : UserControl
    {
        public event Action<int> RequestChangeToThanhToan;

        public OrderGUI()
        {
            InitializeComponent();
        }

        Decimal tongTien = 0;



        private void btnThanhToan_Click(object sender, EventArgs e) 
        {


            //kiểm tra giỏ hàng có sản phẩm không
            if (dataGridView1.Rows.Count <= 1)  
            {
                errorProvider1.SetError(dataGridView1, "Giỏ hàng trống!");
                return;
            }
            errorProvider1.SetError(dataGridView1, "");
            //kiểm tra số lượng 
            int i = 0;
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {

                if (Convert.ToInt32(Row.Cells["SoLuong"].Value) == 0)
                {
                    errorProvider1.SetError(dataGridView1, "Nhap So Luong");
                    return;
                }
                i++;
                if (i >= dataGridView1.Rows.Count - 1) break;
            }
            errorProvider1.SetError(dataGridView1, "");

            //kiểm tra tổng tiền
            if (tongTien <= 0)
            {
                errorProvider1.SetError(txtTong, "Tổng tiền phải lớn hơn 0!");
                return;
            }
            errorProvider1.SetError(txtTong, "");

            //
            if(cbb_Ban.SelectedIndex < 0)
            {
                errorProvider1.SetError(cbb_Ban, "chọn bàn !!!!");
                return;
            }
            errorProvider1.SetError(cbb_Ban, "");

            //
            // Tạo hóa đơn mới
            //
            int banID = 0;
            bool coNguoiDatBanHomNay = false;

            if (cbb_Ban.SelectedItem != null)
            {
                try
                {
                    string[] banInfo = cbb_Ban.SelectedItem.ToString().Split('-');
                    banID = Convert.ToInt32(banInfo[0].Trim());
                    List<DatBanDTO> dsDatBan = DatBanBUS.ChuyenDataTableSangDTO(DatBanBUS.LayDatBanTheoBan(banID));

                    foreach (var item in dsDatBan)
                    {
                        if (DateTime.Now.TimeOfDay < item.GioBatDau && !coNguoiDatBanHomNay)
                        {
                            MessageBox.Show("Có người đặt bàn trong hôm nay!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            coNguoiDatBanHomNay = true;
                            return;
                        }
                        // Ghép Ngày + Giờ
                        DateTime thoiGianBatDau = item.Ngay.Date + item.GioBatDau; // của bàn đã hẹn

                        // Cho phép khách ngồi trong 1 tiếng trước khi đến hẹn bàn
                        if (DateTime.Now.AddHours(1) >= thoiGianBatDau && thoiGianBatDau >= DateTime.Now)
                        {
                            errorProvider1.SetError(txtTong, "Bàn có người đặt trong 1h tới!");
                            return;
                        }
                    }
                }
                catch
                {
                    errorProvider1.SetError(txtTong, "Dữ liệu không hợp lệ!");
                    return;
                } 
            }




            int KMID;
            if (cbb_KM.SelectedIndex < 0)
            {
                KMID = 0;
            }
            else
            {
                string[] KMInfo = cbb_KM.SelectedItem.ToString().Split('-');
                KMID = Convert.ToInt32(KMInfo[0].Trim());
                DataTable DTKM = KhuyenMaiBUS.GetKM_ID(KMID);
                if (DTKM.Rows[0]["LoaiKhuyenMai"].ToString() == "Phần trăm") {
                    tongTien = tongTien - (tongTien * (Convert.ToDecimal(DTKM.Rows[0]["GiaTri"]) / 100));
                }
                else {
                    tongTien = tongTien - Convert.ToInt32(DTKM.Rows[0]["GiaTri"]);
                }

            }


            DateTime ngayLap = DateTime.Now;

            int IDuser = Session.CurrentUser.UserID;
            DataTable dt = new DataTable();
            dt = NhanVienBUS.LayNV_userID(IDuser);
            int nhanVienID = Convert.ToInt32(dt.Rows[0]["NhanVienID"]); 
            decimal tongTienDecimal = tongTien;
            HoaDonBUS.TaoHoaDon(nhanVienID, banID, ngayLap, tongTienDecimal, "Đang phục vụ", KMID);

            // Cập nhật trạng thái bàn
            BanBUS.CapNhatTrangThaiBan(banID, "Đang sử dụng");
            DatBanDTO datBanDTO = new DatBanDTO();
            datBanDTO.BanID = banID;
            datBanDTO.Ngay = DateTime.Now;
            datBanDTO.GioBatDau = DateTime.Now.TimeOfDay;
            datBanDTO.GioKetThuc = DateTime.Now.AddHours(1).TimeOfDay;  
            DatBanBUS.DatBan(datBanDTO);

            //lấy ID hóa đơn vừa tạo
            int HoaDonID = TaoID.LayHoaDonIDMoiNhat();

            // Tạo chi tiết hóa đơn cho mỗi sản phẩm trong giỏ hàng
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells["SanPhamID"].Value) == 0) continue; 
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal donGia = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                int sanPhamID = Convert.ToInt32(row.Cells["SanPhamID"].Value);
                int ThanhTien = soLuong * (int)donGia;
                HoaDonBUS.TaoChiTietHoaDon(soLuong, donGia, HoaDonID, sanPhamID, ThanhTien);
            }

            // Thông báo thành công
            //MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa giỏ hàng
            dataGridView1.Rows.Clear();
            txtTong.Text = "0";
            cbb_Ban.SelectedItem = null;

            //request to change to thanh toan form
            RequestChangeToThanhToan?.Invoke(HoaDonID);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            tongTien = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                tongTien += Convert.ToInt32(row.Cells["GiaBan"].Value) * Convert.ToInt32(row.Cells["SoLuong"].Value);
            }
            txtTong.Text = tongTien.ToString();
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            tongTien = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                tongTien += Convert.ToInt32(row.Cells["GiaBan"].Value) * Convert.ToInt32(row.Cells["SoLuong"].Value);
            }
            txtTong.Text = tongTien.ToString();
        }

        private void OrderGUI_Load(object sender, EventArgs e)
        {   
            DataTable banTable = BanBUS.LayTatCaBanHoatDong();
            DataTable Km = KhuyenMaiBUS.GetActiveKM();
            
            cbb_Ban.Items.Clear();
            cbb_KM.Items.Clear();   
            foreach (DataRow row in banTable.Rows)
            {
                
                string item = row["BanID"] + " - " + row["TenBan"];
                cbb_Ban.Items.Add(item);
            }
            foreach (DataRow row in Km.Rows) 
            {
                string item = row["KhuyenMaiID"] + " - " + row["TenKhuyenMai"];
                cbb_KM.Items.Add(item);
            }
        }

        private void OrderGUI_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size((int)(this.Width * 0.8),(int)(this.Height * 0.45));
            dataGridView1.Location = new Point((int)((this.Width - dataGridView1.Width) / 2), dataGridView1.Location.Y);

            label1.Location = new Point((int)((this.Width - label1.Width) / 2), label1.Location.Y);
            label2.Location = new Point((int)((this.Width - label2.Width) / 2), label2.Location.Y);

            label5.Location = new Point(label5.Location.X, dataGridView1.Location.Y + dataGridView1.Size.Height + 20 + 10);
            label6.Location = new Point(label5.Location.X, label5.Location.Y + 40);
            label4.Location = new Point(label4.Location.X, label6.Location.Y + 40);
            label5.Size = label5.Size;
            label6.Size = label6.Size;
            label4.Size = label4.Size;


            cbb_Ban.Location = new Point(label5.Location.X + label5.Width +30, label5.Location.Y);
            cbb_KM.Location = new Point(label6.Location.X + label6.Width + 30, label6.Location.Y);
            txtTong.Location = new Point(label4.Location.X + label4.Width + 30, label4.Location.Y);
            cbb_Ban.Size = new Size(this.Width - cbb_Ban.Location.X - 20, cbb_Ban.Size.Height);
            cbb_KM.Size = new Size(this.Width - cbb_KM.Location.X - 20, cbb_KM.Size.Height);
            txtTong.Size = new Size(this.Width - txtTong.Location.X - 20, txtTong.Size.Height);

            btnThanhToan.Location = new  Point((int)((this.Width - btnThanhToan.Width) / 2), txtTong.Location.Y + 40);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        public void OrderGUI_ParentChanged(object sender, EventArgs e)
        {
            OrderGUI_Load(sender, e);
        }

        private void OrderGUI_Load_1(object sender, EventArgs e)
        {

        }

        private void OrderGUI_Load_2(object sender, EventArgs e)
        {

        }





        /*
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }*/
    }
}
