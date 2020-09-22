using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Configuration;
using RandevuNokta.Search.Api.Models;
using RandevuNokta.Search.Api.Models.CoreModels;
using RandevuNokta.Search.Api.Models.DTO;
using RandevuNokta.Search.Api.ServiceInterfaces;

namespace RandevuNokta.Search.Api.Services
{
    public class CoreSearch : ICoreSearch
    {
        private readonly IConfiguration _configuration;
        private readonly IClinicSearch _clinicSearch;
        private readonly IDoctorSearch _doctorSearch;

        public CoreSearch(IConfiguration configuration, IClinicSearch clinicSearch, IDoctorSearch doctorSearch)
        {
            _configuration = configuration;
            _clinicSearch = clinicSearch;
            _doctorSearch = doctorSearch;
        }

        public async Task<List<DoctorWithClinic>> SearchDoctorListByTerm(string term, int district, int city,
            double lat = 0, double lng = 0)
        {
            var response = new List<DoctorWithClinic>();

            //########### Klinik Arama Parametreleri ##############
            var clinicSearchParam = new SearchParameters();
            clinicSearchParam.Handler = "select";
            var clinicFilterData = new Dictionary<string, dynamic>
            {
                {"CityId", city},
                {"DistrictId", district}
            };
            clinicSearchParam.FilterBy = clinicFilterData;

            var clinicList = await _clinicSearch.Search(clinicSearchParam);
            var clinicIds = clinicList.Select(x => x.Id).ToList();
            var doctorClinics = $"({string.Join(' ', clinicIds)})";

            //########### Doctor Arama Parametreleri ##############
            var doctorSearchParam = new SearchParameters();
            doctorSearchParam.Handler = "select";
            var doctorSearchTerm = new Dictionary<string, dynamic>
            {
                {"{!parent which='-_nest_path_:* *:*'}TermToken", term}
            };
            var doctorFilterData = new Dictionary<string, dynamic>
            {
                {"Clinic", doctorClinics}
            };

            doctorSearchParam.SearchFor = doctorSearchTerm;
            doctorSearchParam.FilterBy = doctorFilterData;
            doctorSearchParam.FieldList = "*, [child limit=100]";
            var doctorList = await _doctorSearch.Search(doctorSearchParam);

            foreach (var doctor in doctorList)
            {
                var doctorWithClinic = doctor.Adapt<DoctorWithClinic>();
                doctorWithClinic.Clinic = clinicList.FirstOrDefault(x => doctor.Clinic.Contains(x.Id));

                response.Add(doctorWithClinic);
            }

            return response;
        }

        public async Task<List<ClinicWithDoctors>> SearchClinicListByTerm(string term, int district, int city,
            double lat = 0, double lng = 0, int pageIndex = 0, int pageSize = 4)
        {
            var response = new List<ClinicWithDoctors>();

            //########### Klinik Arama Parametreleri ##############
            var clinicSearchParam = new SearchParameters();
            clinicSearchParam.Handler = "select";
            var clinicFilterData = new Dictionary<string, dynamic>
            {
                {"CityId", city},
                {"DistrictId", district}
            };
            clinicSearchParam.FilterBy = clinicFilterData;

            clinicSearchParam.PageIndex = pageIndex;
            clinicSearchParam.PageSize = pageSize;

            var clinicList = await _clinicSearch.Search(clinicSearchParam);
            var clinicIds = clinicList.Select(x => x.Id).ToList();
            var doctorClinics = $"({string.Join(' ', clinicIds)})";

            //########### Doctor Arama Parametreleri ##############
            var doctorSearchParam = new SearchParameters();
            doctorSearchParam.Handler = "select";

            if (!string.IsNullOrEmpty(term))
            {
                var doctorSearchTerm = new Dictionary<string, dynamic>
                {
                    {"{!parent which='-_nest_path_:* *:*'}TermToken", term}
                };
                doctorSearchParam.SearchFor = doctorSearchTerm;
            }

            var doctorFilterData = new Dictionary<string, dynamic>
            {
                {"Clinic", doctorClinics}
            };


            doctorSearchParam.FilterBy = doctorFilterData;
            doctorSearchParam.FieldList = "*, [child limit=100]";
            var doctorList = await _doctorSearch.Search(doctorSearchParam);

            foreach (var clinic in clinicList)
            {
                var clinicWithDoctor = clinic.Adapt<ClinicWithDoctors>();
                clinicWithDoctor.Doctors = new List<DoctorC>();

                clinicWithDoctor.Doctors = doctorList.Where(x => x.Clinic.Contains(clinic.Id)).ToList();

                response.Add(clinicWithDoctor);
            }

            return response;
        }
    }
}