using System;

namespace RouletteBettingAPI.Business.Model
{
    public class RouletteModel
    {
        public int IdRoulette { get; set; }
        public bool RouletteStatus { get; set; }
        public DateTime DateCreateRoulette { get; set; }
        public DateTime DateLastUpdateRoulette { get; set; }
    }
}
