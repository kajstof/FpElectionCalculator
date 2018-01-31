using System;
using System.Linq;
using System.Net.Http;

namespace FpElectionCalculator
{
    public class RawCommunication
    {
        private bool xml;

        public RawCommunication(bool xml = false)
        {
            this.xml = xml;
        }

        private string getHttp(string queryString)
        {
            string url = $"http://webtask.future-processing.com:8069/{queryString}";
            var httpClient = new HttpClient();
            if (xml)
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
            }
            return httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public string getCandidates()
        {
            return getHttp("candidates");
        }

        public string getPeopleDisallowedToVote()
        {
            return getHttp("blocked");
        }
    }
}
