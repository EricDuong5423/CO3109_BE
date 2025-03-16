using CO3109_BE.Entities.Component.Axis;
using CO3109_BE.Repository.Component.Axis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.Axis
{
    [Route("api/[controller]")]
    [Tags("vòng đàn hồi")]
    [ApiController]
    public class vong_dan_hoiController : ControllerBase
    {
        private readonly Ivong_dan_hoiRepository _vong_dan_hoiRepository;
        public vong_dan_hoiController(Ivong_dan_hoiRepository vong_dan_hoiRepository)
        {
            _vong_dan_hoiRepository = vong_dan_hoiRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _vong_dan_hoiRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var vong_dan_hoi = await _vong_dan_hoiRepository.GetByIdAsync(id);
            if (vong_dan_hoi == null)
            {
                return NotFound();
            }
            return Ok(vong_dan_hoi);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] vong_dan_hoi vong_dan_hoi)
        {
            await _vong_dan_hoiRepository.CreateAsync(vong_dan_hoi);
            return Ok(vong_dan_hoi);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] vong_dan_hoi vong_dan_hoi)
        {
            await _vong_dan_hoiRepository.UpdateAsync(id, vong_dan_hoi);
            return Ok(vong_dan_hoi);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _vong_dan_hoiRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
