using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeeManagement.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAO nhanVienDAO = new NhanVienDAO();
        private UserBUS userBUS = new UserBUS();

        public NhanVienBUS()
        {

        }

        // Get all employees
        public List<NhanVienDTO> GetAllNhanVien()
        {
            return nhanVienDAO.GetAll();
        }

        // Add new employee (with validation if needed)
        public bool AddNhanVien(NhanVienDTO nv)
        {
            var errors = ValidateNhanVien(nv);
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Lỗi dữ liệu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            UserDTO user = userBUS.GetUserByID(nv.UserID);
            if (user != null)
            {
                NhanVienDTO nhanVienDTO = GetNhanVienByNhanVienID(user.UserID);
                if (nhanVienDTO != null)
                {   
                    MessageBox.Show("Không thể tạo NV:" + nhanVienDTO.FullName + "đã sở hữu tài khoản này!", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }    
            }

            nv.NgayKhoiTao = DateTime.Now;
            return nhanVienDAO.Insert(nv);
        }

        // Update employee
        public bool UpdateNhanVien(NhanVienDTO nv)
        {
            var errors = ValidateNhanVien(nv);
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Lỗi dữ liệu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            UserDTO user = userBUS.GetUserByID(nv.UserID); 
            if (user != null)
            {
                // Nếu tồn tại 1 NV đã đc gán vào user
                if (GetNhanVienByNhanVienID(user.UserID) != null)
                {
                    // không cho cập nhật userID nếu có 1 nv đã sở hữu userID đó
                    nv.UserID = -1;
                    //MessageBox.Show("Không thể update userID của:" + nv.FullName + " vì NV khác đã sở hữu tài khoản này!", "Thông báo",
                    //    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

           
            nv.NgayCapNhat = DateTime.Now;
            return nhanVienDAO.Update(nv);
        }

        // Delete employee
        public bool DeleteNhanVien(NhanVienDTO nhanVien)
        {
            UserDTO user = userBUS.GetUserByID(nhanVien.UserID);

            if (user != null)
            {
                int adminCount = userBUS.GetUsers().Count(u => u.RoleID == 1);

                if (user.RoleID == 1 && adminCount == 1)
                {
                    MessageBox.Show("Không thể xóa Admin cuối cùng trong hệ thống!");
                    return false;
                }
            }

            // check nếu NV có hóa đơn thì soft delete
            int soHDMaNVSoHuu = HoaDonBUS.GetAllListHD()
                .Count(h => h.NhanVienID == nhanVien.NhanVienID);
            if (soHDMaNVSoHuu > 0) 
            {
                return userBUS.SoftDeleteUser(nhanVien.UserID);
            }
            else
            {
                // Xóa nhân viên trước
                if (!nhanVienDAO.Delete(nhanVien.NhanVienID))
                    return false;
                // Sau đó xóa user liên kết
                return userBUS.DeleteUser(nhanVien.UserID);
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

            nv.DateJoin = row["DateJoin"] == DBNull.Value
                ? (DateTime?)null
                : (DateTime?)Convert.ToDateTime(row["DateJoin"]);

            nv.NgayCapNhat = row["NgayCapNhat"] == DBNull.Value
                ? (DateTime?)null
                : (DateTime?)Convert.ToDateTime(row["NgayCapNhat"]);


            nv.NgayKhoiTao = Convert.ToDateTime(row["NgayKhoiTao"]);
            nv.UserID = Convert.ToInt32(row["UserID"]);

            return nv;
        }

        public static List<string> ValidateNhanVien(NhanVienDTO nv)
        {
            List<string> errors = new List<string>();

            // Validate Ho
            if (string.IsNullOrWhiteSpace(nv.Ho))
            {
                errors.Add("Họ không được bỏ trống.");
            }

            // Validate Ten
            if (string.IsNullOrWhiteSpace(nv.Ten))
            {
                errors.Add("Tên không được bỏ trống.");
            }

            // Validate Phone
            if (string.IsNullOrWhiteSpace(nv.Phone))
            {
                errors.Add("Số điện thoại không được bỏ trống.");
            }
            else
            {
                // phone = 10 số, bắt đầu từ 0
                if (!System.Text.RegularExpressions.Regex.IsMatch(nv.Phone, @"^0\d{9}$"))
                {
                    errors.Add("Số điện thoại phải gồm 10 chữ số và bắt đầu bằng số 0.");
                }
            }

            // Validate TrangThai
            if (string.IsNullOrWhiteSpace(nv.TrangThai))
            {
                errors.Add("Trạng thái không được bỏ trống.");
            }

            // Validate DateJoin
            if (nv.DateJoin.HasValue)
            {
                if (nv.DateJoin.Value > DateTime.Now)
                {
                    errors.Add("Ngày vào làm (DateJoin) không được lớn hơn ngày hiện tại.");
                }
            }

            // Validate UserID
            if (nv.UserID < 1)
            {
                errors.Add("UserID không hợp lệ.");
            }

            return errors;
        }

        public bool PhoneExists(string phone)
        {
            return nhanVienDAO.ExistsByPhone(phone);
        }
    }
}
