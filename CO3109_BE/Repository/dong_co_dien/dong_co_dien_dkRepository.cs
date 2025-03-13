using CO3109_BE.Entities.dong_co_dien;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.dong_co_dien
{
    public interface Idong_co_dien_dkRepository : IRepository<dong_co_dien_dk>
    {

    }
    public class dong_co_dien_dkRepository : Repository<dong_co_dien_dk>, Idong_co_dien_dkRepository
    {
        public dong_co_dien_dkRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {

        }
    }
}
