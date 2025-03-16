using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CO3109_BE.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly IMongoCollection<T> _collection;
        public Repository(IOptions<MongoDbSettings> settings, String? collectionName = null) 
        {
            //Instance of Mongo Client
            var client = new MongoClient(settings.Value.ConnectionString);
            //Database instances
            var database = client.GetDatabase(settings.Value.DatabaseName);
            //Collection instances
            _collection = database.GetCollection<T>(collectionName ?? typeof(T).Name);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<T?> GetByIdAsync(String id)
        {
            var objectID = new ObjectId(id);
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectID)).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(String id, T entity)
        {
            var objectID = new ObjectId(id);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectID), entity);
        }
        public async Task DeleteAsync(String id)
        {
            var objectID = new ObjectId(id);
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectID));
        }
    }
}
