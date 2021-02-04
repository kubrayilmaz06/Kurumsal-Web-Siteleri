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
                    var resultNews = (from tableNews in dc.Pages
                                      where
                                        tableNews.iActive == 1 &&
                                        tableNews.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                        tableNews.iCodePages == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"])
                                      select tableNews).FirstOrDefault();

                    if (resultNews != null)
                    {

                        if (!String.IsNullOrEmpty(resultNews.cTitle))
                        {
                            news.cTitle = resultNews.cTitle;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cDescription))
                        {
                            news.cDescription = resultNews.cDescription;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cUrl))
                        {
                            news.cUrl = resultNews.cUrl;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(3).OrderBy(x => x.iOrder).ToList();
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
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    newsListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cDescription))
                                {
                                    newsListModel.cDescription = resultPageList[i].cDescription;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cUrl))
                                {
                                    newsListModel.cUrl = resultPageList[i].cUrl;
                                }
                                if (resultPageList[i].dRecordDateTime != null)
                                {
                                    newsListModel.dNewDate = Convert.ToDateTime(resultPageList[i].dRecordDateTime);
                                }
                                if (resultPageList[i].iCount != null)
                                {
                                    newsListModel.iRead = Convert.ToInt32(resultPageList[i].iCount);
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
