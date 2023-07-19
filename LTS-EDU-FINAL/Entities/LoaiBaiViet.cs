using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class LoaiBaiViet
    {
        public int LoaiBaiVietID { get; set; }
        [MaxLength(50)]
        public string? TenLoaiBaiViet { get; set; }
    }
}
