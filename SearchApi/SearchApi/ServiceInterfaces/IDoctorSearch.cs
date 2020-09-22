using System.Collections.Generic;
using System.Threading.Tasks;
using RandevuNokta.Search.Api.Models;
using RandevuNokta.Search.Api.Models.CoreModels;
using RandevuNokta.Search.Api.Models.DTO;

namespace RandevuNokta.Search.Api.ServiceInterfaces
{
    public interface IDoctorSearch
    {
        Task<List<DoctorC>> Search(SearchParameters parameters);
    }
}