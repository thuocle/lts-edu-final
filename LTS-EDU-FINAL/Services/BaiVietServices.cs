using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace LTS_EDU_FINAL.Services
{
    public class BaiVietServices : IBaiViet
    {
        private readonly AppDbContext dbContext;

        public BaiVietServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<BaiViet> GetBaiViet(int bvID)
        {
            return await dbContext.BaiViet.FirstOrDefaultAsync(x => x.BaiVietID == bvID);
        }
        private async Task<bool> TenBaiVietExistenceAsync(string tenBV, string tenTG)
        {
            return await dbContext.BaiViet.AnyAsync(x => x.TenBaiViet == tenBV && x.TenTacGia == tenTG);
        }
        #endregion
        public async Task<PageInfo<BaiViet>> HienThiBaiVietAsync(Pagination page)
        {
            var lst = dbContext.BaiViet.AsQueryable();
            var data = PageInfo<BaiViet>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<BaiViet>(page, data);
        }

        public async Task<ErrorMessage> SuaBaiVietAsync(BaiViet bv, int bvID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra bv de sua
                    var bvNow = await GetBaiViet(bvID);
                    if (bvNow == null)
                        return ErrorMessage.KhongTonTai;
                    
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<BaiViet, BaiViet>()
                         .ForMember(dest => dest.BaiVietID, opt => opt.Ignore());
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ bv vào bvNow
                    mapper.Map(bv, bvNow);
                    dbContext.Update(bvNow);
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

        public async Task<ErrorMessage> ThemBaiVietAsync(BaiViet bv)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await TenBaiVietExistenceAsync(bv.TenBaiViet, bv.TenTacGia))
                        return ErrorMessage.DaTonTai;

                    await dbContext.AddAsync(bv);
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

        public async Task<PageInfo<BaiViet>> TimKiemBaiVietAsync(Pagination page, string? tenBV)
        {
            var lst = dbContext.BaiViet.AsQueryable();
            if (!tenBV.IsNullOrEmpty())
                lst = lst.Where(x => x.TenBaiViet.Contains(tenBV));
            var data = PageInfo<BaiViet>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<BaiViet>(page, data);
        }

        public async Task<ErrorMessage> XoaBaiVietAsync(int bvID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra bv de xoa
                    var bvNow = await GetBaiViet(bvID);
                    if (bvNow == null)
                        return ErrorMessage.KhongTonTai;

                    dbContext.Remove(bvNow);
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
