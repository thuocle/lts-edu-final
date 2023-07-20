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
    public class KhoaHocController : ControllerBase
    {
        private readonly IKhoaHoc _khoaHocServices;
        public KhoaHocController()
        {
            _khoaHocServices = new KhoaHocServices();
        }
        [HttpPost("themKhoaHoc")]
        public async Task<IActionResult> ThemKhoaHoc([FromBody] KhoaHoc kh)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ret = await _khoaHocServices.ThemKhoaHocAsync(kh);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaKhoaHoc")]
        public async Task<IActionResult> SuaKhoaHoc([FromBody] KhoaHoc kh,[FromQuery] int khID)
        {
            var ret = await _khoaHocServices.SuaKhoaHocAsync(kh, khID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaKhoaHoc")]
        public async Task<IActionResult> XoaKhoaHoc([FromQuery] int khID)
        {
            var ret = await _khoaHocServices.XoaKhoaHocAsync(khID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiKhoaHoc")]
        public async Task<IActionResult> HienThiKhoaHoc([FromQuery] Pagination page) 
        {
            return Ok(await _khoaHocServices.HienThiKhoaHocAsync(page));
        }
        [HttpGet("timKiemtKhoaHoc")]
        public async Task<IActionResult> TimKiemKhoaHoc([FromQuery] Pagination page, [FromQuery] string keyword) 
        {
            return Ok(await _khoaHocServices.TimKiemKhoaHocAsync(page, keyword));
        }
    }
}
