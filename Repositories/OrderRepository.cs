using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {

        }

        /// <summary>
        /// Creates a new order using entity framework. It creates an order, order lines and store it in database.
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <param name="articles">List of articles in the order.</param>
        public Order SaveOrder(string userId, List<Article> articles)
        {
            Order order = null;

            using (var webShopModel = new WebShopModel())
            {
                webShopModel.Database.CreateIfNotExists();

                webShopModel.Database.Initialize(true);
                order = new Order()
                {
                    UserID = userId,
                    OrderLines = articles.Select(a => new OrderLine()
                    {
                        ArticleID = a.Id,
                        Quantity = 1
                    }).ToList(),

                    TotalWithoutVAT = articles.Sum(a => a.TotalWithoutVAT),
                    TotalWithVAT = articles.Sum(a => a.TotalWithVAT)


                };

                webShopModel.Orders.Add(order);

                webShopModel.SaveChanges();
            }

            return order;
        }
    }
}
