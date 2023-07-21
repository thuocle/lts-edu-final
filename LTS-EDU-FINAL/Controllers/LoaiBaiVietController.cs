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
    public class LoaiBaiVietController : ControllerBase
    {
        private readonly ILoaiBaiViet _LoaiBaiVietServices;
        public LoaiBaiVietController()
        {
            _LoaiBaiVietServices = new LoaiBaiVietServices();
        }
        [HttpPost("themLoaiBaiViet")]
        public async Task<IActionResult> ThemLoaiBaiViet([FromBody] LoaiBaiViet loai)
        {
            if (!ModelState.IsValid)
                return BadRequest("DuLieuNhapVaoKhongDu");
            var ret = await _LoaiBaiVietServices.ThemLoaiBaiVietAsync(loai);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            if (ret == ErrorMessage.DaTonTai)
                return BadRequest("Loai bai viet đã tồn tại");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaLoaiBaiViet")]
        public async Task<IActionResult> SuaLoaiBaiViet([FromBody] LoaiBaiViet loai, [FromQuery] int loaiID)
        {
            var ret = await _LoaiBaiVietServices.SuaLoaiBaiVietAsync(loai, loaiID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Loai bai viet khong tồn tại");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaLoaiBaiViet")]
        public async Task<IActionResult> xoaLoaiBaiViet([FromQuery] int loaiID)
        {
            var ret = await _LoaiBaiVietServices.XoaLoaiBaiVietAsync(loaiID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Loai bai viet khong tồn tại");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiLoaiBaiViet")]
        public async Task<IActionResult> HienThiLoaiBaiViet([FromQuery] Pagination page)
        {
            return Ok(await _LoaiBaiVietServices.HienThiLoaiBaiVietAsync(page));
        }
    }
}
