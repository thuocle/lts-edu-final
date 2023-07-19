using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class ChuDe
    {
        public int ChuDeID { get; set; }
        [MaxLength(50)]
        public string? TenChuDe { get; set; }
        public string? NoiDung { get; set; }
        public int? LoaiBaiVietID { get; set; }
        public LoaiBaiViet? LoaiBaiViet { get; set; }

    }
}
