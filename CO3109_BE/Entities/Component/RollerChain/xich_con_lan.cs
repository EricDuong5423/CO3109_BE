namespace CO3109_BE.Entities.Component.RollerChain
{
    public class xich_con_lan: thanh_phan
    {
        public decimal buoc_xich { get; set; }
        public decimal chieu_rong_mat_xich_trong { get; set; }
        public decimal duong_kinh_ngoai_con_lan { get; set; }
        public decimal duong_kinh_chot_xich { get; set; }
        public decimal chieu_dai_chot_xich { get; set; }
        public decimal chieu_cao_toi_da_ma_trong { get; set; }
        public decimal chieu_rong_toi_da_ma_ngoai { get; set; }
        public decimal tai_trong_pha_hong { get; set; }
        public decimal khoi_luong_met_xich_lan { get; set; }
        public String loai_xich_con_lan { get; set; }

        public xich_con_lan()
        {
            this.loai_xich_con_lan = "1 dãy";
        }
    }
}
