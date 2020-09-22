using System.Threading.Tasks;
using RandevuNokta.Search.Api.Models;

namespace RandevuNokta.Search.Api.Services
{
    public interface IErrorService
    {
        Task<bool> InsertErrorLog(ErrorLog errorLog);
    }
}