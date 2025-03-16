using CO3109_BE.Entities.Account;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CO3109_BE.Repository.Account
{
    public interface Itai_khoan_quan_liRepository: IRepository<tai_khoan_quan_li>
    {
        public Task<IEnumerable<tai_khoan_quan_li>> getAllAdminAsync(Boolean isAdmin);
        public Task<tai_khoan_quan_li?> LoginAsync(String username, String password);
    }
    public class tai_khoan_quan_liRepository: Repository<tai_khoan_quan_li>, Itai_khoan_quan_liRepository
    {
        public tai_khoan_quan_liRepository(IOptions<MongoDbSettings> settings) : base(settings, "tai_khoan")
        {

        }
        public async Task<IEnumerable<tai_khoan_quan_li>> getAllAdminAsync(Boolean isAdmin = true)
        {
            var filter = Builders<tai_khoan_quan_li>.Filter.Eq("isAdmin", isAdmin);
            return await _collection.Find(filter).ToListAsync();
        }
        public async Task<tai_khoan_quan_li?> LoginAsync(String username, String password)
        {
            var filter = Builders<tai_khoan_quan_li>.Filter.Eq("username", username) & Builders<tai_khoan_quan_li>.Filter.Eq("password", password);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
