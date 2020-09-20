using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cogenNuGetService2.Class.HomePage
{
    public class Service11
    {
        public Models.HomePage.Service11 Send()
        {
            try
            {
                Models.HomePage.Service11 service11 = new Models.HomePage.Service11();
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

                        if (!String.IsNullOrEmpty(resultPage.cTitle))
                        {
                            service11.cTitle = resultPage.cTitle;
                        }

                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            service11.cSubTitle = resultCustomers.cCustomerFooter;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(4).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            service11.listModel = new List<Models.HomePage.Service11.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.Service11.ListModel Service11ListModel = new Models.HomePage.Service11.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cSayfaResmi))
                                {
                                    Service11ListModel.cImage = resultPageList[i].cSayfaResmi;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    Service11ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    Service11ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    Service11ListModel.cDescription = resultPageList[i].cPageDescription;
                                }
                                service11.listModel.Add(Service11ListModel);
                            }
                        }
                    }
                }
                return service11;
            }

            catch
            {
                return null;
            }
        }
    }
}