using RandevuNokta.Search.Api.Enums;

namespace RandevuNokta.Search.Api.Models
{
    public class SortQuery
    {
        public string FieldName { get; set; }
        public SortOrder order { get; set; }
    }
}