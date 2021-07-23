using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.CrossCutting.Interfaces;
using RouletteBettingAPI.CrossCutting.Model;
using RouletteBettingAPI.DataAccess.ImplementationRouletteData;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Business.Implementation
{
    public class RouletteBusiness : IRouletteBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisCachingStorage _redisStorageCache;
        private readonly IDatabase _databaseRedis;
        public RouletteBusiness(IConfiguration configuration, IRedisCachingStorage redisStorage, IDatabase databaseRedis)
        {
            _configuration = configuration;
            _redisStorageCache = redisStorage;
            _databaseRedis = databaseRedis ?? throw new ArgumentNullException(nameof(databaseRedis));
        }

        private async Task<RouletteModel> getRouletteFromRedisCache(int idRoulette)
        {
            var getRouletteFromCache = await _databaseRedis.StringGetAsync(idRoulette.ToString());
            RouletteModel rouletteModelDeserialized = JsonConvert.DeserializeObject<RouletteModel>(getRouletteFromCache);

            return rouletteModelDeserialized;
        }
        public async Task<long> CreateRouletteBusiness()
        {
            var dataAccess = new RouletteDataAccess(_configuration);
            var getIdOfRouletteCreated = await dataAccess.CreateRouletteDataAccess();
            return getIdOfRouletteCreated;
        }

        public async void RouletteOpeningByIDBusiness(int idRoulette)
        {
            var dataAccess = new RouletteDataAccess(_configuration);
            await dataAccess.ChangeStatusToOpeningRouletteDataAcces(idRoulette: idRoulette);
            RouletteModel getObjectRouletteUpdated = await dataAccess.GetRouletteObjectById(idRoulette: idRoulette);
            var obj = JsonConvert.SerializeObject(getObjectRouletteUpdated);
            _databaseRedis.StringSet(idRoulette.ToString(), obj);
        }
        public async Task<string> CreateBetInRouletteBusiness(RequestBetRouletteModel betRequest)
        {
            bool ValidateCustomerHasCreditRequired = true;
            RouletteModel rouletteFoundInRedisCache = await getRouletteFromRedisCache(idRoulette: betRequest.idRoulette);
            if (ValidateCustomerHasCreditRequired && rouletteFoundInRedisCache.idRoulette != 0 && rouletteFoundInRedisCache.statusIsOpenRoulette)
            {
                var dataAccess = new RouletteDataAccess(_configuration);
                var idBetCreated = await dataAccess.CreateBetDataAccess(betRequest);
                return "Bet made created correctly, the id bet is: " + idBetCreated;
            }
            else {
                return "Bet not made, please review your bet and try again.";
            }
        }

        
    }
}
