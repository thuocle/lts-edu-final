﻿using AutoMapper;
using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Context;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LTS_EDU_FINAL.Services
{
    public class HocVienServices : IHocVien
    {
        private readonly AppDbContext dbContext;

        public HocVienServices()
        {
            this.dbContext = new AppDbContext();
        }
        #region Private 
        private async Task<HocVien> GetHocVien(int hvID)
        {
            return await dbContext.HocVien.FirstOrDefaultAsync(x => x.HocVienID == hvID);
        }
        private async Task<bool> SDTExistenceBeforeUpdateAsync(string sdt, HocVien? hvNow)
        {
            return await dbContext.HocVien.AnyAsync(x => x.SoDienThoai == sdt && sdt != hvNow.SoDienThoai);
        } 
        private async Task<bool> EmailBeforeUpdateExistenceAsync(string email, HocVien? hvNow)
        {
           return await dbContext.HocVien.AnyAsync(x => x.Email == email && email != hvNow.Email);
        }
        private async Task<bool> SDTExistenceBeforeAddAsync(string sdt)
        {
            return await dbContext.HocVien.AnyAsync(x => x.SoDienThoai == sdt);
        } 
        private async Task<bool> EmailBeforeAddExistenceAsync(string email)
        {
           return await dbContext.HocVien.AnyAsync(x => x.Email == email);
        }
        private async Task<string> FormatName(string name)
        {
            // Loại bỏ các dấu cách không cần thiết ở đầu và cuối chuỗi
            name = name.Trim();
            // Loại bỏ các dấu cách nhiều hơn 1 dấu cách ở giữa các từ
            name = Regex.Replace(name, @"\s+", " ");
            // Chuyển đổi chuỗi thành chữ hoa đầu của mỗi từ (title case)
            TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
            name = textInfo.ToTitleCase(name);
            return name;
        }
        #endregion
        public async Task<PageInfo<HocVien>> HienThiHocVienAsync(Pagination page)
        {
            var lst = dbContext.HocVien.AsQueryable();
            var data = PageInfo<HocVien>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<HocVien>(page, data);
        }

        public async Task<ErrorMessage> SuaHocVienAsync(HocVien hv, int hvID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {   
                    //tim ra hoc vien de sua
                    var hvNow = await GetHocVien(hvID);
                    if (hvNow == null)
                        return ErrorMessage.KhongTonTai;
                    //kiem tra sdt email + format ten
                    if ((await SDTExistenceBeforeUpdateAsync(hv.SoDienThoai, hvNow) || await EmailBeforeUpdateExistenceAsync(hv.Email, hvNow)))
                        return ErrorMessage.SDTOrEmailDaTonTai;

                    if (!hv.IsValidPhoneNumber() || !hv.IsValidEmail())
                        return ErrorMessage.SDTOrEmailKhongDungDinhDang;
                    hv.HoTen = await FormatName(hv.HoTen);

                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<HocVien, HocVien>()
                         .ForMember(dest => dest.HocVienID, opt => opt.Ignore());
                    });
                    var mapper = new Mapper(config);
                    // Ánh xạ thông tin từ kh vào hvNow
                    mapper.Map(hv, hvNow);
                    dbContext.Update(hvNow);
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

        public async Task<ErrorMessage> ThemHocVienAsync(HocVien hv)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await SDTExistenceBeforeAddAsync(hv.SoDienThoai) || await EmailBeforeAddExistenceAsync(hv.Email))
                        return ErrorMessage.SDTOrEmailDaTonTai;
                    if (!hv.IsValidPhoneNumber() || !hv.IsValidEmail())
                        return ErrorMessage.SDTOrEmailKhongDungDinhDang;
                    hv.HoTen = await FormatName(hv.HoTen);
                    await dbContext.AddAsync(hv);
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

        public async Task<PageInfo<HocVien>> TimKiemHocVienAsync(Pagination page, string? tenhv, string? email)
        {
            var lst = dbContext.HocVien.AsQueryable();
            if(!tenhv.IsNullOrEmpty() ||  !email.IsNullOrEmpty()) 
                lst = lst.Where(x => x.HoTen.Contains(tenhv) || x.Email.Contains(email)); 
            if(!tenhv.IsNullOrEmpty() &&  !email.IsNullOrEmpty()) 
                lst = lst.Where(x => x.HoTen.Contains(tenhv) && x.Email.Contains(email));
            var data = PageInfo<HocVien>.ToPageInfo(page, lst);
            page.TotalItem = await lst.CountAsync();
            return new PageInfo<HocVien>(page, data);
        }

        public async Task<ErrorMessage> XoaHocVienAsync(int hvID)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var hv = await GetHocVien(hvID);
                    if (hv == null)
                        return ErrorMessage.KhongTonTai;
                    dbContext.Remove(hv);
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
