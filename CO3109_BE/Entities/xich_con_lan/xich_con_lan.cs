using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.xich_con_lan
{
    public class xich_con_lan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal buoc_xich_mm { get; set; }
        public decimal chieu_rong_mat_xich_trong_mm { get; set; }
        public decimal duong_kinh_ngoai_con_lan { get; set; }
        public decimal duong_kinh_chot_xich_mm { get; set; }
        public decimal? chieu_dai_chot_xich_mm { get; set; }
        public decimal chieu_cao_toi_da_ma_trong_mm { get; set; }
        public decimal chieu_rong_toi_da_ma_ngoai_mm { get; set; }
        public decimal tai_trong_pha_hong_kN { get; set; }
        public decimal khoi_luong_1met_xich_kg { get; set; }
        public String loai_xich_con_lan { get; set; }
    }
}
