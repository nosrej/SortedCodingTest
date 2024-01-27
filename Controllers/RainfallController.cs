using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SortedCodingTest.Models;
using SortedCodingTest.Responses;
using SortedCodingTest.Services;
using System.Net;

namespace SortedCodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private readonly ThirdPartyAPIService _thirdPartyAPIService;

        public RainfallController(ThirdPartyAPIService thirdPartyAPIService)
        {
            _thirdPartyAPIService = thirdPartyAPIService;
        }

        [HttpGet("id/{stationId}/readings")]
        public async Task<RainfallReadingResponse> Get(int stationId, [FromQuery]QueryParameter parameter)
        {
            try
            {
                var thirdPartyAPIResponse = await _thirdPartyAPIService.GetRainfallReadingsAsync(stationId, parameter.Count);

                // I'm not sure what is the expected behaviour in here based from the document. So I'm just returning a generic error response.
                // If we will return this ErrorResponse, will change the return type of this method to Task<object>, so that it returns dynamic object.
                //if (!thirdPartyAPIResponse.IsSuccessStatusCode)
                //{
                //    return new ErrorResponse(thirdPartyAPIResponse.StatusCode == HttpStatusCode.NotFound
                //        ? "Not Found"
                //        : thirdPartyAPIResponse.StatusCode == HttpStatusCode.BadRequest
                //            ? "Invalid Request"
                //            : "Error", new List<ErrorDetail> { new ErrorDetail { Message = thirdPartyAPIResponse.Content, PropertyName = "" } });
                //}

                var response = new RainfallReadingResponse();

                if (thirdPartyAPIResponse.IsSuccessStatusCode)
                {
                    var dataObject = JObject.Parse(thirdPartyAPIResponse.Content)["items"];

                    response.Readings = dataObject?.Select(x => new RainfallReading
                    {
                        AmountMeasured = x["value"].Value<decimal>(),
                        DateMeasured = x["dateTime"].Value<string>()
                    }).ToList() ?? new List<RainfallReading>();
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
