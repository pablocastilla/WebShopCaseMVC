namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class WebShopModel : DbContext
    {
        // Your context has been configured to use a 'WebShopModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Model.WebShopModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WebShopModel' 
        // connection string in the application configuration file.
        public WebShopModel()
            : base("name=WebShopModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrdersLines { get; set; }
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserID { get; set; }

        public double TotalWithoutVAT { get; set; }

        public double TotalWithVAT { get; set; }

        public List<OrderLine> OrderLines { get; set; }
        
    }

    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }


        public int OrderId { get; set; }
        
        public int ArticleID { get; set; }

        public int Quantity { get; set; }


    }


}