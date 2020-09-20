using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Class.HomePage
{
    public class News30
    {
        public Models.HomePage.News30 Send()
        {
            try
            {
                Models.HomePage.News30 news30 = new Models.HomePage.News30();
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

                        var resultPageList = (from table in dc.Pages
                                              where
                                              table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                              table.iParent == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeNews"]) &&
                                              table.iActive == 1
                                              select table).Skip(0).Take(5).OrderBy(x => x.iOrder).ToList();
                        if (resultPageList != null && resultPageList.Count > 0)
                        {
                            news30.listModel = new List<Models.HomePage.News30.ListModel>();

                            for (int i = 0; i < resultPageList.Count; i++)
                            {
                                Models.HomePage.News30.ListModel news30ListModel = new Models.HomePage.News30.ListModel();

                                if (!String.IsNullOrEmpty(resultPageList[i].cSayfaResmi))
                                {
                                    news30ListModel.cImage = resultPageList[i].cSayfaResmi;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cTitle))
                                {
                                    news30ListModel.cTitle = resultPageList[i].cTitle;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cPageDescription))
                                {
                                    news30ListModel.cDescription = resultPageList[i].cPageDescription;
                                }
                                if (!String.IsNullOrEmpty(resultPageList[i].cSeo))
                                {
                                    news30ListModel.cUrl = resultPageList[i].cSeo;
                                }
                                if (resultPageList[i].dRecordDateTime != null)
                                {
                                    news30ListModel.dNewDate = Convert.ToDateTime(resultPageList[i].dRecordDateTime);
                                }
                                if (resultPageList[i].iCount != null)
                                {
                                    news30ListModel.iOkunma = Convert.ToInt32(resultPageList[i].iCount);
                                }
                                if (!String.IsNullOrEmpty(resultNews.cTitle))
                                {
                                    news30ListModel.cKategori = resultNews.cTitle;
                                }

                                news30.listModel.Add(news30ListModel);
                            }
                        }
                    }
                }
                return news30;
            }
            catch
            {
                return null;
            }
        }
    }
}
