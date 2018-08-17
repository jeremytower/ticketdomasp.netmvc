namespace TicketDom.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TicketDomContext : DbContext
    {
        // Your context has been configured to use a 'TicketDomContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TicketDom.Models.TicketDomContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TicketDomContext' 
        // connection string in the application configuration file.
        public TicketDomContext()
            : base("name=TicketDomContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}