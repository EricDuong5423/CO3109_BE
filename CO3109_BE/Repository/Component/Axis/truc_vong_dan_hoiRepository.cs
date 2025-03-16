using CO3109_BE.Entities.Component.Axis;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.Component.Axis
{
    public interface Itruc_vong_dan_hoiRepository: IRepository<truc_vong_dan_hoi>
    {
    }
    public class truc_vong_dan_hoiRepository: Repository<truc_vong_dan_hoi>, Itruc_vong_dan_hoiRepository
    {
        public truc_vong_dan_hoiRepository(IOptions<MongoDbSettings> settings) : base(settings, "truc_vong_dan_hoi")
        {

        }
    }
}
