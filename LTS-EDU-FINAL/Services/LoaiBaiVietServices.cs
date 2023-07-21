using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Services
{
    public class LoaiBaiVietServices : ILoaiBaiViet
    {
        private readonly AppDbContext dbContext;

        public LoaiBaiVietServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<LoaiBaiViet> GetLoaiBaiViet(int loaiID)
        {
            return await dbContext.LoaiBaiViet.FirstOrDefaultAsync(x => x.LoaiBaiVietID == loaiID);
        }
        private async Task<bool> TenLoaiBaiVietExistenceAsync(string tenloai)
        {
            return await dbContext.LoaiBaiViet.AnyAsync(x => x.TenLoaiBaiViet == tenloai);
        }
        #endregion
        public async Task<PageInfo<LoaiBaiViet>> HienThiLoaiBaiVietAsync(Pagination page)
        {
            var lst = dbContext.LoaiBaiViet.AsQueryable();
            var data = PageInfo<LoaiBaiViet>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<LoaiBaiViet>(page, data);
        }

        public async Task<ErrorMessage> SuaLoaiBaiVietAsync(LoaiBaiViet loai, int loaiID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra  de sua
                    var loaiNow = await GetLoaiBaiViet(loaiID);
                    if (loaiNow == null)
                        return ErrorMessage.KhongTonTai;
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<LoaiBaiViet, LoaiBaiViet>()
                         .ForMember(dest => dest.LoaiBaiVietID, opt => opt.Ignore());
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ loai vào Now
                    mapper.Map(loai, loaiNow);
                    dbContext.Update(loaiNow);
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
        public async Task<ErrorMessage> ThemLoaiBaiVietAsync(LoaiBaiViet loai)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await TenLoaiBaiVietExistenceAsync(loai.TenLoaiBaiViet))
                        return ErrorMessage.DaTonTai;
                    await dbContext.AddAsync(loai);
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

        public async Task<ErrorMessage> XoaLoaiBaiVietAsync(int loaiID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra  de xoa
                    var loaiNow = await GetLoaiBaiViet(loaiID);
                    if (loaiNow == null)
                        return ErrorMessage.KhongTonTai;

                    dbContext.Remove(loaiNow);
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
