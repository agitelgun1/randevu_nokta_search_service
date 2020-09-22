using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RandevuNokta.Search.Api.Models.CoreModels;

namespace RandevuNokta.Search.Api.Helper
{
    public static class SolrDataHelper
    {
        public static async Task<List<DoctorC>> GetSolrDoctorData(string query, string handler, IConfiguration configuration)
        {
            var response = new List<DoctorC>();

            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var responseData = await client.GetAsync($"{configuration["SolrUrlDoctorCore"]}{handler}?{query}");
                var json = responseData.Content.ReadAsStringAsync().Result;
                dynamic data = JToken.Parse(json);

                if (data["responseHeader"] != null && data["responseHeader"]["status"] != 0)
                    throw new Exception("Search Engine Response Error:" + data["Error"]["Message"]);

                foreach (var doc in data["response"]["docs"])
                {
                    var doctor = await GenerateModelHelper.GenerateDoctorCModel(doc);

                    response.Add(doctor);
                }

                return response;
            }
        }
        
        public static async Task<List<ClinicC>> GetSolrClinicData(string query,string handler, IConfiguration configuration)
        {
            var response = new List<ClinicC>();

            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var responseData = await client.GetAsync($"{configuration["SolrUrlClinicCore"]}{handler}?{query}");
                var json = responseData.Content.ReadAsStringAsync().Result;
                dynamic data = JToken.Parse(json);

                if (data["responseHeader"] != null && data["responseHeader"]["status"] != 0)
                    throw new Exception("Search Engine Response Error:" + data["Error"]["Message"]);

                foreach (var doc in data["response"]["docs"])
                {
                    var clinic = GenerateModelHelper.GenerateClinicCModel(doc);

                    response.Add(clinic);
                }

                return response;
            }
        }
    }
}