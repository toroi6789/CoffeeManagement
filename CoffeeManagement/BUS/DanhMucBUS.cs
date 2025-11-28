using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using CoffeeManagement.BUS;

namespace CoffeeManagement.BUS
{
    public class DanhMucBUS
    {
        private DanhMucDAO dao = new DanhMucDAO();
        public List<DanhMucDTO> LayTatCaDanhMuc()
        {
            return dao.GetAll();
        }
<<<<<<< HEAD
        // Lấy tất cả danh mục
        public List<DanhMucDTO> GetAllDanhMuc()
        {
            DataTable dt = DanhMucDAO.GetAllDanhMuc();
            List<DanhMucDTO> list = new List<DanhMucDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DanhMucDTO(
                    Convert.ToInt32(row["DanhMucID"]),   // FIXED
                    row["TenDanhMuc"].ToString(),
                    row["TrangThai"].ToString(),
                    row["MoTa"].ToString(),
                    Convert.ToDecimal(row["GiaBan"])
                ));
            }

            return list;
        }

        // Lấy theo ID
        public DanhMucDTO GetDanhMucByID(int id)
        {
            DataTable dt = DanhMucDAO.GetDanhMucByID(id);

            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];

            return new DanhMucDTO(
                Convert.ToInt32(r["DanhMucID"]),   // FIXED
                r["TenDanhMuc"].ToString(),
                r["TrangThai"].ToString(),
                r["MoTa"].ToString(),
                Convert.ToDecimal(r["GiaBan"])
            );
        }

        // Thêm (không return)
        public void Insert(DanhMucDTO dm)
        {
            DanhMucDAO.InsertDanhMuc(dm.TenDanhMuc, dm.TrangThai, dm.MoTa, dm.GiaBan);
        }

        // Cập nhật (không return)
        public void Update(DanhMucDTO dm)
        {
            DanhMucDAO.UpdateDanhMuc(dm.DanhMucID, dm.TenDanhMuc, dm.TrangThai, dm.MoTa, dm.GiaBan);
        }

        // Xóa (không return)
        public void Delete(int id)
        {
            DanhMucDAO.DeleteDanhMuc(id);
        }

        //Tìm kiếm
        public List<DanhMucDTO> Search(string keyword)
    {
            DataTable dt = DanhMucDAO.Search(keyword);
            List<DanhMucDTO> list = new List<DanhMucDTO>();

            foreach (DataRow r in dt.Rows)
        {
                list.Add(new DanhMucDTO(
                    Convert.ToInt32(r["DanhMucID"]),
                    r["TenDanhMuc"].ToString(),
                    r["TrangThai"].ToString(),
                    r["MoTa"].ToString(),
                    Convert.ToDecimal(r["GiaBan"])
                ));
            }

            return list;
        }
=======

>>>>>>> 4480848b6efa00e72e27f1eee3407df8954d318e
    }
}
