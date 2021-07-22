using RouletteBettingAPI.CrossCutting.Model;

namespace RouletteBettingAPI.Business.Interfaces
{
    public interface IValidParametersRequestEndpoints
    {
        string checkParametersOfBetRequest(RequestBetRouletteModel requestBetRouletteModel);
    }
}
