using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Models.HomePage
{
    public class News
    {
        public string cTitle { get; set; }
        public List<ListModel> listModel { get; set; }

        public class ListModel
        {
            public string cTitle { get; set; }
            public string cUrl { get; set; }
            public string cImage { get; set; }
            public string cDescription { get; set; }
            public int iRead { get; set; }
            public string cCategory { get; set; }
            public DateTime dNewDate { get; set; }
        }
    }
}
