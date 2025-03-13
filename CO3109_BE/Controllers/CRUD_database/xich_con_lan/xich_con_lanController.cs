using CO3109_BE.Entities.xich_con_lan;
using CO3109_BE.Repository.XichConLan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.CRUD_database.XichConLanController
{
    [Route("api/[controller]")]
    [ApiController]
    public class xich_con_lanController : ControllerBase
    {
        private readonly XichConLanRepository _repository;
        public xich_con_lanController(XichConLanRepository repository)
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
        public async Task<IActionResult> Create(xich_con_lan xich_con_lan_Object)
        {
            await _repository.CreateAsync(xich_con_lan_Object);
            return CreatedAtAction(nameof(Get), new { id = xich_con_lan_Object.Id }, xich_con_lan_Object);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, xich_con_lan xich_con_lan_Object)
        {
            await _repository.UpdateAsync(id, xich_con_lan_Object);
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
