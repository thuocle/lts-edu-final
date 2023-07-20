using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class KhoaHoc
    {
        public int KhoaHocID { get; set; }
        [Required]
        [MaxLength(50)]
        public string TenKhoaHoc { get; set; }
        [Required]
        public int ThoiGianHoc { get; set; }
        [Required]
        public string GioiThieu { get; set; }
        [Required]
        public string NoiDung { get; set; }
        [Required]
        public double HocPhi { get; set; }
        public int? SoHocVien { get; set; }
        [Required]
        public int SoLuongMon { get; set; }
        [Required]
        public string HinhAnh { get; set; }
        [Required]
        public int LoaiKhoaHocID { get; set; }
        [JsonIgnore]
        public LoaiKhoaHoc? LoaiKhoaHoc { get; set; }
        [JsonIgnore]
        public IEnumerable<DangKyHoc>? DangKyHoc { get; set; }
    }
}
