using CO3109_BE.Entities.CalcHist;
using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Entities.CalcHist.InputData;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CO3109_BE.Repository.CalcHist
{
    public interface Ilich_su_tinh_toanRepository: IRepository<lich_su_tinh_toan>
    {
        public Task<lich_su_tinh_toan> CreateUpdateAsync(String tai_khoan_khachId, data_dau_vao data_dau_vao, Chuong_2 chuong_2);
    }
    public class lich_su_tinh_toanRepository: Repository<lich_su_tinh_toan>, Ilich_su_tinh_toanRepository
    {
        public lich_su_tinh_toanRepository(IOptions<MongoDbSettings> settings) : base(settings, "lich_su_tinh_toan")
        {
            
        }
        public async Task<lich_su_tinh_toan> CreateUpdateAsync(String tai_khoan_khachId, data_dau_vao data_dau_vao, Chuong_2 chuong_2)
        {

            lich_su_tinh_toan newHistory = new lich_su_tinh_toan
            {
                tai_khoan_khachId = tai_khoan_khachId,
                data_dau_vao = data_dau_vao.Id,
                chuong_2 = chuong_2.Id
            };
            await _collection.InsertOneAsync(newHistory);
            return newHistory;
        }
    }
}
