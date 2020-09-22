using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandevuNokta.Search.Api.Models
{
    public class SolrResponse<T>
    {
        public int status { get; set; }
        public int QTime { get; set; }
        public SolrResponseObj<T> response { get; set; }
    }

    public class SolrResponseObj<T>
    {
        public int numFound { get; set; }
        public int start { get; set; }
        public List<T> docs { get; set; }
    }
}
