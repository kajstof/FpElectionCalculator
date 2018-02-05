using System.Net.Http;

namespace FpElectionCalculator.Domain.Services
{
    public class WebserviceRawCommunication
    {
        private readonly bool _useXml;

        public WebserviceRawCommunication(bool useXml = false)
        {
            _useXml = useXml;
        }

        private string GetHttp(string queryString)
        {
            string url = $"http://webtask.future-processing.com:8069/{queryString}";
            HttpClient httpClient = new HttpClient();
            if (_useXml)
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
            }

            return httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public string GetCandidates()
        {
            return GetHttp("dbCandidates");
        }

        public string GetPeopleDisallowedToVote()
        {
            return GetHttp("blocked");
        }
    }
}
