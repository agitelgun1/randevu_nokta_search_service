namespace RandevuNokta.Search.Core.Models
{
    public class ErrorLogModel
    {
        public string Host { get; set; }
        public string MethodType { get; set; }
        public string MethodName { get; set; }
        public string QueryString { get; set; }
        public string Body { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public long ResponseTime { get; set; }
        public string ContextGuid { get; set; }
    }
}
