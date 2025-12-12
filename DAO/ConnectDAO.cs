using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class DBConnect
    {
        private static string connectionString =
             "server=localhost;port=3306;userid=root;password=N241206h@;database=coffeemanagement;charset=utf8mb4;";
            //ConfigurationManager.ConnectionStrings["CoffeeManagement_full"]?.ConnectionString 
            //?? "server=localhost;port=3306;user id=root;password=123456789;database=coffeemanagement;charset=utf8mb4;";

        // Hàm trả về đối tượng kết nối MySQL
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // hàm trả về DataTable từ truy vấn SQL
        public static DataTable ExecuteQuery(string query)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Hàm thực thi câu lệnh SQL không trả về dữ liệu
        public static void ExecuteNonQuery(string query)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // Hàm kiểm tra kết nối
        public static void TestConnection()
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Kết nối thành công!");

                    // Ví dụ: Thực thi truy vấn thử
                    string query = "SELECT * FROM chitiethoadon;";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine($"{row["ChiTietHoaDonID"]} - {row["SoLuong"]} - {row["DonGia"]} - {row["HoaDonID"]} - {row["SanPhamID"]}");
                    }
                    
                    Console.WriteLine("Truy vấn thực thi thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                    
                }
            }
        }

        // Hàm thực thi SELECT (trả về DataTable) voi 2 agrument
        protected static DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();


                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        // Hàm thực thi INSERT, UPDATE, DELETE
        protected static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Hàm thực thi query trả về giá trị đầu tiên của dòng đầu tiên
        protected static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteScalar();
                }
            }
        }

    }
}
