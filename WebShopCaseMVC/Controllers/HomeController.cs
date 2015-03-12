using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Model;
using WebShopCaseMVC.Properties;

namespace WebShopCaseMVC.Controllers
{
    public class HomeController : Controller
    {
        
        /// <summary>
        /// Main page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            XElement xelement = XElement.Load(path + @"\\Articles.xml", LoadOptions.None);
            var xmlArticlesNodes = xelement.Elements("Article");

             var totalArticles = xmlArticlesNodes.Count();
           

            //the number of pages are calculated in the server
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = totalArticles / Settings.Default.PAGESIZE;

            if (Session["Cart"] == null)
            { 
                ViewBag.TotalItemInCart = 0;
                
            }
            else
            {
                ViewBag.TotalItemInCart = ((List<Article>)Session["Cart"]).Count();

            }

            

            return View();
        }

        /// <summary>
        /// Returns an HTML view with a list of articles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ArticleList(int id)
        {                         


            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            XElement xelement = XElement.Load(path + @"\\Articles.xml", LoadOptions.None);
            var xmlArticlesNodes = xelement.Elements("Article");


            var articles = xmlArticlesNodes.Skip((id - 1) * Settings.Default.PAGESIZE).Take(Settings.Default.PAGESIZE).
                                            Select(a => new Article()
                                            {
                                                Id = (int)a.Element("Id"),
                                                Name = (string)a.Element("Name"),
                                                Description = (string)a.Element("Description"),
                                                TotalWithoutVAT = (double)a.Element("TotalWithoutVAT"),
                                                TotalWithVAT = (double)a.Element("TotalWithVAT")
                                            });



           

            ViewBag.CurrentPage = id;

            return View(articles);
        }

       
        /// <summary>
        /// Adds an item to the cart. It uses the session object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult addItemToCart(string id, string quantity)
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


            var article= articles.First(a => a.Id == Convert.ToInt32(id));


            if (Session["Cart"] == null)
            { 
                Session["Cart"] = new List<Article>();
                
            }

            var cartInSession = (List<Article>)Session["Cart"];

            for (int i = 0; i < Convert.ToInt32(quantity); i++)
            {
                cartInSession.Add(article);

            }

            return Json(new { Count = cartInSession.Count() ,Status = 1, Message = "Success" });
        }

        /// <summary>
        /// Deletes an item from the session object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult removeItemFromCart(string id)
        {           
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<Article>();

            }

            var cartInSession = (List<Article>)Session["Cart"];

            cartInSession.RemoveAll(a => a.Id == Convert.ToInt32(id));

            return Json(new { Count = cartInSession.Count(), Status = 1, Message = "Success" });
        }


        /// <summary>
        /// List with the articles in the cart.
        /// </summary>
        /// <returns></returns>
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                ViewBag.TotalItemInCart = 0;

            }
            else
            {
                ViewBag.TotalItemInCart = ((List<Article>)Session["Cart"]).Count();

            }


            var cart = (List<Article>)(Session["Cart"]??new List<Article>());

            return View(cart);
        }

        /// <summary>
        /// Returns the list in the cart for the final approval.
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOut()
        {

            if (Session["Cart"] == null)
            {
                ViewBag.TotalItemInCart = 0;

            }
            else
            {
                ViewBag.TotalItemInCart = ((List<Article>)Session["Cart"]).Count();

            }

            var cartInSession = (List<Article>)(Session["Cart"] ?? new List<Article>());

            var totalWithoutVAT = cartInSession.Sum(a => a.TotalWithoutVAT);
            var totalWithVAT = cartInSession.Sum(a => a.TotalWithVAT);

            ViewBag.TotalWithoutVAT = totalWithoutVAT;

            ViewBag.TotalWithVAT = totalWithVAT;
            ViewBag.TotalVAT = totalWithVAT - totalWithoutVAT;

            return View(cartInSession);
        }

        /// <summary>
        /// Calls when the user wants to save the order.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ThankYou()
        {

            //add the information to the database about the order.
            if (Session["Cart"] == null)
            {
                //failure control here
            }

            var cartInSession = (List<Article>)Session["Cart"];

           

            Order order = null;

            using (var webShopModel = new WebShopModel())
            {
                webShopModel.Database.CreateIfNotExists();

                webShopModel.Database.Initialize(true);
                order = new Order()
                {
                    UserID = User.Identity.Name,
                    OrderLines = cartInSession.Select(a => new OrderLine()
                    {
                        ArticleID = a.Id,
                        Quantity = 1
                    }).ToList(),

                    TotalWithoutVAT = cartInSession.Sum(a => a.TotalWithoutVAT),
                    TotalWithVAT = cartInSession.Sum(a => a.TotalWithVAT)


                };

                webShopModel.Orders.Add(order);

                webShopModel.SaveChanges();
            }

           

           
            @ViewBag.OrderId = order.OrderId;
            Session["Cart"] = null;
            ViewBag.TotalItemInCart = 0;

            return View();
        }

        /// <summary>
        /// For the RESTFul external page.
        /// </summary>
        /// <returns></returns>
        public ActionResult UsingREST()
        {
            if (Session["Cart"] == null)
            {
                ViewBag.TotalItemInCart = 0;

            }
            else
            {
                ViewBag.TotalItemInCart = ((List<Article>)Session["Cart"]).Count();

            }

            return View();
        }

        public ActionResult UsingRESTDetails()
        {
            return View();
        }
    }
}