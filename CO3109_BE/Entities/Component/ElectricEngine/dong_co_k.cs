namespace CO3109_BE.Entities.Component.ElectricEngine
{
    public class dong_co_k : dong_co_dien
    {
        public decimal hieu_suat_dong_co_dien { get; set; }
        public decimal he_so_cong_suat { get; set; }
        public decimal ti_so_dong_khoi_dong { get; set; }
        public decimal ti_so_momen_khoi_dong { get; set; }
        public decimal khoi_luong { get; set; }

        public dong_co_k()
        {
            this.loai_dong_co = "k";
        }
    }
}
