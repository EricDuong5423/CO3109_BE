using CO3109_BE.Entities.Account;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.ObjectModel;

namespace CO3109_BE.Entities.CalcHist
{
    public class CalcHist 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public String? Id { get; set; }
        [BsonElement("createAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
    public class lich_su_tinh_toan: CalcHist
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String? tai_khoan_khachId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public String? data_dau_vao { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public String? chuong_2 { get; set; }
    }
}
