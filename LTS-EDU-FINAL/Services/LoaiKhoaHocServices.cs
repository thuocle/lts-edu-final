using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Services
{
    public class LoaiKhoaHocServices : ILoaiKhoaHoc
    {
        private readonly AppDbContext dbContext;

        public LoaiKhoaHocServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<LoaiKhoaHoc> GetLoaiKhoaHoc(int khID)
        {
            return await dbContext.LoaiKhoaHoc.FirstOrDefaultAsync(x => x.LoaiKhoaHocID == khID);
        }
        private async Task<bool> CheckLoaiKhoaHocExistenceAsync(string tenKH)
        {
            return await dbContext.LoaiKhoaHoc.AnyAsync(x => x.TenLoaiKhoaHoc == tenKH);
        }
        #endregion
        public async Task<ErrorMessage> SuaLoaiKhoaHocAsync(LoaiKhoaHoc kh, int khID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var khNow = await GetLoaiKhoaHoc(khID);
                    if (khNow == null)
                        return ErrorMessage.KhongTonTai;
                    khNow.TenLoaiKhoaHoc = kh.TenLoaiKhoaHoc;
                    dbContext.Update(khNow);
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

        public async Task<ErrorMessage> ThemLoaiKhoaHocAsync(LoaiKhoaHoc kh)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await CheckLoaiKhoaHocExistenceAsync(kh.TenLoaiKhoaHoc))
                        return ErrorMessage.DaTonTai;
                    await dbContext.AddAsync(kh);
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
        public async Task<ErrorMessage> XoaLoaiKhoaHocAsync(int khID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var kh = await GetLoaiKhoaHoc(khID);
                    if (kh == null)
                        return ErrorMessage.KhongTonTai;
                    dbContext.Remove(kh);
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
    }
}
