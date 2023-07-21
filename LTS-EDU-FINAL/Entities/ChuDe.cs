using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class ChuDe
    {
        public int ChuDeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string? TenChuDe { get; set; }
        [Required]
        public string? NoiDung { get; set; }
        [Required]
        public int? LoaiBaiVietID { get; set; }
        [JsonIgnore]
        public LoaiBaiViet? LoaiBaiViet { get; set; }
        [JsonIgnore]
        public IEnumerable<BaiViet>? BaiViet { get; set; }

    }
}
