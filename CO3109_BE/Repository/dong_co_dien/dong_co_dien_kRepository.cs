using CO3109_BE.Entities.dong_co_dien;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository
{
    public interface Idong_co_dien_kRepository: IRepository<dong_co_dien_k>
    {
    }
    public class dong_co_dien_kRepository : Repository<dong_co_dien_k>, Idong_co_dien_kRepository
    {
        public dong_co_dien_kRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }
    }
}
