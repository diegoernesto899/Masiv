using System.Threading.Tasks;

namespace RouletteBettingAPI.DataAccess.InterfaceRouletteData
{
    public interface IRouletteDataAccess
    {
        Task<long> createRouletteDataAccess();
        Task<int> ChangeStatusToOpeningRouletteDataAcces(int idRoulette);
    }
}
