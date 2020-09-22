using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace RandevuNokta.Search.Core.Helpers
{
    public class ConnectionHelper : IConnectionHelper
    {
        private readonly IConfiguration _configuration;

        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetOpenAppointmentConnection()
        {
            var connStr = _configuration["ConnectionStrings:Appointment"];
            var connection = new NpgsqlConnection(connStr);
            connection.Open();
            return connection;
        }
    }
}
