using RouletteBettingAPI.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Business.Interfaces
{
    public interface IRouletteBusiness
    {
        Task<RouletteModel> CreateRouletteRedisBusiness();
    }
}
