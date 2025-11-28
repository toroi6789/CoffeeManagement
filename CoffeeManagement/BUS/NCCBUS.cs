using System.Collections.Generic;
using System.Data;
using CoffeeManagement.DTO;
using CoffeeManagement.DAO;

namespace CoffeeManagement.BUS
{
    public class NCCBUS
    {
        public static List<NCCDTO> GetAll()
        {
            DataTable dt = NhaCungCapDAO.GetAll();
            List<NCCDTO> list = new List<NCCDTO>();

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new NCCDTO(
                    int.Parse(r["NhaCungCapID"].ToString()),
                    r["TenNhaCungCap"].ToString(),
                    r["DiaChi"].ToString(),
                    r["SoDienThoai"].ToString(),
                    r["Email"].ToString(),
                    r["Website"].ToString(),
                    r["TrangThai"].ToString()
                ));
            }

            return list;
        }

        public static void Insert(NCCDTO n)
        {
            NhaCungCapDAO.Insert(n.TenNhaCungCap, n.DiaChi, n.SoDienThoai, n.Email, n.Website, n.TrangThai);
        }

        public static void Update(NCCDTO n)
        {
            NhaCungCapDAO.Update(n.NhaCungCapID, n.TenNhaCungCap, n.DiaChi, n.SoDienThoai, n.Email, n.Website, n.TrangThai);
        }

        public static void Delete(int id)
        {
            NhaCungCapDAO.Delete(id);
        }

        public static List<NCCDTO> Search(string keyword)
        {
            DataTable dt = NhaCungCapDAO.Search(keyword);
            List<NCCDTO> list = new List<NCCDTO>();

            foreach (DataRow r in dt.Rows)
            {
                list.Add(new NCCDTO(
                    int.Parse(r["NhaCungCapID"].ToString()),
                    r["TenNhaCungCap"].ToString(),
                    r["DiaChi"].ToString(),
                    r["SoDienThoai"].ToString(),
                    r["Email"].ToString(),
                    r["Website"].ToString(),
                    r["TrangThai"].ToString()
                ));
            }

            return list;
        }
    }
}
