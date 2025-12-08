using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class PhieuNhapDAO
    {
        //lay tat ca phieu nhap
        public static DataTable PhieuNhap()
        {
            //code lay tat ca phieu nhap
            string query = "SELECT * FROM phieunhap";
            return DBConnect.ExecuteQuery(query);
        }
        //lay phieu nhap theo id
        public static DataTable PhieuNhapID(int phieuNhapID)
        {
            //code lay phieu nhap theo id
            string query = "SELECT * FROM phieunhap WHERE PhieuNhapID = " + phieuNhapID;
            return DBConnect.ExecuteQuery(query);
        }
    }
}
