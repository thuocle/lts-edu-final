using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface IDangKyHoc
    {
        Task<ErrorMessage> ThemDangKyHocAsync(DangKyHoc dk);
        Task<ErrorMessage> SuaDangKyHocAsync(DangKyHoc dk, int dkID);
        Task<ErrorMessage> XoaDangKyHocAsync(int dkID);
        Task<PageInfo<DangKyHoc>> HienThiDangKyHocAsync(Pagination page);
    }
}
