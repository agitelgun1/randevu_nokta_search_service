using System.Collections.Generic;
using System.Threading.Tasks;
using RandevuNokta.Search.Api.Models.DTO;

namespace RandevuNokta.Search.Api.Services
{
    public interface ICoreSearch
    {
        Task<List<DoctorWithClinic>> SearchDoctorListByTerm(string term, int district, int city, double lat = 0,
            double lng = 0);

        Task<List<ClinicWithDoctors>> SearchClinicListByTerm(string term, int district, int city, double lat = 0,
            double lng = 0, int pageIndex = 0, int pageSize = 0);
    }
}