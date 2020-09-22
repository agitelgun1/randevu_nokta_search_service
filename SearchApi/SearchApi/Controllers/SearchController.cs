using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandevuNokta.Search.Api.Models.Response;
using RandevuNokta.Search.Api.ServiceInterfaces;

namespace RandevuNokta.Search.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISolrSearch _solrSearch;

        public SearchController(ISolrSearch solrSearch)
        {
            _solrSearch = solrSearch;
        }

        [HttpPost("autocomplete")]
        public async Task<AutocompleteResponse> AutoComplete(string searchText,int? cityId,int? districtId)
        {
            return await _solrSearch.GetDoctorAutocomplete(searchText, cityId, districtId);
        }
    }
}