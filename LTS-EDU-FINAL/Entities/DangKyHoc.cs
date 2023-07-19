namespace LTS_EDU_FINAL.Entities
{
    public class DangKyHoc
    {
        public int DangKyHocID { get; set; }
        public int? KhoaHocID { get; set; }
        public KhoaHoc? KhoaHoc { get; set; }
        public int? HocVienID { get; set; }
        public HocVien? HocVien { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TinhTrangHocID { get; set; }
        public TinhTrangHoc? TinhTrangHoc { get; set; }
        public int? TaiKhoanID { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }  
    }
}
