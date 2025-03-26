using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CO3109_BE.Entities.CalcHist.InputData;
using CO3109_BE.Repository.CalcHist.InputData;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CO3109_BE.Controllers.CalcHist.InputData
{
    [Route("api/[controller]")]
    public class data_dau_vaoController : Controller
    {
        private readonly Idata_dau_vaoRepository _data_dau_vaoRepository;
        public data_dau_vaoController(Idata_dau_vaoRepository data_dau_vaoRepository)
        {
            _data_dau_vaoRepository = data_dau_vaoRepository;
        }
        [HttpGet("id_data_dau_vao")]
        public async Task<IActionResult> Get(String id_data_dau_vao)
        {
            data_dau_vao? inputData = await _data_dau_vaoRepository.GetByIdAsync(id_data_dau_vao);
            if (inputData == null)
            {
                return NotFound();
            }
            return Ok(inputData);
        }
    }
}

