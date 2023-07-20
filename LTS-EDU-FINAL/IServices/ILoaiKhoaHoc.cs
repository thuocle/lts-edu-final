using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface ILoaiKhoaHoc
    {
        Task<ErrorMessage> ThemLoaiKhoaHocAsync(LoaiKhoaHoc kh);
        Task<ErrorMessage> SuaLoaiKhoaHocAsync(LoaiKhoaHoc kh, int khID);
        Task<ErrorMessage> XoaLoaiKhoaHocAsync(int khID);
    }
}
