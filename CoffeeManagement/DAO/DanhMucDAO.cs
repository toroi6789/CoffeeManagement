using CoffeeManagement.DTO;
using CoffeeManagement.BUS; 
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

        // Lấy tất cả danh mục
        public static DataTable GetAllDanhMuc()
        {
            string query = "SELECT * FROM DanhMuc;";
            return DBConnect.ExecuteQuery(query);
        }

        // Lấy danh mục theo ID
        public static DataTable GetDanhMucByID(int id)
        {
            string query = $"SELECT * FROM DanhMuc WHERE DanhMucID = {id};";
            return DBConnect.ExecuteQuery(query);
        }

        // Thêm danh mục
        public static void InsertDanhMuc(string ten, string trangthai, string mota, decimal giaban)
        {
            string query =
                $"INSERT INTO DanhMuc (TenDanhMuc, TrangThai, MoTa, GiaBan) " +
                $"VALUES ('{ten}', '{trangthai}', '{mota}', {giaban});";

            DBConnect.ExecuteNonQuery(query);
        }

        // Cập nhật danh mục
        public static void UpdateDanhMuc(int id, string ten, string trangthai, string mota, decimal giaban)
        {
            string query =
                $"UPDATE DanhMuc SET " +
                $"TenDanhMuc='{ten}', TrangThai='{trangthai}', MoTa='{mota}', GiaBan={giaban} " +
                $"WHERE DanhMucID={id};";

            DBConnect.ExecuteNonQuery(query);
        }

        // Xóa danh mục
        public static void DeleteDanhMuc(int id)
    {
            string query = $"DELETE FROM DanhMuc WHERE DanhMucID = {id};";
            DBConnect.ExecuteNonQuery(query);
        }

        //search
        public static DataTable Search(string keyword)
        {
            string query =
                $"SELECT * FROM DanhMuc " +
                $"WHERE DanhMucID LIKE '%{keyword}%' OR TenDanhMuc LIKE '%{keyword}%';";

            return DBConnect.ExecuteQuery(query);
        }

        // Reset AUTO_INCREMENT
        public static void ResetAutoIncrement()
                {
            string query = "ALTER TABLE DanhMuc AUTO_INCREMENT = 1;";
            DBConnect.ExecuteNonQuery(query);
        }
    }
}
