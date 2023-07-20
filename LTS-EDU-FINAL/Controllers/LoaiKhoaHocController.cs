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
    public class LoaiKhoaHocController : ControllerBase
    {
        private readonly ILoaiKhoaHoc _loaiKhoaHocServices;
        public LoaiKhoaHocController()
        {
            _loaiKhoaHocServices = new LoaiKhoaHocServices();
        }
        [HttpPost("themLoaiKhoaHoc")]
        public async Task<IActionResult> ThemLoaiKhoaHoc([FromBody] LoaiKhoaHoc kh)
        {
            var ret = await _loaiKhoaHocServices.ThemLoaiKhoaHocAsync(kh);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaLoaiKhoaHoc")]
        public async Task<IActionResult> SuaLoaiKhoaHoc([FromBody] LoaiKhoaHoc kh, [FromQuery] int khID)
        {
            var ret = await _loaiKhoaHocServices.SuaLoaiKhoaHocAsync(kh, khID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaLoaiKhoaHoc")]
        public async Task<IActionResult> XoaLoaiKhoaHoc([FromQuery] int khID)
        {
            var ret = await _loaiKhoaHocServices.XoaLoaiKhoaHocAsync(khID);
            if (ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            return BadRequest("Xoa That bai");
        }
    }
}
