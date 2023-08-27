namespace MS.Services.Gateway.WebAPI.Infrastructure
{
    internal class HttpRequestInfo
    {
        public HttpRequestInfo()
        {
        }

        public string Host { get; set; }
        public PathString Path { get; set; }
        public string Scheme { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public Dictionary<string, string> QueryString { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Cookies { get; set; }
        public string Body { get; set; }
    }
}