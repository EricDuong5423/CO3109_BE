using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.Component
{
    public class thanh_phan
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public String? Id { get; set; }
        [BsonElement("createAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
