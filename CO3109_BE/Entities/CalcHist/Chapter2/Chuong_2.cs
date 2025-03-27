using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.CalcHist.Chapter2
{
    public class Chuong_2: CalcHist
    {
        [JsonPropertyName("u")]
        public decimal u { get; set; }
        [JsonPropertyName("ux")]
        public decimal ux { get; set; }
        [JsonPropertyName("Pbt")]
        public decimal Pbt { get; set; }
        [JsonPropertyName("P1")]
        public decimal P1 { get; set; }
        [JsonPropertyName("P2")]
        public decimal P2 { get; set; }
        [JsonPropertyName("P3")]
        public decimal P3 { get; set; }
        [JsonPropertyName("Pm")]
        public decimal Pm { get; set; }
        [JsonPropertyName("ndc")]
        public decimal ndc { get; set; }
        [JsonPropertyName("n1")]
        public decimal n1 { get; set; }
        [JsonPropertyName("n2")]
        public decimal n2 { get; set; }
        [JsonPropertyName("n3")]
        public decimal n3 { get; set; }
        [JsonPropertyName("nbt")]
        public decimal nbt { get; set; }
        [JsonPropertyName("Tm")]
        public decimal Tm { get; set; }
        [JsonPropertyName("T1")]
        public decimal T1 { get; set; }
        [JsonPropertyName("T2")]
        public decimal T2 { get; set; }
        [JsonPropertyName("T3")]
        public decimal T3 { get; set; }
        [JsonPropertyName("Tbt")]
        public decimal Tbt { get; set; }
        [JsonPropertyName("Pct")]
        public decimal Pct { get; set; }
        [JsonPropertyName("nsb")]
        public decimal nsb { get; set; }
        [JsonPropertyName("Plv")]
        public decimal Plv { get; set; }
        [JsonPropertyName("n")]
        public decimal n { get; set; }
        [JsonPropertyName("Ptd")]
        public decimal Ptd { get; set; }
        [JsonPropertyName("nlv")]
        public decimal nlv { get; set; }
        [JsonPropertyName("usb")]
        public decimal usb { get; set; }
        public dong_co_chon? dong_co_chon;
    }
    public class dong_co_chon
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String? Id_dong_co;
        public String? loai_dong_co;
        public dong_co_chon(String id, String type)
        {
            Id_dong_co = id;
            loai_dong_co = type;
        }
    }
}
