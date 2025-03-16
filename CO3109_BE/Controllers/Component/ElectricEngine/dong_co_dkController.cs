using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Repository.Component.ElectricEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.ElectricEngine
{
    [Route("api/[controller]")]
    [Tags("động cơ dk")]
    [ApiController]
    public class dong_co_dkController : ControllerBase
    {
        private readonly Idong_co_dkRepository _dong_co_dkRepository;
        public dong_co_dkController(Idong_co_dkRepository dong_co_dkRepository) 
        {
            _dong_co_dkRepository = dong_co_dkRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dong_co_dkRepository.GetAllByTypeAsync("dk"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var dong_co_dk = await _dong_co_dkRepository.GetByIdTypeAsync(id, "dk");
            if (dong_co_dk == null)
            {
                return NotFound();
            }
            return Ok(dong_co_dk);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] dong_co_dk dong_co_dk)
        {
            await _dong_co_dkRepository.CreateAsync(dong_co_dk);
            return Ok(dong_co_dk);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] dong_co_dk dong_co_dk)
        {
            await _dong_co_dkRepository.UpdateAsync(id, dong_co_dk);
            return Ok(dong_co_dk);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _dong_co_dkRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
