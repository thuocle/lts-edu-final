using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Services
{
    public class TinhTrangServices : ITinhTrangHoc
    {
        private readonly AppDbContext dbContext;

        public TinhTrangServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<TinhTrangHoc> GetTinhTrangHoc(int ttID)
        {
            return await dbContext.TinhTrangHoc.FirstOrDefaultAsync(x => x.TinhTrangHocID == ttID);
        }
        private async Task<bool> TenTinhTrangHocExistenceAsync(string tenTT)
        {
            return await dbContext.TinhTrangHoc.AnyAsync(x => x.TenTinhTrang == tenTT);
        }
        #endregion
        public async Task<ErrorMessage> SuaTinhTrangHocAsync(TinhTrangHoc tt, int ttID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra  de sua
                    var ttNow = await GetTinhTrangHoc(ttID);
                    if (ttNow == null)
                        return ErrorMessage.KhongTonTai;
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<TinhTrangHoc, TinhTrangHoc>()
                         .ForMember(dest => dest.TinhTrangHocID, opt => opt.Ignore());
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ tt vào ttNow
                    mapper.Map(tt, ttNow);
                    dbContext.Update(ttNow);
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

        public async Task<ErrorMessage> ThemTinhTrangHocAsync(TinhTrangHoc tt)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await TenTinhTrangHocExistenceAsync(tt.TenTinhTrang))
                        return ErrorMessage.TenTaiKhoanDaTonTai;
                    await dbContext.AddAsync(tt);
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

        public async Task<ErrorMessage> XoaTinhTrangHocAsync(int ttID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var ttNow = await GetTinhTrangHoc(ttID);
                    if (ttNow == null)
                        return ErrorMessage.KhongTonTai;
                    dbContext.Remove(ttNow);
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

        public async Task<PageInfo<TinhTrangHoc>> HienThiTinhTrangHocAsync(Pagination page)
        {
            var lst = dbContext.TinhTrangHoc.AsQueryable();
            var data = PageInfo<TinhTrangHoc>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<TinhTrangHoc>(page, data);
        }
    }
}
