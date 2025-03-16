using CO3109_BE.Entities.Component.Axis;
using CO3109_BE.Repository.Component.Axis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.Axis
{
    [Route("api/[controller]")]
    [Tags("trục đàn hồi")]
    [ApiController]
    public class truc_vong_dan_hoiController : ControllerBase
    {
        private readonly Itruc_vong_dan_hoiRepository _truc_vong_dan_hoiRepository;
        public truc_vong_dan_hoiController(Itruc_vong_dan_hoiRepository truc_vong_dan_hoiRepository)
        {
            _truc_vong_dan_hoiRepository = truc_vong_dan_hoiRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _truc_vong_dan_hoiRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var truc_vong_dan_hoi = await _truc_vong_dan_hoiRepository.GetByIdAsync(id);
            if (truc_vong_dan_hoi == null)
            {
                return NotFound();
            }
            return Ok(truc_vong_dan_hoi);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] truc_vong_dan_hoi truc_vong_dan_hoi)
        {
            await _truc_vong_dan_hoiRepository.CreateAsync(truc_vong_dan_hoi);
            return Ok(truc_vong_dan_hoi);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] truc_vong_dan_hoi truc_vong_dan_hoi)
        {
            await _truc_vong_dan_hoiRepository.UpdateAsync(id, truc_vong_dan_hoi);
            return Ok(truc_vong_dan_hoi);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _truc_vong_dan_hoiRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
