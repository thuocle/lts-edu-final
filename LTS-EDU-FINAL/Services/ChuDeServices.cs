using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Services
{
    public class ChuDeServices : IChuDe
    {
        private readonly AppDbContext dbContext;

        public ChuDeServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<ChuDe> GetChuDe(int cdID)
        {
            return await dbContext.ChuDe.FirstOrDefaultAsync(x => x.ChuDeID == cdID);
        }
        private async Task<bool> TenChuDeExistenceAsync(string tencd)
        {
            return await dbContext.ChuDe.AnyAsync(x => x.TenChuDe == tencd);
        }
        #endregion
        public async Task<PageInfo<ChuDe>> HienThiChuDeAsync(Pagination page)
        {
            var lst = dbContext.ChuDe.AsQueryable();
            var data = PageInfo<ChuDe>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<ChuDe>(page, data);
        }

        public async Task<ErrorMessage> SuaChuDeAsync(ChuDe cd, int cdID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra  de sua
                    var cdNow = await GetChuDe(cdID);
                    if (cdNow == null)
                        return ErrorMessage.KhongTonTai;
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<ChuDe, ChuDe>()
                         .ForMember(dest => dest.ChuDeID, opt => opt.Ignore());
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ cd vào Now
                    mapper.Map(cd, cdNow);
                    dbContext.Update(cdNow);
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

        public async Task<ErrorMessage> ThemChuDeAsync(ChuDe cd)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await TenChuDeExistenceAsync(cd.TenChuDe))
                        return ErrorMessage.DaTonTai;
                    await dbContext.AddAsync(cd);
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

        public async Task<ErrorMessage> XoaChuDeAsync(int cdID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra  de sua
                    var cdNow = await GetChuDe(cdID);
                    if (cdNow == null)
                        return ErrorMessage.KhongTonTai;

                    dbContext.Remove(cdNow);
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
