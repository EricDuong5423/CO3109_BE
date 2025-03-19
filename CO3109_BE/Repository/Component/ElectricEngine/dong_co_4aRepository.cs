using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CO3109_BE.Repository.Component.ElectricEngine
{
    public interface Idong_co_4aRepository: IRepository<dong_co_4a>
    {
        public Task<IEnumerable<dong_co_4a>> GetAllByTypeAsync(String type);
        public Task<dong_co_4a?> GetByIdTypeAsync(String id, String type);
    }
    public class dong_co_4aRepository: Repository<dong_co_4a>, Idong_co_4aRepository
    {
        public dong_co_4aRepository(IOptions<MongoDbSettings> settings) : base(settings, "dong_co_dien")
        {

        }
        public async Task<IEnumerable<dong_co_4a>> GetAllByTypeAsync(String Type)
        {
            return await _collection.Find(Builders<dong_co_4a>.Filter.Eq("loai_dong_co", Type)).ToListAsync();
        }
        public async Task<dong_co_4a?> GetByIdTypeAsync(String id, String type)
        {
            var objectID = new ObjectId(id);
            return await _collection.Find(Builders<dong_co_4a>.Filter.And(Builders<dong_co_4a>.Filter.Eq("_id", objectID), Builders<dong_co_4a>.Filter.Eq("loai_dong_co", type))).FirstOrDefaultAsync();
        }
    }
}
