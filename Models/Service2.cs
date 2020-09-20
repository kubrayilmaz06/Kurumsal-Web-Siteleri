using System.Collections.Generic;

namespace Service2.Models.HomePage
{
    public class Service2
    {
        public string cTitle { get; set; }
        public string cSubTitle { get; set; }
        public string cDescription { get; set; }
        public string cUrl { get; set; }
        public List<ListModel> listModel { get; set; }
        
        public class ListModel
        {
            public string cTitle { get; set; }
            public string cSubTitle { get; set; }
            public string cDescription { get; set; }
            public string cUrl { get; set; }
            public string cIcon { get; set; }
        }
  
    }
}
