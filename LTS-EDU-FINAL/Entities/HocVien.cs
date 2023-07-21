using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LTS_EDU_FINAL.Entities
{
    public class HocVien
    {
        public int HocVienID { get; set; }
        public string? HinhAnh { get; set; }
        [MaxLength(50)]
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(11)]
        public string? SoDienThoai { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(40)]
        public string? Email { get; set; }
        [MaxLength (50)]
        public string? TinhThanh { get; set; }
        [MaxLength(50)]
        public string? QuanHuyen { get; set; }
        [MaxLength(50)]
        public string? PhuongXa { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string? SoNha { get; set; }
        [JsonIgnore]
        public IEnumerable<DangKyHoc>? DangKyHoc { get; set; }
        public bool IsValidEmail()
        {
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(Email, emailPattern);
        }

        public bool IsValidPhoneNumber()
        {
            string phoneNumberPattern = @"^0[0-9]{9,10}$";
            return Regex.IsMatch(SoDienThoai, phoneNumberPattern);
        }

        
    }
}
