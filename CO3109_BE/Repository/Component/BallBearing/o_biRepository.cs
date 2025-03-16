using CO3109_BE.Entities.Component.BallBearing;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.Component.BallBearing
{
    public interface Io_biRepository: IRepository<o_bi>
    {
    }
    public class o_biRepository: Repository<o_bi>, Io_biRepository
    {
        public o_biRepository(IOptions<MongoDbSettings> settings) : base(settings, "o_bi")
        {

        }
    }
}
