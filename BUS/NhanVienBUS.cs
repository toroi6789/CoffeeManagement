using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAO nhanVienDAO = new NhanVienDAO();
        private readonly UserBUS userBUS = new UserBUS();

        public List<NhanVienDTO> GetAllNhanVien()
        {
            return nhanVienDAO.GetAll();
        }

        // Thêm nhân viên
        public Result AddNhanVien(NhanVienDTO nv)
        {
            var errors = ValidateNhanVien(nv);
            if (errors.Any())
            {
                return new Result
                {
                    Success = false,
                    Message = string.Join("\n", errors)
                };
            }

            // Kiểm tra userID có hợp lệ
            UserDTO user = userBUS.GetUserByID(nv.UserID);
            if (user != null)
            {
                NhanVienDTO existing = GetNhanVienByNhanVienID(user.UserID);
                if (existing != null)
                {
                    return new Result
                    {
                        Success = false,
                        Message = $"Không thể tạo NV: {existing.FullName} đã sở hữu tài khoản này!"
                    };
                }
            }

            nv.NgayKhoiTao = DateTime.Now;

            bool ok = nhanVienDAO.Insert(nv);
            return new Result
            {
                Success = ok,
                Message = ok ? "Thêm nhân viên thành công!" : "Thêm nhân viên thất bại!"
            };
        }

        // Cập nhật nhân viên
        public Result UpdateNhanVien(NhanVienDTO nv)
        {
            var errors = ValidateNhanVien(nv);
            if (errors.Any())
            {
                return new Result
                {
                    Success = false,
                    Message = string.Join("\n", errors)
                };
            }

            UserDTO user = userBUS.GetUserByID(nv.UserID);

            if (user != null)
            {
                if (GetNhanVienByNhanVienID(user.UserID) != null)
                {
                    // Nếu UserID đang được NV khác dùng → không cho sửa UserID
                    nv.UserID = -1;
                }
            }

            nv.NgayCapNhat = DateTime.Now;

            bool ok = nhanVienDAO.Update(nv);
            return new Result
            {
                Success = ok,
                Message = ok ? "Cập nhật nhân viên thành công!" : "Cập nhật nhân viên thất bại!"
            };
        }

        // Xóa nhân viên
        public Result DeleteNhanVien(NhanVienDTO nhanVien)
        {
            UserDTO user = userBUS.GetUserByID(nhanVien.UserID);

            if (user != null)
            {
                int adminCount = userBUS.GetUsers().Count(u => u.RoleID == 1);

                if (user.RoleID == 1 && adminCount == 1)
                {
                    return new Result
                    {
                        Success = false,
                        Message = "Không thể xóa Admin cuối cùng!"
                    };
                }
            }

            // Nếu NV có hoá đơn → soft delete
            int soHD = HoaDonBUS.GetAllListHD().Count(h => h.NhanVienID == nhanVien.NhanVienID);

            if (soHD > 0)
            {
                Result r = userBUS.SoftDeleteUser(nhanVien.UserID);
                MessageBox.Show("Tạm dừng hoạt động vì NV ảnh hưởng đến nhiều HĐ", "Thông báo");
                return r;
            }
            else
            {
                if (!nhanVienDAO.Delete(nhanVien.NhanVienID))
                {
                    return new Result { Success = false, Message = "Xóa nhân viên thất bại!" };
                }

                Result userDelete = userBUS.DeleteUser(nhanVien.UserID);
                return userDelete;
            }
        }

        public static DataTable LayNV_userID(int UserID)
        {
            return NhanVienDAO.LayNV_userID(UserID);
        }

        public NhanVienDTO GetNhanVienByNhanVienID(int nhanVienID)
        {
            return nhanVienDAO.GetByNhanVienID(nhanVienID);
        }

        public NhanVienDTO ConvertRowToDTO(DataRow row)
        {
            NhanVienDTO nv = new NhanVienDTO();

            nv.NhanVienID = Convert.ToInt32(row["NhanVienID"]);
            nv.Ho = row["Ho"].ToString();
            nv.Ten = row["Ten"].ToString();
            nv.Phone = row["Phone"].ToString();
            nv.TrangThai = row["TrangThai"].ToString();

            nv.DateJoin = row["DateJoin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DateJoin"]);
            nv.NgayCapNhat = row["NgayCapNhat"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayCapNhat"]);
            nv.NgayKhoiTao = Convert.ToDateTime(row["NgayKhoiTao"]);
            nv.UserID = Convert.ToInt32(row["UserID"]);

            return nv;
        }

        public static List<string> ValidateNhanVien(NhanVienDTO nv)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nv.Ho))
                errors.Add("Họ không được bỏ trống.");

            if (string.IsNullOrWhiteSpace(nv.Ten))
                errors.Add("Tên không được bỏ trống.");

            if (string.IsNullOrWhiteSpace(nv.Phone))
                errors.Add("Số điện thoại không được bỏ trống.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nv.Phone, @"^0\d{9}$"))
                errors.Add("Số điện thoại phải gồm 10 chữ số và bắt đầu bằng 0.");

            if (string.IsNullOrWhiteSpace(nv.TrangThai))
                errors.Add("Trạng thái không được bỏ trống.");

            if (nv.DateJoin.HasValue && nv.DateJoin.Value > DateTime.Now)
                errors.Add("Ngày vào làm không được lớn hơn hiện tại.");

            if (nv.UserID < 1)
                errors.Add("UserID không hợp lệ.");

            return errors;
        }

        public bool PhoneExists(string phone)
        {
            return nhanVienDAO.ExistsByPhone(phone);
        }
    }
}
