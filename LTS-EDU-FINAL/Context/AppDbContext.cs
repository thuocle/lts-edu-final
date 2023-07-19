using LTS_EDU_FINAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LTS_EDU_FINAL.Context
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<QuyenHan> QuyenHan { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<TinhTrangHoc> TinhTrangHoc { get; set; }
        public virtual DbSet<LoaiBaiViet> LoaiBaiViet { get; set; }
        public virtual DbSet<ChuDe> ChuDe { get; set; }
        public virtual DbSet<BaiViet> BaiViet { get; set; }
        public virtual DbSet<LoaiKhoaHoc> LoaiKhoaHoc { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHoc { get; set; }
        public virtual DbSet<HocVien> HocVien { get; set; }
        public virtual DbSet<DangKyHoc> DangKyHoc { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = THUOCLE\\THUOCLE; Database = QLKhoaHoc; Trusted_Connection = True;TrustServerCertificate=True");
        }
    }
}
