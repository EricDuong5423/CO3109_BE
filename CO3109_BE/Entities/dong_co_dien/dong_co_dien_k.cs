using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.dong_co_dien
{
    public class dong_co_dien_k:dong_co_dienAbstractClass
    {
        public int van_toc_quay_vgph_50Hz { get; set; }
        public int van_toc_quay_vgph_60Hz { get; set; }
        public decimal hieu_suat_dong_co_dien { get; set; }
        public decimal he_so_cong_suat { get; set; }
        public decimal ti_so_dong_khoi_dong { get; set; }
        public int khoi_luong_kg { get; set; }
    }
}
