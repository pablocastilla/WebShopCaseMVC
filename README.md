Implementation process: 
			I will create the model and its repositories, leaving the page for the end. I will do this way because I will be able to use the repositories in the controllers and the model in the UI scaffolds. Also, as the problem is very easy and it won’t change in the future, I haven’t implemented the unittesting. I have used StructureMap for IoC. The page is not perfect at css level or error handling, but I suppose this is a prototipe and not a final solution, also going to a final solution is possible with this code without any problem. 
			 
			In the page I show 3 ways of loading web pages using ajax, there is no ajax at "menu" level: 
			The article list is created with a partial view retrieved from the server using JQuery.ajax() and injected in the page with JQuery.html(). This is not the best solution at bandwidth level but faster in implementation. 
			The detail list is created calling to the REST API with JQuery.ajax, getting the JSON and inserting the data in the HTML using JQuery.text(). More efficient. 
			RESTApi page is implemented using AngularJS. Of course I know AngularJS is better for doing a whole SPA with this technology, but I just want to show that I know how to use angujarJS. 
			 
			Add and remove item to cart are implemented using JQuery and Post methods in the HomeController. It adds the item in the user session. 
			 
			I expanded the IdentityUser and AccountViewModel with the user new information required. 
 
Important stuff: 
		WebShopCaseMVC 
				Bundles 
						Create bundles with js and css. 
				Scripts 
						WebShopCaseMVC/Scripts/application/application.js -> my javascript functions with jquery. 
				Controllers 
						HomeController: controller for the pages in /Home/. It uses the Session object for storing the cart between calls. It also uses StructureMap as IoC. 
						ArticleController: this is the REST API. 
				Models: 
						IdentityModels: here the application user is define, with the properties to be saved that has been requested: Title, FirstName, LastName, Address, HouseNumber… 
				Views: 
						Account: Microsoft account management views. 
				Home 
						ArticleList: asp.net mvc page that is called through ajax and injected in the page. 
						Cart: List with the cart, for previous customer validation. 
						Checkout: list with the cart and final cost. 
						Index: it loads the ArticleList html through JQuery and inserts it. It has the page buttons below. It shows an AJAX retrieving html from the server. It also has the article details hidden. 
						Thankyou: final view giving thanks. The controller stores the order information. It has the tag [Authorize] in the controller, that way only a logged in user can go in. Because of that the login/register process is launched automatically if the user is not loggedin. In this controller the order is saved. 
						UsingREST: page that uses the REST api for retrieving the articles list. It is implemented with AngularJS. It uses the UsingRESTDetails page as template for the detail page in the popup. All the javascript is embedded in the page. 
				Global.asax: inits everything including the StructureMap for IoC. 
				
		Model 
				Article: entity representing an article. 
				WebShopModel: using EF (code first) we have two entities: order and order lines. 

Repositories 
				IArticleRepository: repository for loading articles 
				ArticleRepository: loads articles from the XML using linq to xml. 
				IOrderRepository: repository for performing commands to the orders 
				OrderRepository: it has only one command, saving an order aggregate. 
