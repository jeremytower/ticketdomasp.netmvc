using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketDom.Models
{
    public class Tickets
    {
        public Tickets()
        {

        }
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Seating { get; set; }
        public string Seat { get; set; }
    }
}