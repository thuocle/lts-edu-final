using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? TenNguoiDung { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? TenTaiKhoan { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? MatKhau { get; set; }
        public int? QuyenHanID { get; set; }
        public QuyenHan? QuyenHan { get; set; }
        public IEnumerable<DangKyHoc>? DangKyHoc { get; set; }
        public IEnumerable<BaiViet>? BaiViet { get; set; }
    }
}
