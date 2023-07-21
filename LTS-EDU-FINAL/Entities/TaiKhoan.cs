using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace LTS_EDU_FINAL.Entities
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? TenNguoiDung { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? TenTaiKhoan { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? MatKhau { get; set; }
        public int? QuyenHanID { get; set; }
        [JsonIgnore]
        public QuyenHan? QuyenHan { get; set; }
        [JsonIgnore]
        public IEnumerable<DangKyHoc>? DangKyHoc { get; set; }
        [JsonIgnore]
        public IEnumerable<BaiViet>? BaiViet { get; set; }
        public bool IsValidPassword()
        {
            // Mật khẩu phải có ít nhất 8 ký tự
            // Phải chứa ít nhất một chữ số và một ký tự đặc biệt
            // Không được chứa khoảng trắng
            string passwordPattern = @"^(?=.*[0-9])(?=.*[!@#\$%\^&\*\(\)_\-\+=\[\]\{\}\\\|:;'"",<>\?])[a-zA-Z0-9!@#\$%\^&\*\(\)_\-\+=\[\]\{\}\\\|:;'"",<>\?]{8,}$";
            return Regex.IsMatch(MatKhau, passwordPattern);
        }
    }
}
