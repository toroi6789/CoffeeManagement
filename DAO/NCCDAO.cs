using System.Data;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class NhaCungCapDAO
    {
        public static DataTable GetAll()
        {
            string query = "SELECT * FROM NhaCungCap";
            return DBConnect.ExecuteQuery(query);
        }

        public static void Insert(string ten, string diachi, string sdt, string email, string web, string trangthai)
        {
            string query =
                $"INSERT INTO NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, Website, TrangThai) " +
                $"VALUES ('{ten}', '{diachi}', '{sdt}', '{email}', '{web}', '{trangthai}');";

            DBConnect.ExecuteNonQuery(query);
        }

        public static void Update(int id, string ten, string diachi, string sdt, string email, string web, string trangthai)
        {
            string query =
                $"UPDATE NhaCungCap SET " +
                $"TenNhaCungCap='{ten}', DiaChi='{diachi}', SoDienThoai='{sdt}', " +
                $"Email='{email}', Website='{web}', TrangThai='{trangthai}' " +
                $"WHERE NhaCungCapID={id}";

            DBConnect.ExecuteNonQuery(query);
        }

        public static void Delete(int id)
        {
            string query = $"DELETE FROM NhaCungCap WHERE NhaCungCapID={id}";
            DBConnect.ExecuteNonQuery(query);
        }

        public static DataTable Search(string keyword)
        {
            string query =
                $"SELECT * FROM NhaCungCap " +
                $"WHERE TenNhaCungCap LIKE '%{keyword}%' OR NhaCungCapID LIKE '%{keyword}%';";

            return DBConnect.ExecuteQuery(query);
        }

        public static void ResetAI()
        {
            DBConnect.ExecuteNonQuery("ALTER TABLE NhaCungCap AUTO_INCREMENT = 1;");
        }
    }
}
