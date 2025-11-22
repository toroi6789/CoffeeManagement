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
    public partial class ThanhToanGUI : UserControl
    {
        int hoaDonID;
        public event Action RequestPnlBodyToBanHang;

        public ThanhToanGUI()
        {
            InitializeComponent();
        }
        public ThanhToanGUI(int HoaDonID)
        {
            InitializeComponent();
            hoaDonID = HoaDonID;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            // check phuong thuc thanh toan
            if (comboBox1.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox1, "Vui lòng chọn phương thức thanh toán!");
                return;
            }
            errorProvider1.SetError(comboBox1, "");
            // check tien nhan
            if (Convert.ToDecimal(txtTienNhan.Text) < Convert.ToDecimal(txtTongTien.Text) || Convert.ToDecimal(txtTienNhan.Text) == 0)
            {
                errorProvider1.SetError(txtTienNhan, "Số tiền nhận không đủ để thanh toán!");
                return;
            }
            errorProvider1.SetError(txtTienNhan, "");

            //luu ThanhToan
            BUS.ThanhToanBUS.TaoThanhToan(
                Convert.ToInt32(txtIDHD.Text),
                Convert.ToInt32(txtIDNV.Text),
                Convert.ToDecimal(txtTongTien.Text),
                comboBox1.SelectedItem.ToString(),
                DateTime.Now,
                "Hoàn tất"
                );

            //sua hoa don
            BUS.HoaDonBUS.SuaTrangThai(hoaDonID, "Đã thanh toán");
            //cap nhat phuong thuc
            BUS.HoaDonBUS.Capnhatphuongthuc(hoaDonID, comboBox1.SelectedItem.ToString());

            // Chuyen ve giao dien ban hang
            RequestPnlBodyToBanHang?.Invoke();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string dateString = now.ToString("dd-MM_yyyy HH:mm:ss");
            txtNgay.Text = dateString;

            //lay thong tin hoa don
            DataTable hoaDon = BUS.HoaDonBUS.HoaDonID(hoaDonID);
            txtIDHD.Text = hoaDonID.ToString();
            txtIDNV.Text = hoaDon.Rows[0]["NhanVienID"].ToString();
            txtTongTien.Text = hoaDon.Rows[0]["TongTien"].ToString();
        }

        private void txtIDNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //tinh tien thoi
        private void txtTienNhan_TextChanged(object sender, EventArgs e)
        {
            txtTienThoi.Text = (Convert.ToDouble(txtTienNhan.Text) - Convert.ToDouble(txtTongTien.Text)).ToString();
        }

        private void ThanhToanGUI_SizeChanged(object sender, EventArgs e)
        {
            groupBox1.Location = new Point((this.Width - groupBox1.Width) / 2, (this.Height - groupBox1.Height) / 2);
            btn_ThanhToan.Location = new Point((this.Width - btn_ThanhToan.Width) / 2, groupBox1.Bottom + 20);
        }
    }
}
