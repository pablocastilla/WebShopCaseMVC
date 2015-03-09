using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Repositories;
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
            IArticleRepository domainRepository = new ArticleRepository();

            

            if(pageSize<Settings.Default.PAGESIZE)
                pageSize = Settings.Default.PAGESIZE;

            var result = domainRepository.GetArticles(page, pageSize);                       

            return result;
        }

        // GET: api/Article/5
        public Article Get(int id)
        {
            IArticleRepository domainRepository = new ArticleRepository();

            var result = domainRepository.GetArticle(id);

            return result;
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
