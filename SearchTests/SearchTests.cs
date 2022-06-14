using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TakeHome_FE;

namespace SearchTests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void InfoTrackSearch()
        {
            //Test the example case of the prompt

            //Setup Search
            Search search = new Search()
            {
                KeywordString = "efiling integration",
                InputURL = "www.infotrack.com",
                ResultsToCheck = 100,
                Engine = Constants.SearchEngine.Google,
            };

            //Get actual results
            List<SearchResult> results = search.Run();

            //Set expected results
            SearchResult sr1 = new SearchResult()
            {
                id = 1,
                LocationOnPage = 1,
                URL = "https://www.infotrack.com/blog/explained-integrated-efiling-and-continued-innovation/",

            };
            SearchResult sr2 = new SearchResult()
            {
                id = 2,
                LocationOnPage = 2,
                URL = "https://www.infotrack.com/clio/",
            };
            List<SearchResult> expectedResults = new List<SearchResult> { sr1, sr2 };

            //Assert
            Assert.AreEqual(expectedResults.Count, results.Count);
            Assert.AreEqual(expectedResults[0].URL, results[0].URL);
            Assert.AreEqual(expectedResults[1].URL, results[1].URL);
        }

        [TestMethod]
        public void IrrelevantKeywordsForURL()
        {
            //Test when the input URL is irrelevant for the input keywords

            //Setup Search
            Search search = new Search()
            {
                KeywordString = "efiling integration",
                InputURL = "www.nba.com",
                ResultsToCheck = 100,
                Engine = Constants.SearchEngine.Google,
            };

            //Get actual results
            List<SearchResult> results = search.Run();

            //Set expected results
            List<SearchResult> expectedResults = new List<SearchResult>(); //Expect empty

            //Assert
            Assert.AreEqual(expectedResults.Count, results.Count);
            Assert.IsTrue(expectedResults.Count == 0);
        }

    }
}
