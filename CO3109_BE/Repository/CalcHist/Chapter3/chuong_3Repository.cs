using System;
using CO3109_BE.Entities.CalcHist.Chapter2;
using CO3109_BE.Entities.CalcHist.Chapter3;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;

namespace CO3109_BE.Repository.CalcHist.Chapter3
{
	public interface Ichuong_3Repository: IRepository<chuong_3>
	{
        public Task<chuong_3> CreateReturnAsync(chuong_3 chuong_3);
    }
	public class chuong_3Repository: Repository<chuong_3>, Ichuong_3Repository
	{
		public chuong_3Repository(IOptions<MongoDbSettings> settings) : base(settings, "chuong_3")
		{

		}
        public async Task<chuong_3> CreateReturnAsync(chuong_3 chuong_3)
        {
            await _collection.InsertOneAsync(chuong_3);
            return chuong_3;
        }
    }
}

