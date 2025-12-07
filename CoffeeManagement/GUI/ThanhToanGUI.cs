using CoffeeManagement.BUS;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
        }
        public ThanhToanGUI(int HoaDonID)
        {
            InitializeComponent();
            hoaDonID = HoaDonID;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
            
            pictureBox1.Visible = false;
            string path = Path.Combine(Application.StartupPath, @"Images", "qrcode.png");
            if (!File.Exists(path))
            {
                path = Path.Combine(Application.StartupPath, @"Images", "null.png");
            }
            Image img2 = null;
            if (File.Exists(path))
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    img2 = Image.FromStream(stream);
                }
            }
            pictureBox1.Image = Compoment.ResizeImage(img2, 180, 180);
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

            //
            if (string.IsNullOrEmpty(txtTienNhan.Text))
            {
                errorProvider1.SetError(txtTienNhan, "Vui lòng nhập số tiền nhận!");
                return;
            }
            errorProvider1.SetError(comboBox1, "");
            // check tien nhan
            try
            {
                if (Convert.ToDecimal(txtTienNhan.Text) < Convert.ToDecimal(txtTongTien.Text) || Convert.ToDecimal(txtTienNhan.Text) == 0)
                {
                    errorProvider1.SetError(txtTienNhan, "Số tiền nhận không đủ để thanh toán!");
                    return;
                }
                errorProvider1.SetError(txtTienNhan, "");
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(txtTienNhan, "Vui lòng nhập số nguyên!");
                return;
            }

            try
            {
                //luu ThanhToan
                BUS.ThanhToanBUS.TaoThanhToan(
                    hoaDonID,
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

                // Trừ nguyên liệu sử dụng cho sp
                DataTable sp = HoaDonBUS.ChiTietHoaDonID(hoaDonID);
                
                foreach (DataRow row in sp.Rows)
                {
                    int sanPhamID = Convert.ToInt32(row["SanPhamID"]);

                    SanPhamNguyenLieuBUS spnlBUS = new SanPhamNguyenLieuBUS();
                    NguyenLieuBUS nlBUS = new NguyenLieuBUS();

                    List<SanPhamNguyenLieuDTO> listSPNL = spnlBUS.LayCongThucTheoSanPhamBUS(sanPhamID);

                    string message = "";
                    string error = "";
                    foreach (var item in listSPNL)
                    {
                        NguyenLieuDTO nl = item.NguyenLieu;
                        nl.SoLuongTon -= item.SoLuongSuDung * Convert.ToDecimal(row["SoLuong"]);
                        MessageBox.Show(
                            "NL ID = " + nl.NguyenLieuID + "Sản phẩm: " + sanPhamID + "\nNguyên liệu: " + nl.TenNguyenLieu + "\nTrừ: " + item.SoLuongSuDung,
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        nlBUS.busSuaNguyenLieu(nl, out message, out error);
                    }
                }

                // check cac sp het hang
                //BUS.SanPhamBUS.KiemTraSanPhamHetHang();


                // Chuyen ve giao dien ban hang
                RequestPnlBodyToBanHang?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thanh toán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            txt_KM.Text = hoaDon.Rows[0]["KhuyenMaiID"].ToString();
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
            try
            {
                if (txtTienNhan.Text.Length <= 0) return;
                txtTienThoi.Text = (Convert.ToDouble(txtTienNhan.Text) - Convert.ToDouble(txtTongTien.Text)).ToString();
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(txtTienNhan, "Vui lòng nhập số nguyên!");
                return;
            }
           
        }

        private void ThanhToanGUI_SizeChanged(object sender, EventArgs e)
        {
            label1.Location= new Point((int)(this.Width - label1.Width) / 2, label1.Location.Y);
            groupBox1.Size = new Size((int)(this.Width * 0.8), (int)(this.Height * 0.65));
            groupBox1.Location = new Point((int)(this.Width - groupBox1.Width) / 2, label1.Location.Y + label1.Height + 20);
            btn_ThanhToan.Location = new Point((int)(this.Width - btn_ThanhToan.Width) / 2, groupBox1.Height + groupBox1.Location.Y + 20);

        }

        private void groupBox1_SizeChanged(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 1)
            {
                pictureBox1.Visible = false;
            }
            if (comboBox1.SelectedIndex == 1) 
            { 
                pictureBox1.Visible = true;
            }

        }
    }
}
