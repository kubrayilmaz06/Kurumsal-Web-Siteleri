using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Class.HomePage
{
    public class News24
    {
        public Models.HomePage.News24 Send()
        {
            try
            {
                Models.HomePage.News24 news24 = new Models.HomePage.News24();
                using (Data.DCCogenNugetContent dc = new Data.DCCogenNugetContent())
                {
                    var resultNews = (from tableNews in dc.Pages
                                      where
                                        tableNews.iActive == 1 &&
                                        tableNews.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                        tableNews.iCodePages == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"])
                                      select tableNews).FirstOrDefault();

                    if (resultNews != null && resultNews != null)
                    {

                        if (!String.IsNullOrEmpty(resultNews.cTitle))
                        {
                            news24.cTitle = resultNews.cTitle;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cPageDescription))
                        {
                            news24.cDescription = resultNews.cPageDescription;
                        }
                        if (!String.IsNullOrEmpty(resultNews.cSeo))
                        {
                            news24.cUrl = resultNews.cSeo;
                        }

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(3).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            news24.listModel = new List<Models.HomePage.News24.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.News24.ListModel news24ListModel = new Models.HomePage.News24.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cSayfaResmi))
                                {
                                    news24ListModel.cImage = resultPageList[i].cSayfaResmi;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    news24ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    news24ListModel.cDescription = resultPageList[i].cPageDescription;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    news24ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (resultPageList[i].dRecordDateTime != null)
                                {
                                    news24ListModel.dNewDate = Convert.ToDateTime(resultPageList[i].dRecordDateTime);
                                }
                                if (resultPageList[i].iCount != null)
                                {
                                    news24ListModel.iOkunma = Convert.ToInt32(resultPageList[i].iCount);
                                }

                                news24.listModel.Add(news24ListModel);
                            }
                        }
                    }
                }
                    return news24;
            }
            catch
            {
                return null;
            }
        }
    }
}
