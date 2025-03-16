using CO3109_BE.Entities.Component.Gear;
using CO3109_BE.Repository.Component.Gear;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Component.Gear
{
    [Route("api/[controller]")]
    [Tags("bánh răng")]
    [ApiController]
    public class banh_rangController : ControllerBase
    {
        private readonly Ibanh_rangRepository _banh_rangRepository;
        public banh_rangController(Ibanh_rangRepository banh_rangRepository)
        {
            _banh_rangRepository = banh_rangRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _banh_rangRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var banh_rang = await _banh_rangRepository.GetByIdAsync(id);
            if (banh_rang == null)
            {
                return NotFound();
            }
            return Ok(banh_rang);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] banh_rang banh_rang)
        {
            await _banh_rangRepository.CreateAsync(banh_rang);
            return Ok(banh_rang);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] banh_rang banh_rang)
        {
            await _banh_rangRepository.UpdateAsync(id, banh_rang);
            return Ok(banh_rang);
        }
    }
}
