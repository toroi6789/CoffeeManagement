using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class PhieuNhapBUS
    {
        //lay tat ca phieu nhap
        public static DataTable PhieuNhap()
        {
            return DAO.PhieuNhapDAO.PhieuNhap();
        }
        //lay phieu nhap theo id
        public static DataTable PhieuNhapID(int phieuNhapID)
        {
            return DAO.PhieuNhapDAO.PhieuNhapID(phieuNhapID);
        }
    }
}
