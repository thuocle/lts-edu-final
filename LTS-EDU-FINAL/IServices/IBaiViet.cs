using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface IBaiViet
    {
        Task<ErrorMessage> ThemBaiVietAsync(BaiViet bv);
        Task<ErrorMessage> SuaBaiVietAsync(BaiViet bv, int bvID);
        Task<ErrorMessage> XoaBaiVietAsync(int bvID);
        Task<PageInfo<BaiViet>> TimKiemBaiVietAsync(Pagination page, string? tenBV);
        Task<PageInfo<BaiViet>> HienThiBaiVietAsync(Pagination page);
    }
}
