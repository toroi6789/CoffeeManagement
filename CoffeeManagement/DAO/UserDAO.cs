using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class UserDAO : DBConnect
    {
        // Lấy thông tin user theo Email và Mật khẩu
        public UserDTO GetUserByEmailAndPassword(string email, string password)
        {
            string query = @"
                SELECT u.UserID, u.Email, u.MatKhau, u.TrangThai, u.RoleID, u.NgayDangNhapCuoi, r.TenRole
                FROM `User` u
                INNER JOIN `Role` r ON u.RoleID = r.RoleID
                WHERE u.Email = @Email AND u.MatKhau = @MatKhau AND u.TrangThai = 'Hoạt động' ";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email),
                new MySqlParameter("@MatKhau", password)
            };

            DataTable dt = ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new UserDTO
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Email = row["Email"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    RoleID = Convert.ToInt32(row["RoleID"]),
                    TenRole = row["TenRole"].ToString(),
                    NgayDangNhapCuoi = row["NgayDangNhapCuoi"] != DBNull.Value ? Convert.ToDateTime(row["NgayDangNhapCuoi"]) : (DateTime?)null
                };
            }

            return null;
        }

        // Cập nhật ngày đăng nhập cuối
        public void UpdateLastLoginDate(int userID)
        {
            string query = "UPDATE `User` SET NgayDangNhapCuoi = NOW() WHERE UserID = @UserID";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@UserID", userID)
            };
            ExecuteNonQuery(query, parameters);
        }

        // Lấy tất cả users
        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();
            string query = @"
                SELECT u.UserID, u.Email, u.MatKhau, u.TrangThai, u.RoleID, u.NgayDangNhapCuoi, r.TenRole
                FROM `User` u
                INNER JOIN `Role` r ON u.RoleID = r.RoleID";

            DataTable dt = ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                users.Add(new UserDTO
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Email = row["Email"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    RoleID = Convert.ToInt32(row["RoleID"]),
                    TenRole = row["TenRole"].ToString(),
                    NgayDangNhapCuoi = row["NgayDangNhapCuoi"] != DBNull.Value ? Convert.ToDateTime(row["NgayDangNhapCuoi"]) : (DateTime?)null
                });
            }

            return users;
        }

        // Thêm user mới
        public void Add(UserDTO user)
        {
            string query = @"
                INSERT INTO `User` (Email, MatKhau, TrangThai, RoleID, NgayKhoiTao)
                VALUES (@Email, @MatKhau, @TrangThai, @RoleID, NOW())";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", user.Email),
                new MySqlParameter("@MatKhau", user.MatKhau),
                new MySqlParameter("@TrangThai", user.TrangThai),
                new MySqlParameter("@RoleID", user.RoleID)
            };

            ExecuteNonQuery(query, parameters);
        }
    }
}
