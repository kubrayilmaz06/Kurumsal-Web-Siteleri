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
                        if (!String.IsNullOrEmpty(resultPage.cBackground))
                        {
                            service.cBackground = resultPage.cBackground;
                        }
                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            service.cSubTitle = resultCustomers.cCustomerFooter;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cTitle))
                        {
                            service.cTitle = resultPage.cTitle;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cDescription))
                        {
                            service.cDescription = resultPage.cDescription;
                        }
                        if (!String.IsNullOrEmpty(resultPage.cUrl))
                        {
                            service.cUrl = resultPage.cUrl;
                        }
                      

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeService"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(5).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            service.listModel = new List<Models.HomePage.Service.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.Service.ListModel ServiceListModel = new Models.HomePage.Service.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    ServiceListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cDescription))
                                {
                                    ServiceListModel.cDescription = resultPageList[i].cDescription;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cUrl))
                                {
                                    ServiceListModel.cUrl = resultPageList[i].cUrl;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cIcon))
                                {
                                    ServiceListModel.cIcon = resultPageList[i].cIcon;
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



