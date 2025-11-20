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
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
        }

        #region Properties
        private string _tensanpham;
        //private string _mota;
        private double _giaban;
        private string _url;


        [Category("Custom Props")]
        public string Tensanpham
        {
            get { return _tensanpham; }
            set { _tensanpham = value; lblTenSP.Text = value; }
        }


        //[Category("Custom Props")]
        //public string Mota
        //{
        //    get { return _mota; }
        //    set { _mota = value; lblMoTa.Text ="Mô tả : "+ value; }
        //}


        [Category("Custom Props")]
        public double Giaban
        {
            get { return _giaban; }
            set { _giaban= value; lblGiaBan.Text = "Giá: " + value.ToString() + " VND"; }
        }


        [Category("Custom Props")]
        public string URL
        {
            get { return _url; }
            set
            {
                _url = value;
                HinhSP.LoadAsync(value);
                HinhSP.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        #endregion

        private void CustomListItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void CustomListItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HinhSP_Click(object sender, EventArgs e)
        {

        }
    }
}
