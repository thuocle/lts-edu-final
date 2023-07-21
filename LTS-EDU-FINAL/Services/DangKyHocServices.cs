using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Services
{
    public class DangKyHocServices : IDangKyHoc
    {
        private readonly AppDbContext dbContext;

        public DangKyHocServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<DangKyHoc> GetDangKyHoc(int dkID)
        {
            return await dbContext.DangKyHoc.FirstOrDefaultAsync(x => x.DangKyHocID == dkID);
        }
        //kiem tra khoa hoc, hoc vien, tai khoan, tinh trang da ton tai
        private KhoaHoc GetKhoaHoc(int? khID)
        {
            return dbContext.KhoaHoc.FirstOrDefault(x => x.KhoaHocID == khID);
        }
        #endregion
        public async Task<PageInfo<DangKyHoc>> HienThiDangKyHocAsync(Pagination page)
        {
            var lst = dbContext.DangKyHoc.AsQueryable();
            var data = PageInfo<DangKyHoc>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<DangKyHoc>(page, data);
        }

        public async Task<ErrorMessage> SuaDangKyHocAsync(DangKyHoc dk, int dkID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //lay ra dk der sua
                    var dkNow = await GetDangKyHoc(dkID);
                    if (await GetDangKyHoc(dkID) == null)
                        return ErrorMessage.KhongTonTai;

                    var kh = GetKhoaHoc(dk.KhoaHocID);
                    //set cac ngay
                    if (dkNow.TinhTrangHocID == 1 && dk.TinhTrangHocID == 2)
                    {
                        dkNow.NgayBatDau = DateTime.Now;
                        dkNow.NgayKetThuc = dkNow.NgayBatDau.AddMonths(kh.ThoiGianHoc);
                        dkNow.KhoaHocID = dk.KhoaHocID;
                        dkNow.HocVienID = dk.HocVienID;
                        dkNow.TinhTrangHocID = dk.TinhTrangHocID;
                        kh.SoHocVien += 1;
                        dbContext.Update(kh);
                        dbContext.Update(dkNow);
                        await dbContext.SaveChangesAsync();
                    }//hoc xong
                    if (dkNow.TinhTrangHocID == 2 && dkNow.NgayKetThuc < DateTime.Now)
                    {
                        dkNow.TinhTrangHocID = 3;
                        dbContext.Update(dkNow);
                        await dbContext.SaveChangesAsync();
                    }
                    if (dkNow.TinhTrangHocID != 3 && dk.TinhTrangHocID == 4)
                    {
                        dkNow.TinhTrangHocID = 4;
                        dbContext.Update(dkNow);
                        await dbContext.SaveChangesAsync();
                    }
                    dkNow.KhoaHocID = dk.KhoaHocID;
                    dkNow.HocVienID = dk.HocVienID;
                    dbContext.Update(dkNow);
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

        public async Task<ErrorMessage> ThemDangKyHocAsync(DangKyHoc dk)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var kh = GetKhoaHoc(dk.KhoaHocID);
                    dk.TinhTrangHocID = 1;
                    await dbContext.AddAsync(dk);
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

        public async Task<ErrorMessage> XoaDangKyHocAsync(int dkID)
        {

            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var dk = await GetDangKyHoc(dkID);
                    if (dk == null)
                        return ErrorMessage.KhongTonTai;
                    dbContext.Remove(dk);
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
