using CO3109_BE.Entities.Component.BallBearing;
using CO3109_BE.Repository.Component.BallBearing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.BallBearing
{
    [Route("api/[controller]")]
    [Tags("ổ bi")]
    [ApiController]
    public class o_biController : ControllerBase
    {
        private readonly Io_biRepository _o_biRepository;
        public o_biController(Io_biRepository o_biRepository)
        {
            _o_biRepository = o_biRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _o_biRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var o_bi = await _o_biRepository.GetByIdAsync(id);
            if (o_bi == null)
            {
                return NotFound();
            }
            return Ok(o_bi);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] o_bi o_bi)
        {
            await _o_biRepository.CreateAsync(o_bi);
            return Ok(o_bi);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] o_bi o_bi)
        {
            await _o_biRepository.UpdateAsync(id, o_bi);
            return Ok(o_bi);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _o_biRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
