using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface ITinhTrangHoc
    {
        Task<ErrorMessage> ThemTinhTrangHocAsync(TinhTrangHoc tt);
        Task<ErrorMessage> SuaTinhTrangHocAsync(TinhTrangHoc tt, int ttID);
        Task<ErrorMessage> XoaTinhTrangHocAsync(int ttID);
        Task<PageInfo<TinhTrangHoc>> HienThiTinhTrangHocAsync(Pagination page);
    }
}
