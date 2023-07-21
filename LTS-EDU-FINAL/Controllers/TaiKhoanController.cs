using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using LTS_EDU_FINAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTS_EDU_FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoan _TaiKhoanServices;
        public TaiKhoanController()
        {
            _TaiKhoanServices = new TaiKhoanServices();
        }
        [HttpPost("themTaiKhoan")]
        public async Task<IActionResult> ThemTaiKhoan([FromBody] TaiKhoan tk)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessage.DuLieuNhapVaoKhongDu);
            var ret = await _TaiKhoanServices.ThemTaiKhoanAsync(tk);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            if (ret == ErrorMessage.TenTaiKhoanDaTonTai)
                return BadRequest("Tên tài khoản đã tồn tại");
            if (ret == ErrorMessage.MatKhauKhongDungYeuCau) 
                return BadRequest("Mật khẩu phải 8 ky tu, có cả chữ, số và ký tự!");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaTaiKhoan")]
        public async Task<IActionResult> SuaTaiKhoan([FromBody] TaiKhoan tk, [FromQuery] int tkID)
        {
            var ret = await _TaiKhoanServices.SuaTaiKhoanAsync(tk, tkID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            if (ret == ErrorMessage.TenTaiKhoanDaTonTai)
                return BadRequest("Tên tài khoản đã tồn tại");
            if (ret == ErrorMessage.MatKhauKhongDungYeuCau)
                return BadRequest("Mật khẩu phải 8 ky tu, có cả chữ, số và ký tự!");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaTaiKhoan")]
        public async Task<IActionResult> XoaTaiKhoan([FromQuery] int tkID)
        {
            var ret = await _TaiKhoanServices.XoaTaiKhoanAsync(tkID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiTaiKhoan")]
        public async Task<IActionResult> HienThiTaiKhoan([FromQuery] Pagination page)
        {
            return Ok(await _TaiKhoanServices.HienThiTaiKhoanAsync(page));
        }
        [HttpGet("timKiemTaiKhoan")]
        public async Task<IActionResult> TimKiemTaiKhoan([FromQuery] Pagination page, [FromQuery] string? tenTK)
        {
            return Ok(await _TaiKhoanServices.TimKiemTaiKhoanAsync(page, tenTK));
        }
    }
}
