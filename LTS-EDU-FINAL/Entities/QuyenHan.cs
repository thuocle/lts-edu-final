using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTS_EDU_FINAL.Entities
{
    public class QuyenHan
    {
        public int QuyenHanID { get; set; }
        [MaxLength(50)]
        public string? TenQuyenHan { get; set; }
        [JsonIgnore]
        public IEnumerable<TaiKhoan>? TaiKhoan { get; set; }
    }
}
