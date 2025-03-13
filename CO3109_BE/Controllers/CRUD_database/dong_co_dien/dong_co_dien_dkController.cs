using CO3109_BE.Entities.dong_co_dien;
using CO3109_BE.Repository.dong_co_dien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.CRUD_database.dong_co_dien
{
    [Route("api/[controller]")]
    [ApiController]
    public class dong_co_dien_dkController : ControllerBase
    {
        private readonly Idong_co_dien_dkRepository _repository;
        public dong_co_dien_dkController(Idong_co_dien_dkRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create(dong_co_dien_dk dong_co_dien_dk_Object)
        {
            await _repository.CreateAsync(dong_co_dien_dk_Object);
            return CreatedAtAction(nameof(Get), new { id = dong_co_dien_dk_Object.Id }, dong_co_dien_dk_Object);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, dong_co_dien_dk dong_co_dien_dk_Object)
        {
            await _repository.UpdateAsync(id, dong_co_dien_dk_Object);
            return Ok(new { Success = true, Message = "Product updated" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return Ok(new { Success = true, Message = "Product deleted" });
        }
    }
}
