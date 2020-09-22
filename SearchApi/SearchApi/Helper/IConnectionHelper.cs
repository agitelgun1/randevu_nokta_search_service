using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace RandevuNokta.Search.Api.Helper
{
    public interface IConnectionHelper
    {
        IDbConnection GetOpenAppointmentConnection();

    }

    public class ConnectionHelper : IConnectionHelper
    {
        private readonly IConfiguration _configuration;
        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetOpenAppointmentConnection()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var connStr = _configuration["ConnectionStrings:Appointment"];
            var connection = new NpgsqlConnection(connStr);
           
            return connection;
        }

    }
}