using CO3109_BE.Entities.CalcHist;
using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Entities.CalcHist.InputData;
using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Repository.Account;
using CO3109_BE.Repository.CalcHist;
using CO3109_BE.Repository.CalcHist.Chapter2;
using CO3109_BE.Repository.CalcHist.InputData;
using CO3109_BE.Repository.Component.ElectricEngine;
using CO3109_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace CO3109_BE.Controllers.CalcHist
{
    [Route("api/[controller]")]
    [Tags("lịch sử tính toán")]
    [ApiController]
    public class lich_su_tinh_toanController : ControllerBase
    {
        private readonly Ilich_su_tinh_toanRepository _lich_su_tinh_toanRepository;
        private readonly Idata_dau_vaoRepository _data_dau_vaoRepository;
        private readonly Ichuong_2Repository _chuong_2Repository;
        private readonly Itai_khoan_khachRepository _tai_khoan_khachRepository;
        private readonly Idong_co_4aRepository _dong_co_4aRepository;
        private readonly Idong_co_dkRepository _dong_co_dkRepository;
        private readonly Idong_co_kRepository _dong_co_kRepository;
        private readonly calculatingMethod _calculatingMethod;
        private readonly AiApiService _apiService;
        public lich_su_tinh_toanController(Ilich_su_tinh_toanRepository lich_su_tinh_toanRepository,
            Idata_dau_vaoRepository data_dau_vaoRepository,
            Ichuong_2Repository chuong_2Repository,
            Itai_khoan_khachRepository tai_khoan_khachRepository,
            calculatingMethod calculatingMethod,
            Idong_co_4aRepository dong_co_4aRepository,
            Idong_co_dkRepository dong_co_dkRepository,
            Idong_co_kRepository dong_co_kRepository,
            AiApiService aiApiservice)
        {
            _lich_su_tinh_toanRepository = lich_su_tinh_toanRepository;
            _data_dau_vaoRepository = data_dau_vaoRepository;
            _chuong_2Repository = chuong_2Repository;
            _tai_khoan_khachRepository = tai_khoan_khachRepository;
            _calculatingMethod = calculatingMethod;
            _dong_co_dkRepository = dong_co_dkRepository;
            _dong_co_4aRepository = dong_co_4aRepository;
            _dong_co_kRepository = dong_co_kRepository;
            _apiService = aiApiservice;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _lich_su_tinh_toanRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var lich_su_tinh_toan = await _lich_su_tinh_toanRepository.GetByIdAsync(id);
            if (lich_su_tinh_toan == null)
            {
                return NotFound();
            }
            return Ok(lich_su_tinh_toan);
        }
        /// <summary>
        /// Tạo một lịch sử tính toán mới cần id của khách và các thông tin của data_dau_vao
        /// </summary>
        /// <param name="id_khach">Cần id của khách mới xài được</param>
        /// <param name="data_dau_vao"></param>
        /// <returns></returns>
        [HttpPost("{id_khach}")]
        public async Task<IActionResult> Create(String id_khach, [FromBody] data_dau_vao data_dau_vao)
        {
            try
            {
                //Declare a link and endpoint for api call
                //Check user and create new input data
                var user = await _tai_khoan_khachRepository.GetByIdAsync(id_khach);
                if (user == null) return NotFound("Tài khoản khách không tồn tại.");
                //Calculating for finding the right engine
                decimal Plv = _calculatingMethod.chapter2.ShaftCapacity(data_dau_vao.F, data_dau_vao.v);
                decimal n = _calculatingMethod.chapter2.GeneralEfficiency(data_dau_vao.nol, data_dau_vao.nbr, data_dau_vao.nx);
                decimal Ptd = _calculatingMethod.chapter2.EquivalentCapacity(data_dau_vao.T1, data_dau_vao.T2, data_dau_vao.t1, data_dau_vao.t2, Plv);
                decimal Pct = _calculatingMethod.chapter2.MinimalCapacity(Ptd, n);
                decimal nlv = _calculatingMethod.chapter2.NumberOfRotation(data_dau_vao.v, data_dau_vao.D);
                decimal usb = _calculatingMethod.chapter2.PreliminaryGearRatio(data_dau_vao.uh, data_dau_vao.ux);
                decimal nsb = _calculatingMethod.chapter2.BasicGearRatio(nlv, usb);
                //Data for AI API
                var jsonData = new
                {
                    cong_suat_can_tim = (double)Pct,
                    van_toc_quay_can_tim = (double)nsb
                };
                //Call AI API
                var reponse = await _apiService.FindBestEngine(jsonData);
                if(reponse == "Bad request")
                {
                    return NotFound("AI API gặp một số trục trặc mong bạn thử lại sau !!");
                }
                //JSON the reponse data and take the data
                using JsonDocument doc = JsonDocument.Parse(reponse);
                var root = doc.RootElement;
                String? bestEngineId = root.GetProperty("best_motor_id").GetString();
                String? reason = root.GetProperty("reason").GetString();
                if(bestEngineId == null || reason == null)
                {
                    return NotFound("AI API gặp một số trục trặc mong bạn thử lại sau");
                }
                //Find the motor with the id
                object? dongCo = await _dong_co_4aRepository.GetByIdTypeAsync(bestEngineId, "4a");
                if (dongCo == null) dongCo = await _dong_co_dkRepository.GetByIdTypeAsync(bestEngineId, "dk");
                if (dongCo == null) dongCo = await _dong_co_kRepository.GetByIdTypeAsync(bestEngineId, "k");
                if (dongCo == null)
                {
                    return NotFound("Không tìm thấy động cơ phù hợp.");
                }
                var takeDataDongCo = dongCo switch
                {
                    dong_co_4a dc => new
                    {
                        LoaiDongCo = "4a",
                        Ten = dc.ten_dong_co,
                        CongSuat = dc.cong_suat,
                        VanTocQuay = dc.van_toc_vong_quay
                    },
                    dong_co_dk dc => new
                    {
                        LoaiDongCo = "dk",
                        Ten = dc.ten_dong_co,
                        CongSuat = dc.cong_suat,
                        VanTocQuay = dc.van_toc_vong_quay
                    },
                    dong_co_k dc => new
                    {
                        LoaiDongCo = "k",
                        Ten = dc.ten_dong_co,
                        CongSuat = dc.cong_suat,
                        VanTocQuay = dc.van_toc_vong_quay
                    },
                    _ => throw new Exception("Loại động cơ không hợp lệ")
                };
                //Calc transmission
                decimal u = _calculatingMethod.chapter2.GenTransRatio((decimal)takeDataDongCo.VanTocQuay, nlv);
                decimal ux = _calculatingMethod.chapter2.ChainDriveCoef(u, data_dau_vao.u1, data_dau_vao.u2);
                //Calc power
                decimal Pbt = _calculatingMethod.chapter2.Pbt(Plv, data_dau_vao.nol);
                decimal P3 = _calculatingMethod.chapter2.P3(Pbt, data_dau_vao.nol, data_dau_vao.nx);
                decimal P2 = _calculatingMethod.chapter2.P2(P3, data_dau_vao.nol, data_dau_vao.nbr);
                decimal P1 = _calculatingMethod.chapter2.P1(P2, data_dau_vao.nol, data_dau_vao.nbr);
                decimal Pm = _calculatingMethod.chapter2.Pm(P1);
                //Calc rotation velocity
                decimal n1 = (decimal)takeDataDongCo.VanTocQuay;
                decimal ndc = n1;
                decimal n2 = _calculatingMethod.chapter2.n2(n1, data_dau_vao.u1);
                decimal n3 = _calculatingMethod.chapter2.n3(n2, data_dau_vao.u2);
                decimal nbt = _calculatingMethod.chapter2.nbt(n3, ux);
                //Calc torque
                decimal T1 = _calculatingMethod.chapter2.Tm(Pm, ndc);
                decimal Tm = T1;
                decimal T2 = _calculatingMethod.chapter2.T2(P2, n2);
                decimal T3 = _calculatingMethod.chapter2.T3(P3, n3);
                decimal Tbt = _calculatingMethod.chapter2.Tbt(Pbt, nbt);
                Chuong_2 newChapter2 = new Chuong_2
                {
                    //Choose engine section calc
                    Plv = Plv,
                    n = n,
                    Ptd = Ptd,
                    Pct = Pct,
                    nlv = nlv,
                    usb = usb,
                    nsb = nsb,
                    //Transmission calc
                    u = u,
                    ux = ux,
                    //Calc power
                    Pbt = Pbt,
                    P1 = P1,
                    P2 = P2,
                    P3 = P3,
                    Pm = Pm,
                    //Calc rotation velocity
                    n1 = n1,
                    ndc = ndc,
                    n2 = n2,
                    n3 = n3,
                    nbt = nbt,
                    //Calc torque
                    T1 = T1,
                    Tm = Tm,
                    T2 = T2,
                    T3 = T3,
                    Tbt = Tbt,
                    dong_co_duoc_chon = new dong_co_chon(takeDataDongCo.Ten, takeDataDongCo.VanTocQuay, takeDataDongCo.CongSuat)
                };
                var newChapter2Data = await _chuong_2Repository.CreateReturnAsync(newChapter2);
                var newInputData = await _data_dau_vaoRepository.CreateReturnAsync(data_dau_vao);
                var newHistory = await _lich_su_tinh_toanRepository.CreateUpdateAsync(id_khach, newInputData, newChapter2Data);
                return Ok(new { takeDataDongCo, reason, newChapter2});
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
