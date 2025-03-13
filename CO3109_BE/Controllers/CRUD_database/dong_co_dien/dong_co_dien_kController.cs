using CO3109_BE.Entities.dong_co_dien;
using CO3109_BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.CRUD_database.dong_co_dien
{
    [Route("api/[controller]")]
    [ApiController]
    public class dong_co_dien_kController : ControllerBase
    {
        private readonly Idong_co_dien_kRepository _repository;
        public dong_co_dien_kController(Idong_co_dien_kRepository repository)
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
        public async Task<IActionResult> Create(dong_co_dien_k dong_co_dien_k_Object)
        {
            await _repository.CreateAsync(dong_co_dien_k_Object);
            return CreatedAtAction(nameof(Get), new { id = dong_co_dien_k_Object.Id }, dong_co_dien_k_Object);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, dong_co_dien_k dong_co_dien_k_Object)
        {
            await _repository.UpdateAsync(id, dong_co_dien_k_Object);
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
