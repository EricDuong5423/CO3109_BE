using CO3109_BE.Entities.xich_con_lan;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.XichConLan
{
    public interface XichConLanIRepository: IRepository<xich_con_lan>
    {
    }
    public class XichConLanRepository : Repository<xich_con_lan>, XichConLanIRepository
    {
        public XichConLanRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {

        }
    }
}
