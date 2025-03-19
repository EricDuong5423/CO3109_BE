using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.Account
{
    public class tai_khoan
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public String? Id { get; set; }
        [BsonElement("createAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public String? name { get; set; }
        public String? username { get; set; }
        public String? password { get; set; }
        public Boolean isAdmin { get; set; }
    }
}
