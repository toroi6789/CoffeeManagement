using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class TaoID
    {
        public static int TaoHoaDonID()
        {
            int IDmax = DAO.LayID.LayHoaDonIDMoiNhat("hoadon");
            return IDmax++;
        }
        public static int TaoChiTietHoaDonID()
        {
            int IDmax = DAO.LayID.LayHoaDonIDMoiNhat("chitiethoadon");
            return IDmax++;
        }
        //Lay ID moi nhat cua bang Hoa Don de tao ID moi
        
        public static int LayHoaDonIDMoiNhat()
        {
            return DAO.LayID.LayHoaDonIDMoiNhat("hoadon");
        }
    }
}
