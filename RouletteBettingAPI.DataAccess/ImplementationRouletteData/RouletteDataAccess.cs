using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RouletteBettingAPI.DataAccess.InterfaceRouletteData;
using System;
using System.Threading.Tasks;

namespace RouletteBettingAPI.DataAccess.ImplementationRouletteData
{
    public class RouletteDataAccess : IRouletteDataAccess
    {
        private IConfiguration _configuration;
        private string GetDateTimeNowInUtcFormat()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");
        }

        public RouletteDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<long> createRouletteDataAccess()
        {
            string GetConnectionStringFromAppSetings = _configuration.GetSection("ConnectionString:ConnectionDbMysql").Value;
            var OpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: GetConnectionStringFromAppSetings);
            var dateCreateRoulette = GetDateTimeNowInUtcFormat();
            string queryToInsertRoulette = string.Format("INSERT INTO Roulette(statusIsOpenRoulette,dateCreationRoulette,dateLastUpdateRoulette) values ('{0}','{1}','{2}')", false, dateCreateRoulette, dateCreateRoulette);
            MySqlCommand commandInsertRouletteInDB = new MySqlCommand(queryToInsertRoulette, OpenConectionMySql);
            await commandInsertRouletteInDB.ExecuteNonQueryAsync();
            long GetIdRouletteCreated = commandInsertRouletteInDB.LastInsertedId;

            return GetIdRouletteCreated;
        }
        public async Task<int> ChangeStatusToOpeningRouletteDataAcces(int idRoulette)
        {
            string GetConnectionStringFromAppSetings = _configuration.GetSection("ConnectionString:ConnectionDbMysql").Value;
            var GetOpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: GetConnectionStringFromAppSetings);
            var dateOfOpeningRoulette = GetDateTimeNowInUtcFormat();
            string queryTochangeStatusOfRoulette = string.Format("UPDATE Roulette SET statusIsOpenRoulette = 1 ,dateLastUpdateRoulette = '{0}' where idRoulette = {1}", dateOfOpeningRoulette, idRoulette);
            MySqlCommand commandUpdateStatusOpeniningRoulette = new MySqlCommand(queryTochangeStatusOfRoulette, GetOpenConectionMySql);

            return await commandUpdateStatusOpeniningRoulette.ExecuteNonQueryAsync();
        }

        
    }
}

