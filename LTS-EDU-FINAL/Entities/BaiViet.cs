using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class BaiViet
    {
        public int BaiVietID { get; set; }
        [MaxLength(50)]
        public string? TenBaiViet { get; set; }
        [MaxLength(50)]
        public string? TenTacGia { get; set; }
        public string? NoiDung { get; set; }
        [MaxLength(1000)]
        public string? NoiDungNgan { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public string? HinhAnh { get; set; }
        public int ChuDeID { get; set; }
        [JsonIgnore]
        public ChuDe? ChuDe { get; set; }
        public int? TaiKhoanID { get; set; }
        [JsonIgnore]
        public TaiKhoan? TaiKhoan { get; set; }
    }
}
