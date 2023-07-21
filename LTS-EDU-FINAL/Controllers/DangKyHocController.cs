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
    public class DangKyHocController : ControllerBase
    {
        private readonly IDangKyHoc _DangKyHocServices;
        public DangKyHocController()
        {
            _DangKyHocServices = new DangKyHocServices();
        }
        [HttpPost("themDangKyHoc")]
        public async Task<IActionResult> ThemDangKyHoc([FromBody] DangKyHoc dk)
        {
            if (!ModelState.IsValid)
                return BadRequest("Du lieu nhap khong du");
            var ret = await _DangKyHocServices.ThemDangKyHocAsync(dk);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaDangKyHoc")]
        public async Task<IActionResult> SuaDangKyHoc([FromBody] DangKyHoc dk, [FromQuery] int dkID)
        {
            var ret = await _DangKyHocServices.SuaDangKyHocAsync(dk, dkID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaDangKyHoc")]
        public async Task<IActionResult> XoaDangKyHoc([FromQuery] int dkID)
        {
            var ret = await _DangKyHocServices.XoaDangKyHocAsync(dkID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("XOa thanh cong");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiDangKyHoc")]
        public async Task<IActionResult> HienThiDangKyHoc([FromQuery] Pagination page)
        {
            return Ok(await _DangKyHocServices.HienThiDangKyHocAsync(page));
        }
    }
}
