using System.Data;

namespace RandevuNokta.Search.Core.Helpers
{
   public interface IConnectionHelper
    {
        IDbConnection GetOpenAppointmentConnection();
    }
}
