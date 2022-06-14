using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TakeHome_FE
{
    public class SearchResult
    {
        public int id { get; set; }
        public int LocationOnPage { get; set; }
        public string URL { get; set; }
        public string URLDecoded
        {
            get
            {
                return HttpUtility.UrlDecode(URL);
            }
        }
    }
}