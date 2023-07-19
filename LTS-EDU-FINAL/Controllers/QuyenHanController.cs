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
    public class QuyenHanController : ControllerBase
    {
        private readonly IQuyenHan _quyenHanServices;
        public QuyenHanController()
        {
            _quyenHanServices = new QuyenHanServices();
        }
        [HttpPost("themQuyenHan")]
        public async Task<IActionResult> ThemQuyenHan([FromBody] QuyenHan qh)
        {
            var ret =await _quyenHanServices.ThemQuyenHanAsync(qh);
            if(ret == ErrorMessage.ThanhCong)
                return Ok("Them thanh cong");
            return BadRequest("Them That bai");
        }
        [HttpPut("suaQuyenHan")]
        public async Task<IActionResult> SuaQuyenHan([FromBody] QuyenHan qh, [FromQuery] int qhID)
        {
            var ret =await _quyenHanServices.SuaQuyenHanAsync(qh, qhID);
            if(ret == ErrorMessage.ThanhCong)
                return Ok("Sua thanh cong");
            return BadRequest("Sua That bai");
        }
        [HttpDelete("xoaQuyenHan")]
        public async Task<IActionResult> XoaQuyenHan( [FromQuery] int qhID)
        {
            var ret =await _quyenHanServices.XoaQuyenHanAsync(qhID);
            if(ret == ErrorMessage.ThanhCong)
                return Ok("Xoa thanh cong");
            return BadRequest("Xoa That bai");
        }
        [HttpGet("hienQuyenHan")]
        public async Task<IActionResult> HienThiQuyenHan([FromQuery] Pagination page)
        {
            var ret = await _quyenHanServices.HienThiQuyenHanAsync(page);
            return Ok(ret);
        }
    }
}
