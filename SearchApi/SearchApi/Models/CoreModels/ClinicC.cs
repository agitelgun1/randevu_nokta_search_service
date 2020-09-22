using System.Collections.Generic;
using Newtonsoft.Json;

namespace RandevuNokta.Search.Api.Models.CoreModels
{
    public class ClinicC : BaseCoreModel
    {
        public ClinicC()
        {
            Branch = new List<BranchC>();
            PaymentMethods = new List<long>();
            Insurance = new List<InsuranceC>();
            ContentType = "parentDocument";
        }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        
        [JsonProperty("phone")]
        public string Phone { get; set; } = "";
        
        [JsonProperty("address")]
        public string Address { get; set; } = "";
        
        [JsonProperty("email")]
        public string Email { get; set; } = "";
        
        [JsonProperty("fax")]
        public string Fax { get; set; } = "";
        
        [JsonProperty("score")]
        public double Score { get; set; }
        
        [JsonProperty("cityId")]
        public int CityId { get; set; }
        
        [JsonProperty("districtId")]
        public int DistrictId { get; set; }
        
        [JsonProperty("logourl")]
        public string LogoUrl { get; set; } = "";
        
        [JsonProperty("capacity")]
        public int Capacity { get; set; }
        
        [JsonProperty("vendorid")]
        public int VendorId { get; set; }
        
        [JsonProperty("lat")]
        public double Lat { get; set; }
        
        [JsonProperty("lng")]
        public double Lng { get; set; }
        
        public string ClinicType { get; set; } = "";
        public List<long> PaymentMethods { get; set; }

        public List<BranchC> Branch { get; set; }
        public List<InsuranceC> Insurance { get; set; }
        public string Coordinate => $"{Lat.ToString().Replace(',', '.')},{Lng.ToString().Replace(',', '.')}";
    }
}