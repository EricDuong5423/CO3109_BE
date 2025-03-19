using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.CalcHist.Chapter2
{
    public interface Ichuong_2Repository: IRepository<Chuong_2>
    {
        public Task<Chuong_2> CreateReturnAsync(Chuong_2 chuong_2);
    }
    public class chuong_2Repository: Repository<Chuong_2>, Ichuong_2Repository
    {
        public chuong_2Repository(IOptions<MongoDbSettings> settings): base(settings, "chuong_2")
        {

        }
        public async Task<Chuong_2> CreateReturnAsync(Chuong_2 chuong_2)
        {
            await _collection.InsertOneAsync(chuong_2);
            return chuong_2;
        }
    }
}
