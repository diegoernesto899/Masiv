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
        private string GetConnectionStringFromAppSetings()
        {
            return _configuration.GetSection("ConnectionString:ConnectionDbMysql").Value;
        }

        public async Task<long> createRouletteDataAccess()
        {
            string ConnectionStringDB = GetConnectionStringFromAppSetings();
            var OpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
            var dateCreateRoulette = GetDateTimeNowInUtcFormat();
            string queryToInsertRoulette = string.Format("INSERT INTO Roulette(statusIsOpenRoulette,dateCreationRoulette,dateLastUpdateRoulette) values ('{0}','{1}','{2}')", false, dateCreateRoulette, dateCreateRoulette);
            MySqlCommand commandInsertRouletteInDB = new MySqlCommand(queryToInsertRoulette, OpenConectionMySql);
            await commandInsertRouletteInDB.ExecuteNonQueryAsync();
            long GetIdRouletteCreated = commandInsertRouletteInDB.LastInsertedId;

            return GetIdRouletteCreated;
        }
        public async Task<object> ChangeStatusToOpeningRouletteDataAcces(int idRoulette)
        {
            string ConnectionStringDB = GetConnectionStringFromAppSetings();
            var GetOpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
            var dateOfOpeningRoulette = GetDateTimeNowInUtcFormat();
            string queryTochangeStatusOfRoulette = string.Format("UPDATE Roulette SET statusIsOpenRoulette = 1 ,dateLastUpdateRoulette = '{0}' where idRoulette = {1}", dateOfOpeningRoulette, idRoulette);
            MySqlCommand commandUpdateStatusOpeniningRoulette = new MySqlCommand(queryTochangeStatusOfRoulette, GetOpenConectionMySql);
            
            return await commandUpdateStatusOpeniningRoulette.ExecuteNonQueryAsync();            
        }

        public async Task<RouletteModel> GetRouletteObjectById(int idRoulette)
        {
            string ConnectionStringDB = GetConnectionStringFromAppSetings();
            var GetOpenConectionMySql = ConnectionDataAccess.GetMySqlConnection(mySqlConnectionString: ConnectionStringDB);
            string queryToGetRouletteById = string.Format("select idRoulette,statusIsOpenRoulette,dateCreationRoulette, dateLastUpdateRoulette from Roulette where idRoulette = {0}", idRoulette);
            MySqlCommand commandGetObjRoulette = new MySqlCommand(queryToGetRouletteById, GetOpenConectionMySql);
            var ObjectRouletteResponseDB = await commandGetObjRoulette.ExecuteScalarAsync();
            using MySqlDataReader readResponseObject = commandGetObjRoulette.ExecuteReader();
            var RouletteObject = mappingRouletteDto(readResponseObject);

            return RouletteObject;
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
    }
}

