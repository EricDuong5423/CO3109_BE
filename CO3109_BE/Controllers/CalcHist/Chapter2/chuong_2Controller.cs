using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Repository.CalcHist.Chapter2;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CO3109_BE.Controllers.CalcHist.Chapter2
{
    [Route("api/[controller]")]
    [Tags("Chương 2")]
    public class chuong_2Controller : Controller
    {
        private readonly Ichuong_2Repository _chuong_2Repository;
        public chuong_2Controller(Ichuong_2Repository chuong_2Repository)
        {
            _chuong_2Repository = chuong_2Repository;
        }
        // GET api/values/5
        [HttpGet("{id_chuong_2}")]
        public async Task<IActionResult> Get(String id_chuong_2)
        {
            Chuong_2? chuong_2 = await _chuong_2Repository.GetByIdAsync(id_chuong_2);
            if (chuong_2 == null)
            {
                return NotFound();
            }
            return Ok(chuong_2);
        }
    }
}