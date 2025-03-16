using CO3109_BE.Entities.Account;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CO3109_BE.Repository.Account
{
    public interface Itai_khoan_khachRepository: IRepository<tai_khoan_khach>
    {
        public Task<IEnumerable<tai_khoan_khach>> getAllUserAsync(bool isAdmin);
        public Task<tai_khoan_khach?> LoginAsync(String username, String password);
    }
    public class tai_khoan_khachRepository: Repository<tai_khoan_khach>, Itai_khoan_khachRepository
    {
        public tai_khoan_khachRepository(IOptions<MongoDbSettings> settings) : base(settings, "tai_khoan")
        {

        }
        public async Task<IEnumerable<tai_khoan_khach>> getAllUserAsync(bool isAdmin = false)
        {
            var filter = Builders<tai_khoan_khach>.Filter.Eq("isAdmin", isAdmin);
            return await _collection.Find(filter).ToListAsync();
        }
        public async Task<tai_khoan_khach?> LoginAsync(String username, String password)
        {
            var filter = Builders<tai_khoan_khach>.Filter.Eq("username", username) & Builders<tai_khoan_khach>.Filter.Eq("password", password);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
