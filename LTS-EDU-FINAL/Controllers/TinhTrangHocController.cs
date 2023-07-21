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
    public class TinhTrangHocController : ControllerBase
    {
        private readonly ITinhTrangHoc _TinhTrangHocServices;
        public TinhTrangHocController()
        {
            _TinhTrangHocServices = new TinhTrangServices();
        }
        [HttpPost("themTinhTrangHoc")]
        public async Task<IActionResult> ThemTinhTrangHoc([FromBody] TinhTrangHoc tt)
        {
            if (!ModelState.IsValid)
                return BadRequest("DuLieuNhapVaoKhongDu");
            var ret = await _TinhTrangHocServices.ThemTinhTrangHocAsync(tt);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            if (ret == ErrorMessage.DaTonTai)
                return BadRequest("Ten tinh trang đã tồn tại");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaTinhTrangHoc")]
        public async Task<IActionResult> SuaTinhTrangHoc([FromBody] TinhTrangHoc tt, [FromQuery] int ttID)
        {
            var ret = await _TinhTrangHocServices.SuaTinhTrangHocAsync(tt, ttID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Ten tinh trang khong tồn tại");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaTinhTrangHoc")]
        public async Task<IActionResult> xoaTinhTrangHoc([FromQuery] int ttID)
        {
            var ret = await _TinhTrangHocServices.XoaTinhTrangHocAsync( ttID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            if (ret == ErrorMessage.KhongTonTai)
                return BadRequest("Ten tinh trang khong tồn tại");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienThiTinhTrangHoc")]
        public async Task<IActionResult> HienThiTinhTrangHoc([FromQuery] Pagination page)
        {
            return Ok(await _TinhTrangHocServices.HienThiTinhTrangHocAsync(page));
        }
    }
}
