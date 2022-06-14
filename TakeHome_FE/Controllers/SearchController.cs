using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeHome_FE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<SearchResult> Get(string keywords, string url, int resultsToCheck, Constants.SearchEngine searchEngine)
        {
            List<SearchResult> results = new List<SearchResult>();

            //Validate inputs
            if (string.IsNullOrEmpty(keywords) || string.IsNullOrEmpty(url) || resultsToCheck < 1)
            {
                return results;
            }

            //Setup search object
            Search search = new Search()
            {
                KeywordString = keywords.ToLower(),
                InputURL = url.ToLower(),
                ResultsToCheck = resultsToCheck,
                Engine = searchEngine,
                
            };

            //Run search
            results = search.Run();
            return results;
        }
    }
}
