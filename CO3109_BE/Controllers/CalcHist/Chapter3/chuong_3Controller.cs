using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CO3109_BE.Entities.CalcHist.Chapter3;
using CO3109_BE.Repository.CalcHist.Chapter3;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CO3109_BE.Controllers.CalcHist.Chapter2
{
    [Route("api/[controller]")]
    [Tags("Chương 3")]
    public class chuong_3Controller : Controller
    {
        private readonly Ichuong_3Repository _chuong_3Repository;
        public chuong_3Controller(Ichuong_3Repository chuong_3Repository)
        {
            _chuong_3Repository = chuong_3Repository;
        }
        // GET api/values/5
        [HttpGet("{id_chuong_3}")]
        public async Task<IActionResult> Get(String id_chuong_3)
        {
            chuong_3? chuong_3 = await _chuong_3Repository.GetByIdAsync(id_chuong_3);
            if (chuong_3 == null)
            {
                return NotFound();
            }
            return Ok(chuong_3);
        }
    }
}

