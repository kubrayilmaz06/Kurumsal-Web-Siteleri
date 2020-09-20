using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cogenNuGetService2.Class.HomePage
{
    public class Service2
    {
        public Models.HomePage.Service2 Send()
        {
            try
            {
                Models.HomePage.Service2 service2 = new Models.HomePage.Service2();
                using (Data.DCCogenNuGetContent dc = new Data.DCCogenNuGetContent())
                {
                    var resultCustomers = (from table in dc.Customers
                                           where
                                           table.iCodeCustomer == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeCustomer"]) &&
                                           table.iAktifMi == 1
                                           select table).FirstOrDefault();

                    var resultPage = (from table in dc.Pages
                                      where
                                      table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                      table.iCodePages == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                      table.iActive == 1
                                      select table).FirstOrDefault();

                    if (resultCustomers != null && resultPage != null)
                    {
                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            service2.cSubTitle = resultCustomers.cCustomerFooter;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cTitle))
                        {
                            service2.cTitle = resultPage.cTitle;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cPageDescription))
                        {
                            service2.cDescription = resultPage.cPageDescription;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cSeo))
                        {
                            service2.cUrl = resultPage.cSeo;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(3).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            service2.listModel = new List<Models.HomePage.Service2.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.Service2.ListModel Service2ListModel = new Models.HomePage.Service2.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    Service2ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    Service2ListModel.cDescription = resultPageList[i].cPageDescription;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    Service2ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cIcon))
                                {
                                    Service2ListModel.cIcon = resultPageList[i].cIcon;
                                }

                                service2.listModel.Add(Service2ListModel);
                            }
                        }
                    }
                }
                return service2;
            }

            catch
            {
                return null;
            }
        }
    }
}