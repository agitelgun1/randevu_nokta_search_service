using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RandevuNokta.Search.Api.Models.Response;

namespace RandevuNokta.Search.Api.ServiceInterfaces
{
    public interface ISolrSearch
    {
        Task<AutocompleteResponse> GetDoctorAutocomplete(string term, int? cityId, int? districtId);
    }
}