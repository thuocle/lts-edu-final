using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface ITaiKhoan
    {
        Task<ErrorMessage> ThemTaiKhoanAsync(TaiKhoan tk);
        Task<ErrorMessage> SuaTaiKhoanAsync(TaiKhoan tk, int tkID);
        Task<ErrorMessage> XoaTaiKhoanAsync(int tkID);
        Task<PageInfo<TaiKhoan>> TimKiemTaiKhoanAsync(Pagination page, string tentk);
        Task<PageInfo<TaiKhoan>> HienThiTaiKhoanAsync(Pagination page);
    }
}
