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
    public class ChuDeController : ControllerBase
    {
        private readonly IChuDe _ChuDeServices;
        public ChuDeController()
        {
            _ChuDeServices = new ChuDeServices();
        }
        [HttpPost("themChuDe")]
        public async Task<IActionResult> ThemChuDe([FromBody] ChuDe cd)
        {
            if (!ModelState.IsValid)
                return BadRequest("DuLieuNhapVaoKhongDu");
            var ret = await _ChuDeServices.ThemChuDeAsync(cd);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            if (ret == ErrorMessage.DaTonTai)
                return BadRequest("Chu de đã tồn tại");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaChuDe")]
        public async Task<IActionResult> SuaChuDe([FromBody] ChuDe cd, [FromQuery] int cdID)
        {
            var ret = await _ChuDeServices.SuaChuDeAsync(cd, cdID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Chu de khong tồn tại");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaChuDe")]
        public async Task<IActionResult> XoaChuDe([FromQuery] int cdID)
        {
            var ret = await _ChuDeServices.XoaChuDeAsync(cdID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Chu de khong tồn tại");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiChuDe")]
        public async Task<IActionResult> HienThiChuDe([FromQuery] Pagination page)
        {
            return Ok(await _ChuDeServices.HienThiChuDeAsync(page));
        }
    }
}
