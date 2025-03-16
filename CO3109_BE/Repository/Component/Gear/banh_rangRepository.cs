using CO3109_BE.Entities.Component.Gear;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.Component.Gear
{
    public interface Ibanh_rangRepository: IRepository<banh_rang>
    {
    }
    public class banh_rangRepository: Repository<banh_rang>, Ibanh_rangRepository
    {
        public banh_rangRepository(IOptions<MongoDbSettings> settings) : base(settings, "banh_rang")
        {

        }
    }
}
