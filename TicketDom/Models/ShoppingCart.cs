using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TicketDom.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }
        [Key]
        public int Id { get; set; }
        public virtual List<CartItem> Items { get; set; }

    }
}