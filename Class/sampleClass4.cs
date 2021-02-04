using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Service.Class.HomePage
{
    public class Service
    {
        public Models.HomePage.Service Send()
        {
            try
            {
                Models.HomePage.Service service = new Models.HomePage.Service();
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

                        if (!String.IsNullOrEmpty(resultPage.cTitle))
                        {
                            service.cTitle = resultPage.cTitle;
                        }

                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            service.cSubTitle = resultCustomers.cCustomerFooter;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(4).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            service.listModel = new List<Models.HomePage.Service.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.Service.ListModel ServiceListModel = new Models.HomePage.Service.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cSayfaResmi))
                                {
                                    ServiceListModel.cImage = resultPageList[i].cSayfaResmi;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    ServiceListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    ServiceListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    ServiceListModel.cDescription = resultPageList[i].cPageDescription;
                                }
                                service.listModel.Add(ServiceListModel);
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
