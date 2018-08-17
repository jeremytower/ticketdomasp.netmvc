using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketDom.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int CartId { get; set; }

        public int TicketId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public double TicketPrice { get; set; }

        public virtual Tickets Ticket { get; set; }
    }
}