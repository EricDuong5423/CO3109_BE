using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CO3109_BE.Entities.dong_co_dien
{
    public abstract class dong_co_dienAbstractClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string kieu_dong_co { get; set; }
        public decimal cong_suat_kW { get; set; }
        public decimal ti_so_momen_khoi_dong { get; set; }
    }
}
