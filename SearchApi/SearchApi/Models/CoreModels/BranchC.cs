using System.Collections.Generic;

namespace RandevuNokta.Search.Api.Models.CoreModels
{
    public class BranchC : BaseCoreModel
    {
        public BranchC()
        {
            content_type = "parentDocument";
        }

        public string content_type { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<DiseaseC> Disease { get; set; }
    }
}