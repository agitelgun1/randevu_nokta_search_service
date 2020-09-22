using System;

namespace RandevuNokta.Search.Api.Models.CoreModels
{
    public class ExperienceC : BaseCoreModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}