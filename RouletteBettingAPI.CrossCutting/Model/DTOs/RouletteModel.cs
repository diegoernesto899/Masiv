using System;

namespace RouletteBettingAPI.CrossCutting.Model
{
    public class RouletteModel
    {
        public int idRoulette { get; set; }
        public bool statusIsOpenRoulette { get; set; }
        public DateTime dateCreationRoulette { get; set; }
        public DateTime dateLastUpdateRoulette { get; set; }
    }
}
