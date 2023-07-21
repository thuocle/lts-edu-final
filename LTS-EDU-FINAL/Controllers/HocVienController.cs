using LTS_EDU_FINAL.Constant;
using LTS_EDU_FINAL.Entities;
using LTS_EDU_FINAL.IServices;
using LTS_EDU_FINAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace LTS_EDU_FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly IHocVien _HocVienServices;
        public HocVienController()
        {
            _HocVienServices = new HocVienServices();
        }
        [HttpPost("themHocVien")]
        public async Task<IActionResult> ThemHocVien([FromBody] HocVien kh)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessage.DuLieuNhapVaoKhongDu);
            var ret = await _HocVienServices.ThemHocVienAsync(kh);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            if (ret == ErrorMessage.TenOrEmailDaTonTai)
                return BadRequest("Họ tên hoặc Email đã tồn tại");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaHocVien")]
        public async Task<IActionResult> SuaHocVien([FromBody] HocVien kh, [FromQuery] int hvID)
        {
            var ret = await _HocVienServices.SuaHocVienAsync(kh, hvID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            if (ret == ErrorMessage.TenOrEmailDaTonTai)
                return BadRequest("Họ tên hoặc Email đã tồn tại");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaHocVien")]
        public async Task<IActionResult> XoaHocVien([FromQuery] int hvID)
        {
            var ret = await _HocVienServices.XoaHocVienAsync(hvID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiHocVien")]
        public async Task<IActionResult> HienThiHocVien([FromQuery] Pagination page)
        {
            return Ok(await _HocVienServices.HienThiHocVienAsync(page));
        }
        [HttpGet("timKiemtHocVien")]
        public async Task<IActionResult> TimKiemHocVien([FromQuery] Pagination page, [FromQuery] string? ten, [FromQuery] string? email )
        {
            return Ok(await _HocVienServices.TimKiemHocVienAsync(page, ten, email));
        }
    }
}
