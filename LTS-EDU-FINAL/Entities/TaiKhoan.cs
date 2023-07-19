using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public QuyenHan? QuyenHan { get; set; }
        [JsonIgnore]
        public IEnumerable<DangKyHoc>? DangKyHoc { get; set; }
        [JsonIgnore]
        public IEnumerable<BaiViet>? BaiViet { get; set; }
    }
}
