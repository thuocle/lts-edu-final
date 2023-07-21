using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LTS_EDU_FINAL.Services
{
    public class TaiKhoanServices : ITaiKhoan
    {
        private readonly AppDbContext dbContext;

        public TaiKhoanServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<TaiKhoan> GetTaiKhoan(int tkID)
        {
            return await dbContext.TaiKhoan.FirstOrDefaultAsync(x => x.TaiKhoanID == tkID);
        }
        private async Task<bool> TenTaiKhoanExistenceAsync(string tenTK)
        {
            return await dbContext.TaiKhoan.AnyAsync(x => x.TenTaiKhoan == tenTK);
        }
        #endregion
        public async Task<PageInfo<TaiKhoan>> HienThiTaiKhoanAsync(Pagination page)
        {
            var lst = dbContext.TaiKhoan.AsQueryable();
            var data = PageInfo<TaiKhoan>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<TaiKhoan>(page, data);
        }

        public async Task<ErrorMessage> SuaTaiKhoanAsync(TaiKhoan tk, int tkID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //tim ra tai khoan de sua
                    var tkNow = await GetTaiKhoan(tkID);
                    if (tkNow == null)
                        return ErrorMessage.KhongTonTai;
                    //kiem tra password va tentaikhoan
                    if (await TenTaiKhoanExistenceAsync(tk.TenTaiKhoan))
                        return ErrorMessage.TenTaiKhoanDaTonTai;
                    if (!tk.IsValidPassword())
                        return ErrorMessage.MatKhauKhongDungYeuCau;

                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<TaiKhoan, TaiKhoan>()
                         .ForMember(dest => dest.TaiKhoanID, opt => opt.Ignore());
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ tk vào tkNow
                    mapper.Map(tk, tkNow);
                    dbContext.Update(tkNow);
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

        public async Task<ErrorMessage> ThemTaiKhoanAsync(TaiKhoan tk)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await TenTaiKhoanExistenceAsync(tk.TenTaiKhoan))
                        return ErrorMessage.TenTaiKhoanDaTonTai;
                    if (!tk.IsValidPassword())
                        return ErrorMessage.MatKhauKhongDungYeuCau;
                    await dbContext.AddAsync(tk);
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

        public async Task<PageInfo<TaiKhoan>> TimKiemTaiKhoanAsync(Pagination page, string? tenTK)
        {
            var lst = dbContext.TaiKhoan.AsQueryable();
            if (!tenTK.IsNullOrEmpty())
                lst = lst.Where(x => x.TenTaiKhoan.ToLower().Contains(tenTK.ToLower()));
            var data = PageInfo<TaiKhoan>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<TaiKhoan>(page, data);
        }
        public async Task<ErrorMessage> XoaTaiKhoanAsync(int tkID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var tk = await GetTaiKhoan(tkID);
                    if (tk == null)
                        return ErrorMessage.KhongTonTai;
                    dbContext.Remove(tk);
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
