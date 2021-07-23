using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteBettingAPI.CrossCutting.Model
{
    public class RequestBetRouletteModel
    {
        public int numberBet { get; set; }
        public string colorBet { get; set; }
        public double moneyBet { get; set; }
        public int idRoulette { get; set; }
        public int idCustomer { get; set; }
    }
}
