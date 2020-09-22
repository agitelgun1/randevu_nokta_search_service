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
    public class DoctorController : ControllerBase
    {
        private readonly ICoreSearch _coreSearch;

        public DoctorController(ICoreSearch coreSearch)
        {
            _coreSearch = coreSearch;
        }
        
        [HttpGet("doctorsearch")]
        public async Task<Response<List<DoctorWithClinic>>> DoctorSearch(string term, int district, int city, double lat, double lng)
        {
            var response = new Response<List<DoctorWithClinic>>();

            try
            {
                var result = await _coreSearch.SearchDoctorListByTerm(term, district, city, lat, lng);
                response.Success = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Result = result;
                response.TotalCount = result.Count;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Result = new List<DoctorWithClinic>();
                response.ErrorDescription = e.Message;
            }
            return response;
        }
    }
}