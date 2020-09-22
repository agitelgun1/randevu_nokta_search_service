using System;

namespace RandevuNokta.Search.Core.Models
{
   public class ErrorLogDto
    {
        public int Id { get; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public string Culture { get; set; }
        public string MethodName { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string MachineName { get; set; }
        public string Host { get; set; }
        public string MethodType { get; set; }
        public string QueryString { get; set; }
        public string Body { get; set; }
        public long ResponseTime { get; set; }
        public string ContextGuid { get; set; }
    }
}
