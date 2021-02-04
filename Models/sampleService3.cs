using System;
using System.Collections.Generic;

namespace Service.Models.HomePage
{
    public class Service
    {
        public string cTitle { get; set; }
        public List<ListModel> listModel { get; set; }
        public class ListModel
        {
            public string cTitle { get; set; }
            public string cImage { get; set; }
            public string cPrice { get; set; }
            public string cUrl { get; set; }
            public string cServiceName { get; set; }

            public List<ListModel.ListItem> ProductList { get; set; }

            public class ListItem
            {
                public string cServiceName { get; set; }
                public string cServiceDescription { get; set; }
                public string cImagePad { get; set; }
                public string cURL { get; set; }
                public int iOrder { get; set; }
            }
        }
    }
}


