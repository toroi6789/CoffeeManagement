using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class DanhMucBUS
    {
        private DanhMucDAO dao = new DanhMucDAO();
        public List<DanhMucDTO> LayTatCaDanhMuc()
        {
            return dao.GetAll();
        }
    }
}
