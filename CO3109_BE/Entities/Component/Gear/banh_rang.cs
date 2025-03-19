using CO3109_BE.Entities.Component;

namespace CO3109_BE.Entities.Component.Gear
{
    public class banh_rang: thanh_phan
    {
        public String? vat_lieu { get; set; }
        public decimal Ka_nghieng { get; set; }
        public decimal Ka_chuV { get; set; }
        public decimal Kd_nghieng { get; set; }
        public decimal Kd_chuV { get; set; }
        public decimal Zm { get; set; }
    }
}
