using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LTS_EDU_FINAL.Entities
{
    public class LoaiKhoaHoc
    {
        public int LoaiKhoaHocID { get; set; }
        [MaxLength(30)]
        public string? TenLoaiKhoaHoc { get; set; }
    }
}
