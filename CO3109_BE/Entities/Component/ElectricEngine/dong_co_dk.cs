namespace CO3109_BE.Entities.Component.ElectricEngine
{
    public class dong_co_dk: dong_co_dien
    {
        public decimal he_so_cong_suat { get; set; }
        public decimal he_so_qua_tai_momen_xoan { get; set; }
        public decimal ti_so_momen_khoi_dong { get; set; }
        public decimal moment_vo_lang { get; set; }
        public decimal khoi_luong { get; set; }
        public dong_co_dk()
        {
            this.loai_dong_co = "dk";
        }
    }
}
