using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteBettingAPI.CrossCutting.Model
{
    public class RequestBetRouletteModel
    {
        public int numberBetRoulette { get; set; }
        public string colorBetRoulette { get; set; }
        public double stakeValue { get; set; }
        public int idRoulette { get; set; }
    }
}
