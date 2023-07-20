using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LTS_EDU_FINAL.Services
{
    public class KhoaHocServices : IKhoaHoc
    {
        private readonly AppDbContext dbContext;

        public KhoaHocServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<KhoaHoc> GetKhoaHoc(int khID)
        {
            return await dbContext.KhoaHoc.FirstOrDefaultAsync(x => x.KhoaHocID == khID);
        }
        private async Task<bool> CheckKhoaHocExistenceAsync(string tenKH)
        {
            return await dbContext.KhoaHoc.AnyAsync(x => x.TenKhoaHoc == tenKH);
        }
        #endregion
        public async Task<PageInfo<KhoaHoc>> HienThiKhoaHocAsync(Pagination page)
        {
            var lst = dbContext.KhoaHoc.AsQueryable();
            var data = PageInfo<KhoaHoc>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<KhoaHoc>(page, data);
        }

        public async Task<ErrorMessage> SuaKhoaHocAsync(KhoaHoc kh, int khID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var khNow = await GetKhoaHoc(khID);
                    if (khNow == null)
                        return ErrorMessage.KhongTonTai;

                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<KhoaHoc, KhoaHoc>()
                         .ForMember(dest => dest.KhoaHocID, opt => opt.Ignore()); 
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ kh vào khNow
                    mapper.Map(kh, khNow);
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

        public async Task<ErrorMessage> ThemKhoaHocAsync(KhoaHoc kh)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await CheckKhoaHocExistenceAsync(kh.TenKhoaHoc))
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

        public async Task<PageInfo<KhoaHoc>> TimKiemKhoaHocAsync(Pagination page, string tenKH)
        {
            var lst = dbContext.KhoaHoc.AsQueryable();
            if (!string.IsNullOrEmpty(tenKH))
            {
                lst = lst.Where(x=>x.TenKhoaHoc.Contains(tenKH));
            }
            var data = PageInfo<KhoaHoc>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<KhoaHoc>(page, data);
        }

        public async Task<ErrorMessage> XoaKhoaHocAsync(int khID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var kh = await GetKhoaHoc(khID);
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
