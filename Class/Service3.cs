using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Service2.Class.HomePage
{
    public class Service3 
    {
        public Models.HomePage.Service3 Send()
        {
            try
            {
                Models.HomePage.Service3 service3 = new Models.HomePage.Service3();
                using (Data.DCContent dc = new Data.DCContent())
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
                        if (!String.IsNullOrEmpty(resultPage.cArkaPlan))
                        {
                            service3.cArkaPlan = resultPage.cArkaPlan;
                        }
                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            service3.cSubTitle = resultCustomers.cCustomerFooter;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cTitle))
                        {
                            service3.cTitle = resultPage.cTitle;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cPageDescription))
                        {
                            service3.cDescription = resultPage.cPageDescription;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cSeo))
                        {
                            service3.cUrl = resultPage.cSeo;
                        }
                      

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(4).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            service3.listModel = new List<Models.HomePage.Service3.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.Service3.ListModel Service3ListModel = new Models.HomePage.Service3.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    Service3ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    Service3ListModel.cDescription = resultPageList[i].cPageDescription;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    Service3ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cIcon))
                                {
                                    Service3ListModel.cIcon = resultPageList[i].cIcon;
                                }

                                service3.listModel.Add(Service3ListModel);
                            }
                        }
                    }
                }
                return service3;
            }

            catch
            {
                return null;
            }
        }
    }
}
