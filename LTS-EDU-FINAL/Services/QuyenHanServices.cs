using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Services
{
    public class QuyenHanServices : IQuyenHan
    {
        private readonly AppDbContext dbContext;

        public QuyenHanServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<QuyenHan> GetQuyenHan(int qhID)
        {
            return await dbContext.QuyenHan.FirstOrDefaultAsync(x => x.QuyenHanID == qhID);
        }
        private async Task<bool> CheckPermissionExistenceAsync(string tenQH)
        {
            return await dbContext.QuyenHan.AnyAsync(x => x.TenQuyenHan == tenQH);
        }
        #endregion
        public async Task<ErrorMessage> ThemQuyenHanAsync(QuyenHan qh)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await CheckPermissionExistenceAsync(qh.TenQuyenHan))
                        return ErrorMessage.DaTonTai;
                    await dbContext.AddAsync(qh);
                    await dbContext.SaveChangesAsync();
                    // Commit transaction
                    await trans.CommitAsync();
                    return ErrorMessage.ThanhCong;
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<ErrorMessage> SuaQuyenHanAsync(QuyenHan qh, int qhID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var qhNow = await GetQuyenHan(qhID);
                    if (qhNow == null)
                        return ErrorMessage.KhongTonTai;
                    qhNow.TenQuyenHan = qh.TenQuyenHan;
                    dbContext.Update(qhNow);
                    await dbContext.SaveChangesAsync();
                    // Commit transaction
                    await trans.CommitAsync();
                    return ErrorMessage.ThanhCong;
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<ErrorMessage> XoaQuyenHanAsync(int qhID)
        {

            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var qh = await GetQuyenHan(qhID);
                    if (qh == null)
                        return ErrorMessage.KhongTonTai;
                    dbContext.Remove(qh);
                    await dbContext.SaveChangesAsync();
                    // Commit transaction
                    await trans.CommitAsync();
                    return ErrorMessage.ThanhCong;
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction và ném ra ngoại lệ
                    await trans.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<PageInfo<QuyenHan>> HienThiQuyenHanAsync(Pagination page)
        {
            var lst = dbContext.QuyenHan.AsQueryable();
            var data =PageInfo<QuyenHan>.ToPageInfo(page, lst);
            page.TotalItem =await lst.CountAsync();
            return new PageInfo<QuyenHan>(page, data);
        }
    }
}
