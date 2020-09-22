using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Configuration;
using RandevuNokta.Search.Api.Helper;
using RandevuNokta.Search.Api.Models.CoreModels;
using RandevuNokta.Search.Api.Models.DTO;
using RandevuNokta.Search.Api.Models.Response;
using RandevuNokta.Search.Api.ServiceInterfaces;

namespace RandevuNokta.Search.Api.Services
{
    public class SolrSearch : ISolrSearch
    {
        private readonly IConfiguration _configuration;

        public SolrSearch(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AutocompleteResponse> GetDoctorAutocomplete(string term, int? cityId, int? districtId)
        {
            var response = new AutocompleteResponse();

            var q = "q=*:*";
            var districtAndCityQuery = "content_type:parentDocument";
            
            var fqList = new List<string>();

            if (cityId.HasValue && districtId.HasValue)
            {
                districtAndCityQuery = $"(CityId:{cityId} AND DistrictId:{districtId})";
            }
            fqList.Add($"{{!join from=Id fromIndex=clinic_core to=Clinic}}{districtAndCityQuery}");
            fqList.Add(
                $"_query_:\"TermToken:{term}^2 SurnameToken:{term}^1\" OR _query_:\"{{!parent which='-_nest_path_:* *:*'}}TermToken:{term}\"");

            var fq = "fq=" + string.Join("&fq=", fqList);
            var fl =
                $"fl=Id,Name,Surname,Image,UniqueKey,Branch,Disease,Treatment,[child childFilter=TermToken:{term} limit=100]";

            var query = $"{q}&{fq}&{fl}";
            var doctors = await SolrDataHelper.GetSolrDoctorData(query, "suggest", _configuration);
            var clinics = await GetClinicList(term, cityId, districtId);

            var branchesList = doctors.Select(x => x.Branch);
            var treatmentsList = doctors.Select(x => x.Treatment);
            var diseasesList = new List<List<DiseaseC>>();
            var branchList = new List<BranchC>();
            var diseaseList = new List<DiseaseC>();
            var treatmentList = new List<TreatmentC>();

            foreach (var branches in branchesList)
            {
                if (branches == null || branches.Count <= 0) continue;
                branchList.AddRange(branches);
                diseasesList = branches.Select(x => x.Disease).ToList();
            }

            foreach (var diseases in diseasesList)
            {
                if (diseases == null || diseases.Count <= 0) continue;
                diseaseList.AddRange(diseases);
            }

            foreach (var treatments in treatmentsList)
            {
                if (treatments == null || treatments.Count <= 0) continue;
                treatmentList.AddRange(treatments);
            }

            response.Doctors = doctors.Adapt<List<DoctorDTO>>().GroupBy(x => x.Id).Select(x => x.First()).ToList();
            response.Branches = branchList.Adapt<List<DynamicSolrDTO>>().GroupBy(x => x.Id).Select(x => x.First())
                .ToList();
            response.Treatments = treatmentList.Adapt<List<DynamicSolrDTO>>().GroupBy(x => x.Id).Select(x => x.First())
                .ToList();
            response.Diseases = diseaseList.Adapt<List<DynamicSolrDTO>>().GroupBy(x => x.Id).Select(x => x.First())
                .ToList();
            response.Clinics = clinics.Adapt<List<DynamicSolrDTO>>().GroupBy(x => x.Id).Select(x => x.First()).ToList();

            return response;
        }

        private async Task<List<ClinicC>> GetClinicList(string term, int? cityId, int? districtId)
        {
            var fq = string.Empty;
            var fqList = new List<string>();
            var queryList = new List<string>();
            
            var q = $"q={term}";
            queryList.Add(q);

            if (cityId.HasValue)
                fqList.Add($"CityId:{cityId}");
            if (cityId.HasValue)
                fqList.Add($"DistrictId:{districtId}");

            if (fqList.Any())
            {
                fq = "fq=" + string.Join(" AND ", fqList);
                queryList.Add(fq);
            }
            
            var fl = $"fl=Id,Name,Image,UniqueKey";
            queryList.Add(fl);

            var query = string.Join("&", queryList);
            
            return await SolrDataHelper.GetSolrClinicData(query, "suggest", _configuration);
        }
    }
}