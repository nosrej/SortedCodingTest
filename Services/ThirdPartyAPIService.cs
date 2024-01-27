using SortedCodingTest.Responses;

namespace SortedCodingTest.Services
{
    public class ThirdPartyAPIService
    {
        private readonly HttpClient _httpClient;
        private const int _defaultCount = 10;

        public ThirdPartyAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ThirdPartyRainfallAPIServiceResponse> GetRainfallReadingsAsync(int statationId, int? count)
        {
            var response = await _httpClient.GetAsync($"https://environment.data.gov.uk/flood-monitoring/id/stations/{statationId}/readings?_sorted&_limit={count ?? _defaultCount}");
            
            var result = new ThirdPartyRainfallAPIServiceResponse
            {
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                Content = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };

            return result;
        }
    }
}
