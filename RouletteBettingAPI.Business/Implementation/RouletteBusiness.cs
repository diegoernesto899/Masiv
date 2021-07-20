using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.Business.Model;
using System;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Business.Implementation
{
    public class RouletteBusiness: IRouletteBusiness
    {
        private readonly IDistributedCache _redisCache;
        public RouletteBusiness(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<RouletteModel> GetRoulette(int id)
        {
            var basket = await _redisCache.GetStringAsync(id.ToString());
            if (string.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<RouletteModel>(basket);
        }
        public async Task<RouletteModel> CreateRouletteRedisBusiness() {
            var obj = new RouletteModel();
            obj.DateCreateRoulette = DateTime.UtcNow;
            obj.IdRoulette = 1;
            await _redisCache.SetStringAsync(obj.IdRoulette.ToString(), JsonConvert.SerializeObject(obj));
            return await GetRoulette(obj.IdRoulette);
        }
        
    }
}
