using CO3109_BE.Entities.CalcHist;
using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Entities.CalcHist.Chapter3;
using CO3109_BE.Entities.CalcHist.InputData;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CO3109_BE.Repository.CalcHist
{
    public interface Ilich_su_tinh_toanRepository: IRepository<lich_su_tinh_toan>
    {
        public Task<lich_su_tinh_toan> CreateUpdateAsync(String tai_khoan_khachId, data_dau_vao data_dau_vao, Chuong_2 chuong_2);
        public Task<lich_su_tinh_toan> UpdateChapter3Async(String lich_su_tinh_toanID, chuong_3 chuong_3);
    }
    public class lich_su_tinh_toanRepository : Repository<lich_su_tinh_toan>, Ilich_su_tinh_toanRepository
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
        public async Task<lich_su_tinh_toan> UpdateChapter3Async(String lich_su_tinh_toanID, chuong_3 chuong_3)
        {
            var lich_su_tinh_toan = Builders<lich_su_tinh_toan>.Filter.Eq("_id", new ObjectId(lich_su_tinh_toanID)) ;
            var updateForChapter3 = Builders<lich_su_tinh_toan>.Update.Set("chuong_3", chuong_3.Id);
            var updatedDoc = await _collection.FindOneAndUpdateAsync(lich_su_tinh_toan, updateForChapter3, new FindOneAndUpdateOptions<lich_su_tinh_toan>
            {
                ReturnDocument = ReturnDocument.After
            });

            if (updatedDoc == null)
            {
                throw new Exception($"Không tìm thấy lịch sử tính toán có ID: {lich_su_tinh_toanID}");
            }

            return updatedDoc;
        }
    }
}
