using RouletteBettingAPI.CrossCutting.Model;
using System.Threading.Tasks;

namespace RouletteBettingAPI.DataAccess.InterfaceRouletteData
{
    public interface IRouletteDataAccess
    {
        Task<long> createRouletteDataAccess();
        Task<object> ChangeStatusToOpeningRouletteDataAcces(int idRoulette);
        Task<RouletteModel> GetRouletteObjectById(int idRoulette);
    }
}
