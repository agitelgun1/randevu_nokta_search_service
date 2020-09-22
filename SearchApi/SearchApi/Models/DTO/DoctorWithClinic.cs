using System.Collections.Generic;
using RandevuNokta.Search.Api.Models.CoreModels;

namespace RandevuNokta.Search.Api.Models.DTO
{
    public class DoctorWithClinic
    {
        public DoctorWithClinic()
        {
            Branch = new List<BranchC>();
            Insurance = new List<InsuranceC>();
            Language = new List<LanguageC>();
            Profession = new List<ProfessionC>();
            Treatment = new List<TreatmentC>();
            Experience = new List<ExperienceC>();
            Clinic = new ClinicC();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int AppointmentCount { get; set; }
        public decimal Score { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public ClinicC Clinic { get; set; }
        public List<BranchC> Branch { get; set; }
        public List<InsuranceC> Insurance { get; set; }
        public List<LanguageC> Language { get; set; }
        public List<ProfessionC> Profession { get; set; }
        public List<TreatmentC> Treatment { get; set; }
        public List<ExperienceC> Experience { get; set; }
    }
}