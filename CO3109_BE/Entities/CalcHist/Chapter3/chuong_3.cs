using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CO3109_BE.Entities.CalcHist.Chapter3
{
	public class chuong_3:CalcHist
	{
		[JsonPropertyName("Z1")]
		public decimal Z1 { get; set; }
		[JsonPropertyName("Z2")]
		public decimal Z2 { get; set; }
		[JsonPropertyName("kz")]
		public decimal kz { get; set; }
		[JsonPropertyName("kn")]
		public decimal kn { get; set; }
		[JsonPropertyName("k")]
		public decimal k { get; set; }
		[JsonPropertyName("Pt")]
		public decimal Pt { get; set; }
		[JsonPropertyName("a")]
		public decimal a { get; set; }
		[JsonPropertyName("x")]
		public decimal x { get; set; }
		[JsonPropertyName("a_sao")]
		public decimal a_sao { get; set; }
		[JsonPropertyName("Da")]
		public decimal Da { get; set; }
		[JsonPropertyName("i")]
		public decimal i { get; set; }
		[JsonPropertyName("v")]
		public decimal v { get; set; }
		[JsonPropertyName("Ft")]
		public decimal Ft { get; set; }
		[JsonPropertyName("Fv")]
		public decimal Fv { get; set; }
		[JsonPropertyName("F0")]
		public decimal F0 { get; set; }
		[JsonPropertyName("s")]
		public decimal s { get; set; }
		[JsonPropertyName("d1")]
		public decimal d1 { get; set; }
		[JsonPropertyName("d2")]
		public decimal d2 { get; set; }
		[JsonPropertyName("da1")]
		public decimal da1 { get; set; }
		[JsonPropertyName("da2")]
		public decimal da2 { get; set; }
		[JsonPropertyName("r")]
		public decimal r { get; set; }
		[JsonPropertyName("df1")]
		public decimal df1 { get; set; }
		[JsonPropertyName("df2")]
		public decimal df2 { get; set; }
		[JsonPropertyName("Fvd")]
		public decimal Fvd { get; set; }
		[JsonPropertyName("Fr")]
		public decimal Fr { get; set; }
		[JsonPropertyName("sH1")]
		public decimal sH1 { get; set; }
		[JsonPropertyName("sH2")]
		public decimal sH2 { get; set; }
		[JsonPropertyName("vat_lieu_cho_banh_rang_nho")]
		public String? vat_lieu_cho_banh_rang_nho;
		[JsonPropertyName("vat_lieu_cho_banh_rang_lon")]
		public String? vat_lieu_cho_banh_rang_lon;
        [BsonRepresentation(BsonType.ObjectId)]
        public String? xich_con_lanId { get; set; }
    }
}

