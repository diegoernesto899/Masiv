using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RouletteBettingAPI.CrossCutting.Model;
using RouletteBettingAPI.DataAccess.InterfaceRouletteData;
using System;
using System.Threading.Tasks;

namespace RouletteBettingAPI.DataAccess.ImplementationRouletteData
{
    public class RouletteDataAccess : IRouletteDataAccess
    {
        private IConfiguration _configuration;
        public RouletteDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetDateTimeNowInUtcFormat()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");
        }
        private string GetConnectionStringFromAppSettings()
        {
            return _configuration.GetSection("ConnectionString:ConnectionDbMysql").Value;
        }
        private RouletteModel mappingRouletteDto(MySqlDataReader readResponseObject)
        {
            var RouletteObject = new RouletteModel();
            while (readResponseObject.Read())
            {
                RouletteObject.idRoulette = readResponseObject.GetInt32(0);
                RouletteObject.statusIsOpenRoulette = readResponseObject.GetBoolean(1);
                RouletteObject.dateCreationRoulette = readResponseObject.GetDateTime(2);
                RouletteObject.dateLastUpdateRoulette = readResponseObject.GetDateTime(3);
            }

            return RouletteObject;
        }

        public async Task<long> CreateRouletteDataAccess()
        {
            try
            {
                string ConnectionStringDB = GetConnectionStringFromAppSettings();
                var OpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
                var dateCreateRoulette = GetDateTimeNowInUtcFormat();
                string queryToInsertRoulette = string.Format("INSERT INTO ROULETTES(statusIsOpenRoulette,dateCreationRoulette,dateOpeningRoulette,dateClosingRoulette)" +
                                                             "values ('{0}','{1}','{2}','{3}')", false, dateCreateRoulette, null, null);
                MySqlCommand commandInsertRouletteInDB = new MySqlCommand(queryToInsertRoulette, OpenConectionMySql);
                await commandInsertRouletteInDB.ExecuteNonQueryAsync();
                long GetIdRouletteCreated = commandInsertRouletteInDB.LastInsertedId;

                return GetIdRouletteCreated;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<object> ChangeStatusToOpeningRouletteDataAcces(int idRoulette)
        {
            try
            {
                string ConnectionStringDB = GetConnectionStringFromAppSettings();
                var GetOpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
                var dateOfOpeningRoulette = GetDateTimeNowInUtcFormat();
                string queryTochangeStatusOfRoulette = string.Format("UPDATE ROULETTES SET statusIsOpenRoulette = 1 ,dateOpeningRoulette = '{0}' where idRoulette = {1}", dateOfOpeningRoulette, idRoulette);
                MySqlCommand commandUpdateStatusOpeniningRoulette = new MySqlCommand(queryTochangeStatusOfRoulette, GetOpenConectionMySql);

                return await commandUpdateStatusOpeniningRoulette.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<RouletteModel> GetRouletteObjectById(int idRoulette)
        {
            try
            {
                string ConnectionStringDB = GetConnectionStringFromAppSettings();
                var GetOpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
                string queryToGetRouletteById = string.Format("select idRoulette,statusIsOpenRoulette,dateCreationRoulette, dateOpeningRoulette, dateClosingRoulette from ROULETTES where idRoulette = {0}", idRoulette);
                MySqlCommand commandGetObjectRoulette = new MySqlCommand(queryToGetRouletteById, GetOpenConectionMySql);
                await commandGetObjectRoulette.ExecuteScalarAsync();
                using MySqlDataReader readResponseObject = commandGetObjectRoulette.ExecuteReader();
                var RouletteObject = mappingRouletteDto(readResponseObject);

                return RouletteObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<long> CreateBetDataAccess(RequestBetRouletteModel betRequest)
        {
            try
            {
                string ConnectionStringDB = GetConnectionStringFromAppSettings();
                var OpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
                var dateMadeBet = GetDateTimeNowInUtcFormat();
                string queryToInsertBet = string.Format("INSERT INTO BETS(numberBet, colorBet, dateMadeBet, moneyBet, winnerBet, moneyWinnerBet, ROULETTES_idRoulette, CUSTOMERS_idCustomers)" +
                    "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    betRequest.numberBet, betRequest.colorBet, dateMadeBet, betRequest.moneyBet, false, 0, betRequest.idRoulette, betRequest.idCustomer);
                MySqlCommand commandInsertBetInDB = new MySqlCommand(queryToInsertBet, OpenConectionMySql);
                await commandInsertBetInDB.ExecuteNonQueryAsync();
                long GetIdBetCreated = commandInsertBetInDB.LastInsertedId;

                return GetIdBetCreated;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

