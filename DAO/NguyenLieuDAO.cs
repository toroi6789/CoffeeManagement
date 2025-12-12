using DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NguyenLieuDAO:DBConnect
    {
        private static List<NguyenLieuDTO> dsNguyenLieu = new List<NguyenLieuDTO>();
        public List<NguyenLieuDTO> GetAll()
        {
            List<NguyenLieuDTO> dsNL = new List<NguyenLieuDTO>();
            string sql = @"
                    SELECT NguyenLieuID, TenNguyenLieu, GiaNhap, MoTa, TrangThai, DanhMucID, SoLuongTon, DonVi, Hinh
                    FROM NguyenLieu ";
            DataTable dt = ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                NguyenLieuDTO nl = new NguyenLieuDTO
                {
                    NguyenLieuID = int.Parse(row["NguyenLieuID"].ToString()),
                    TenNguyenLieu = row["TenNguyenLieu"].ToString(),
                    GiaNhap = (Decimal)float.Parse(row["GiaNhap"].ToString()),
                    MoTa = row["MoTa"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    DanhMucID = int.Parse(row["DanhMucID"].ToString()),
                    DonVi = row["DonVi"].ToString(),
                    SoLuongTon = (Decimal)float.Parse(row["SoLuongTon"].ToString()),
                    Hinh = row["Hinh"].ToString(),
                };
                dsNL.Add(nl);
            }
            return dsNL;
        }

        public bool IsNguyenLieuIdExists(int nguyenLieuId)
        {
            string sql = "SELECT COUNT(*) FROM NguyenLieu WHERE NguyenLieuID = @id";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@id", MySqlDbType.Int32) { Value = nguyenLieuId }
            };
            DataTable dt = ExecuteQuery(sql, parameters);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public int GetNextNguyenLieuId()
        {
            string sql = "SELECT COUNT(*) FROM NguyenLieu";
            DataTable dt = ExecuteQuery(sql);
            return Convert.ToInt32(dt.Rows[0][0]) + 1;
        }

        public bool daoThemNguyenLieu(NguyenLieuDTO nl)
        {
            string sql = @"
            INSERT INTO NguyenLieu 
            (NguyenLieuID, TenNguyenLieu, GiaNhap, MoTa, TrangThai, DanhMucID, SoLuongTon, DonVi, Hinh) 
            VALUES 
            (@NguyenLieuID, @TenNguyenLieu, @GiaNhap, @MoTa, @TrangThai, @DanhMucID, @SoLuongTon, @DonVi, @Hinh)";
            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@NguyenLieuID", MySqlDbType.Int32) { Value = nl.NguyenLieuID },
            new MySqlParameter("@TenNguyenLieu", MySqlDbType.VarChar) { Value = nl.TenNguyenLieu ?? (object)DBNull.Value },
            new MySqlParameter("@GiaNhap", MySqlDbType.Decimal) { Value = nl.GiaNhap },
            new MySqlParameter("@MoTa", MySqlDbType.Text) { Value = nl.MoTa ?? (object)DBNull.Value },
            new MySqlParameter("@TrangThai", MySqlDbType.VarChar) { Value = nl.TrangThai ?? "Hoạt động" },
            new MySqlParameter("@DanhMucID", MySqlDbType.Int32) { Value = nl.DanhMucID },
            new MySqlParameter("@DonVi", MySqlDbType.VarChar) { Value = nl.DonVi ?? (object)DBNull.Value },
            new MySqlParameter("@SoLuongTon", MySqlDbType.Decimal) { Value = nl.SoLuongTon },
            new MySqlParameter("@Hinh", MySqlDbType.VarChar) { Value = nl.Hinh ?? (object)DBNull.Value }
            };

            try
            {
                int rowsAffected = ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi INSERT NguyenLieu: " + ex.Message);
                return false;
            }
        }

        public bool daoSuaNguyenLieu(NguyenLieuDTO nl)
        {
            string sql = @"
            UPDATE NguyenLieu SET 
                TenNguyenLieu = @Ten, 
                GiaNhap = @Gia, 
                MoTa = @MoTa, 
                TrangThai = @TrangThai, 
                DanhMucID = @DanhMuc, 
                DonVi = @DonVi,
                SoLuongTon = @SoLuong,
                Hinh = @Hinh
            WHERE NguyenLieuID = @ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ID", nl.NguyenLieuID),
                new MySqlParameter("@Ten", nl.TenNguyenLieu),
                new MySqlParameter("@Gia", nl.GiaNhap),
                new MySqlParameter("@MoTa", nl.MoTa ?? (object)DBNull.Value),
                new MySqlParameter("@TrangThai", nl.TrangThai ?? "Hoạt động"),
                new MySqlParameter("@DanhMuc", nl.DanhMucID),
                new MySqlParameter("@DonVi", nl.DonVi ?? (object)DBNull.Value),
                new MySqlParameter("@Hinh", nl.Hinh ?? (object)DBNull.Value),
                new MySqlParameter("@SoLuong", nl.SoLuongTon),
            };
            return ExecuteNonQuery(sql, parameters) > 0;
        }


        public bool daoCapNhatTrangThaiNguyenLieu(int nguyenLieuID, string trangThaiMoi)
        {
            string sql = "UPDATE NguyenLieu SET TrangThai = @TrangThai WHERE NguyenLieuID = @ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ID", nguyenLieuID),
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

        public int LayNguyenLieuIDLonNhat()
        {
            try
            {
                string sql = "SELECT COALESCE(MAX(NguyenLieuID), 0) FROM NguyenLieu";
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

        public NguyenLieuDTO LayNguyenLieuTheoID(int id)
        {
            string sql = @"
                        SELECT NguyenLieuID, TenNguyenLieu, GiaNhap, MoTa, TrangThai, DanhMucID, SoLuongTon, DonVi, Hinh
                        FROM NguyenLieu 
                        WHERE NguyenLieuID = @id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", MySqlDbType.Int32) { Value = id }
            };
            DataTable dt = ExecuteQuery(sql, parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow row = dt.Rows[0];
            return new NguyenLieuDTO
            {
                NguyenLieuID = Convert.ToInt32(row["NguyenLieuID"]),
                TenNguyenLieu = row["TenNguyenLieu"].ToString(),
                GiaNhap = Convert.ToDecimal(row["GiaNhap"]),
                MoTa = row["MoTa"].ToString(),
                TrangThai = row["TrangThai"].ToString(),
                DanhMucID = Convert.ToInt32(row["DanhMucID"]),
                DonVi = row["DonVi"].ToString(),
                SoLuongTon = Convert.ToDecimal(row["SoLuongTon"]),
                Hinh = row["Hinh"].ToString()
            };
        }
    }
}
