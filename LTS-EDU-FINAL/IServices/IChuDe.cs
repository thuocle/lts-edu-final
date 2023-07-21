using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface IChuDe
    {
        Task<ErrorMessage> ThemChuDeAsync(ChuDe cd);
        Task<ErrorMessage> SuaChuDeAsync(ChuDe cd, int cdID);
        Task<ErrorMessage> XoaChuDeAsync(int cdID);
        Task<PageInfo<ChuDe>> HienThiChuDeAsync(Pagination page);
    }
}
