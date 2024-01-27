using SortedCodingTest.Models;

namespace SortedCodingTest.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<ErrorDetail> Detail { get; set; }

        public ErrorResponse(string message, List<ErrorDetail> errorDetails)
        {
            Message = message;
            Detail = errorDetails;
        }
    }
}
