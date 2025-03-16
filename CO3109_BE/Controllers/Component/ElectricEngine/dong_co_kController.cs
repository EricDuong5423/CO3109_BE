using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Repository.Component.ElectricEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.ElectricEngine
{
    [Route("api/[controller]")]
    [Tags("động cơ k")]
    [ApiController]
    public class dong_co_kController : ControllerBase
    {
        private readonly Idong_co_kRepository _dong_co_kRepository;
        public dong_co_kController(Idong_co_kRepository dong_co_kRepository)
        {
            _dong_co_kRepository = dong_co_kRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dong_co_kRepository.GetAllByTypeAsync("k"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var dong_co_k = await _dong_co_kRepository.GetByIdTypeAsync(id, "k");
            if (dong_co_k == null)
            {
                return NotFound();
            }
            return Ok(dong_co_k);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]dong_co_k dong_co_k)
        {
            await _dong_co_kRepository.CreateAsync(dong_co_k);
            return Ok(dong_co_k);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] dong_co_k dong_co_k)
        {
            await _dong_co_kRepository.UpdateAsync(id, dong_co_k);
            return Ok(dong_co_k);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _dong_co_kRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
