using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RandevuNokta.Search.Api.Helper;
using RandevuNokta.Search.Api.Models;
using RandevuNokta.Search.Api.Models.CoreModels;
using RandevuNokta.Search.Api.ServiceInterfaces;

namespace RandevuNokta.Search.Api.Services
{
    public class ClinicSearch : IClinicSearch
    {
        private readonly IConfiguration _configuration;

        public ClinicSearch(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ClinicC>> Search(SearchParameters parameters)
        {
            var query = new StringBuilder();

            var solrQField = SolrQueryHelper.BuildQuery(parameters);
            var solrFqField = SolrQueryHelper.BuildFilterQueries(parameters);
            var solrSortField = SolrQueryHelper.GetSelectedSort(parameters);
            var solrPagination = SolrQueryHelper.GetPagination(parameters);

            query.Append(solrQField);
            query.Append(solrFqField);
            query.Append(solrSortField);
            query.Append(solrPagination);


            var response = await SolrDataHelper.GetSolrClinicData(query.ToString(), parameters.Handler, _configuration);

            return response;
        }
        
    }
}