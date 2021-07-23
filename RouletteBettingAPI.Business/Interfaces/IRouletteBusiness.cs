using RouletteBettingAPI.Business.Model;
using RouletteBettingAPI.CrossCutting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Business.Interfaces
{
    public interface IRouletteBusiness
    {
        //Task<RouletteModel> CreateRouletteRedisBusiness();
        Task<long> CreateRouletteBusiness();
        void RouletteOpeningByIDBusiness(int idRoulette);
        Task<string> CreateBetInRouletteBusiness(RequestBetRouletteModel betRequest);
    }
}
