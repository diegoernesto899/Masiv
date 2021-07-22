using Newtonsoft.Json;
using RouletteBettingAPI.CrossCutting.Interfaces;
using RouletteBettingAPI.CrossCutting.Model;
using StackExchange.Redis;

namespace RouletteBettingAPI.CrossCutting.Implementation
{
    public class RedisCachingStorage : IRedisCachingStorage
    {
        //    public RedisCachingStorage(IDistributedCache cache) {
        //        _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        //    }

        //    private readonly IDistributedCache _redisCache;
        private IDatabase GetDataBaseRedisCacheAWS()
        {
            string primaryEndpoint = "masiv-redis-cache.a9gxo3.ng.0001.use2.cache.amazonaws.com:6379";
            string readerEndpoint = "masiv-redis-cache-ro.a9gxo3.ng.0001.use2.cache.amazonaws.com:6379";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{primaryEndpoint},{readerEndpoint}");
            IDatabase db = redis.GetDatabase();            
            return db;
        }
        public RouletteModel GetRouletteFromRedis(int id)
        {
            var dataBaseRedis = GetDataBaseRedisCacheAWS();
            var getRouletteObject = dataBaseRedis.StringGet(id.ToString());
            if (string.IsNullOrEmpty(getRouletteObject))
                return null;
            return JsonConvert.DeserializeObject<RouletteModel>(getRouletteObject);
        }
        public RouletteModel AddRouletteObjectToRedis(RouletteModel roulette)
        {
            var dataBaseRedis = GetDataBaseRedisCacheAWS();
            dataBaseRedis.StringSet(roulette.idRoulette.ToString(), JsonConvert.SerializeObject(roulette));
            return GetRouletteFromRedis(roulette.idRoulette);
        }
    }
}
