using CO3109_BE.Entities.Account;
using CO3109_BE.Repository.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CO3109_BE.Controllers.Account
{
    [Route("api/[controller]")]
    [Tags("tài khoản khách")]
    [ApiController]
    public class tai_khoan_khachController : ControllerBase
    {
        private readonly Itai_khoan_khachRepository _tai_khoan_khachRepository;
        public tai_khoan_khachController(Itai_khoan_khachRepository tai_khoan_khachRepository)
        {
            _tai_khoan_khachRepository = tai_khoan_khachRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _tai_khoan_khachRepository.getAllUserAsync(false));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var tai_khoan_khach = await _tai_khoan_khachRepository.GetByIdAsync(id);
            if (tai_khoan_khach == null)
            {
                return NotFound();
            }
            return Ok(tai_khoan_khach);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] tai_khoan_khach tai_khoan_khach)
        {
            await _tai_khoan_khachRepository.CreateAsync(tai_khoan_khach);
            return Ok(tai_khoan_khach);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(String id, [FromBody] tai_khoan_khach tai_khoan_khach)
        {
            await _tai_khoan_khachRepository.UpdateAsync(id, tai_khoan_khach);
            return Ok(tai_khoan_khach);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            await _tai_khoan_khachRepository.DeleteAsync(id);
            return Ok();
        }
        /// <summary>
        /// Login By Username And Password Of User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/tai_khoan_khach/login
        ///     {
        ///        "username": "username",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <returns>Id and Name Of User Or Null</returns>
        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody] dynamic data)
        {
            try
            {
                String username = data.username;
                String password = data.password;
                var tai_khoan_khach = await _tai_khoan_khachRepository.LoginAsync(username, password);
                if (tai_khoan_khach == null)
                {
                    return NotFound(new { message = "Wrong username or password" });
                }
                return Ok(new { Id = tai_khoan_khach.Id, name = tai_khoan_khach.name });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = e });
            }
        }
    }
}
