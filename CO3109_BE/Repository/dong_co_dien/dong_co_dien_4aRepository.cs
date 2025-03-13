using CO3109_BE.Entities.dong_co_dien;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.dong_co_dien
{
    public interface Idong_co_dien_4aRepository : IRepository<dong_co_dien_4a>
    {

    }
    public class dong_co_dien_4aRepository: Repository<dong_co_dien_4a>, Idong_co_dien_4aRepository 
    {
        public dong_co_dien_4aRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }
    }
}
