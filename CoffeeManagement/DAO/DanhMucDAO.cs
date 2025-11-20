using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class DanhMucDAO : DBConnect
    {
        public List<DanhMucDTO> GetAll()
        {
            string sql = @"
                SELECT DanhMucID, TenDanhMuc, TrangThai, MoTa 
                FROM DanhMuc 
                WHERE TrangThai = 'Hoạt động'"; // Chỉ lấy danh mục đang hoạt động

            DataTable dt = ExecuteQuery(sql);

            return dt.AsEnumerable()
                .Select(row => new DanhMucDTO
                {
                    DanhMucID = row.Field<int>("DanhMucID"),
                    TenDanhMuc = row.Field<string>("TenDanhMuc"),
                    TrangThai = row.Field<string>("TrangThai") ?? "Hoạt động",
                    MoTa = row.Field<string>("MoTa") ?? ""
                })
                .ToList();
        }
    }
}
