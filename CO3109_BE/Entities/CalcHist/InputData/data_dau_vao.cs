using System.Text.Json.Serialization;

namespace CO3109_BE.Entities.CalcHist.InputData
{
    public class data_dau_vao: CalcHist
    {
        [JsonPropertyName("F")]
        public decimal F { get; set; }
        public decimal v { get; set; }
        [JsonPropertyName("D")]
        public decimal D { get; set; }
        [JsonPropertyName("L")]
        public decimal L { get; set; }
        public decimal t1 { get; set; }
        public decimal t2 { get; set; }
        [JsonPropertyName("T1")]
        public decimal T1 { get; set; }
        [JsonPropertyName("T2")]
        public decimal T2 { get; set; }
    }
}
