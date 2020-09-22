using System.Collections.Generic;

namespace RandevuNokta.Search.Api.Models
{
    public class SearchParameters
    {
        public const int DefaultPageSize = 4;
 
        public SearchParameters () {
            SearchFor = new Dictionary<string, dynamic>();
            Exclude = new Dictionary<string, dynamic>();
            SortBy = new List<SortQuery>();
            FilterBy = new Dictionary<string, dynamic>();
            FieldList = string.Empty;
            PageSize = DefaultPageSize;
            PageIndex = 0;
        }
 
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IDictionary<string, dynamic> SearchFor { get; set; }
        public IDictionary<string, dynamic> Exclude { get; set; }
        public string Handler { get; set; }
        public IList<SortQuery> SortBy { get; set; }
        public string FieldList { get; set; }
        public  IDictionary<string, dynamic>  FilterBy{ get; set; }
    }
}
