using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class TinhTrangHoc
    {
        public int TinhTrangHocID { get; set; }
        [MaxLength(40)]
        public string? TenTinhTrang { get; set; }
        [JsonIgnore]
        public IEnumerable<DangKyHoc>? DangKyHoc { get; set; }
    }
}
