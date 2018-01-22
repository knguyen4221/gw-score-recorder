using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using RDotNet;
using Newtonsoft.Json;

namespace UNFScoreRecorder
{
    class UNFModel
    {

        private HttpClient _client;
        private HttpClientHandler _handler;
        private Uri _baseAddress;
        private string _authLink;
        private string _dataAccessLink;

        public string Cookie { get; set; }
        public string cachedData { get; set; }
        
        #region Constructor
        public UNFModel()
        {
            _baseAddress = new Uri("http://game.granbluefantasy.jp");
            _handler = new HttpClientHandler() { UseCookies = false };
            _client = new HttpClient(_handler) { BaseAddress = _baseAddress };
            _authLink = "/#authentication";
            _dataAccessLink = "/teamraid035/bookmaker/content/top";
        }
        #endregion

        #region Methods
        /*
         * @parameters
         * DateTime timeStamp: when the data was gathered
         * int *Score: scores of region * 
         */
        public void update()
        { 

        }
        #endregion

        #region Helper Functions
        /*
         * Mimics the authentication process for grabnblue application in order to grab data
        */
        private void authenticate()
        {

        }

        private void updateData(DateTime timeStamp, int northScore, int southScore, int eastScore, int westScore)
        {

        }

        /*
        * Grabs information from application about current scores
        */
        private void getData()
        {

        }
        #endregion
    }
}
