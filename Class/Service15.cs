using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cogenNuGetService2.Class.HomePage
{
    public class Service15
    {
        public Models.HomePage.Service15 Send()
        {
            try
            {
                Models.HomePage.Service15 service = new Models.HomePage.Service15();
                service.list = new List<Models.HomePage.Service15.List>();

                using (Data.DCCogenNuGetContent dc = new Data.DCCogenNuGetContent())
                {
                    var resultService = (from tableService in dc.Pages
                                         where
                                           tableService.iActive == 1 &&
                                           tableService.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                           tableService.iCodePages == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"])
                                         select tableService).FirstOrDefault();


                    if (resultService != null)
                    {
                        if (resultService.cTitle != null)
                        {
                            service.cTitle = resultService.cTitle;
                        }
                        if (resultService.cSeo != null)
                        {
                            service.cUrl = resultService.cSeo;
                        }

                        service.list = (from tableServiceList in dc.Pages
                                        where
                                          tableServiceList.iActive == 1 &&
                                          tableServiceList.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                          tableServiceList.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                          tableServiceList.iCodeModule == 10
                                        select new Models.HomePage.Service15.List
                                        {
                                            cServiceName = tableServiceList.cTitle,
                                            cServiceDescription = tableServiceList.cPageDescription,
                                            cImagePad = tableServiceList.cSayfaResmi,
                                            cURL = tableServiceList.cSeo,
                                            iOrder = (int)tableServiceList.iOrder,
                                            iCodePages = tableServiceList.iCodePages
                                        }).OrderBy(x => x.iOrder).ToList();

                        if (service.list != null)
                        {
                            for (int i = 0; i < service.list.Count; i++)
                            {
                                service.list[i].UrunListesi = (from tableServiceList in dc.Pages
                                                               where
                                                                 tableServiceList.iActive == 1 &&
                                                                 tableServiceList.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                                                 tableServiceList.iParent == (int)service.list[i].iCodePages &&
                                                                 tableServiceList.iCodeModule == 2
                                                               select new Models.HomePage.Service15.List.ListItem
                                                               {
                                                                   cServiceName = tableServiceList.cTitle,
                                                                   cServiceDescription = tableServiceList.cPageDescription,
                                                                   cImagePad = tableServiceList.cSayfaResmi,
                                                                   cURL = tableServiceList.cSeo,
                                                                   iOrder = (int)tableServiceList.iOrder
                                                               }).OrderBy(x => x.iOrder).ToList();
                            }

                        }

                    }
                }

                return service;
            }
            catch
            {
                return null;
            }
        }

    }

}