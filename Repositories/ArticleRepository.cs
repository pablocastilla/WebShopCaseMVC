using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Model;

namespace Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public ArticleRepository()
        {

        }

        /// <summary>
        /// Gets every article paging
        /// </summary>
        /// <param name="page">page wanted (1-N)</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public IEnumerable<Article> GetArticles(int page, int pageSize)
        {
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            XElement xelement = XElement.Load(path+@"\\Articles.xml",LoadOptions.None);
            var xmlArticlesNodes = xelement.Elements("Article");            
            

            var articles = xmlArticlesNodes.Skip((page - 1) * pageSize).Take(pageSize).
                                            Select(a => new Article() 
                                            {
                                                Id=(int)a.Element("Id"),
                                                Name = (string)a.Element("Name"),
                                                Description = (string)a.Element("Description"),
                                                TotalWithoutVAT = (double)a.Element("TotalWithoutVAT"),
                                                TotalWithVAT = (double)a.Element("TotalWithVAT")
                                            });



            return articles;
        }


        /// <summary>
        /// Gets an article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public Article GetArticle(int articleId)
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


            return articles.First(a => a.Id == articleId);
        }

        /// <summary>
        /// Counts the total articles in the xml.
        /// </summary>
        /// <returns></returns>
        public int CountTotalArticles()
        {
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            XElement xelement = XElement.Load(path + @"\\Articles.xml", LoadOptions.None);
            var xmlArticlesNodes = xelement.Elements("Article");

            return xmlArticlesNodes.Count();
        }
    }
}
