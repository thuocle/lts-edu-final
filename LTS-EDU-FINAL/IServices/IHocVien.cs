using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface IHocVien
    {
        Task<ErrorMessage> ThemHocVienAsync(HocVien hv);
        Task<ErrorMessage> SuaHocVienAsync(HocVien hv, int hvID);
        Task<ErrorMessage> XoaHocVienAsync(int hvID);
        Task<PageInfo<HocVien>> TimKiemHocVienAsync(Pagination page, string? tenhv, string? email);
        Task<PageInfo<HocVien>> HienThiHocVienAsync(Pagination page);
    }
}
