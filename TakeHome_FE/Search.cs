using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TakeHome_FE
{
    public class Search
    {
        //Properties
        public string KeywordString { get; set; }
        public string KeywordsConverted
        {
            get
            {
                return HttpUtility.UrlEncode(KeywordString);
            }
        }
        public string InputURL { get; set; }
        public int ResultsToCheck { get; set; }
        public Constants.SearchEngine Engine { get; set; }
        public string RawWebpage { get; set; }
        public List<SearchResult> Results { get; set; }


        //Private Methods
        private string SetSearchEngineURL(Constants.SearchEngine engine)
        {
            switch (engine)
            {
                case Constants.SearchEngine.Google:
                    return $"https://www.google.com/search?num={ResultsToCheck}&q={KeywordsConverted}";

                default:
                    break;
            }
            return "";
        }

        private string SetSearchEngineParseKey(Constants.SearchEngine engine)
        {
            switch (engine)
            {
                case Constants.SearchEngine.Google:
                    return "kCrYT\"><a href";

                default:
                    break;
            }
            return "";
        }

        private void ParseRawWebpage()
        {
            string resultEntryKey = SetSearchEngineParseKey(Engine); //This indicates that a search result has started
            int resultEntryKeyIndex = 0; //This is the index where the search result starts

            string urlEntryKey = "http"; //This indicates that a search result URL has started
            int urlEntryKeyIndex = 0; //This is the index where the search result URL starts

            string urlExitKey = "&"; //This indicates that a search result URL has ended
            int urlExitKeyIndex = 0; //This is the index where the search result URL ends

            int overallResultCounter = 1;
            int matchingResultCounter = 1;

            while (resultEntryKeyIndex != -1)
            {
                //Find each search result
                resultEntryKeyIndex = RawWebpage.IndexOf(resultEntryKey, resultEntryKeyIndex);

                //Stop at each search result and extract URL
                if (resultEntryKeyIndex != -1)
                {
                    //Find the entry point and exit point for the URL
                    urlEntryKeyIndex = RawWebpage.IndexOf(urlEntryKey, resultEntryKeyIndex);
                    urlExitKeyIndex = RawWebpage.IndexOf(urlExitKey, urlEntryKeyIndex);

                    //Extract the URL from the result
                    string result = RawWebpage.Substring(urlEntryKeyIndex, urlExitKeyIndex - urlEntryKeyIndex);
                    result = result.Replace(resultEntryKey, "");

                    //Test if search result URL contains input URL. If yes, add to storage
                    if (result.Contains(InputURL))
                    {
                        SearchResult searchResult = new SearchResult()
                        {
                            id = matchingResultCounter,
                            URL = result,
                            LocationOnPage = overallResultCounter,
                        };
                        Results.Add(searchResult);
                        matchingResultCounter++;
                    }

                    //Increment index and counter
                    resultEntryKeyIndex++;
                    overallResultCounter++;
                }

            }
        }


        //Public Methods
        public List<SearchResult> Run()
        {
            //Build search engine URL
            string searchEngineURL = SetSearchEngineURL(Engine);

            //Get raw webpage
            RawWebpage = Managers.WebManager.GetRawWebpage(searchEngineURL);

            //Parse results from webpage
            ParseRawWebpage();

            //Return
            return Results;
        }

        //Constructor
        public Search()
        {
            Results = new List<SearchResult>();
        }

    }
}
