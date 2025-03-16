using CO3109_BE.Entities.Account;
using CO3109_BE.Repository.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Account
{
    [Route("api/[controller]")]
    [Tags("tài khoản quản lí")]
    [ApiController]
    public class tai_khoan_quan_liController : ControllerBase
    {
        private readonly Itai_khoan_quan_liRepository _tai_khoan_quan_liRepository;
        public tai_khoan_quan_liController(Itai_khoan_quan_liRepository tai_khoan_quan_liRepository)
        {
            _tai_khoan_quan_liRepository = tai_khoan_quan_liRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _tai_khoan_quan_liRepository.getAllAdminAsync(true));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var tai_khoan_quan_li = await _tai_khoan_quan_liRepository.GetByIdAsync(id);
            if (tai_khoan_quan_li == null)
            {
                return NotFound();
            }
            return Ok(tai_khoan_quan_li);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] tai_khoan_quan_li tai_khoan_quan_li)
        {
            await _tai_khoan_quan_liRepository.CreateAsync(tai_khoan_quan_li);
            return Ok(tai_khoan_quan_li);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] tai_khoan_quan_li tai_khoan_quan_li)
        {
            await _tai_khoan_quan_liRepository.UpdateAsync(id, tai_khoan_quan_li);
            return Ok(tai_khoan_quan_li);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _tai_khoan_quan_liRepository.DeleteAsync(id);
            return Ok();
        }
        /// <summary>
        /// Login By Username And Password Of Admin
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/tai_khoan_quan_li/login
        ///     {
        ///        "username": "username",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <returns>Id and Name Of Admin Or Null</returns>
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody] dynamic data)
        {
            try
            {
                String username = data.username;
                String password = data.password;
                var tai_khoan_quan_li = await _tai_khoan_quan_liRepository.LoginAsync(username, password);
                if (tai_khoan_quan_li == null)
                {
                    return NotFound(new { message = "Wrong username or password" });
                }
                return Ok(new { Id= tai_khoan_quan_li.Id, Name = tai_khoan_quan_li.name});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
