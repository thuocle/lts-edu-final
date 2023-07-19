using Newtonsoft.Json;

namespace LTS_EDU_FINAL.Entities
{
    public class DangKyHoc
    {
        public int DangKyHocID { get; set; }
        public int? KhoaHocID { get; set; }
        [JsonIgnore]
        public KhoaHoc? KhoaHoc { get; set; }
        public int? HocVienID { get; set; }
        [JsonIgnore]
        public HocVien? HocVien { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TinhTrangHocID { get; set; }
        [JsonIgnore]
        public TinhTrangHoc? TinhTrangHoc { get; set; }
        public int? TaiKhoanID { get; set; }
        [JsonIgnore]
        public TaiKhoan? TaiKhoan { get; set; }  
    }
}
