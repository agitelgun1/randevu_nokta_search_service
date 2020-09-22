using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RandevuNokta.Search.Api.Models.CoreModels;

namespace RandevuNokta.Search.Api.Helper
{
    public static class GenerateModelHelper
    {
        private static BranchC GenerateBranchCModel(dynamic item)
        {
            var response = new BranchC
            {
                Name = Convert.ToString(item["Name"]),
                Image = Convert.ToString(item["Image"]),
                Id = Convert.ToInt32(Convert.ToString(item["Id"])),
                UniqueKey = Convert.ToString(item["UniqueKey"]),
                Disease = new List<DiseaseC>()
            };


            return response;
        }

        private static TreatmentC GenerateTreatmentCModel(dynamic item)
        {
            var response = new TreatmentC
            {
                Name = Convert.ToString(item["Name"]),
                Id = Convert.ToInt32(Convert.ToString(item["Id"])),
                UniqueKey = Convert.ToString(item["UniqueKey"]),
                Disease = new List<DiseaseC>()
            };


            return response;
        }

        private static List<DiseaseC> GenerateDiseaseCModel(dynamic item)
        {
            var response = new List<DiseaseC>();

            if (item != null && item is JArray)
            {
                foreach (var disease in item)
                {
                    var diseaseObj = new DiseaseC
                    {
                        Name = Convert.ToString(disease["Name"]),
                        Image = Convert.ToString(disease["Image"]),
                        Id = Convert.ToInt32(Convert.ToString(disease["Id"])),
                        UniqueKey = Convert.ToString(disease["UniqueKey"])
                    };

                    response.Add(diseaseObj);
                }
            }
            else if (item != null && !(item is JArray))
            {
                var diseaseObj = new DiseaseC
                {
                    Name = Convert.ToString(item),
                    Image = Convert.ToString(item),
                    Id = Convert.ToInt32(Convert.ToString(item)),
                    UniqueKey = Convert.ToString(item)
                };

                response.Add(diseaseObj);
            }

            return response;
        }


        public static async Task<DoctorC> GenerateDoctorCModel(dynamic item)
        {
            var response = new DoctorC
            {
                Name = Convert.ToString(item["Name"]),
                Surname = Convert.ToString(item["Surname"]),
                Id = Convert.ToInt32(Convert.ToString(item["Id"])),
                Image = Convert.ToString(item["Image"]),
                UniqueKey = Convert.ToString(item["UniqueKey"])
            };
            
            if (item["Clinic"] != null && item["Clinic"] is JArray)
            {
                foreach (int id in item["Clinic"])
                {
                    response.Clinic.Add(id);
                }
            }

            if (item["Branch"] != null && item["Branch"] is JArray)
            {
                foreach (var branch in item["Branch"])
                {
                    var branchObj = GenerateBranchCModel(branch);
                    branchObj.Disease = GenerateDiseaseCModel(branch["Disease"]);
                    response.Branch.Add(branchObj);
                }
            }
            else if (item["Branch"] != null)
            {
                var branchObj = GenerateBranchCModel(item["Branch"]);
                var branch = item["Branch"];
                branchObj.Disease = GenerateDiseaseCModel(branch["Disease"]);
                response.Branch.Add(branchObj);
            }

            if (item["Treatment"] != null && item["Treatment"] is JArray)
            {
                foreach (var treatment in item["Treatment"])
                {
                    var treatmentObj = GenerateTreatmentCModel(treatment);
                    treatmentObj.Disease = GenerateDiseaseCModel(treatment["Disease"]);
                    response.Treatment.Add(treatmentObj);
                }
            }
            else if (item["Treatment"] != null)
            {
                var treatmentObj = GenerateTreatmentCModel(item["Treatment"]);
                var treatment = item["Treatment"];
                treatmentObj.Disease = GenerateDiseaseCModel(treatment["Disease"]);
                response.Treatment.Add(treatmentObj);
            }

            return response;
        }

        public static ClinicC GenerateClinicCModel(dynamic item)
        {
            var response = new ClinicC
            {
                Name = Convert.ToString(item["Name"]),
                Id = Convert.ToInt32(Convert.ToString(item["Id"])),
                UniqueKey = Convert.ToString(item["UniqueKey"]),
                Phone = Convert.ToString(item["Phone"]),
                Address = Convert.ToString(item["Address"]),
                Score = Convert.ToDouble(Convert.ToString(item["Score"])),
                CityId = Convert.ToInt32(Convert.ToString(item["CityId"])),
                DistrictId = Convert.ToInt32(Convert.ToString(item["DistrictId"])),
                LogoUrl = Convert.ToString(item["LogoUrl"]),
                Capacity = Convert.ToInt32(Convert.ToString(item["Capacity"])),
                VendorId = Convert.ToInt32(Convert.ToString(item["VendorId"])),
                Lat = Convert.ToDouble(Convert.ToString(item["Lat"])),
                Lng = Convert.ToDouble(Convert.ToString(item["Lng"]))
            };

            return response;
        }
    }
}