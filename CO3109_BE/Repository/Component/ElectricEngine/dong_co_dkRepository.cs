using CO3109_BE.Entities.Component.ElectricEngine;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CO3109_BE.Repository.Component.ElectricEngine
{
    public interface Idong_co_dkRepository: IRepository<dong_co_dk>
    {
        //Take all data by type
        public Task<IEnumerable<dong_co_dk>> GetAllByTypeAsync(String type);
        public Task<dong_co_dk?> GetByIdTypeAsync(String id, String type);
    }
    public class dong_co_dkRepository: Repository<dong_co_dk>, Idong_co_dkRepository
    {
        public dong_co_dkRepository(IOptions<MongoDbSettings> settings): base(settings, "dong_co_dien")
        {

        }
        public async Task<IEnumerable<dong_co_dk>> GetAllByTypeAsync(String Type)
        {
            return await _collection.Find(Builders<dong_co_dk>.Filter.Eq("loai_dong_co", Type)).ToListAsync();
        }
        public async Task<dong_co_dk?> GetByIdTypeAsync(String id, String type)
        {
            var objectID = new ObjectId(id);
            return await _collection.Find(Builders<dong_co_dk>.Filter.And(Builders<dong_co_dk>.Filter.Eq("_id", objectID), Builders<dong_co_dk>.Filter.Eq("Type", type))).FirstOrDefaultAsync();
        }
    }
}
