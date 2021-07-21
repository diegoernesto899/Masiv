using Microsoft.Extensions.Configuration;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.DataAccess.ImplementationRouletteData;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Business.Implementation
{
    public class RouletteBusiness : IRouletteBusiness
    {
        //private readonly IDistributedCache _redisCache;
        private readonly IConfiguration _configuration;
        public RouletteBusiness(IConfiguration configuration/*, IRouletteDataAccess rouletteDataAccess , IDistributedCache cache*/)
        {
            _configuration = configuration;
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
        }


        //public async Task<RouletteModel> GetRoulette(int id)
        //{
        //    var basket = await _redisCache.GetStringAsync(id.ToString());
        //    if (string.IsNullOrEmpty(basket))
        //        return null;
        //    return JsonConvert.DeserializeObject<RouletteModel>(basket);
        //}
        //public async Task<RouletteModel> CreateRouletteRedisBusiness() {
        //    var obj = new RouletteModel();
        //    obj.DateCreateRoulette = DateTime.UtcNow;
        //    obj.IdRoulette = 1;
        //    await _redisCache.SetStringAsync(obj.IdRoulette.ToString(), JsonConvert.SerializeObject(obj));
        //    return await GetRoulette(obj.IdRoulette);
        //}
    }
}
