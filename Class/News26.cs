using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cogenNuGetNews.Class.HomePage
{
    public class News26 
    {
        public Models.HomePage.News26 Send()
        {
            try
            {
                Models.HomePage.News26 news26 = new Models.HomePage.News26();
                using (Data.DCCogenNugetContent dc = new Data.DCCogenNugetContent())
                {
                    var resultCustomers = (from table in dc.Customers
                                           where
                                           table.iCodeCustomer == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeCustomer"]) &&
                                           table.iAktifMi == 1
                                           select table).FirstOrDefault();

                    var resultNews = (from tableNews in dc.Pages
                                      where
                                        tableNews.iActive == 1 &&
                                        tableNews.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                        tableNews.iCodePages == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"])
                                      select tableNews).FirstOrDefault();

                    var resultBlog = (from tableBlog in dc.Pages
                                      where
                                        tableBlog.iActive == 1 &&
                                        tableBlog.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                        tableBlog.iCodePages == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeBlog"])
                                      select tableBlog).FirstOrDefault();

                    if (resultNews != null && resultNews != null)
                    {
                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            news26.cSubTitle = resultCustomers.cCustomerFooter;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cSeo))
                        {
                            news26.cUrl = resultNews.cSeo;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cTitle))
                        {
                            news26.cTitle = resultNews.cTitle;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(3).OrderBy(x => x.iOrder).ToList();

                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            news26.listModel = new List<Models.HomePage.News26.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.News26.ListModel news26ListModel = new Models.HomePage.News26.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cSayfaResmi))
                                {
                                    news26ListModel.cImage = resultPageList[i].cSayfaResmi;
                                }
                                if (resultPageList[i].dRecordDateTime != null)
                                {
                                    news26ListModel.dNewDate = Convert.ToDateTime(resultPageList[i].dRecordDateTime);
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    news26ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    news26ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    news26ListModel.cDescription = resultPageList[i].cPageDescription;
                                }

                                news26.listModel.Add(news26ListModel);
                            }
                        }
                    }
                }
                return news26;
            }
            catch
            {
                return null;
            }
        }
    }
}