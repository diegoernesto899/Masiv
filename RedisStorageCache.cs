using System;

public class Class1
{
    private async Task<RouletteModel> GetRoulette(int id)
    {
        var basket = await _redisCache.GetStringAsync(id.ToString());
        if (string.IsNullOrEmpty(basket))
            return null;
        return JsonConvert.DeserializeObject<RouletteModel>(basket);
    }
    private async Task<RouletteModel> CreateRouletteRedisBusiness()
    {
        var obj = new RouletteModel();
        obj.DateCreateRoulette = DateTime.UtcNow;
        obj.IdRoulette = 1;
        await _redisCache.SetStringAsync(obj.IdRoulette.ToString(), JsonConvert.SerializeObject(obj));
        return await GetRoulette(obj.IdRoulette);
    }
}
