using System;
using System.Linq;
using System.Net.Http;

namespace FpElectionCalculator
{
    public class RawCommunication
    {
        private static string getHttp(string queryString)
        {
            string url = $"http://webtask.future-processing.com:8069/{queryString}";
            return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
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
