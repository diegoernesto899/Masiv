using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.CrossCutting.Model;

namespace RouletteBettingAPI.Business.Implementation
{
    public class ValidParametersRequestEndpoints: IValidParametersRequestEndpoints
    {
        private const double maxStakeValue = 1000;
        private const double minStakeValue = 1000;
        public string checkParametersOfBetRequest(RequestBetRouletteModel requestBetRouletteModel)
        {
            var errorFoundInValidations = string.Empty;
            if (requestBetRouletteModel.numberBetRoulette >= 0 &&
                requestBetRouletteModel.numberBetRoulette <= 36)
                errorFoundInValidations = "-The number of bet is invalid.";
            if (requestBetRouletteModel.colorBetRoulette != string.Empty &&
                requestBetRouletteModel.colorBetRoulette.ToLower().Equals("red") ||
                requestBetRouletteModel.colorBetRoulette.ToLower().Equals("black"))
                errorFoundInValidations = errorFoundInValidations + "-The color sended is invalid.";
            if (requestBetRouletteModel.stakeValue > minStakeValue && requestBetRouletteModel.stakeValue < maxStakeValue)
                errorFoundInValidations = errorFoundInValidations + "The stake value is invalid";
            return errorFoundInValidations;
        }
    }
}
