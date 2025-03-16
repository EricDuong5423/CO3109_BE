using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CO3109_BE.Repository.Component.ElectricEngine
{
    public interface Idong_co_kRepository : IRepository<dong_co_k>
    {
        //Take all data by type
        public Task<IEnumerable<dong_co_k>> GetAllByTypeAsync(String type);
        //Take data by id and type
        public Task<dong_co_k?> GetByIdTypeAsync(String id, String type);
    }
    public class dong_co_kRepository: Repository<dong_co_k>, Idong_co_kRepository
    {
        public dong_co_kRepository(IOptions<MongoDbSettings> settings) : base(settings, "dong_co_dien")
        { 

        }
        public async Task<IEnumerable<dong_co_k>> GetAllByTypeAsync(String Type)
        {
            return await _collection.Find(Builders<dong_co_k>.Filter.Eq("loai_dong_co", Type)).ToListAsync();
        }
        public async Task<dong_co_k?> GetByIdTypeAsync(String id, String type)
        {
            var objectID = new ObjectId(id);
            return await _collection.Find(Builders<dong_co_k>.Filter.And(Builders<dong_co_k>.Filter.Eq("_id", objectID), Builders<dong_co_k>.Filter.Eq("Type", type))).FirstOrDefaultAsync();
        }
    }
}
