using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandevuNokta.Search.Api.Models.DTO;
using RandevuNokta.Search.Api.Models.Response;
using RandevuNokta.Search.Api.Services;

namespace RandevuNokta.Search.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly ICoreSearch _coreSearch;

        public ClinicController(ICoreSearch coreSearch)
        {
            _coreSearch = coreSearch;
        }

        [HttpGet("clinicsearch")]
        public async Task<Response<List<ClinicWithDoctors>>> ClinicSearch(string term, int district, int city,
            double lat, double lng, int pageIndex, int pageSize)
        {
            var response = new Response<List<ClinicWithDoctors>>();

            try
            {
                var result =
                    await _coreSearch.SearchClinicListByTerm(term, district, city, lat, lng, pageIndex, pageSize);
                response.Success = true;
                response.StatusCode = (int) HttpStatusCode.OK;
                response.Result = result;
                response.TotalCount = result.Count;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                response.Result = new List<ClinicWithDoctors>();
                response.ErrorDescription = e.Message;
            }

            return response;
        }
    }
}