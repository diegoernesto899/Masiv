using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.CrossCutting.Model;

namespace RouletteBettingAPI.Business.Implementation
{
    public class ValidParametersRequestEndpoints: IValidParametersRequestEndpoints
    {
        private const double maxStakeValue = 10000;
        private const double minStakeValue = 0;
        public string checkParametersOfBetRequest(RequestBetRouletteModel requestBetRouletteModel)
        {
            var errorFoundInValidations = string.Empty;
            if (requestBetRouletteModel.numberBet < 0 && requestBetRouletteModel.numberBet > 36)
                errorFoundInValidations = "-The number of bet is invalid.";
            //if (!requestBetRouletteModel.colorBet.Equals(string.Empty) && !requestBetRouletteModel.colorBet.ToLower().Equals("red") || !requestBetRouletteModel.colorBet.ToLower().Equals("black"))
            //    errorFoundInValidations = errorFoundInValidations + "-The color sended is invalid.";
            
            if (requestBetRouletteModel.moneyBet < minStakeValue && requestBetRouletteModel.moneyBet <= maxStakeValue)
                errorFoundInValidations = errorFoundInValidations + "The stake value is invalid";

            return errorFoundInValidations;
        }
    }
}
