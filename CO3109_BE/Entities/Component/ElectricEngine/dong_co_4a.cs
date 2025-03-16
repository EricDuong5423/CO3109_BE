namespace CO3109_BE.Entities.Component.ElectricEngine
{
    public class dong_co_4a: dong_co_dien
    {
        public decimal he_so_cong_suat { get; set; }
        public decimal hieu_suat_dong_co_dien { get; set; }
        public decimal he_so_qua_tai_momen_xoan { get; set; }
        public decimal ti_so_moment_khoi_dong { get; set; }

        public dong_co_4a()
        {
            this.loai_dong_co = "4a";
        }
    }
}
