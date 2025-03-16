using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Repository.Component.ElectricEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.ElectricEngine
{
    [Route("api/[controller]")]
    [Tags("động cơ điện 4a")]
    [ApiController]
    public class dong_co_4aController : ControllerBase
    {
        private readonly Idong_co_4aRepository _dong_co_4aRepository;
        public dong_co_4aController(Idong_co_4aRepository dong_co_4aRepository)
        {
            _dong_co_4aRepository = dong_co_4aRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dong_co_4aRepository.GetAllByTypeAsync("4a"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var dong_co_4a = await _dong_co_4aRepository.GetByIdTypeAsync(id, "4a");
            if (dong_co_4a == null)
            {
                return NotFound();
            }
            return Ok(dong_co_4a);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] dong_co_4a dong_co_4a)
        {
            await _dong_co_4aRepository.CreateAsync(dong_co_4a);
            return Ok(dong_co_4a);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] dong_co_4a dong_co_4a)
        {
            await _dong_co_4aRepository.UpdateAsync(id, dong_co_4a);
            return Ok(dong_co_4a);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _dong_co_4aRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
