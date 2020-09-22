using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Configuration;
using RandevuNokta.Search.Api.Helper;
using RandevuNokta.Search.Api.Models;
using RandevuNokta.Search.Api.Models.CoreModels;
using RandevuNokta.Search.Api.Models.DTO;
using RandevuNokta.Search.Api.ServiceInterfaces;

namespace RandevuNokta.Search.Api.Services
{
    public class DoctorSearch : IDoctorSearch
    {
        private readonly IConfiguration _configuration;
        public DoctorSearch(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<DoctorC>> Search(SearchParameters parameters)
        {
            var query = new StringBuilder();

            var solrQField = SolrQueryHelper.BuildQuery(parameters);
            var solrFqField = SolrQueryHelper.BuildFilterQueries(parameters);
            var solrSortField = SolrQueryHelper.GetSelectedSort(parameters);
            var solrFieldList = SolrQueryHelper.GetFieldList(parameters);

            query.Append(solrQField);
            query.Append(solrFieldList);
            query.Append(solrFqField);
            query.Append(solrSortField);

            if (parameters.PageIndex > 0)

            {
                query.Append($"&start={parameters.PageIndex}");
            }

            if (parameters.PageSize > 0)

            {
                query.Append($"&rows={parameters.PageSize}");
            }
            
            var response = await SolrDataHelper.GetSolrDoctorData(query.ToString(), parameters.Handler, _configuration);
            
            return response;
        }

        
    }
}