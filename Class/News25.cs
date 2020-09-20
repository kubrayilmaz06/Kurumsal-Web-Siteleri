using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using News.Models.HomePage;

namespace News.Class.HomePage
{
    public class News25 
    {
        public Models.HomePage.News25 Send()
        {
            try
            {
                Models.HomePage.News25 news25 = new Models.HomePage.News25();
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
                            news25.cSubTitle = resultCustomers.cCustomerFooter;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cSeo))
                        {
                            news25.cUrl = resultNews.cSeo;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cTitle))
                        {
                            news25.cTitle = resultNews.cTitle;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(3).OrderBy(x => x.iOrder).ToList();

                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            news25.listModel = new List<Models.HomePage.News25.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.News25.ListModel news25ListModel = new Models.HomePage.News25.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cSayfaResmi))
                                {
                                    news25ListModel.cImage = resultPageList[i].cSayfaResmi;
                                }
                                if (resultPageList[i].dRecordDateTime != null)
                                {
                                    news25ListModel.dNewDate = Convert.ToDateTime(resultPageList[i].dRecordDateTime);
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    news25ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    news25ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    news25ListModel.cDescription = resultPageList[i].cPageDescription;
                                }

                                news25.listModel.Add(news25ListModel);
                            }
                        }
                    }
                }
                return news25;
            }
            catch
            {
                return null;
            }
        }
    }
}
