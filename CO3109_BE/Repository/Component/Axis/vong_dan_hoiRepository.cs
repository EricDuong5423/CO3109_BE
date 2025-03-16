using CO3109_BE.Entities.Component.Axis;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.Component.Axis
{
    public interface Ivong_dan_hoiRepository: IRepository<vong_dan_hoi>
    {
    }
    public class vong_dan_hoiRepository: Repository<vong_dan_hoi>, Ivong_dan_hoiRepository
    {
        public vong_dan_hoiRepository(IOptions<MongoDbSettings> settings) : base(settings, "vong_dan_hoi")
        {

        }
    }
}
