using System.Collections.Generic;

namespace RandevuNokta.Search.Api.Models.CoreModels
{
    public class TreatmentC: BaseCoreModel
    {
        public TreatmentC()
        {
            content_type = "parentDocument";
        }

        public string content_type { get; set; }
        public string Name { get; set; }
        public List<DiseaseC> Disease { get; set; }
    }
}