using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface ILoaiBaiViet
    {
        Task<ErrorMessage> ThemLoaiBaiVietAsync(LoaiBaiViet loai);
        Task<ErrorMessage> SuaLoaiBaiVietAsync(LoaiBaiViet loai, int loaiID);
        Task<ErrorMessage> XoaLoaiBaiVietAsync(int loaiID);
        Task<PageInfo<LoaiBaiViet>> HienThiLoaiBaiVietAsync(Pagination page);
    }
}
