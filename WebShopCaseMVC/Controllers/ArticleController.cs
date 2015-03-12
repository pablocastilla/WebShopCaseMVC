using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Model;
using WebShopCaseMVC.Properties;

namespace WebShopCaseMVC.Controllers
{
    /// <summary>
    /// REST api for managing articles. It uses the same repositories than the web page.
    /// </summary>
    public class ArticleController : ApiController
    {
        // GET: api/Article
        public IEnumerable<Article> Get(int page,int pageSize)
        {
                        

            if(pageSize<Settings.Default.PAGESIZE)
                pageSize = Settings.Default.PAGESIZE;

                   

            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            XElement xelement = XElement.Load(path + @"\\Articles.xml", LoadOptions.None);
            var xmlArticlesNodes = xelement.Elements("Article");


            var articles = xmlArticlesNodes.Skip((page - 1) * pageSize).Take(pageSize).
                                            Select(a => new Article()
                                            {
                                                Id = (int)a.Element("Id"),
                                                Name = (string)a.Element("Name"),
                                                Description = (string)a.Element("Description"),
                                                TotalWithoutVAT = (double)a.Element("TotalWithoutVAT"),
                                                TotalWithVAT = (double)a.Element("TotalWithVAT")
                                            });



            return articles;
                      
        }

        // GET: api/Article/5
        public Article Get(int id)
        {
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            XElement xelement = XElement.Load(path + @"\\Articles.xml", LoadOptions.None);
            var xmlArticlesNodes = xelement.Elements("Article");

            var articles = xmlArticlesNodes.
                                            Select(a => new Article()
                                            {
                                                Id = (int)a.Element("Id"),
                                                Name = (string)a.Element("Name"),
                                                Description = (string)a.Element("Description"),
                                                TotalWithoutVAT = (double)a.Element("TotalWithoutVAT"),
                                                TotalWithVAT = (double)a.Element("TotalWithVAT")
                                            });


            return articles.First(a => a.Id == id);

           
        }

        // POST: api/Article
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Article/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Article/5
        public void Delete(int id)
        {
        }
    }
}
