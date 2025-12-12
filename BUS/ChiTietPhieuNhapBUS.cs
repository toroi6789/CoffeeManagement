using DAO;
using DTO;
using System.Collections.Generic;

namespace BUS
{
    public class ChiTietPhieuNhapBUS
    {
        private ChiTietPhieuNhapDAO dao = new ChiTietPhieuNhapDAO();

        public List<ChiTietPhieuNhapDTO> LayChiTietTheoPhieuNhapID(int phieuNhapID)
        {
            return dao.GetChiTietByPhieuNhapID(phieuNhapID);
        }

        // Thêm chi tiết (nếu cần)
        public bool ThemChiTiet(ChiTietPhieuNhapDTO ct)
        {
            // Validation nếu cần
            return dao.InsertChiTiet(ct);
        }
    }
}
