using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repositories
{
    /// <summary>
    /// Interface for retrieving the articles from a XML
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Gets every article paging
        /// </summary>
        /// <param name="page">page wanted (1-N)</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        IEnumerable<Article> GetArticles(int page, int pageSize);

        /// <summary>
        /// Gets an article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Article GetArticle(int articleId);

        /// <summary>
        /// Counts the total articles in the xml.
        /// </summary>
        /// <returns></returns>
        int CountTotalArticles();
    }
}
