using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Class.HomePage
{
    public class News 
    {
        public Models.HomePage.News Send()
        {
            try
            {
                Models.HomePage.News news = new Models.HomePage.News();
                using (Data.DCContent dc = new Data.DCContent())
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

                    if (resultNews != null && resultBlog != null)
                    {
                        if (!String.IsNullOrEmpty(resultCustomers.cCustomerFooter))
                        {
                            news.cSubTitle = resultCustomers.cCustomerFooter;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cUrl))
                        {
                            news.cUrl = resultNews.cUrl;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cTitle))
                        {
                            news.cTitle = resultNews.cTitle;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(4).OrderBy(x => x.iOrder).ToList();

                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            news.listModel = new List<Models.HomePage.News.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.News.ListModel newsListModel = new Models.HomePage.News.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cPageImage))
                                {
                                    newsListModel.cImage = resultPageList[i].cPageImage;
                                }
                                if (resultPageList[i].dRecordDateTime != null)
                                {
                                    newsListModel.dNewDate = Convert.ToDateTime(resultPageList[i].dRecordDateTime);
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    newsListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cUrl))
                                {
                                    newsListModel.cUrl = resultPageList[i].cUrl;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cDescription))
                                {
                                    newsListModel.cDescription = resultPageList[i].cDescription;
                                }

                                news.listModel.Add(newsListModel);
                            }
                        }
                    }
                }
                return news;
            }
            catch
            {
                return null;
            }
        }
    }
}


