using System.Net;

namespace SortedCodingTest.Responses
{
    public class ThirdPartyRainfallAPIServiceResponse
    {
        public bool IsSuccessStatusCode { get; set; }
        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
