using CO3109_BE.Entities.Component.RollerChain;
using CO3109_BE.Repository.Component.RollerChain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.RollerChain
{
    [Route("api/[controller]")]
    [Tags("xích con lăn")]
    [ApiController]
    public class xich_con_lanController : ControllerBase
    {
        private readonly Ixich_con_lanRepository _xich_con_lanRepository;
        public xich_con_lanController(Ixich_con_lanRepository xich_con_lanRepository)
        {
            _xich_con_lanRepository = xich_con_lanRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _xich_con_lanRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var xich_con_lan = await _xich_con_lanRepository.GetByIdAsync(id);
            if (xich_con_lan == null)
            {
                return NotFound();
            }
            return Ok(xich_con_lan);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] xich_con_lan xich_con_lan)
        {
            await _xich_con_lanRepository.CreateAsync(xich_con_lan);
            return Ok(xich_con_lan);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] xich_con_lan xich_con_lan)
        {
            await _xich_con_lanRepository.UpdateAsync(id, xich_con_lan);
            return Ok(xich_con_lan);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _xich_con_lanRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
