using System;
using System.Collections.Generic;

namespace RandevuNokta.Search.Api.Models.CoreModels
{
    public class DoctorC : BaseCoreModel
    {
        public DoctorC()
        {
            Branch = new List<BranchC>();
            Insurance = new List<InsuranceC>();
            Language = new List<LanguageC>();
            Profession = new List<ProfessionC>();
            Treatment = new List<TreatmentC>();
            Experience = new List<ExperienceC>();
            Clinic = new List<int>();
            content_type = "parentDocument";
        }

        public string content_type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Graduated { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public int AppointmentCount { get; set; }
        public decimal Score { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string ProfessionalExperience { get; set; }
        public decimal Price { get; set; }

        public List<BranchC> Branch { get; set; }
        public List<InsuranceC> Insurance { get; set; }
        public List<LanguageC> Language { get; set; }
        public List<ProfessionC> Profession { get; set; }
        public List<TreatmentC> Treatment { get; set; }
        public List<ExperienceC> Experience { get; set; }
        public List<int> Clinic { get; set; }
    }
}