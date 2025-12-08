using CoffeeManagement.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CoffeeManagement.DAO
{
    public class NhanVienDAO : DBConnect
    {
        // Get all employees
        public List<NhanVienDTO> GetAll()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            string query = "SELECT * FROM NhanVien";

            DataTable dt = ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVienDTO()
                {
                    NhanVienID = Convert.ToInt32(row["NhanVienID"]),
                    Ho = row["Ho"]?.ToString(),
                    Ten = row["Ten"]?.ToString(),
                    Phone = row["Phone"]?.ToString(),
                    TrangThai = row["TrangThai"]?.ToString(),
                    DateJoin = row["DateJoin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DateJoin"]),
                    NgayCapNhat = row["NgayCapNhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayCapNhat"]),
                    NgayKhoiTao = Convert.ToDateTime(row["NgayKhoiTao"]),
                    UserID = Convert.ToInt32(row["UserID"])
                });
            }

            return list;
        }

        // Insert employee
        public bool Insert(NhanVienDTO nv)
        {
            string query = @"INSERT INTO NhanVien (Ho, Ten, Phone, TrangThai, DateJoin, NgayCapNhat, UserID)
                             VALUES (@Ho, @Ten, @Phone, @TrangThai, @DateJoin, @NgayCapNhat, @UserID)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@Ho", nv.Ho ?? (object)DBNull.Value),
                new MySqlParameter("@Ten", nv.Ten ?? (object)DBNull.Value),
                new MySqlParameter("@Phone", nv.Phone ?? (object)DBNull.Value),
                new MySqlParameter("@TrangThai", nv.TrangThai ?? (object)DBNull.Value),
                new MySqlParameter("@DateJoin", nv.DateJoin ?? (object)DBNull.Value),
                new MySqlParameter("@NgayCapNhat", nv.NgayCapNhat ?? (object)DBNull.Value),
                new MySqlParameter("@UserID", nv.UserID)
            };

            return ExecuteNonQuery(query, parameters) > 0;
        }

        // Update employee
        public bool Update(NhanVienDTO nv)
        {
            Console.WriteLine("UPDATE ID = " + nv.NhanVienID);

            bool valid = nv.UserID > 0;

            string query = @"UPDATE NhanVien 
                     SET Ho=@Ho, Ten=@Ten, Phone=@Phone, TrangThai=@TrangThai,
                         DateJoin=@DateJoin, NgayCapNhat=@NgayCapNhat";

            // chỉ thêm UserID nếu hợp lệ
            if (valid)
            {   
                query += ", UserID=@UserID";
            }

            query += " WHERE NhanVienID=@ID";

            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@Ho", nv.Ho ?? (object)DBNull.Value),
                new MySqlParameter("@Ten", nv.Ten ?? (object)DBNull.Value),
                new MySqlParameter("@Phone", nv.Phone ?? (object)DBNull.Value),
                new MySqlParameter("@TrangThai", nv.TrangThai ?? (object)DBNull.Value),
                new MySqlParameter("@DateJoin", nv.DateJoin ?? (object)DBNull.Value),
                new MySqlParameter("@NgayCapNhat", nv.NgayCapNhat ?? (object)DBNull.Value),
                new MySqlParameter("@ID", nv.NhanVienID)
            };

            if (valid)
            {
                parameters.Add(new MySqlParameter("@UserID", nv.UserID));
            }

            return ExecuteNonQuery(query, parameters.ToArray()) > 0;
        }

        // Delete employee
        public bool Delete(int id)
        {
            string query = "DELETE FROM NhanVien WHERE NhanVienID=@ID";
            MySqlParameter[] parameters = {
                new MySqlParameter("@ID", id)
            };

            return ExecuteNonQuery(query, parameters) > 0;
        }
        public static DataTable LayNV_userID(int IDuser)
        {
            string query = $"SELECT * FROM coffeemanagement.nhanvien where UserID = '{IDuser}';";
            return DBConnect.ExecuteQuery(query) ;
        }

        // Check if a phone number already exists
        public bool ExistsByPhone(string phone)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE Phone = @Phone";

            MySqlParameter[] parameters =
            {
        new MySqlParameter("@Phone", phone)
    };

            DataTable dt = ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }

            return false;
        }

        // Get employee by NhanVienID
        public NhanVienDTO GetByNhanVienID(int nvID)
        {
            string query = "SELECT * FROM NhanVien WHERE NhanVienID = @ID";

            MySqlParameter[] parameters = {
        new MySqlParameter("@ID", nvID)
    };

            DataTable dt = ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            return new NhanVienDTO()
            {
                NhanVienID = Convert.ToInt32(row["NhanVienID"]),
                Ho = row["Ho"]?.ToString(),
                Ten = row["Ten"]?.ToString(),
                Phone = row["Phone"]?.ToString(),
                TrangThai = row["TrangThai"]?.ToString(),
                DateJoin = row["DateJoin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DateJoin"]),
                NgayCapNhat = row["NgayCapNhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayCapNhat"]),
                NgayKhoiTao = Convert.ToDateTime(row["NgayKhoiTao"]),
                UserID = Convert.ToInt32(row["UserID"])
            };
        }

    }
}
