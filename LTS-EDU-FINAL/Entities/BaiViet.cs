using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class BaiViet
    {
        public int BaiVietID { get; set; }
        [Required]
        [MaxLength(50)]
        public string? TenBaiViet { get; set; }
        [Required]
        [MaxLength(50)]
        public string? TenTacGia { get; set; }
        [Required]
        public string? NoiDung { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? NoiDungNgan { get; set; }
        public DateTime? ThoiGianTao { get; set; } = DateTime.Now;
        [Required]
        public string? HinhAnh { get; set; }
        [Required]
        public int ChuDeID { get; set; }
        [JsonIgnore]
        public ChuDe? ChuDe { get; set; }
        [Required]
        public int? TaiKhoanID { get; set; }
        [JsonIgnore]
        public TaiKhoan? TaiKhoan { get; set; }
    }
}
