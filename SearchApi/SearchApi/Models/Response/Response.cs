using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RandevuNokta.Search.Api.Models.Response
{
    public class Response<T>
    {
        public bool Success { get; set; } = true;
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public T Result { get; set; }
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public string ErrorDescription { get; set; } = "";
    }
}
