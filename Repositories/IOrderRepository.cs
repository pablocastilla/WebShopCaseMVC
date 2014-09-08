using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="articles">List of articles in the order.</param>
        /// <returns></returns>
        Order SaveOrder(string userId, List<Article> articles);
    }
}
