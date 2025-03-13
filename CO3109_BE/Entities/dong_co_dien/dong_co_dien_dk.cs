using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.dong_co_dien
{
    public class dong_co_dien_dk:dong_co_dienAbstractClass
    {
        public int van_toc_quay_vgph { get; set; }
        public decimal he_so_cong_suat { get; set; }
        public decimal he_so_qua_tai_momen_xoan { get; set; }
        public decimal momen_volang_cua_roto { get; set; }
        public int khoi_luong_kg { get; set; }
    }
}
