using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CoffeeManagement.BUS;
using CoffeeManagement.DTO;



namespace CoffeeManagement.GUI
{
    public partial class OrderGUI : UserControl
    {
        public event Action<int> RequestChangeToThanhToan;

        public OrderGUI()
        {
            InitializeComponent();
        }

        int tongTien = 0;

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            //kiểm tra giỏ hàng có sản phẩm không
            if (dataGridView1.Rows.Count <= 1)
            {
                errorProvider1.SetError(dataGridView1, "Giỏ hàng trống!");
                return;
            }
            errorProvider1.SetError(dataGridView1, "");
            // Kiểm tra chọn bàn
            if (comboBox1.SelectedItem == null)
            {
                errorProvider1.SetError(comboBox1, "Vui lòng chọn bàn!");
                return;
            }
            errorProvider1.SetError(comboBox1, "");
            //kiểm tra tổng tiền
            if (tongTien <= 0)
            {
                errorProvider1.SetError(txtTong, "Tổng tiền phải lớn hơn 0!");
                return;
            }
            errorProvider1.SetError(txtTong, "");


            //
            // Tạo hóa đơn mới
            // 
            string[] banInfo = comboBox1.SelectedItem.ToString().Split('-');
            int banID = Convert.ToInt32(banInfo[0].Trim());
            DateTime ngayLap = DateTime.Now;
            int nhanVienID = 1; // Giả sử ID nhân viên là 1
            decimal tongTienDecimal = Convert.ToDecimal(tongTien);
            HoaDonBUS.TaoHoaDon(nhanVienID, banID, ngayLap, tongTienDecimal, "Đang phục vụ");

            // Cập nhật trạng thái bàn
            BanBUS.CapNhatTrangThaiBan(banID, "Đang sử dụng");

            //lấy ID hóa đơn vừa tạo
            int HoaDonID = TaoID.LayHoaDonIDMoiNhat();

            // Tạo chi tiết hóa đơn cho mỗi sản phẩm trong giỏ hàng
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells["SanPhamID"].Value) == 0) continue; 
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal donGia = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                int sanPhamID = Convert.ToInt32(row.Cells["SanPhamID"].Value);
                HoaDonBUS.TaoChiTietHoaDon(soLuong, donGia, HoaDonID, sanPhamID);
            }

            // Thông báo thành công
            MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa giỏ hàng
            dataGridView1.Rows.Clear();
            txtTong.Text = "0";
            comboBox1.SelectedItem = null;

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
             
            foreach (DataRow row in banTable.Rows)
            {
                string item = row["BanID"] + " - " + row["TenBan"];
                comboBox1.Items.Add(item);
            }
        }



        /*
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }*/
    }
}
