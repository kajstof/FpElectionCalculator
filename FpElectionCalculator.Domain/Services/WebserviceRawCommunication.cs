using System.Net.Http;

namespace FpElectionCalculator.Domain.Services
{
    public static class WebserviceRawCommunication
    {
        private static string GetHttp(string queryString, bool useXml)
        {
            string url = $"http://webtask.future-processing.com:8069/{queryString}";
            var httpClient = new HttpClient();
            if (useXml)
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
            }
            return httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public static string GetCandidates(bool useXml = false)
        {
            return GetHttp("candidates", useXml);
        }

        public static string GetPeopleDisallowedToVote(bool useXml = false)
        {
            return GetHttp("blocked", useXml);
        }
    }
}
