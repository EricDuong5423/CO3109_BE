using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;
using OpenAI.Chat;
using System.Text;
using Newtonsoft.Json;

namespace CO3109_BE.Controllers.Chapter2API
{
    public class method
    {
        public method()
        {

        }
        // Method for calculating capacity
        public decimal ShaftCapacity(decimal F, decimal v)
        {
            return (F * v) / 1000;
        }
        public decimal GeneralEfficiency(decimal nk, decimal nol, decimal nbr, decimal nx)
        {
            return nk * (decimal)Math.Pow((double)nol, 4) * (decimal)Math.Pow((double)nbr, 3) * nx;
        }
        public decimal EquivalentCapacity(decimal T1, decimal T2, decimal t1, decimal t2, decimal Plv)
        {
            return Plv * (decimal)Math.Sqrt((double)((Math.Pow((double)T1, 2) * (double)t1 + Math.Pow((double)T2, 2) * (double)t2) / ((double)t1 + (double)t2)));
        }
        public decimal MinimalCapacity(decimal Peq, decimal n)
        {
            return Peq / n;
        }
        public decimal BasicGearRatio(decimal nlv, decimal usb)
        {
            return nlv * usb;
        }

        //Method for rotation speed
        public decimal NumberOfRotation(decimal v, decimal D)
        {
            return (60000 * v) / ((decimal)Math.PI * D);
        }
        public decimal PreliminaryGearRatio(decimal uh, decimal ux)
        {
            return uh * ux;
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralCalcController : ControllerBase
    {
        public GeneralCalcController()
        {
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> ChooseEngineCalculate([FromBody] dynamic data)
        { 
            try
            {
                decimal F = data.F;
                decimal v = data.v;
                decimal D = data.D;
                decimal L = data.L;
                decimal t1 = data.t1;
                decimal t2 = data.t2;
                decimal T1 = data.T1;
                decimal T2 = data.T2;
                String type = data.type;
                decimal nk = 1;
                decimal nol = 0.993m;
                decimal nbr = 0.97m;
                decimal nx = 0.91m;
                decimal uh = 8;
                decimal ux = 2;
                method Method = new method();
                //Calculate Engine
                decimal Plv = Method.ShaftCapacity(F, v);
                decimal n = Method.GeneralEfficiency(nk, nol, nbr, nx);
                decimal Peq = Method.EquivalentCapacity(T1, T2, t1, t2, Plv);
                decimal Pct = Method.MinimalCapacity(Peq, n);
                decimal usb = Method.PreliminaryGearRatio(uh, ux);
                decimal nlv = Method.NumberOfRotation(v, D);
                decimal nsb = Method.BasicGearRatio(nlv, usb);
                //_lich_su_tinh_toanRepository.CreateAsync(new lich_su_tinh_toan
                //{
                //    F = F,
                //    D = D,
                //    v = v,
                //    L = L,
                //    t1 = t1,
                //    t2 = t2,
                //    T1 = T1,
                //    T2 = T2
                //});
                
                return Ok(new { });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
