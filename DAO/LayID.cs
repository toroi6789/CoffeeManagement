using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class LayID
    {
        public static int LayHoaDonIDMoiNhat(string table)
        {
            switch (table)
            {
                case "hoadon":
                    string query = "SELECT MAX(HoaDonID) AS HoaDonID FROM hoadon;";
                    var result = DBConnect.ExecuteQuery(query);
                    if (result.Rows.Count > 0 && result.Rows[0]["HoaDonID"] != DBNull.Value)
                    {
                        return Convert.ToInt32(result.Rows[0]["HoaDonID"]);
                    }
                    return -1; // Hoặc giá trị khác để biểu thị không tìm thấy
                // Thêm các bảng khác nếu cần
                case "chitiethoadon":
                    string queryCTHD = "SELECT MAX(ChiTietHoaDonID) AS ChiTietHoaDonID FROM chitiethoadon;";
                    var resultCTHD = DBConnect.ExecuteQuery(queryCTHD);
                    if (resultCTHD.Rows.Count > 0 && resultCTHD.Rows[0]["ChiTietHoaDonID"] != DBNull.Value)
                    {
                        return Convert.ToInt32(resultCTHD.Rows[0]["ChiTietHoaDonID"]);
                    }
                    return -1; // Hoặc giá trị khác để biểu thị không tìm thấy
                default:
                    throw new ArgumentException("Bảng không hợp lệ.");
            }
            
        }
    }
}
