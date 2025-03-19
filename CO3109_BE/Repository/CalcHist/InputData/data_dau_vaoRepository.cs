using CO3109_BE.Entities.CalcHist.InputData;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.CalcHist.InputData
{
    public interface Idata_dau_vaoRepository: IRepository<data_dau_vao>
    {
        public Task<data_dau_vao> CreateReturnAsync(data_dau_vao data_dau_vao);
    }
    public class data_dau_vaoRepository: Repository<data_dau_vao>, Idata_dau_vaoRepository
    {
        public data_dau_vaoRepository(IOptions<MongoDbSettings> settings): base(settings, "data_dau_vao")
        {

        }
        public async Task<data_dau_vao> CreateReturnAsync(data_dau_vao data_dau_vao) 
        {
            await _collection.InsertOneAsync(data_dau_vao);
            return data_dau_vao;
        }
    }
}
