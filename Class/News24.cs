using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cogenNuGetNews.Models.HomePage
{
    public class News24
    {
        public string cTitle { get; set; }
        public string cDescription { get; set; }
        public string cUrl { get; set; }
        public List<ListModel> listModel { get; set; }

        public class ListModel
        {
            public string cImage { get; set; }
            public DateTime dNewDate { get; set; }
            public string cTitle { get; set; }
            public string cDescription { get; set; }
            public string cUrl { get; set; }
            public int iOkunma { get; set; }

        }
    }
}