using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface IKhoaHoc
    {
        Task<ErrorMessage> ThemKhoaHocAsync(KhoaHoc kh);
        Task<ErrorMessage> SuaKhoaHocAsync(KhoaHoc kh, int khID);
        Task<ErrorMessage> XoaKhoaHocAsync(int khID);
        Task<PageInfo<KhoaHoc>> TimKiemKhoaHocAsync(Pagination page, string tenKH);
        Task<PageInfo<KhoaHoc>> HienThiKhoaHocAsync(Pagination page);
    }
}
