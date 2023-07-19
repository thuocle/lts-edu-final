using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;

namespace LTS_EDU_FINAL.IServices
{
    public interface IQuyenHan
    {
        Task<ErrorMessage> ThemQuyenHanAsync(QuyenHan qh);
        Task<ErrorMessage> SuaQuyenHanAsync(QuyenHan qh,int qhID);
        Task<ErrorMessage> XoaQuyenHanAsync(int qhID);
        Task<PageInfo<QuyenHan>> HienThiQuyenHanAsync(Pagination page);
    }
}
