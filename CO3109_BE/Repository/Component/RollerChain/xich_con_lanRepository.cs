﻿using CO3109_BE.Entities.Component.RollerChain;
using CO3109_BE.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace CO3109_BE.Repository.Component.RollerChain
{
    public interface Ixich_con_lanRepository: IRepository<xich_con_lan>
    {
        public Task<xich_con_lan?> GetDataByOtherProp(decimal findNumber, String attribute);
    }
    public class xich_con_lanRepository : Repository<xich_con_lan>, Ixich_con_lanRepository
    {
        public xich_con_lanRepository(IOptions<MongoDbSettings> settings) : base(settings, "xich_con_lan")
        {

        }
        public async Task<xich_con_lan?> GetDataByOtherProp(decimal findNumber, String attribute)
        {
            return await _collection.Find(Builders<xich_con_lan>.Filter.Eq(attribute, findNumber)).FirstOrDefaultAsync();
        }
    }
}
