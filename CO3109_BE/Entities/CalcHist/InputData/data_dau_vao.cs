using System.Text.Json.Serialization;

namespace CO3109_BE.Entities.CalcHist.InputData
{
    public class data_dau_vao: CalcHist
    {
        [JsonPropertyName("F")]
        public decimal F { get; set; }
        [JsonPropertyName("v")]
        public decimal v { get; set; }
        [JsonPropertyName("D")]
        public decimal D { get; set; }
        [JsonPropertyName("L")]
        public decimal L { get; set; }
        [JsonPropertyName("t1")]
        public decimal t1 { get; set; }
        [JsonPropertyName("t2")]
        public decimal t2 { get; set; }
        [JsonPropertyName("T1")]
        public decimal T1 { get; set; }
        [JsonPropertyName("T2")]
        public decimal T2 { get; set; }
        [JsonPropertyName("nol")]
        public decimal nol { get; set; }
        [JsonPropertyName("nbr")]
        public decimal nbr { get; set; }
        [JsonPropertyName("nx")]
        public decimal nx { get; set; }
        [JsonPropertyName("uh")]
        public decimal uh { get; set; }
        [JsonPropertyName("u1")]
        public decimal u1 { get; set; }
        [JsonPropertyName("u2")]
        public decimal u2 { get; set; }
        [JsonPropertyName("ux")]
        public decimal ux { get; set; }
    }
}
