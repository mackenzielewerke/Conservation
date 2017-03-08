using System;

namespace AngularCircus.web.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string PriceType { get; set; }
        public bool IsDone { get; set; }
        public Zoo Zoo { get; set; }
        public DateTime ShowDate { get; set; }

        public Ticket()
        {

        }

        public Ticket(decimal price, string pricetype, string zoo, DateTime showdate)
        {
            Zoo.Name = zoo;
            PriceType = pricetype;
            ShowDate = showdate;
            Price = price;

        }

    }
}