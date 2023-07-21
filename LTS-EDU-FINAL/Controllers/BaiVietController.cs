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
    public class BaiVietController : ControllerBase
    {
        private readonly IBaiViet _BaiVietServices;
        public BaiVietController()
        {
            _BaiVietServices = new BaiVietServices();
        }
        [HttpPost("themBaiViet")]
        public async Task<IActionResult> ThemBaiViet([FromBody] BaiViet bv)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessage.DuLieuNhapVaoKhongDu);
            var ret = await _BaiVietServices.ThemBaiVietAsync(bv);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            if (ret == ErrorMessage.DaTonTai)
                return BadRequest("Bai viet đã tồn tại");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaBaiViet")]
        public async Task<IActionResult> SuaBaiViet([FromBody] BaiViet bv, [FromQuery] int bvID)
        {
            var ret = await _BaiVietServices.SuaBaiVietAsync(bv, bvID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Bai viet khong tồn tại");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaBaiViet")]
        public async Task<IActionResult> XoaBaiViet([FromQuery] int bvID)
        {
            var ret = await _BaiVietServices.XoaBaiVietAsync(bvID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Bai viet khong tồn tại");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiBaiViet")]
        public async Task<IActionResult> HienThiBaiViet([FromQuery] Pagination page)
        {
            return Ok(await _BaiVietServices.HienThiBaiVietAsync(page));
        }
        [HttpGet("timKiemBaiViet")]
        public async Task<IActionResult> TimKiemBaiViet([FromQuery] Pagination page, [FromQuery] string? tenBV)
        {
            return Ok(await _BaiVietServices.TimKiemBaiVietAsync(page, tenBV));
        }
    }
}
