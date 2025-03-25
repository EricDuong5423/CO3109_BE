using CO3109_BE.Entities.CalcHist;
using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Entities.CalcHist.Chapter3;
using CO3109_BE.Entities.CalcHist.InputData;
using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Entities.Component.RollerChain;
using CO3109_BE.Repository.Account;
using CO3109_BE.Repository.CalcHist;
using CO3109_BE.Repository.CalcHist.Chapter2;
using CO3109_BE.Repository.CalcHist.Chapter3;
using CO3109_BE.Repository.CalcHist.InputData;
using CO3109_BE.Repository.Component.ElectricEngine;
using CO3109_BE.Repository.Component.RollerChain;
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
        private readonly Ichuong_3Repository _chuong_3Repository;
        private readonly Itai_khoan_khachRepository _tai_khoan_khachRepository;
        private readonly Idong_co_4aRepository _dong_co_4aRepository;
        private readonly Idong_co_dkRepository _dong_co_dkRepository;
        private readonly Idong_co_kRepository _dong_co_kRepository;
        private readonly Ixich_con_lanRepository _xich_con_lanRepository;
        private readonly calculatingMethod _calculatingMethod;
        private readonly AiApiService _apiService;
        public lich_su_tinh_toanController(Ilich_su_tinh_toanRepository lich_su_tinh_toanRepository,
            Idata_dau_vaoRepository data_dau_vaoRepository,
            Ichuong_2Repository chuong_2Repository,
            Ichuong_3Repository chuong_3Repository,
            Itai_khoan_khachRepository tai_khoan_khachRepository,
            calculatingMethod calculatingMethod,
            Idong_co_4aRepository dong_co_4aRepository,
            Idong_co_dkRepository dong_co_dkRepository,
            Idong_co_kRepository dong_co_kRepository,
            Ixich_con_lanRepository xich_con_lanRepository,
            AiApiService aiApiservice)
        {
            _lich_su_tinh_toanRepository = lich_su_tinh_toanRepository;
            _data_dau_vaoRepository = data_dau_vaoRepository;
            _chuong_2Repository = chuong_2Repository;
            _chuong_3Repository = chuong_3Repository;
            _tai_khoan_khachRepository = tai_khoan_khachRepository;
            _calculatingMethod = calculatingMethod;
            _dong_co_dkRepository = dong_co_dkRepository;
            _dong_co_4aRepository = dong_co_4aRepository;
            _dong_co_kRepository = dong_co_kRepository;
            _xich_con_lanRepository = xich_con_lanRepository;
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
        [HttpPost("Chapter2/{id_khach}")]
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
                if (reponse == "Bad request")
                {
                    return NotFound("AI API gặp một số trục trặc mong bạn thử lại sau !!");
                }
                //JSON the reponse data and take the data
                using JsonDocument doc = JsonDocument.Parse(reponse);
                var root = doc.RootElement;
                String? bestEngineId = root.GetProperty("best_motor_id").GetString();
                String? reason = root.GetProperty("reason").GetString();
                if (bestEngineId == null || reason == null)
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
                return Ok(new { takeDataDongCo, reason, newChapter2, newHistory.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Chapter3/{id_lich_su_tinh_toan}")]
        public async Task<IActionResult> UpdateChuong3(String id_lich_su_tinh_toan, [FromBody]decimal n01)
        {
            try
            {
                lich_su_tinh_toan? CalcHistObject = await _lich_su_tinh_toanRepository.GetByIdAsync(id_lich_su_tinh_toan);
                if (CalcHistObject == null) throw new Exception("Không tìm thấy lịch sử tính toán");
                Chuong_2? Chapter2Object = await _chuong_2Repository.GetByIdAsync(CalcHistObject.chuong_2);
                if (Chapter2Object == null) throw new Exception("Không tìm thấy chương 2");
                decimal Z1 = Math.Round(_calculatingMethod.chapter3.SmallDiscTeeth(Chapter2Object.ux));
                decimal Z2 = Math.Round(_calculatingMethod.chapter3.BigDiscTeeth(Z1, Chapter2Object.ux));
                decimal kz = _calculatingMethod.chapter3.ToothCoefficent(Z1);
                decimal kn = _calculatingMethod.chapter3.RotationCoefficent(Chapter2Object.n3, n01);
                decimal k = _calculatingMethod.chapter3.Coefficent();
                decimal Pt = _calculatingMethod.chapter3.CalculatePower(Chapter2Object.P3, k, kn, kz);
                decimal p = _calculatingMethod.chapter3.ChooseChainRange(n01, Pt);
                xich_con_lan? RollerChainObject = await _xich_con_lanRepository.GetDataByOtherProp(p, "buoc_xich");
                if (RollerChainObject == null) throw new Exception("Vui lòng chọn lại n01 do không có xích con lăn 1 dãy nào phù hợp với yêu cầu của bạn");
                decimal a = _calculatingMethod.chapter3.AxialDistance(p);
                decimal x = Math.Round(_calculatingMethod.chapter3.NumberOfLinks(Z1, Z2, a, p));
                decimal a_sao = _calculatingMethod.chapter3.ReCalcAxialDistance(p, x, Z1, Z2);
                decimal Da = _calculatingMethod.chapter3.DeltaA(a);
                decimal newA = _calculatingMethod.chapter3.ReCalcA(Da, a_sao);
                decimal i = _calculatingMethod.chapter3.ChainImpacts(Z1, Chapter2Object.n3, x);
                if (!_calculatingMethod.chapter3.CheckChainImpact(p, i)) throw new Exception("Vui lòng chọn lại n01 do số lần va đập không thoả mãn");
                decimal v = _calculatingMethod.chapter3.Velocity(Z1, p, Chapter2Object.n3);
                decimal Ft = _calculatingMethod.chapter3.CircularForce(Chapter2Object.P3, v);
                decimal Fv = _calculatingMethod.chapter3.CentrifugalForce(RollerChainObject.khoi_luong_met_xich_lan, v);
                decimal Fo = _calculatingMethod.chapter3.GravityForce(RollerChainObject.khoi_luong_met_xich_lan, a);
                decimal s = _calculatingMethod.chapter3.SafetyCoefficient(RollerChainObject.tai_trong_pha_hong, Ft, Fo, Fv);
                if (!_calculatingMethod.chapter3.CheckSafetyCoefficent(p, n01, s)) throw new Exception("Vui lòng chọn lại n01 do hệ số an toàn của bạn không đáp ứng");
                decimal d1 = _calculatingMethod.chapter3.d1(p, Z1);
                decimal d2 = _calculatingMethod.chapter3.d2(p, Z2);
                decimal da1 = _calculatingMethod.chapter3.da1(p, Z1);
                decimal da2 = _calculatingMethod.chapter3.da2(p, Z2);
                decimal dl = RollerChainObject.duong_kinh_ngoai_con_lan;
                decimal r = _calculatingMethod.chapter3.r(dl);
                decimal df1 = _calculatingMethod.chapter3.df1(d1, r);
                decimal df2 = _calculatingMethod.chapter3.df2(d2, r);
                decimal kr1 = _calculatingMethod.chapter3.kr1(Z1);
                decimal kr2 = _calculatingMethod.chapter3.kr2(Z2);
                decimal Fvd = _calculatingMethod.chapter3.Fvd(Chapter2Object.n3, p);
                decimal A = _calculatingMethod.chapter3.A(p);
                decimal Fr = _calculatingMethod.chapter3.Fr(Ft);
                decimal sH1 = _calculatingMethod.chapter3.sH1(kr1, Ft, Fvd, A);
                decimal sH2 = _calculatingMethod.chapter3.sH2(kr2, Ft, Fvd, A);
                //Data for AI API
                var jsonData1 = new
                {
                    sH = (double)sH1,
                    z = (double)Z1,
                    v = (double)v
                };
                var jsonData2 = new
                {
                    sH = (double)sH2,
                    z = (double)Z1,
                    v = (double)v
                };
                //Call AI API
                var reponse_for_first = await _apiService.FindMaterial(jsonData1);
                var reponse_for_second = await _apiService.FindMaterial(jsonData2);
                if (reponse_for_first == "Bad request" || reponse_for_second == "Bad request")
                {
                    throw new Exception("AI API gặp một số trục trặc mong bạn thử lại sau !!");
                }
                //JSON the reponse data and take the data
                JsonDocument doc1 = JsonDocument.Parse(reponse_for_first);
                JsonDocument doc2 = JsonDocument.Parse(reponse_for_second);
                var root1 = doc1.RootElement;
                var root2 = doc2.RootElement;
                var xichNhoObject = JsonSerializer.Deserialize<Dictionary<string, string>>(root1.GetRawText());
                var xichLonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(root2.GetRawText());
                chuong_3 Chapter3New = new chuong_3()
                {
                    Z1 = Z1,
                    Z2 = Z2,
                    kz = kz,
                    kn = kn,
                    k = k,
                    Pt = Pt,
                    a = newA,
                    x = x,
                    a_sao = a_sao,
                    Da = Da,
                    i = i,
                    v = v,
                    Ft = Ft,
                    Fv = Fv,
                    F0 = Fo,
                    s = s,
                    d1 = d1,
                    d2 = d2,
                    df1 = df1,
                    df2 = df2,
                    da1 = da1,
                    da2 = da2,
                    r = r,
                    Fvd = Fvd,
                    Fr = Fr,
                    sH1 = sH1,
                    sH2 = sH2,
                    vat_lieu_cho_banh_rang_nho = root1.GetProperty("vat_lieu").GetString(),
                    vat_lieu_cho_banh_rang_lon = root2.GetProperty("vat_lieu").GetString(),
                    xich_con_lanId = RollerChainObject.Id
                };
                chuong_3 chuong3 = await _chuong_3Repository.CreateReturnAsync(Chapter3New);
                await _lich_su_tinh_toanRepository.UpdateChapter3Async(id_lich_su_tinh_toan, chuong3);
                return Ok(new
                {
                    chuong3,
                    xich_nho = xichNhoObject,
                    xich_lon = xichLonObject,
                    xich_con_lan = RollerChainObject
                });
            }
            catch(Exception e)
            {
                return BadRequest( new { errorMessage = e.Message});
            }
        }
    }
}