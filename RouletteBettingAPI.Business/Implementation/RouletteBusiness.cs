using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.CrossCutting.Interfaces;
using RouletteBettingAPI.CrossCutting.Model;
using RouletteBettingAPI.DataAccess.ImplementationRouletteData;
using System;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Business.Implementation
{
    public class RouletteBusiness : IRouletteBusiness
    {        
        private readonly IConfiguration _configuration;
        private readonly IRedisCachingStorage _redisStorageCache;
        public RouletteBusiness(IConfiguration configuration, IRedisCachingStorage redisStorage)
        {
            _configuration = configuration;
            _redisStorageCache = redisStorage;
            //_redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<long> CreateRouletteBusiness()
        {
            var accessToDataAccess = new RouletteDataAccess(_configuration);
            var getIdOfRouletteCreated = await accessToDataAccess.createRouletteDataAccess();
            return getIdOfRouletteCreated;
        }

        public async void RouletteOpeningByIDBusiness(int idRoulette) {
            var accessToDataAccess = new RouletteDataAccess(_configuration);
            await accessToDataAccess.ChangeStatusToOpeningRouletteDataAcces(idRoulette : idRoulette);
            RouletteModel getObjectRoulette = await accessToDataAccess.GetRouletteObjectById(idRoulette: idRoulette);
            var saveRouletteOpeningInCache = _redisStorageCache.AddRouletteObjectToRedis(getObjectRoulette);

        }
    }
}
