using RouletteBettingAPI.CrossCutting.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteBettingAPI.CrossCutting.Interfaces
{
    public interface IRedisCachingStorage
    {
        RouletteModel GetRouletteFromRedis(int id);
        RouletteModel AddRouletteObjectToRedis(RouletteModel roulette);

    }
}
