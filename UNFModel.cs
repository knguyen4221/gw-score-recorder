using System;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using RDotNet;


namespace UNFScoreRecorder
{
    public class UNFModel
    {

        private HttpClient _client;
        private HttpClientHandler _handler;
        private Uri _baseAddress;
        private string _authLink;
        private string _dataAccessLink;

        public string cachedData { get; set; }
        
        //Grabbed from browser information
        public string Cookie { get; set; }
        public string XVERSION { get; set; }

        
        #region Constructor
        public UNFModel()
        {
            _baseAddress = new Uri("http://game.granbluefantasy.jp");
            _handler = new HttpClientHandler() { UseCookies = false };
            _client = new HttpClient(_handler) { BaseAddress = _baseAddress };
            _authLink = "/#authentication";
            _dataAccessLink = "/teamraid035/bookmaker/content/top";

        }

        public UNFModel(string Cookie, string xversion)
        {
            _baseAddress = new Uri("http://game.granbluefantasy.jp");
            _handler = new HttpClientHandler() { UseCookies = false };
            _client = new HttpClient(_handler) { BaseAddress = _baseAddress };
            _authLink = "/#authentication";
            _dataAccessLink = "/teamraid035/bookmaker/content/top";
            this.Cookie = Cookie;
            this.XVERSION = xversion;

        }
        #endregion

        #region Methods
        /*
         * @parameters
         * DateTime timeStamp: when the data was gathered
         * int *Score: scores of region * 
         */
        public async void update()
        {
            await this.authenticate();
            UNFDataEntry result = await this.getJsonData();
            this.updateData(result);
        }
        #endregion

        #region Helper Functions
        /*
         * Mimics the authentication process for grabnblue application in order to grab data
        */
        private async Task<HttpResponseMessage> authenticate()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authLink);
            request.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
            request.Headers.Add("X-VERSION", this.XVERSION);
            request.Headers.Add("Cookie", Cookie);
            HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private string updateData(UNFDataEntry dataEntry)
        {
            return dataEntry.writeDataEntry(cachedData);
        }

        /*
        * Grabs information from application about current scores
        */
        private async Task<UNFDataEntry> getJsonData()
        {
            HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Get, _dataAccessLink);
            request2.Headers.Add("Cookie", Cookie);
            request2.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36");
            //request2.Content = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = await _client.SendAsync(request2);
            response2.EnsureSuccessStatusCode();
            return parseData(response2.Content.ReadAsStringAsync().Result);
        }

        /*
         * Renders the plots
         * TODO:Implement
         */
        private void renderPlots()
        {
            throw new NotImplementedException();
        }

        private UNFDataEntry parseData(string responseContent)
        {
            if (responseContent.Length == 0)
                throw new ArgumentException("Response content == 0");
            DateTime currentTime = DateTime.Now;
            string nScore, eScore, sScore, wScore;
            Regex re = new Regex("point%22%3E([0-9]+)");
            var match = re.Match(responseContent);
            nScore = match.Groups[1].ToString();
            match = match.NextMatch();
            wScore = match.Groups[1].ToString();
            match = match.NextMatch();
            eScore = match.Groups[1].ToString();
            match = match.NextMatch();
            sScore = match.Groups[1].ToString();
            return new UNFDataEntry(currentTime, nScore, sScore, eScore, wScore);
        }

        #endregion
    }

}
