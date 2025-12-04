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
    public class SanPhamDAO:DBConnect
    {
        // Lấy tất cả sản phẩm
        public static DataTable SanPham()
        {
            string query = "SELECT * FROM sanpham;";
            return DBConnect.ExecuteQuery(query);
        }
        // Lấy sản phẩm theo ID
        public static DataTable SanPhamTheoID(int sanPhamID)
        {
            string query = $"SELECT * FROM sanpham WHERE SanPhamID = {sanPhamID};";
            return DBConnect.ExecuteQuery(query);
        }

        public List<SanPhamDTO> GetAll()
        {
            List<SanPhamDTO> dsSP = new List<SanPhamDTO>();
            string sql = @"
                    SELECT sp.SanPhamID, sp.TenSanPham, sp.GiaBan, sp.MoTa, sp.TrangThai, sp.DanhMucID, sp.Hinh
                    FROM SanPham sp";
            DataTable dt = ExecuteQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                SanPhamDTO sp = new SanPhamDTO
                {
                    SanPhamID = int.Parse(row["SanPhamID"].ToString()),
                    TenSanPham = row["TenSanPham"].ToString(),
                    GiaBan = (Decimal)float.Parse(row["GiaBan"].ToString()),
                    MoTa = row["MoTa"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    DanhMucID = int.Parse(row["DanhMucID"].ToString()),
                    Hinh = row["Hinh"].ToString()
                };
                dsSP.Add(sp);
            }
            return dsSP;
        }


        public bool IsSanPhamIdExists(int sanPhamId)
        {
            string sql = "SELECT COUNT(*) FROM SanPham WHERE SanPhamID = @id";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@id", MySqlDbType.Int32) { Value = sanPhamId }
            };
            DataTable dt = ExecuteQuery(sql, parameters);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public int GetNextSanPhamId()
        {
            string sql = "SELECT COUNT(*) FROM SanPhamm";
            DataTable dt = ExecuteQuery(sql);
            return Convert.ToInt32(dt.Rows[0][0]) + 1;
        }

        public bool daoThemSanPham(SanPhamDTO sp)
        {
            string sql = @"
            INSERT INTO SanPham 
            (SanPhamID, TenSanPham, GiaBan, MoTa, TrangThai, DanhMucID, `Hinh`) 
            VALUES 
            (@SanPhamID, @TenSanPham, @GiaBan, @MoTa, @TrangThai, @DanhMucID, @Hinh)";

            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@SanPhamID", MySqlDbType.Int32) { Value = sp.SanPhamID },
            new MySqlParameter("@TenSanPham", MySqlDbType.VarChar) { Value = sp.TenSanPham ?? (object)DBNull.Value },
            new MySqlParameter("@GiaBan", MySqlDbType.Decimal) { Value = sp.GiaBan },
            new MySqlParameter("@MoTa", MySqlDbType.Text) { Value = sp.MoTa ?? (object)DBNull.Value },
            new MySqlParameter("@TrangThai", MySqlDbType.VarChar) { Value = sp.TrangThai ?? "Còn hàng" },
            new MySqlParameter("@DanhMucID", MySqlDbType.Int32) { Value = sp.DanhMucID },
            new MySqlParameter("@Hinh", MySqlDbType.VarChar) { Value = sp.Hinh ?? (object)DBNull.Value }
            };

            try
            {
                int rowsAffected = ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                Console.WriteLine("Lỗi INSERT SanPham: " + ex.Message);
                return false;
            }
        }

        public bool daoSuaSanPham(SanPhamDTO sp)
        {
            string sql = @"
            UPDATE SanPham SET 
                TenSanPham = @Ten, 
                GiaBan = @Gia, 
                MoTa = @MoTa, 
                TrangThai = @TrangThai, 
                DanhMucID = @DanhMuc, 
                `Hinh` = @Hinh 
            WHERE SanPhamID = @ID";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ID", sp.SanPhamID),
                new MySqlParameter("@Ten", sp.TenSanPham),
                new MySqlParameter("@Gia", sp.GiaBan),
                new MySqlParameter("@MoTa", sp.MoTa ?? (object)DBNull.Value),
                new MySqlParameter("@TrangThai", sp.TrangThai ?? "Còn hàng"),
                new MySqlParameter("@DanhMuc", sp.DanhMucID),
                new MySqlParameter("@Hinh", sp.Hinh ?? (object)DBNull.Value)
            };

            return ExecuteNonQuery(sql, parameters) > 0;
        }


        public bool daoCapNhatTrangThaiSanPham(int sanPhamID, string trangThaiMoi)
        {
            string sql = "UPDATE SanPham SET TrangThai = @TrangThai WHERE SanPhamID = @ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ID", sanPhamID),
                new MySqlParameter("@TrangThai", trangThaiMoi)
            };

            try
            {
                return ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Lỗi cập nhật trạng thái sản phẩm: " + ex.Message);
            }
        }

        // SanPhamDAO.cs
        public int LaySanPhamIDLonNhat()
        {
            try
            {
                string sql = "SELECT COALESCE(MAX(SanPhamID), 0) FROM SanPham";
                // COALESCE = MySQL version của ISNULL
                var dt = DBConnect.ExecuteQuery(sql);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy ID lớn nhất: " + ex.Message);
            }
        }

    }
}
